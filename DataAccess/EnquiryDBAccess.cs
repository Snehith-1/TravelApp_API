using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class EnquiryDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Enquiry GetAll()
        {
            Enquiry enquiry = new Enquiry();
            try
            {
                cmd = new MySqlCommand("sp_sel_enquiry");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<EnquiryList>();
                if (rd.HasRows == true)
               {
                    while (rd.Read())
                    {
                        summary.Add(new EnquiryList
                        {
                            enquiry_gid = int.Parse(rd["enquiry_gid"].ToString()),
                            enquiry_date = rd["enquiry_date"].ToString(),
                            enquiry_refnumber = rd["enquiry_refnumber"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            enquiry_source = rd["enquiry_source"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                            enquiry_status = rd["enquiry_status"].ToString(),
                            branch_name = rd["branch_name"].ToString()
                        });
                    }
                    enquiry.EnquiryList = summary;
                    enquiry.status = true;
                    
                }
                else
                {
                    enquiry.status = false;
                    enquiry.message = "No Records Fund";
                    
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                enquiry.status = false;
                enquiry.message = "Error Occured While Show the Record";
                error = ex.ToString();

            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return enquiry;
        }


        public Enquiry show()
        {
            Enquiry enq = new Enquiry();
            try
            {
                cmd = new MySqlCommand("sp_sel_sysservice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Enquiryshowlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Enquiryshowlist
                        {
                            service_gid = int.Parse(rd["service_gid"].ToString()),
                            service_name = rd["service_name"].ToString(),
                            service_code = rd["service_code"].ToString(),
                        });
                    }
                    enq.Enquiryshowlist = summary;
                    enq.status = true;
                    
                }
                else
                {
                    enq.status = false;
                    enq.message = "No Records Fund";
                    
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                enq.status = false;
                enq.message = "Error Occured While Show the Record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return enq;
        }


        public Enquirydetails Edit(int val)
        {
            Enquirydetails enquiry = new Enquirydetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_enquiryedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    enquiry.enquiry_gid= int.Parse(rd["enquiry_gid"].ToString());
                    enquiry.customer_gid = rd["customer_gid"].ToString();
                    enquiry.customer_name = rd["customer_name"].ToString();
                    enquiry.company_name = rd["company_name"].ToString();
                    enquiry.customer_type = rd["customer_type"].ToString();
                    enquiry.contact_number = rd["contact_number"].ToString();
                    enquiry.passport_number = rd["passport_number"].ToString();
                    enquiry.travel_from = rd["travel_from"].ToString();
                    enquiry.travel_to = rd["travel_to"].ToString();
                    enquiry.email_address = rd["email_address"].ToString();
                    enquiry.remarks = rd["remarks"].ToString();
                    enquiry.numberof_peopletravel = rd["numberof_peopletravel"].ToString();
                    enquiry.enquiry_date = rd["enquiry_date"].ToString();
                    enquiry.adults = int.Parse(rd["adult"].ToString());
                    enquiry.children = int.Parse(rd["children"].ToString());
                    enquiry.infant = int.Parse(rd["infant"].ToString());
                    enquiry.enquiry_refnumber= rd["enquiry_refnumber"].ToString();
                    enquiry.enquiry_status = rd["enquiry_status"].ToString();
                    enquiry.enquiry_source = rd["enquiry_source"].ToString();
                    enquiry.travel_remarks = rd["travel_remarks"].ToString();
                    rd.Close();

                    cmd = new MySqlCommand("sp_sel_enquirydtledit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquiry_gid", val);

                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<traveldetails>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            summary.Add(new traveldetails
                            {
                                enquiry_gid = rd["enquiry_gid"].ToString(),
                                enquirydtl_gid = int.Parse(rd["enquirydtl_gid"].ToString()),
                                service_details = rd["service_details"].ToString(),
                                service_gid = int.Parse(rd["service_gid"].ToString()),
                                chk_status = rd["chk_status"].ToString()
                            });
                        }
                        enquiry.traveldetails = summary;
                        enquiry.status = true;
                        

                    }
                    else
                    {
                        enquiry.status = false;
                        enquiry.message = "No Record found in details!";
                        
                    }
                    enquiry.status = true;
                    //rd.Close();
                }
                else
                {
                    enquiry.status = false;
                    enquiry.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                enquiry.status = false;
                enquiry.message = "Error Occured While Selecting Quotationedit!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return enquiry;
        }

        public Enquirymodel Add(Enquirydetails val, string userGid)
        {
            Enquirymodel Enq = new Enquirymodel();
            string customer_gid = string.Empty;
            string[] arrCustomerData = val.customer_name.Split('|');
            int arrCustomerDataLength = arrCustomerData.Length;
            string strNationalID = "";
            string strCustomerName = arrCustomerData[0].Trim();


            if(arrCustomerDataLength > 1)
            {
                strNationalID = arrCustomerData[1].Trim();
            }
            

            int flagCustomerExists = 0;
            
            try
            {
                cmd = new MySqlCommand("sp_sel_customer_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                rd = DBAccess.ExecuteReader(cmd);   
                if (rd.Read())
                {
                    flagCustomerExists = 1;
                    customer_gid = rd["customer_gid"].ToString();
                }
                else
                {
                    val.status = false;
                    val.message = "ERR078";
                }

                if (flagCustomerExists == 1) // Proceed only when customer_gid exists
                {
                    cmd = new MySqlCommand("sp_sel_enquiryrefnovalidation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquiry_refnumber", val.enquiry_refnumber);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        Enq.status = false;
                        Enq.message = "Enquiry Ref.No Already Exist!";

                    }
                    else
                    {
                        cmd = new MySqlCommand("sp_ins_enquiry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_enquiry_refnumber", val.enquiry_refnumber);
                        cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                        cmd.Parameters.AddWithValue("p_enquiry_source", val.enquiry_source);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);

                        cmd.Parameters.AddWithValue("p_travel_from", val.travel_from);
                        cmd.Parameters.AddWithValue("p_travel_to", val.travel_to);
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_travel_remarks", val.travel_remarks);
                        cmd.Parameters.AddWithValue("p_numberof_peopletravel", val.numberof_peopletravel);
                        cmd.Parameters.AddWithValue("p_adult", val.adults);
                        cmd.Parameters.AddWithValue("p_children", val.children);
                        cmd.Parameters.AddWithValue("p_infant", val.infant);
                        cmd.Parameters.AddWithValue("p_enquiry_status", val.enquiry_status);
                        cmd.Parameters.AddWithValue("p_enquiry_date", val.enquiry_date);
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            cmd = new MySqlCommand("sp_sel_enquiryadd");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            rd = DBAccess.ExecuteReader(cmd);
                            if (rd.Read())
                            {
                                val.enquiry_gid = int.Parse(rd["enquiry_gid"].ToString());

                            }

                            foreach (var data in val.traveldetails)
                            {
                                cmd = new MySqlCommand("sp_ins_enquirydtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                                cmd.Parameters.AddWithValue("p_service_details", data.service_name);
                                cmd.Parameters.AddWithValue("p_service_gid", data.service_gid);
                                cmd.Parameters.AddWithValue("p_chk_status", data.chk_status);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                            }
                            if (mnresult == 1)
                            {
                                val.status = true;
                                val.message = "Records added sucessfully";
                            }
                        }

                        else
                        {
                            val.status = false;
                            val.message = "Error Occured While Adding Customer";
                        }
                        rd.Close();
                    }
                } // End of if condition for flagExists
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "ERR024";
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
        public Enquirymodel Update(Enquirydetails val, string usergid)
        {
            Enquirymodel Enq = new Enquirymodel();
            string customer_gid = string.Empty;
            string[] arrCustomerData = val.customer_name.Split('|');
            int arrCustomerDataLength = arrCustomerData.Length;
            string strNationalID = "";
            string strCustomerName = arrCustomerData[0].Trim();


            if (arrCustomerDataLength > 1)
            {
                strNationalID = arrCustomerData[1].Trim();
            }


            int flagCustomerExists = 0;
            try
            {
                cmd = new MySqlCommand("sp_sel_customer_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    flagCustomerExists = 1;
                    customer_gid = rd["customer_gid"].ToString();
                }
                else
                {
                    val.status = false;
                    val.message = "ERR078";
                }

                if (flagCustomerExists == 1)
                { 
                    cmd = new MySqlCommand("sp_upt_enquiry");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_refnumber", val.enquiry_refnumber);
                cmd.Parameters.AddWithValue("p_enquiry_source", val.enquiry_source);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_travel_from", val.travel_from);
                cmd.Parameters.AddWithValue("p_travel_to", val.travel_to);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_travel_remarks", val.travel_remarks);
                cmd.Parameters.AddWithValue("p_numberof_peopletravel", val.numberof_peopletravel); 
                cmd.Parameters.AddWithValue("p_adult", val.adults);
                cmd.Parameters.AddWithValue("p_children", val.children);
                cmd.Parameters.AddWithValue("p_infant", val.infant);
                cmd.Parameters.AddWithValue("p_enquiry_status", val.enquiry_status);
                cmd.Parameters.AddWithValue("p_enquiry_date", val.enquiry_date);
                cmd.Parameters.AddWithValue("p_updated_by", usergid);
                cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_enquiryadd");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        val.enquiry_gid = int.Parse(rd["enquiry_gid"].ToString());
                    }
                    rd.Close();
                    cmd = new MySqlCommand("sp_del_enquirydtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.traveldetails)
                        {
                            cmd = new MySqlCommand("sp_ins_enquirydtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                            cmd.Parameters.AddWithValue("p_service_details", data.service_name);
                            cmd.Parameters.AddWithValue("p_service_gid", data.service_gid);
                            cmd.Parameters.AddWithValue("p_chk_status", data.chk_status);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                Enq.status = true;
                                Enq.message = "Enquiry Updated Successfully";
                            }
                        }
                    }
                    else
                    {
                        Enq.status = false;
                        Enq.message = "Error While Deleting Enquiry Details";
                    }
                }
                else
                {
                    Enq.status = false;
                    Enq.message = "Error Occured While Updating Enquiry";
                }
              }
            }
            catch (Exception ex)
            {
                Enq.status = false;
                Enq.message = "Error Occured While Editing Enquiry";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Enq;
        }
        public Enquirymodel Delete(int values)
        {
            Enquirymodel enquirydelete = new Enquirymodel();
            try
            {
                cmd = new MySqlCommand("sp_del_enquiry");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    enquirydelete.status = true;
                    enquirydelete.message = "Enquiry Deleted Successfully";
                }
                else
                {
                    enquirydelete.status = false;
                    enquirydelete.message = "Error Occured While Delete the Enquiry";
                }
            }
            catch (Exception ex)
            {
                enquirydelete.status = false;
                enquirydelete.message = "Error Occured While Delete the Enquiry";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return enquirydelete;
        }
        public Enquirymodel Log(Enquirydetails val, string userGid)
        {
            string error;
            Enquirymodel log = new Enquirymodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_enquirylog");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                cmd.Parameters.AddWithValue("p_enquiry_source", val.enquiry_source);
                cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);

                cmd.Parameters.AddWithValue("p_travel_from", val.travel_from);
                cmd.Parameters.AddWithValue("p_travel_to", val.travel_to);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_enquiry_status", val.enquiry_status);
                cmd.Parameters.AddWithValue("p_nextreminder_date", val.nextreminder_date);
                cmd.Parameters.AddWithValue("p_activitylog_remarks", val.activitylog_remarks);
                cmd.Parameters.AddWithValue("p_enquiry", val.enquiry);
                cmd.Parameters.AddWithValue("p_created_by", userGid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "Enquirylog Added Successfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured While Adding Enquirylog!";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Adding Enquirylog!";
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
        public quotationdetail quatationadd(quotationdetail quotationdetail)
        {

            Enquirymodel quotationadd = new Enquirymodel();

            try
            {
                cmd = new MySqlCommand("sp_sel_enquiryquotationadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", quotationdetail.enquiry_gid);
                rd = DBAccess.ExecuteReader(cmd);

                if (rd.Read())
                {
                    quotationdetail.enquiry_gid = int.Parse(rd["enquiry_gid"].ToString());
                    quotationdetail.customer_type = rd["customer_type"].ToString();
                    quotationdetail.customer_name = rd["customer_name"].ToString();
                    quotationdetail.company_name = rd["company_name"].ToString();
                    quotationdetail.contact_number = rd["contact_number"].ToString();
                    quotationdetail.email_address = rd["email_address"].ToString();
                    quotationdetail.address = rd["address"].ToString();
                    quotationdetail.travel_from = rd["travel_from"].ToString();
                    quotationdetail.travel_to = rd["travel_to"].ToString();
                    //rd.Close();

                    cmd = new MySqlCommand("sp_sel_enquiryactivitylog");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquiry_gid", quotationdetail.enquiry_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<Activityloglist>();
                    while (rd.Read())
                    {
                        summary.Add(new Activityloglist
                        {
                            enquiry_status = rd["enquiry_status"].ToString(),
                            enquiry = rd["enquiry"].ToString(),
                            nextreminder_date = rd["nextreminder_date"].ToString(),
                            activitylog_remarks = rd["activitylog_remarks"].ToString()
                        });
                    }
                    //quotationdetail.Activityloglist = summary;
                    quotationdetail.status = true;
                }
                else
                {
                    quotationdetail.status = false;
                    quotationdetail.message = "No Record found in details!"; 
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                quotationdetail.status = false;
                quotationdetail.message = "Error Occured While Selecting Quotationadd!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }
            return quotationdetail;
        }

        public Enquiry LogAll(Enquirydetails val)
        {
            Enquiry enqlog = new Enquiry();
            try
            {
                cmd = new MySqlCommand("sp_sel_enquiryactivitylog");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Activityloglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Activityloglist
                        {
                            enquiry_status = rd["enquiry_status"].ToString(),
                            enquiry = rd["enquiry"].ToString(),
                            enquirylog_gid = int.Parse(rd["enquirylog_gid"].ToString()),
                            nextreminder_date = rd["nextreminder_date"].ToString(),
                            activitylog_remarks = rd["activitylog_remarks"].ToString()

                        });
                    }
                    enqlog.Activityloglist = summary;
                    enqlog.status = true;
                   
                }
                else
                {
                    enqlog.status = false;
                    enqlog.message = "No Records Fund";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                enqlog.status = false;
                enqlog.message = "Error Occured While Show the Record";
                error = ex.ToString();
            }
            finally
            {
                
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }
            return enqlog;
        }
        public Enquirymodel quatationaddall(quotationdetail val, string user_gid)
        {
            Enquirymodel qtnall = new Enquirymodel();
            Enquirydetails Enquirydetails = new Enquirydetails();
            string customer_gid = string.Empty;
            string[] arrCustomerName = val.customer_name.Split('|');
            string stringCustomerName = arrCustomerName[0].Trim();
            string stringNational_id = arrCustomerName[1].Trim();
            try
            {
                cmd = new MySqlCommand("sp_sel_customer_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", stringCustomerName);
                cmd.Parameters.AddWithValue("p_national_id", stringNational_id);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    customer_gid = rd["customer_gid"].ToString();
                }
                cmd = new MySqlCommand("sp_ins_quotation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                cmd.Parameters.AddWithValue("p_quotation_refnumber", val.quotation_refnumber);
                cmd.Parameters.AddWithValue("p_quotation_date", val.quotation_date);
                cmd.Parameters.AddWithValue("p_customer_name", stringCustomerName);
                cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_address", val.address);
                cmd.Parameters.AddWithValue("p_country_name", val.country);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_quotation_amount", val.quotation_amount);
                cmd.Parameters.AddWithValue("p_quotation_status", val.quotation_status);
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_terms_condition", val.terms_conditions);
                 cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                cmd.Parameters.AddWithValue("p_national_id", stringNational_id);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_quotationadd");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        val.quotation_gid = int.Parse(rd["quotation_gid"].ToString());
                    }
                    rd.Close();
                    foreach (var data in val.quotationdtllist)
                    {
                        MySqlCommand quotationdtl = new MySqlCommand("sp_ins_quotationdtl");
                        quotationdtl.CommandType = System.Data.CommandType.StoredProcedure;
                        quotationdtl.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                        quotationdtl.Parameters.AddWithValue("p_service_details", data.service_details);
                        quotationdtl.Parameters.AddWithValue("p_unit_name", "");
                        quotationdtl.Parameters.AddWithValue("p_unit_gid", data.unit);
                        quotationdtl.Parameters.AddWithValue("p_total_amount", data.total_amount);
                        //quotationdtl.Parameters.AddWithValue("p_unitname", data.unitname);
                        quotationdtl.Parameters.AddWithValue("p_remarks", data.remarks);
                        quotationdtl.Parameters.AddWithValue("p_unit_price", data.unit_price);
                        quotationdtl.Parameters.AddWithValue("p_quantity", data.quantity);
                        quotationdtl.Parameters.AddWithValue("p_description", data.description);
                        quotationdtl.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                        quotationdtl.Parameters.AddWithValue("p_net_amount", data.net_amount);
                        quotationdtl.Parameters.AddWithValue("p_service_gid", data.service_gid);
                        mnresult = DBAccess.ExecuteNonQuery(quotationdtl);
                        
                    }
                    if (mnresult == 1)
                    {
                        val.status = true;
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While  Adding quotation";
                    }
                }
            }

            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Editing Enquiry";
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
        public Enquirydetails quotationaddbind(int val)
        {
            Enquirydetails Enquirydetails = new Enquirydetails();
            Enquirymodel quotationadd = new Enquirymodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_enquiryquotationadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_gid", val);                
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Enquirydetails.customer_name = rd["customer_name"].ToString();
                    Enquirydetails.customer_gid = rd["customer_gid"].ToString();
                    Enquirydetails.company_name= rd["company_name"].ToString();
                    Enquirydetails.contact_number = rd["contact_number"].ToString();
                    Enquirydetails.email_address = rd["email_address"].ToString();
                    Enquirydetails.remarks = rd["remarks"].ToString();
                    Enquirydetails.address = rd["address"].ToString();
                    Enquirydetails.customer_type = rd["customer_type"].ToString();
                    rd.Close();

                    cmd = new MySqlCommand("sp_sel_enquirydtledit");  
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquiry_gid", val);                    
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<EnquiryList>();  //quotationdtllist                   
                    while (rd.Read())
                    {
                        summary.Add(new EnquiryList
                        {
                            enquirydtl_gid = int.Parse(rd["enquirydtl_gid"].ToString()),
                            service_details = rd["service_details"].ToString(),
                            //serviceamount = 0,
                            service_gid = rd["service_gid"].ToString(),
                        });
                    }
                    Enquirydetails.EnquiryList = summary;
                    Enquirydetails.status = true;
                    rd.Close();


                    cmd = new MySqlCommand("sp_sel_unit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary1 = new List<Unitlist>();  //quotationdtllist                   
                    while (rd.Read())
                    {
                        summary1.Add(new Unitlist
                        {
                            unit_gid = rd["unit_gid"].ToString(),
                            unit_name = rd["unit_name"].ToString(),
                            //serviceamount = 0,
                            
                        });
                    }
                    Enquirydetails.Unitlist = summary1;
                    Enquirydetails.status = true;
                    rd.Close();

                }
                else
                {
                    Enquirydetails.status = false;
                    Enquirydetails.message = "No Record found in details!";
                    
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                Enquirydetails.status = false;
                Enquirydetails.message = "Error Occured While Selecting Quotationadd!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }               
            }
            return Enquirydetails;
        }
        public Enquirymodel enquirylogdelete(Enquirydetails val)
        {
            Enquirymodel del = new Enquirymodel();
            try
            {
                cmd = new MySqlCommand("sp_del_enquirylogdtldelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquirylog_gid", val.enquirylog_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    del.status = true;
                }
                else
                {
                    del.status = false;
                }
            }
            catch (Exception ex)
            {
                del.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();

                }

            }
            return del;
        }

        public Enquiry unit()
        {
            Enquiry unit = new Enquiry();
            try
            {
                cmd = new MySqlCommand("sp_sel_unit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary1 = new List<Unitlist>();  //quotationdtllist                   
                while (rd.Read())
                {
                    summary1.Add(new Unitlist
                    {
                        unit_gid = rd["unit_gid"].ToString(),
                        unit_name = rd["unit_name"].ToString(),
                        //serviceamount = 0,

                    });
                }
                unit.Unitlist = summary1;
                unit.status = true;
                rd.Close();
            }
            catch (Exception ex)
            {
                unit.status = false;
                unit.message = "Error Occured While Show the Record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return unit;
        }

        public Enquirydetails enquiryreferenceno(string user_gid)
        {
            Enquirydetails Val = new Enquirydetails();
            string enquiry_refnumber;
            try
            {
                cmd = new MySqlCommand("sp_ins_enquiryreferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquiry_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters["p_enquiry_refnumber"].Direction = System.Data.ParameterDirection.Output;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                enquiry_refnumber = cmd.Parameters["p_enquiry_refnumber"].Value.ToString();

                if (mnresult == 1)
                {
                    Val.enquiry_refnumber = enquiry_refnumber;
                    Val.status = true;

                }

            }
            catch (Exception ex)
            {
                Val.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Val;
        }
    }
}
