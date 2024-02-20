using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class PartnerDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Partner partnersummary()
        {

            Partner partner = new Partner();
            try
            {
                cmd = new MySqlCommand("sp_sel_partner");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Partnerlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Partnerlist
                        {
                            partner_gid = int.Parse(rd["partner_gid"].ToString()),
                            partner_code = rd["partner_code"].ToString(),
                            partner_name = rd["partner_name"].ToString(),
                            national_id = rd["national_id"].ToString(),
                            contact_number = rd["contact_number"].ToString(),

                            email_address = rd["email_address"].ToString(),
                            partner_address = rd["partner_address"].ToString(),
                            capitalshare_percent = rd["capitalshare_percent"].ToString(),
                            revenueshare_percent = rd["revenueshare_percent"].ToString(),
                            sharepaid_captial = rd["sharepaid_captial"].ToString(),

                            created_by = rd["created_by"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            updated_by = rd["updated_by"].ToString(),
                            updated_date = rd["updated_date"].ToString(),
                            partner_country = rd["country_name"].ToString()
                            

                        });
                    }
                    partner.Partnerlist = summary;
                    partner.status = true;
                    
                }
                else
                {
                    partner.status = false;
                    partner.message = "No Records Fund";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                partner.status = false;
                partner.message = "Error occured while show the record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return partner;
        }
        public Partnermodel partneradd(Partnerdetails val, string usergid)
        {
            Partnermodel part = new Partnermodel();

            try
            {
                cmd = new MySqlCommand("sp_sel_partnervalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_partner_code", val.partner_code);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Partner Code Already Exist!";
                    
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_partner");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_partner_code", val.partner_code);
                    cmd.Parameters.AddWithValue("p_partner_name", val.partner_name);
                    cmd.Parameters.AddWithValue("p_partner_national_id", val.national_id);
                    cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                    cmd.Parameters.AddWithValue("p_partner_contact_number", val.contact_number);
                    cmd.Parameters.AddWithValue("p_partner_address", val.partner_address);
                    cmd.Parameters.AddWithValue("p_capitalshare_percent", val.capitalshare_percent);
                    cmd.Parameters.AddWithValue("p_revenueshare_percent", val.revenueshare_percent);
                    cmd.Parameters.AddWithValue("p_sharepaid_captial", val.sharepaid_captial);
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    cmd.Parameters.AddWithValue("p_partner_country", val.partner_country);
                    cmd.Parameters.AddWithValue("p_updated_by",usergid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Partner added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error occured while adding partner";
                    }

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error occured while adding partner";
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
        
        public Partnerdetails Get(int val)
        {
            Partnerdetails partnerlist = new Partnerdetails();
            try
            {

                cmd = new MySqlCommand("sp_sel_partneredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_partner_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                if (rd.Read())
                {
                    partnerlist.partner_gid = int.Parse(rd["partner_gid"].ToString());
                    partnerlist.partner_code = rd["partner_code"].ToString();
                    partnerlist.partner_name = rd["partner_name"].ToString();
                    partnerlist.national_id = rd["national_id"].ToString();
                    partnerlist.contact_number = rd["contact_number"].ToString();
                    partnerlist.email_address = rd["email_address"].ToString();                   
                    partnerlist.partner_country = rd["country_name"].ToString();
                    partnerlist.partner_address = rd["partner_address"].ToString();
                    partnerlist.capitalshare_percent = rd["capitalshare_percent"].ToString();
                    partnerlist.revenueshare_percent = rd["revenueshare_percent"].ToString();
                    partnerlist.sharepaid_captial = rd["sharepaid_captial"].ToString();
                    partnerlist.status = true;
                    
                }
                else
                {
                    partnerlist.status = false;
                    partnerlist.message = " No Records Found!";
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                partnerlist.status = false;
                partnerlist.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return partnerlist;


        }

        public Partnermodel Update(Partnerdetails values, string userGid)
        {
            Partnermodel partner = new Partnermodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_partner");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_partner_gid", values.partner_gid);
                cmd.Parameters.AddWithValue("p_partner_code", values.partner_code);
                cmd.Parameters.AddWithValue("p_partner_name", values.partner_name);
                cmd.Parameters.AddWithValue("p_national_id", values.national_id);
                cmd.Parameters.AddWithValue("p_contact_number", values.contact_number);
                cmd.Parameters.AddWithValue("p_email_address", values.email_address);
                cmd.Parameters.AddWithValue("p_partner_address", values.partner_address);
                cmd.Parameters.AddWithValue("p_capitalshare_percent", values.capitalshare_percent);
                cmd.Parameters.AddWithValue("p_revenueshare_percent", values.revenueshare_percent);
                cmd.Parameters.AddWithValue("p_sharepaid_captial", values.sharepaid_captial);
                cmd.Parameters.AddWithValue("p_updated_by", values.updated_by);
                cmd.Parameters.AddWithValue("p_partner_country", values.partner_country);
                cmd.Parameters.AddWithValue("p_updated_date", values.updated_date);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    partner.status = true;
                    partner.message = "Partner updated successfully";
                }
                else
                {
                    partner.status = false;
                    partner.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                partner.status = false;
                partner.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return partner;
        }

        public Partnermodel Delete(int values)
        {
            Partnermodel deletepartner = new Partnermodel();
            try
            {
                cmd = new MySqlCommand("sp_del_partner");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_partner_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    deletepartner.status = true;
                    deletepartner.message = "Partner deleted successfully";
                }
                else
                {
                    deletepartner.status = false;
                    deletepartner.message = "Error occured while deleting partner!";
                }
            }
            catch (Exception ex)
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
            return deletepartner;

        }
    }
}






