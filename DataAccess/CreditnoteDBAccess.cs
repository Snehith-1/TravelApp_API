using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
namespace DataAccess
{
    public class CreditnoteDBAccess
    {
        int mnresult, mnresult1 = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        //double credit_amout = 0;
        //string invoicedebitnote;
        string error;
        public creditnotereceiptlist creditnotesubmit(creditnotereceiptlist val, string user_gid)
        {
           creditnotereceiptlist creditsubmit = new creditnotereceiptlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    creditsubmit.status = false;
                    creditsubmit.message = "Customer Receipt RefNo Already Exists!";

                }
                else
                {
                    creditsubmit.credit_amount = val.credit_amount;
                    cmd = new MySqlCommand("sp_ins_receiptselectmain");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_receipt_amount", val.credit_amount); //credit_amount as receipt_amount
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                    cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);
                    cmd.Parameters.AddWithValue("p_receipt_date", val.credit_date);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid",val.customerinvoice_gid);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", "");
                    cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                    cmd.Parameters.AddWithValue("p_journal_from", val.journal_from);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {

                        cmd = new MySqlCommand("sp_ins_creditnote");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_invoice_gid", val.customerinvoice_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                        cmd.Parameters.AddWithValue("p_credit_amount", val.credit_amount);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                        cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                        cmd.Parameters.AddWithValue("p_credit_date",val.credit_date); //receipt_date as credit_date
                        mnresult = DBAccess.ExecuteNonQuery(cmd);

                        foreach (var data in val.creditnotereceipt)
                        {
                            cmd = new MySqlCommand("sp_ins_receiptselectmaindtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");
                            cmd.Parameters.AddWithValue("p_invoice_gid", data.customerinvoice_gid);
                            cmd.Parameters.AddWithValue("p_paid_amount", creditsubmit.credit_amount);
                            mnresult1 = DBAccess.ExecuteNonQuery(cmd);
                             }
                     
                            creditsubmit.status = true;
                            creditsubmit.message = "Records added successfully!";
                      
                    }

                    
                    else
                    {
                        creditsubmit.status = false;
                        creditsubmit.message = "Internal error occured";
                    }
                    

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                creditsubmit.status = false;
                creditsubmit.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

           return creditsubmit;
        }
        public creditnotereceiptlist creditnoteinvoiceselectlist(creditnotereceiptlist val)
        {
            creditnotereceiptlist customerinvoice = new creditnotereceiptlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerreceiptdetailselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<creditnotereceipt>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new creditnotereceipt
                        {
                            invoice_date = rd["created_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            invoice_amount = Double.Parse(rd["invoice_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()), //paidamt as receipt_amount
                            outstanding_amount = rd["outstanding_amount"].ToString(),  // receiptamt as outstanding_amount

                        });
                    }
                    customerinvoice.creditnotereceipt = summary;
                    customerinvoice.status = true;

                }
                rd.Close();
              
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
        public creditnotereceiptlist creditnotedetails(creditnotereceiptlist val)
        {
            creditnotereceiptlist customerinvoice = new creditnotereceiptlist();
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
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.national_id = rd["national_id"].ToString();
                                     
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

        
        public creditnotereceiptlist creditnotesummary()
        {
            creditnotereceiptlist creditnote = new creditnotereceiptlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_creditnote");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<creditnotereceipt>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new creditnotereceipt
                        {

                            invoice_date = rd["invoice_date"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            total_withtax = rd["total_withtax"].ToString(),
                            credit_amount = rd["credit_amount"].ToString(),
                            credit_date = rd["credit_date"].ToString()
                        });
                    }
                    creditnote.creditnotereceipt = summary;
                    creditnote.status = true;
                    rd.Close();

                }

                else
                {
                    creditnote.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                creditnote.status = false;
                creditnote.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally 
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return creditnote;
        }

        public creditnotereceiptlist debitnotesummary()
        {
            creditnotereceiptlist debitnote = new creditnotereceiptlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_debitnote");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<debitnotereceipt>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new debitnotereceipt
                        {

                            invoice_date = rd["invoice_date"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            net_amount = rd["net_amount"].ToString(), //invoiceamount as net_amount 
                            debit_amount = rd["debit_amount"].ToString(),
                            debit_date = rd["debit_date"].ToString(),
                         });
                    }
                    debitnote.debitnotereceipt = summary;
                    debitnote.status = true;
                    rd.Close();

                }

                else
                {
                    debitnote.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                debitnote.status = false;
                debitnote.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return debitnote;
        }
        public VendorPaymentmodel paymentvendoroverallsubmit(VendorPaymentdetails val, string user_gid)
        {
            VendorPaymentmodel submit = new VendorPaymentmodel();

            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.status = false;
                    submit.message = "Vendor Payment RefNo Alreay Exists!";
                }
                else
                {

                    cmd = new MySqlCommand("sp_ins_vendorpayment");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure; //
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_payment_amount", val.payment_amount);//paymentvalue as  payment_amount
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_reference_number", val.reference_number); //receiptrefno as reference_number
                    cmd.Parameters.AddWithValue("p_receipt_date", val.receipt_date);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", "");
                    cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                    cmd.Parameters.AddWithValue("p_vendorpayment_gid", "");
                    cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        // submit.invoicedebitnote = val.invoicegid;
                        cmd = new MySqlCommand("sp_ins_debitnote"); // newly added for debit notes
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid); //vendorgid changes
                        cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                        cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                        //cmd.Parameters.AddWithValue("p_vendor_details", val.contactno);
                        cmd.Parameters.AddWithValue("p_debit_date", val.debit_date);
                        cmd.Parameters.AddWithValue("p_debit_amount", val.debit_amount);//debitnoteamount as debit_amount
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        mnresult1 = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult1 == 1)
                        {
                            
                            foreach (var data in val.vendorinvoicelist)
                            {
                             
                                cmd = new MySqlCommand("sp_ins_vendorpaymentdtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_vendorpayment_gid", "0");
                                cmd.Parameters.AddWithValue("p_vendorinvoice_gid", data.vendorinvoice_gid);
                                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                cmd.Parameters.AddWithValue("p_vendorinvoice_amount",val.payment_amount); // invoiceamountwithtax as payment_amount
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                            }
                            submit.status = true;
                            submit.message = "Records added successfully!";
                        }
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
      
    }
}