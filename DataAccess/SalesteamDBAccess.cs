using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class SalesteamDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;        
        public Salesteam GetAll()
        {
            Salesteam salesteam = new Salesteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteam");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                ////cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Salesteamlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Salesteamlist
                        {

                            salesteam_gid = int.Parse(rd["salesteam_gid"].ToString()),
                            salesteam_code = rd["salesteam_code"].ToString(),
                            salesteam_name = rd["salesteam_name"].ToString(),
                            soteammanagername = rd["teammanager_name"].ToString(),
                            soteamemployeename = rd["emp_name"].ToString(),
                            sonoofemployee = rd["no_employee"].ToString()


                        });
                    }
                    salesteam.salesteamlist = summary;
                    salesteam.status = true;
                }
                else
                {
                    salesteam.status = false; 
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                salesteam.status = false;
                salesteam.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return salesteam;
        }
        public Salesteamdetail Get(int val)
        {
            Salesteamdetail salesteamdetail = new Salesteamdetail();

            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", val);
                //cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);

                if (rd.Read())
                {
                    salesteamdetail.salesteam_gid = int.Parse(rd["salesteam_gid"].ToString());
                    salesteamdetail.salesteam_name = rd["salesteam_name"].ToString();
                    salesteamdetail.salesteam_code = rd["salesteam_code"].ToString();
                    salesteamdetail.status = true; 
                }
                else
                {
                    salesteamdetail.status = false;
                    salesteamdetail.message = "No Records found!";
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                salesteamdetail.status = false;
                salesteamdetail.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return salesteamdetail;
        }
        public Salesteammodel Add(Salesteamdetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamcodevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_code", val.salesteam_code);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Salesteam Code Already Exist!";
                    
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_salesteamadd");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesteam_name", val.salesteam_name);
                    cmd.Parameters.AddWithValue("p_teamemployee_name", val.soteammanager_name);
                    cmd.Parameters.AddWithValue("p_salesteam_code", val.salesteam_code);
                    cmd.Parameters.AddWithValue("p_teamemployee_gid", val.soteamemployee_gid);
                    cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    //cmd.Connection = con;
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Records added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While Inserting Sales Team";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                error = ex.ToString();
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
        public Salesteammodel Delete(int values)
        {
            Salesteammodel salesteamdelete = new Salesteammodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == false)
                {
                    cmd = new MySqlCommand("sp_sel_salesteammanagerdelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                    //cmd.Connection = con;
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == false)
                    {
                        cmd = new MySqlCommand("sp_del_salesteam");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            salesteamdelete.status = true;
                            salesteamdelete.message = "Deleted Successfully";
                            
                        }
                     }
                   
                    else
                    {
                        salesteamdelete.status = false;
                        salesteamdelete.message = "Error Occured While Deleting Sales Team";
                    }
                    rd1.Close();
                }                                            
                else
                {
                    salesteamdelete.status = false;
                    salesteamdelete.message = "Cannot Delete Team Assigned with the Employee";
                }
                rd.Close();

            }
            catch(Exception ex)
            {
                salesteamdelete.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }            
            return salesteamdelete;
        }
        public Salesteammodel Update(Salesteamdetail val, string user_gid)
        {
            Salesteammodel sales = new Salesteammodel();
            try
            {                
                cmd = new MySqlCommand("sp_upt_salesteamedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                cmd.Parameters.AddWithValue("p_salesteam_code", val.salesteam_code);
                cmd.Parameters.AddWithValue("p_salesteam_name", val.salesteam_name);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    sales.status = true;
                    sales.message = "Salesteam updated succesfully";
                }
                else
                {
                    sales.status = false;
                    sales.message = "Error Occured While Updating Salesteam!";
                }
            }
            catch(Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            
            return sales;
        }
        public Salesteam salesteamemployee(int values)
        {
            Salesteam sof = new Salesteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamunassignemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                //cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<employeelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new employeelist
                        {
                            employee_name = rd["employee_name"].ToString(),
                            employee_gid = rd["user_gid"].ToString()
                        });
                    }
                    sof.status = true;
                    sof.employeelist = summary;
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salesteamassignemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                var sum = new List<Salesteamlist>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        sum.Add(new Salesteamlist
                        {
                            employee_name = rd1["employee_name"].ToString(),
                            employee_gid = rd1["user_gid"].ToString()
                        });
                    }
                }
                sof.salesteamlist = sum;
                sof.status = true;
                rd1.Close();
                //rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "salesteam Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sof;
        }
        public Salesteam salesteammanager(int values)
        {
            Salesteam sof = new Salesteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamunassingmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                //cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<employeelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new employeelist
                        {
                            employee_name = rd["employee_name"].ToString(),
                            employee_gid = rd["user_gid"].ToString()
                        });
                    }
                    sof.employeelist = summary;
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salesteamassignmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", values);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                var sum = new List<Salesteamlist>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        sum.Add(new Salesteamlist
                        {
                            employee_name = rd1["employee_name"].ToString(),
                            employee_gid = rd1["user_gid"].ToString()
                        });
                    }

                }
                sof.salesteamlist = sum;
                sof.status = true;
                rd1.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "salesteam Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sof;
        }
        public Salesteammodel asignemployeesubmit(Salesteamdetail val, string user_gid)
        {
            
            Salesteammodel employee = new Salesteammodel();
            try
            {
                cmd = new MySqlCommand("sp_del_unassignemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                //cmd.Parameters.AddWithValue("p_teamemployee_gid", emp.employeeid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                foreach (var data in val.salesteamlist)
                {
                    cmd = new MySqlCommand("sp_ins_assignemployee");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                    cmd.Parameters.AddWithValue("p_teamemployee_gid", data.employee_gid);
                    cmd.Parameters.AddWithValue("p_teamemployee_name", data.employee_name);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);                      
                }               
                //foreach (var emp in val.employeelist)
                //    {
                       
                //    }
                if (mnresult ==1)
                { 
                    employee.status = true;
                    employee.message = "Employee Assigned and un assigned successfully";
                }
               
            }
            catch (Exception ex)
            {
                employee.status = true;
                employee.message = "Error occured while assigned  Employee";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return employee;
        }
        public Salesteammodel asignmanagersubmit(Salesteamdetail val, string user_gid)
        {
            Salesteammodel manager = new Salesteammodel();
            try
            {
                cmd = new MySqlCommand("sp_del_unassignmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                //cmd.Parameters.AddWithValue("p_teammanager_gid", emp.employeeid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                foreach (var data in val.salesteamlist)
                {
                    cmd = new MySqlCommand("sp_ins_assignmanager");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesteam_gid", val.salesteam_gid);
                    cmd.Parameters.AddWithValue("p_teammanager_gid", data.employee_gid);
                    cmd.Parameters.AddWithValue("p_teammanager_name", data.employee_name);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    ////cmd.Connection = con;
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                }               
                    //foreach (var emp in val.employeelist)
                    //{
                      
                    //}
                    if (mnresult== 1)
                    {
                        manager.status = true;
                        manager.message = "Manager Assigned and un assigned successfully";
                    }              
            }
            catch (Exception ex)
            {
                manager.status = true;
                manager.message = "Error occured while assigned and un assigned Manager";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return manager;

        }
    }
}