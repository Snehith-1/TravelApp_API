using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class QuotationDBAccess
    {
        int mnresult, mnresult1 = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd,rd1;
        string error;
        public Quotation GetAll(string val)
        {
            Quotation quotation = new Quotation();
            try
            {
                cmd = new MySqlCommand("sp_sel_quotation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<QuotationList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new QuotationList
                        {
                            quotation_date = rd["quotation_date"].ToString(),
                            quotation_gid = int.Parse(rd["quotation_gid"].ToString()),
                            quotation_refnumber = rd["quotation_refnumber"].ToString(),
                            enquiry_refnumber= rd["enquiry_refnumber"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            net_amount = double.Parse(rd["net_amount"].ToString()),
                            quotation_status = rd["quotation_status"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            company_code = val
                        });
                    }
                    quotation.quotationlist = summary;
                    quotation.status = true;
                    
                }
                else
                {
                    quotation.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationmodel Add(Quotationdetail val, string user_gid)
        {
            Quotationdetail quotation = new Quotationdetail();

            try
            {

                cmd = new MySqlCommand("sp_sel_quotation_refnumbervalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_refnumber", val.quotation_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Quotation Ref.No Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_quotation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_quotation_date", val.quotation_date);
                    cmd.Parameters.AddWithValue("p_quotation_refnumber", val.quotation_refnumber);
                    cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                    cmd.Parameters.AddWithValue("p_customer_name", val.customer);
                    cmd.Parameters.AddWithValue("p_quotation_amount", val.quotation_amount);
                    cmd.Parameters.AddWithValue("p_quotation_status", val.quotation_status);
                    cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                    cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                    cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                    cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                    cmd.Parameters.AddWithValue("p_address", val.address);
                    cmd.Parameters.AddWithValue("p_country_name", val.country);
                    cmd.Parameters.AddWithValue("p_customer_name", "");
                    cmd.Parameters.AddWithValue("p_terms_condition", val.terms_conditions);
                    cmd.Parameters.AddWithValue("p_user_gid", user_gid);
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
                       
                       
                        foreach (var data in val.quotationlist)
                        {
                          
                            MySqlCommand quotationdtl = new MySqlCommand("sp_ins_quotationdtl");
                            quotationdtl.CommandType = System.Data.CommandType.StoredProcedure;
                            quotationdtl.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                            quotationdtl.Parameters.AddWithValue("p_service_details", val.service_details);
                            quotationdtl.Parameters.AddWithValue("p_unit_name",data.unit_name);
                            quotationdtl.Parameters.AddWithValue("p_unit_gid", data.unit_gid);
                            quotationdtl.Parameters.AddWithValue("p_total_amount", data.total_amount);
                            //quotationdtl.Parameters.AddWithValue("p_unitname", data.unitname);
                            quotationdtl.Parameters.AddWithValue("p_remarks", data.remarks);
                            quotationdtl.Parameters.AddWithValue("p_unit_price", data.unit_price);
                            quotationdtl.Parameters.AddWithValue("p_quantity", data.quantity);
                            quotationdtl.Parameters.AddWithValue("p_description", data.description);
                            quotationdtl.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                            quotationdtl.Parameters.AddWithValue("p_net_amount", data.net_amount);
                            quotationdtl.Parameters.AddWithValue("p_service_gid", data.service_gid);
                            mnresult1 = DBAccess.ExecuteNonQuery(quotationdtl);
                            if (mnresult1 == 1)
                            {
                                quotation.status = true;
                            }
                        }
                    }
                    else
                    {
                        quotation.status = false;
                        quotation.message = "Internal Error Occured";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationdetail Get(int val)
        {
            Quotationdetail quotation = new Quotationdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_quotationenquiry");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_enquirygid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    quotation.enquiry_gid = int.Parse(rd["enquiry_gid"].ToString());
                    quotation.customer = rd["customer_name"].ToString();
                    quotation.company_name = rd["company_name"].ToString();
                    quotation.contact_number = rd["contact_number"].ToString();
                    quotation.email_address = rd["email_address"].ToString();
                    quotation.remarks = rd["remarks"].ToString();
                    rd.Close();
                    cmd = new MySqlCommand("sp_sel_quotationenquirydtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_enquirygid", val);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    var summary = new List<QuotationList>();
                    if (rd1.HasRows == true)
                    {
                        while (rd1.Read())
                        {
                            summary.Add(new QuotationList
                            {
                                enquirydtl_gid = int.Parse(rd1["enquirydtl_gid"].ToString()),
                                service_details = rd1["service_details"].ToString()

                            });
                        }
                        quotation.quotationlist = summary;
                        quotation.status = true;
                        
                    }
                    else
                    {
                        quotation.status = false;
                        quotation.message = "No Record found in details!";
                        
                    }
                    rd1.Close();
                    quotation.status = true;
                }
                else
                {
                    quotation.status = false;
                    quotation.message = "No Records found!";

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "Internal error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationdetail Edit(int val)
        {
            Quotationdetail quotation = new Quotationdetail();
            try 
            {

                cmd = new MySqlCommand("sp_sel_quotationedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val);
                rd = DBAccess.ExecuteReader(cmd);

                if (rd.Read())
                {
                    quotation.quotation_gid = int.Parse(rd["quotation_gid"].ToString());
                    quotation.customer_name = rd["customer_name"].ToString();
                    quotation.company_name = rd["company_name"].ToString();
                    quotation.contact_number = rd["contact_number"].ToString();
                    quotation.email_address = rd["email_address"].ToString();
                    quotation.remarks = rd["remarks"].ToString();
                    quotation.address = rd["address"].ToString();
                    quotation.quotation_date = rd["quotation_date"].ToString();
                    //quotation.exchange = int.Parse(rd["exchange_rate"].ToString());
                    //quotation.currency = rd["currency"].ToString();
                    //quotation.contacttype = rd["contact_type"].ToString();
                    quotation.total_amount = Double.Parse(rd["total_amount"].ToString());
                    quotation.discount_amount = Double.Parse(rd["discount_amount"].ToString());
                    quotation.addon_charge = Double.Parse(rd["addon_charge"].ToString());
                    quotation.net_amount = Double.Parse(rd["net_amount"].ToString()); 
                    quotation.country_name = rd["country_name"].ToString();
                    quotation.quotation_refnumber= rd["quotation_refnumber"].ToString();
                    quotation.customer_type = rd["customer_type"].ToString();
                    quotation.customer_gid = rd["customer_gid"].ToString();
                    //rd.Close();
                    cmd = new MySqlCommand("sp_sel_quotationdtledit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_quotation_gid", val);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    var summary = new List<QuotationList>();
                    if (rd1.HasRows == true)
                    {

                        while (rd1.Read())
                        {
                            summary.Add(new QuotationList
                            {
                                quotationdtlgid = int.Parse(rd1["quotationdtl_gid"].ToString()),
                                service_details = rd1["service_details"].ToString(),
                                total_amount = double.Parse(rd1["total_amount"].ToString()),
                                remarks = rd1["remarks"].ToString(),
                                discount_amount = double.Parse(rd1["discount_amount"].ToString()),
                                unit_price = double.Parse(rd1["unit_price"].ToString()),
                                quantity = rd1["quantity"].ToString(),
                                description = rd1["description"].ToString(),
                                net_amount = double.Parse(rd1["net_amount"].ToString()),
                                unit_gid = int.Parse(rd1["unit_gid"].ToString()),
                                unit_name = rd1["unit_name"].ToString(),
                            });
                        }
                        quotation.quotationlist = summary;
                        quotation.status = true;
                        //rd1.Close();
                    }
                    else
                    {
                        quotation.status = false;
                        quotation.message = "No Record found in details!";
                        
                    }
                    
                    quotation.status = true;
                    rd1.Close();
                }
                else
                {
                    quotation.status = false;
                    quotation.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "Error Occured While Selecting Quotationedit!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationmodel Update(Quotationdetail val, string user_gid)
        {
            Quotationdetail quo = new Quotationdetail();
            try
            {
                cmd = new MySqlCommand("sp_upt_quotation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                cmd.Parameters.AddWithValue("p_address", val.address);
                cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    MySqlCommand cmd = new MySqlCommand("sp_del_quotationdtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.quotationlist)
                        {
                            
                            cmd = new MySqlCommand("sp_ins_quotationdtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                            cmd.Parameters.AddWithValue("p_service_details", data.service_details);
                            cmd.Parameters.AddWithValue("p_total_amount", data.total_amount);
                            cmd.Parameters.AddWithValue("p_remarks", data.remarks);
                            cmd.Parameters.AddWithValue("p_unit_price", data.unit_price);
                            cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                            cmd.Parameters.AddWithValue("p_description", data.description);
                            cmd.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                            cmd.Parameters.AddWithValue("p_net_amount", data.net_amount);
                            cmd.Parameters.AddWithValue("p_service_gid", data.service_gid);
                            if (data.unit_name == null)
                            {
                                data.unit_name = "null";
                            }
                            cmd.Parameters.AddWithValue("p_unit_name", data.unit_name);
                            cmd.Parameters.AddWithValue("p_unit_gid", data.unit_gid);
                            mnresult1 = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult1 == 1)
                            {
                                quo.status = true;
                                quo.message = "Quotation Edited Successfully";
                            }
                        }
                    }
                    else
                    {
                        quo.status = false;
                    }

                }
                else
                {
                    quo.status = false;
                    quo.message = "Error Occured While Editing Quotation";
                }
            }
            catch (Exception ex)
            {
                quo.status = false;
                quo.message = "Error Occured While Editing Quotation";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quo;
        }
        public Quotationmodel Cancel(QuotationList val, string user_gid)
        {
            Quotationmodel quo = new Quotationmodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_quotationcancel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    quo.status = true;
                    quo.message = "Quotation Cancelled Successfully";
                }
                else
                {
                    quo.status = false;
                    quo.message = "Error Occured While Cancelling Quotation";
                }
            }
            catch (Exception ex)
            {
                quo.status = false;
                quo.message = "Error Occured While Cancelling Quotation";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return quo;
        }
        public Quotationmodel directquotationadd(Quotationdetail val, string user_gid)
        {
            Quotationdetail quotation = new Quotationdetail();
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
                    quotation.status = false;
                    quotation.message = "ERR078";
                }
                if (flagCustomerExists == 1)
                {
                    cmd = new MySqlCommand("sp_sel_quotation_refnumbervalidation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_quotation_refnumber", val.quotation_refnumber);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        quotation.status = false;
                        quotation.message = "Quotation Ref.No Already Exist!";

                    }
                    else
                    {
                        cmd = new MySqlCommand("sp_ins_dircetquotation");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_quotation_date", val.quotation_date);
                        cmd.Parameters.AddWithValue("p_quotation_refnumber", val.quotation_refnumber);
                        cmd.Parameters.AddWithValue("p_enquiry_gid", val.enquiry_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_quotation_amount", val.quotation_amount);
                        cmd.Parameters.AddWithValue("p_quotation_status", val.quotation_status);
                        cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                        cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                        cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                        cmd.Parameters.AddWithValue("p_address", val.address);
                        cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                        cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                        cmd.Parameters.AddWithValue("p_national_id", strNationalID);

                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            cmd = new MySqlCommand("sp_sel_quotationadd");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            rd = DBAccess.ExecuteReader(cmd);
                            if (rd.Read())
                            {
                                val.quotation_gid = int.Parse(rd["quotation_gid"].ToString());
                                quotation.quotation_gid = int.Parse(rd["quotation_gid"].ToString());
                            }
                            rd.Close();

                            foreach (var data in val.quotationlist)
                            {
                                MySqlCommand quotationdtl = new MySqlCommand("sp_ins_quotationdtl");
                                quotationdtl.CommandType = System.Data.CommandType.StoredProcedure;
                                quotationdtl.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                                quotationdtl.Parameters.AddWithValue("p_service_details", data.service_name);
                                quotationdtl.Parameters.AddWithValue("p_total_amount", data.total_amount);
                                quotationdtl.Parameters.AddWithValue("p_remarks", data.remarks);
                                quotationdtl.Parameters.AddWithValue("p_unit_price", data.unit_price);
                                quotationdtl.Parameters.AddWithValue("p_quantity", data.quantity);
                                quotationdtl.Parameters.AddWithValue("p_description", data.description);
                                quotationdtl.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                                quotationdtl.Parameters.AddWithValue("p_net_amount", data.net_amount);
                                quotationdtl.Parameters.AddWithValue("p_service_gid", data.service_gid);
                                quotationdtl.Parameters.AddWithValue("p_unit_name", data.unit_name);
                                quotationdtl.Parameters.AddWithValue("p_unit_gid", data.unit_gid);
                                mnresult1 = DBAccess.ExecuteNonQuery(quotationdtl);
                                if (mnresult1 == 1)
                                {
                                    quotation.status = true;
                                }
                            }
                        }
                        else
                        {
                            quotation.status = false;
                            quotation.message = "Internal Error Occured";
                        }
                    }
                    rd.Close();
                }

            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "ERR037";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationdetail directquotationedit(int val)
        {
            Quotationdetail quotation = new Quotationdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_quotationdtledit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                var summary = new List<QuotationList>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new QuotationList
                        {
                            quotationdtl_gid = int.Parse(rd1["quotationdtl_gid"].ToString()),
                            service_details = rd1["service_details"].ToString(),
                            total_amount = double.Parse(rd1["total_amount"].ToString()),
                            remarks = rd1["remarks"].ToString(),
                            discount_amount = double.Parse(rd1["discount_amount"].ToString()),
                            unit_price = double.Parse(rd1["unit_price"].ToString()),
                            quantity = rd1["quantity"].ToString(),
                            description = rd1["description"].ToString(),
                            net_amount = double.Parse(rd1["net_amount"].ToString()),
                            unit_gid = int.Parse(rd1["unit_gid"].ToString()),
                            unit_name = rd1["unit_name"].ToString(),
                        });
                    }
                    quotation.quotationlist = summary;
                    quotation.status = true;
                    
                }
                else
                {
                    quotation.status = false;
                    quotation.message = "No Record found in details!";
                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                quotation.status = false;
                quotation.message = "Error Occured While Selecting Quotationedit!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quotation;
        }
        public Quotationmodel directquotationupdate(Quotationdetail val, string user_gid)
        {
            Quotationdetail quo = new Quotationdetail();
            try
            {
                cmd = new MySqlCommand("sp_upt_directquotation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                cmd.Parameters.AddWithValue("p_terms_condition", val.terms_conditions);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    MySqlCommand cmd = new MySqlCommand("sp_del_quotationdtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.quotationlist)
                        {
                            cmd = new MySqlCommand("sp_ins_quotationdtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_quotation_gid", val.quotation_gid);
                            cmd.Parameters.AddWithValue("p_service_details", data.service_details);
                            cmd.Parameters.AddWithValue("p_total_amount", data.total_amount);
                            cmd.Parameters.AddWithValue("p_remarks", data.remarks);
                            cmd.Parameters.AddWithValue("p_unit_price", data.unit_price);
                            cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                            cmd.Parameters.AddWithValue("p_description", data.description);
                            cmd.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                            cmd.Parameters.AddWithValue("p_net_amount", data.net_amount);
                            cmd.Parameters.AddWithValue("p_service_gid", data.service_gid);
                            cmd.Parameters.AddWithValue("p_unit_name", data.unit_name);
                            cmd.Parameters.AddWithValue("p_unit_gid", data.unit_gid);
                            mnresult1 = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult1 == 1)
                            {
                                quo.status = true;
                                quo.message = "Quotation Edited Successfully";
                            }
                        }
                    }
                    else
                    {
                        quo.status = false;
                    }

                }
                else
                {
                    quo.status = false;
                    quo.message = "Error Occured While Editing Quotation";
                }
            }
            catch (Exception ex)
            {
                quo.status = false;
                quo.message = "Error Occured While Editing Quotation";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return quo;
        }
        public Quotationmodel quotationtosalesorder(int val, string user_gid)
        {
            Quotationmodel quo = new Quotationmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_salestoquotation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if(rd.HasRows ==true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_ins_quotationtosalesorder");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_net_amount", rd["net_amount"].ToString());
                    cmd.Parameters.AddWithValue("p_customer_gid", rd["customer_gid"].ToString());
                    cmd.Parameters.AddWithValue("p_quotation_gid", rd["quotation_gid"].ToString());
                    cmd.Parameters.AddWithValue("p_customer_name", rd["customer_name"].ToString());
                    cmd.Parameters.AddWithValue("p_contact_number", rd["contact_number"].ToString());
                    cmd.Parameters.AddWithValue("p_email_address", rd["email_address"].ToString());
                    cmd.Parameters.AddWithValue("p_currency_gid", rd["currency_gid"].ToString());
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_salesorder_gid","0");
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if(mnresult ==1)
                    {
                        cmd = new MySqlCommand("sp_sel_salestoquotationdtl");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_quotation_gid", val);
                        rd1 = DBAccess.ExecuteReader(cmd);
                        if (rd1.HasRows == true)
                        {
                            while(rd1.Read())
                            { 
                                cmd = new MySqlCommand("sp_ins_quotationtosalesorderdtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_service_details", rd1["service_details"].ToString());
                                cmd.Parameters.AddWithValue("p_total_amount", rd1["total_amount"].ToString());
                                cmd.Parameters.AddWithValue("p_reference_gid","0");
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                cmd.Parameters.AddWithValue("p_salesorder_gid", "0");
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                                if(mnresult ==1)
                                {
                                    quo.status = true;
                                }
                            }
                        }
                       
                        rd1.Close();
                    }
                    else
                    {
                        quo.status = false;
                    }
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salesorder_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                rd1 = DBAccess.ExecuteReader(cmd);
                rd1.Read();
                quo.salesorder_gid = rd1["salesorder_gid"].ToString();
                rd1.Close();
                
            }
            catch (Exception ex)
            {
                quo.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return quo;
        }
        public Quotationdetail quotationreferenceno(string user_gid)
        {
            Quotationdetail Val = new Quotationdetail();
            try
            {
                cmd = new MySqlCommand("sp_ins_quotationreferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_quotation_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_quotationreferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.quotation_refnumber = rd["quotation_refnumber"].ToString();
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