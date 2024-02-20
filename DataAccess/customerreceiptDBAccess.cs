using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class customerreceiptDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;

        public customerinvoicedetail customerreceiptaddselect(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //var summary = new List<customerinvoicelist>();               
                if (rd.Read())
                {
                    customerinvoice.customer_gid = rd["customer_gid"].ToString();
                    customerinvoice.customer_name = rd["customer_name"].ToString();
                    customerinvoice.contact_number= rd["contact_number"].ToString();
                    customerinvoice.national_id = rd["national_id"].ToString();
                    customerinvoice.invoice_amount =double.Parse( rd["invoice_amount"].ToString());
                    customerinvoice.paid_amount = double.Parse(rd["paid_amount"].ToString());
                    //customerinvoice.salesordergid = int.Parse(rd["salesorder_gid"].ToString());
                    //customerinvoice.customergid = int.Parse(rd["customer_gid"].ToString());
                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "internal error occured";
                }
                rd.Close();

                //var summary = new List<customerinvoicelist>();
                //foreach (var data in val.customerinvoicelist)
                //{
                //    cmd = new MySqlCommand("sp_sel_customerreceiptdetailselect");
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("p_customerinvoice_gid", data.invoicegid);
                //    //cmd.Parameters.AddWithValue("p_customer_id", val.customerid);
                //    rd = DBAccess.ExecuteReader(cmd);
                //    //var summary = new List<customerinvoicelist>();
                //    rd.Read();
                //    summary.Add(new customerinvoicelist
                //    {

                //        invoicedate = rd["created_date"].ToString(),
                //        customer = rd["customer_name"].ToString(),
                //        //contactdetils = rd["contact_details"].ToString(),
                //        salesorderrefno = rd["salesorder_refno"].ToString(),
                //        invoicegid = rd["customerinvoice_gid"].ToString(),
                //        invoiceamountwithtax = double.Parse(rd["invoice_value"].ToString()),
                //        createdby = rd["created_by"].ToString(),
                //        createddate = DateTime.Parse(rd["created_date"].ToString()),
                //        paidamt = rd["receipt_amount"].ToString(),
                //        receiptamt = rd["outstanding"].ToString(),
                //        //customerid=int.Parse(rd["customer_gid"].ToString())
                //    });
                //}
                //customerinvoice.customerinvoicelist = summary;
                //customerinvoice.status = true;
                //rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "internal error occured";
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

        

        public customerinvoicedetail receiptoverallsubmit(customerinvoicedetail val, string user_gid)
        {
            customerinvoicedetail submit = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.status = false;
                    submit.message = "Customer Receipt RefNo Already Exists!";
                    
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_receiptselectmain");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_receipt_amount", val.receipt_amount);
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                    cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);//receiptrefno as reference_number
                    cmd.Parameters.AddWithValue("p_receipt_date", val.receipt_date);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid); //changes made in sql
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", "");
                    cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                    cmd.Parameters.AddWithValue("p_journal_from",val.journal_from);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.customerinvoicelist)
                        {
                            cmd = new MySqlCommand("sp_ins_receiptselectmaindtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");
                            cmd.Parameters.AddWithValue("p_invoice_gid", data.customerinvoice_gid);  //changes made in invoicegid to invoiceid
                            cmd.Parameters.AddWithValue("p_paid_amount", data.invoice_amount); //invoiceamountwithtax as paid_amount
                            mnresult = DBAccess.ExecuteNonQuery(cmd);

                        }
                        submit.status = true;
                        submit.message = "Records added successfully!";
                    }
                    else
                    {
                        submit.status = false;
                        submit.message = "Internal error occured";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                submit.status = false;
                submit.message = "Internal Error Occured";
                error = ex.ToString();
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

        public customerinvoicedetail Getall(string val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<customerinvoicelist>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {
                            customerreceipt_gid = rd["customerreceipt_gid"].ToString(),
                            invoice_date = rd["created_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            receipt_method = rd["receipt_method"].ToString(),
                            paid_amount = rd["paid_amount"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            customer_gid =rd["customer_gid"].ToString(),
                            reference_number = rd["reference_number"].ToString(),
                            receipt_date = rd["receipt_date"].ToString(),
                            invoice_refnumber=rd["invoice_refnumber"].ToString(),
                            company_code = val
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;
                    
                }
                rd.Close();
                //else
                //{
                //    customerinvoice.status = false;
                //}
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured!";
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
        public customerinvoicedetail customerreceiptprint(customerinvoicedetail val,string userGid)
        {
            customerinvoicedetail blng = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptallselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerreceipt_gid", val.customerreceipt_gid);               
                rd = DBAccess.ExecuteReader(cmd);
                if(rd.Read())
                {
                    blng.customer_gid =rd["customer_gid"].ToString();
                    blng.customer_name = rd["customer_name"].ToString();
                    blng.national_id = rd["national_id"].ToString();
                    blng.invoice_amount = double.Parse(rd["invoice_amount"].ToString());
                    blng.paid_amount = double.Parse(rd["paid_amount"].ToString());
                    blng.remarks = rd["remarks"].ToString();
                    blng.receipt_method = rd["receipt_method"].ToString();
                    blng.contact_number = rd["contact_number"].ToString();
                    blng.reference_number= rd["reference_number"].ToString();
                    blng.receipt_date = rd["receipt_date"].ToString();
                    blng.bank_name = rd["bank_name"].ToString();
                    blng.transaction_number = rd["transaction_number"].ToString();
                }
                rd.Close();
                var summary = new List<customerinvoicelist>();             
                cmd = new MySqlCommand("sp_sel_ccustomerreceiptsummaryselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerreceipt_gid", val.customerreceipt_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {
                            customerreceipt_gid =rd1["customerreceipt_gid"].ToString(),
                            invoice_date = rd1["invoice_date"].ToString(),
                            customer_name = rd1["customer_name"].ToString(),                               
                            receipt_method = rd1["receipt_method"].ToString(),
                            invoice_amount = rd1["invoice_amount"].ToString(),                               
                            customer_gid = rd1["customer_gid"].ToString(),
                            salesorder_refnumber = rd1["salesorder_refnumber"].ToString (),
                        });
                    }

                }
                blng.customerinvoicelist = summary;
                blng.message = "Records loaded successfully!";
                rd1.Close();                
            }
            catch (Exception ex)
            {
                blng.status = false;
                blng.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return blng;
        }
        public customerinvoicedetail receiptreferenceno(string usergid)
        {
            customerinvoicedetail Val = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_ins_receiptreferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_receipt_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", usergid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_receiptreferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.receipt_refnumber = rd["receipt_refnumber"].ToString();
                    Val.status = true;
                    
                }
                rd.Close();
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
        public customerinvoicedetail customerinvoiceselectlist(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptdetailselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<customerinvoiceselectlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customerinvoiceselectlist
                        {
                            invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                            //invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                            customer_name = rd["customer_name"].ToString(),
                            //contactdetils = rd["contact_details"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            //customerid=int.Parse(rd["customer_gid"].ToString())
                           // companycode=val
                        });
                    }
                    customerinvoice.customerinvoiceselectlist = summary;
                    customerinvoice.status = true;

                }
                rd.Close();
                //else
                //{
                //    customerinvoice.status = false;
                //}
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured!";
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
        public customerinvoicedetail Delete(customerinvoicedetail val)
        {
            customerinvoicedetail deletecustomerreceipt = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_del_customerreceipt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerreceipt_gid", val);
                cmd.Parameters.AddWithValue("p_reference_number", val);

                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    deletecustomerreceipt.status = true;
                    deletecustomerreceipt.message = "Customer Receipt deleted successfully";
                }
                else
                {
                    deletecustomerreceipt.status = false;
                    deletecustomerreceipt.message = "Error occured while deleting Customer Receipt!";
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
            return deletecustomerreceipt;

        }


    }
}