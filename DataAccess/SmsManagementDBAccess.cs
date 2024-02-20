using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace DataAccess
{
    public class SmsManagementDBAccess
    {
        CmnFunctions objcmnfunctions = new CmnFunctions();
        int mnresult = 0;
        MySqlCommand cmd = null;                
        MySqlDataReader rd;
        public SmsManagement GetAll()
        {
            SmsManagement smsmanagement = new SmsManagement();
            try
            { 
                cmd = new MySqlCommand("sp_sel_smsmanagement");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SmsManagementList>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SmsManagementList
                        {

                            smsmanagement_gid = rd["smsmanagement_gid"].ToString(),
                            smsmanagement_code = rd["smsmanagement_code"].ToString(),
                            smsmanagement_name = rd["smsmanagement_name"].ToString(),
                            smsmanagement_message = rd["smsmanagement_message"].ToString(),
                           
                           
                        });
                    }
                    smsmanagement.smsManagementList = summary;
                    smsmanagement.status = true;
                    rd.Close();

                }
               
                else
                {
                    smsmanagement.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception)
            {
                smsmanagement.status = false;
                smsmanagement.message = "Internal error occured";
                //Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return smsmanagement;
        }
        public SmsManagement smsservice(List<customerlist> customer_gid, int smsmanagement_gid)
        {
            SmsManagement smsmanagement = new SmsManagement();
            try
            {
                string smsmanagement_name = "";
                string smsmanagement_message = "";

                string customer_no = ""; 
                try
                {
                    cmd = new MySqlCommand("sp_sel_smsmanagementedit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_activity_gid", smsmanagement_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        smsmanagement_name = rd["smsmanagement_name"].ToString();
                        smsmanagement_message = rd["smsmanagement_message"].ToString();

                    }
                    rd.Close();
                    for (int i = 0; i < customer_gid.Count; i++)
                    {

                        cmd = new MySqlCommand("sp_sel_getcustomermailid");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("customers_gid", customer_gid[i].flag);
                        rd = DBAccess.ExecuteReader(cmd);
                        var summary = new List<MailManagementList>();
                        if (rd.HasRows == true)
                        {
                            if (rd.Read())
                            {
                                customer_no= rd["contact_number"].ToString();
                                objcmnfunctions.SendSMS(customer_no,smsmanagement_message);
                            }
                            smsmanagement.status = true;
                        }
                       

                        else
                        {
                            smsmanagement.status = false;

                        }
                        rd.Close();
                    }
                }
                catch (Exception e)
                {
                    smsmanagement.status = false;
                    smsmanagement.message = "Internal Error Occured";
                    Console.WriteLine("Error: {0}", e);
                }
                finally
                {
                    if (cmd.Connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
                return smsmanagement;
            }
            catch (Exception e)
            {
                smsmanagement.status = false;
                smsmanagement.message = "Internal Error Occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return smsmanagement;
        }

        public SmsManagementdetail Get(string val)
        {
            SmsManagementdetail smsmanagementdetail = new SmsManagementdetail();

            try
            {
                cmd = new MySqlCommand("sp_sel_smsmanagementedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_gid", val);                
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    smsmanagementdetail.smsmanagement_gid = rd["smsmanagement_gid"].ToString();
                    smsmanagementdetail.smsmanagement_code = rd["smsmanagement_code"].ToString();
                    smsmanagementdetail.smsmanagement_name = rd["smsmanagement_name"].ToString();
                    smsmanagementdetail.smsmanagement_message = rd["smsmanagement_message"].ToString();
                    smsmanagementdetail.status = true;
                    
                }                
                else
                {
                    smsmanagementdetail.status = false;
                    smsmanagementdetail.message = "No Records found!";
                    
                }
                rd.Close();
            }
            
            catch
            {
                smsmanagementdetail.status = false;
                smsmanagementdetail.message = "Internal Error Occured!";
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return smsmanagementdetail;
        }
        public SmsManagementmodel Add(SmsManagementdetail val, string userGid)
        {
            try {
                cmd = new MySqlCommand("sp_ins_smsmanage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_smsmanagement_code", val.smsmanagement_code);
                cmd.Parameters.AddWithValue("p_smsmanagement_name", val.smsmanagement_name);
                cmd.Parameters.AddWithValue("p_smsmanagement_message", val.smsmanagement_message);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "SMS added sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal error occured";
                }
            }
            catch (Exception e)
            {
                val.status = false;
                val.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }                        
            return val;
        }
        public SmsManagementmodel Update(SmsManagementdetail val, string usergid)
        {
            SmsManagementmodel smsmanagement = new SmsManagementmodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_smsmanage");
                cmd.Parameters.AddWithValue("p_smsmanagement_gid", Convert.ToInt32(val.smsmanagement_gid));
                cmd.Parameters.AddWithValue("p_smsmanagement_code", val.smsmanagement_code);
                cmd.Parameters.AddWithValue("p_smsmanagement_name", val.smsmanagement_name);
                cmd.Parameters.AddWithValue("p_smsmanagement_message", val.smsmanagement_message);
                cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    smsmanagement.status = true;
                    smsmanagement.message = "SMS updated succesfully";
                }
                else
                {
                    smsmanagement.status = false;
                    smsmanagement.message = "Internal error occured";
                }
            }
            catch(Exception e)
            {
                smsmanagement.status = false;
                smsmanagement.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return smsmanagement;
        }
        public SmsManagementmodel Delete(int values)
        {
            SmsManagementmodel SmsManagementdelete = new SmsManagementmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_smsmanage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_smsmanagement_gid", values); 
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    SmsManagementdelete.status = true;
                    SmsManagementdelete.message = "SMS deleted successfully";

                }
                else
                {
                    SmsManagementdelete.status = false;
                    SmsManagementdelete.message = " Internal error occured!";
                }
            }
            catch(Exception e)
            {
                SmsManagementdelete.status = false;
                SmsManagementdelete.message = " Internal error occured!";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }                                           
            return SmsManagementdelete;
        }
    }
}