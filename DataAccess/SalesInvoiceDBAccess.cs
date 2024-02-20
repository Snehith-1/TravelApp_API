using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace DataAccess
{
    public class SalesInvoiceDBAccess
    {
        int mnresult, mnresult1 = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        DataTable dt;
        string error;
        MySqlDataAdapter sqlad = new MySqlDataAdapter();
        public SalesInvoice Getall(string val)
        {
           
            SalesInvoice salesinvoice = new SalesInvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<salesinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new salesinvoicelist
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            customer_gid = rd["customer_gid"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                            vendor_amount = rd["VendorAmount"].ToString(),
                            company_code = val
                        });
                    }
                    salesinvoice.salesinvoicelist = summary;
                    salesinvoice.status = true;

                }
                else
                {
                    salesinvoice.status = true;
                    salesinvoice.message = "No Records Found";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                salesinvoice.status = false;
                salesinvoice.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }

            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return salesinvoice;
        }
        public SalesInvoice salesinvoiceservicetypelist(SalesInvoice val)
        {

            SalesInvoice salesinvoice = new SalesInvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesinvoiceservicetypelist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);

                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<salesinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new salesinvoicelist
                        {
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            service_type = rd["service_type"].ToString(),
                            vendor_name= rd["vendor_name"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString()
                           
                        });
                    }
                    salesinvoice.salesinvoicelist = summary;
                    salesinvoice.status = true;

                }
                else
                {
                    salesinvoice.status = true;
                    salesinvoice.message = "No Records Found";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                salesinvoice.status = false;
                salesinvoice.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return salesinvoice;
        }
        public billingmodel SalesInvoiceoverallsubmitairfile(Billingdetail val, string user_gid)
        {
            billingmodel submit = new billingmodel();
            string customer_gid = string.Empty;
            string branch_gid = string.Empty;

            string branch_name = string.Empty;
            string[] arrCustomerData = val.customer_name.Split('|');
            int arrCustomerDataLength = arrCustomerData.Length;
            //string strNationalID = "";
            string strCustomerName = arrCustomerData[0].Trim();
            //if (arrCustomerDataLength > 1)
            //{
            //    strNationalID = arrCustomerData[1].Trim();
            //}



            int flagCustomerExists = 0;
            try
            {
                cmd = new MySqlCommand("sp_sel_customer_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                //cmd.Parameters.AddWithValue("p_national_id", strNationalID);

                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    // Karthi: Need to proceed only when customer gid is a valid one
                    flagCustomerExists = 1;
                    customer_gid = rd["customer_gid"].ToString();
                }
                else
                {
                    submit.status = false;
                    submit.message = "ERR078";
                }
                if (flagCustomerExists == 1)
                {
                    cmd = new MySqlCommand("sp_ins_invoicereferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", "");
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        cmd = new MySqlCommand("sp_sel_invoicereferencenumber");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        rd = DBAccess.ExecuteReader(cmd);
                        rd.Read();
                        val.invoice_refnumber = rd["invoice_refnumber"].ToString();
                        val.status = true;
                        int outputparam;
                        cmd = new MySqlCommand("sp_ins_billingsalesinvoice");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("p_salesorder_gidtemp", "");
                        //cmd.Parameters["p_salesorder_gidtemp"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                        cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                        cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_currency_code", val.currency);
                        cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                        cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                        cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);
                        cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                        cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                        cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);
                        cmd.Parameters.AddWithValue("p_branch_name", "");
                        cmd.Parameters.AddWithValue("p_epnr_no", val.epnr_no);
                        cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                        cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                        cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                        cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                        cmd.Parameters.AddWithValue("p_client_notes", val.client_notes);
                        cmd.Parameters.AddWithValue("p_notes", val.notes);
                        cmd.Parameters.AddWithValue("p_servicetype_totalamount", val.servicetype_totalamount);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            submit.status = true;
                            submit.message = "Records added successfully!";
                        }
                        else
                        {
                            cmd = new MySqlCommand("sp_upt_sequencecode");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_referencecode", val);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            submit.status = false;
                            submit.message = "Internal error occured";
                        }
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                submit.status = false;
                submit.message = "Internal Error Occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Sales Invoice Number:" + val.invoice_refnumber);
                    sw.WriteLine("Error: Error occured while Raise Invoice");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return submit;
        }
        public customerinvoicedetail salesinvoiceedit(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_invoiceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //var summary = new List<customerinvoicelist>();               
                if (rd.Read())
                {
                    customerinvoice.customerinvoice_gid = rd["customerinvoice_gid"].ToString();
                    customerinvoice.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    customerinvoice.customer_gid = rd["customer_gid"].ToString();
                    customerinvoice.invoice_refnumber = rd["invoice_refnumber"].ToString();
                    customerinvoice.customer_name = rd["customer_name"].ToString();
                    customerinvoice.invoice_date = rd["invoice_date"].ToString();
                    customerinvoice.customer_type = rd["customer_type"].ToString();
                    customerinvoice.terms_conditions = rd["terms_conditions"].ToString();
                    //customerinvoice.currency_code = rd["currency_code"].ToString();
                    customerinvoice.email_address = rd["email_address"].ToString();
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.receipt_method = rd["receipt_method"].ToString();
                    customerinvoice.bank_name = rd["bank_name"].ToString();
                    customerinvoice.transaction_number = rd["transaction_number"].ToString();
                    customerinvoice.paid_amount = double.Parse(rd["paid_amount"].ToString());
                    customerinvoice.discount_amount = double.Parse(rd["discount_amount"].ToString());
                    customerinvoice.balance_amount = double.Parse(rd["balance_amount"].ToString());
                    customerinvoice.client_notes = rd["client_notes"].ToString();
                    customerinvoice.notes = rd["notes"].ToString();

                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "internal error occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "internal error occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Sales Invoice Number:" + customerinvoice.invoice_refnumber);
                    sw.WriteLine("Error: Error occured while Updating Sales Invoice");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return customerinvoice;
        }
        public customerinvoicedetail servicetypescount(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_servicetypecount");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<servicetypeList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new servicetypeList
                        {
                            service_count = rd["activitycount"].ToString(),
                            service_type = rd["service_type"].ToString()
                        });
                        customerinvoice.servicetypeList = summary;
                        customerinvoice.status = true;
                    }

                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "internal error occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Error Occured While Showing Dashboard ";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();

                }
            }
            return customerinvoice;
        }
        public customerinvoicedetail salesinvoicedetails(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesinvoicedetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);       
                if (rd.Read())
                {
               
                    customerinvoice.invoice_refnumber = rd["invoice_refnumber"].ToString();
        
                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "internal error occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "internal error occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return customerinvoice;
        }
        public customerinvoicedetail Update(customerinvoicedetail val, string user_gid)
        {
            customerinvoicedetail updateinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_upt_salesinvoiceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);
                cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                cmd.Parameters.AddWithValue("p_client_notes", val.client_notes);
                cmd.Parameters.AddWithValue("p_notes", val.notes);
                cmd.Parameters.AddWithValue("p_servicetype_totalamount", val.servicetype_totalamount);
                cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);
                cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    updateinvoice.status = true;
                    updateinvoice.message = "Sales Invoice updated succesfully";
                }
                else
                {
                    updateinvoice.status = false;
                    updateinvoice.message = "Error Occured While Updating Sales Invoice!";
                }
            }
            catch (Exception ex)
            {
                updateinvoice.status = false;
                updateinvoice.message = "Error Occured While Updating Customer!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Sales Order gid:" + val.salesorder_gid);
                    sw.WriteLine("Customer Invoice gid:" + val.customerinvoice_gid);
                    sw.WriteLine("Error: Error occured while Updating Sales Invoice");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return updateinvoice;
        }


    }
}