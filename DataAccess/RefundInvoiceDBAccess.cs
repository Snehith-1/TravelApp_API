using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;
using System.IO;



namespace DataAccess
{
    public class RefundInvoiceDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Refund GetAll(string val)
        {
            Refund refund = new Refund();
            try
            {
                cmd = new MySqlCommand("sp_sel_customeandvendorrefund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Refundlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Refundlist
                        {
                            refund_gid = int.Parse(rd["refund_gid"].ToString()),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            refundreference_gid = rd["refundreference_gid"].ToString(),
                            salesinvoice_refno = rd["salesinvoice_refno"].ToString(),
                            refund_number = rd["refund_number"].ToString(),
                            refund_date = rd["refund_date"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            received_amount = Double.Parse(rd["received_amount"].ToString()),
                            refund_amount = Double.Parse(rd["refund_amount"].ToString()),
                            company_code = val

                        });
                    }
                    refund.refundlist = summary;
                    refund.status = true;

                }
                else
                {
                    refund.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                refund.status = false;
                refund.message = "Internal Error Occured";
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
            return refund;
        }

        public customerinvoicedetail refundaddselect(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundinvoiceselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);

                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //var summary = new List<customerinvoicelist>();               
                if (rd.Read())
                {
                    customerinvoice.customerinvoice_gid = rd["customerinvoice_gid"].ToString();

                    customerinvoice.customer_gid = rd["customer_gid"].ToString();
                    customerinvoice.invoice_amount = double.Parse(rd["invoice_amount"].ToString());
                    customerinvoice.paid_amount = double.Parse(rd["paid_amount"].ToString());
                    customerinvoice.customer_name = rd["customer_name"].ToString();
                    customerinvoice.vendor_name = rd["vendor_name"].ToString();
                    customerinvoice.vendor_amount = double.Parse(rd["vendor_amount"].ToString());
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.national_id = rd["national_id"].ToString();
                    //customerinvoice.salesordergid = int.Parse(rd["salesorder_gid"].ToString());
                    //customerinvoice.customergid = int.Parse(rd["customer_gid"].ToString());
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



        public customerinvoicedetail refundsummarydetails(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundsummarydetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);

                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    {
                        customerinvoice.customerinvoice_gid = rd["customerinvoice_gid"].ToString();
                        customerinvoice.customer_gid = rd["customer_gid"].ToString();
                        customerinvoice.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                        customerinvoice.invoice_refnumber = rd["invoice_refnumber"].ToString();
                        customerinvoice.invoice_date = rd["invoice_date"].ToString();
                        customerinvoice.customer_name = rd["customer_name"].ToString();
                        customerinvoice.invoice_amount = double.Parse(rd["invoice_amount"].ToString());
                        customerinvoice.contact_number = rd["contact_number"].ToString();
                        customerinvoice.paid_amount = double.Parse(rd["paid_amount"].ToString());

                    }
                }

                var summary = new List<customerinvoicelist>();
                cmd = new MySqlCommand("sp_sel_refundsummarydetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {

                            vendor_name = rd1["vendor_name"].ToString(),
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            paymentnote_gid = int.Parse(rd1["paymentnote_gid"].ToString()),
                            service_type = rd1["service_type"].ToString(),
                            reference = rd1["reference"].ToString(),
                            total_amount = Double.Parse(rd1["total_amount"].ToString()),
                            vendor_amount = Double.Parse(rd1["vendor_amount"].ToString()),

                            //status = rd1["status"].ToString(),
                        });
                    }
                }
                rd1.Close();
                customerinvoice.customerinvoicelist = summary;
                customerinvoice.status = true;
                customerinvoice.message = "Records loaded successfully!";



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

        public customerinvoicedetail refundoverallsubmit(customerinvoicedetail val, string user_gid)
        {
            customerinvoicedetail submit = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_number", val.refund_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Refund Ref.No Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_customerandvendorrefund");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customerrrefund_gid", val.customerrrefund_gid);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                    cmd.Parameters.AddWithValue("p_customer_firstname", val.customer_name);
                    cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                    cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                    cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);
                    cmd.Parameters.AddWithValue("p_customer_cancellation", val.customer_cancellation);
                    cmd.Parameters.AddWithValue("p_customer_refund", val.customer_refund);
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                    cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                    cmd.Parameters.AddWithValue("p_vendor_cancellation", val.vendor_cancellation);
                    cmd.Parameters.AddWithValue("p_vendor_refund", val.vendor_refund);

                    cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                    cmd.Parameters.AddWithValue("p_refund_number", val.refund_refnumber);//refudtrefno as reference_number
                    cmd.Parameters.AddWithValue("p_refund_date", val.refund_date);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);


                    //cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");//1
                    //changes made in sql

                    //cmd.Parameters.AddWithValue("p_journal_from",val.journal_from);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        //foreach (var data in val.customerinvoicelist)
                        //{
                        //    cmd = new MySqlCommand("sp_ins_receiptselectmaindtl");
                        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //    cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");
                        //    cmd.Parameters.AddWithValue("p_invoice_gid", data.customerinvoice_gid);  //changes made in invoicegid to invoiceid
                        //    cmd.Parameters.AddWithValue("p_paid_amcount", data.invoice_amount); //invoiceamountwithtax as paid_amount
                        //    mnresult = DBAccess.ExecuteNonQuery(cmd);

                        //}
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
            return submit;
        }

        public MdlRefundServiceType refundServiceType(MdlRefundServiceType val, string user_gid)
        {
            MdlRefundServiceType submit = new MdlRefundServiceType();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_number", val.refund_number);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Refund Ref.No Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_servicetyperefundsubmit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid); //refund ref_gid passed customerinvoice_gid
                    cmd.Parameters.AddWithValue("p_salesinvoice_refno", val.salesinvoice_refno);
                    cmd.Parameters.AddWithValue("p_refund_type", val.refund_type);
                    cmd.Parameters.AddWithValue("p_paid_status", val.paid_status);
                    cmd.Parameters.AddWithValue("p_refund_number", val.refund_number);
                    cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                    cmd.Parameters.AddWithValue("p_refund_date", val.refund_date);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_refnumber", val.transaction_refnumber);
                    cmd.Parameters.AddWithValue("p_received_amount", val.received_amount);
                    cmd.Parameters.AddWithValue("p_cancellation_charge", val.cancellation_charge);
                    cmd.Parameters.AddWithValue("p_vendorcancellation_charges", val.vendorcancellation_charges);
                    cmd.Parameters.AddWithValue("p_vendorrefund_grandtotal", val.vendorrefund_grandtotal);
                    cmd.Parameters.AddWithValue("p_refund_total", val.refund_total);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                    cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);

                    cmd.Parameters.AddWithValue("p_notes", val.notes);

                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.refundServiceTypeList)
                        {
                            if (data.vendorcancellation_charge == null)
                                data.vendorcancellation_charge = "0.000";
                            if (data.customercancellation_charge == null)
                                data.customercancellation_charge = "0.000";
                            if (data.customerrefund_amount == null)
                                data.customerrefund_amount = "0.000";
                            if (data.vendorrefund_amount == null)
                                data.vendorrefund_amount = "0.000";
                                cmd = new MySqlCommand("sp_ins_refundservicetypedtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                                cmd.Parameters.AddWithValue("p_paid_status", val.paid_status);
                                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                                cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);
                                cmd.Parameters.AddWithValue("p_service_type", data.service_type);
                                cmd.Parameters.AddWithValue("p_servicetype_amount", data.total_amount);
                                cmd.Parameters.AddWithValue("p_customercancellation_charges", data.customercancellation_charge);
                                cmd.Parameters.AddWithValue("p_customerrefund_amount", data.customerrefund_amount);
                                cmd.Parameters.AddWithValue("p_vendor_amount", data.vendor_amount);
                                cmd.Parameters.AddWithValue("p_vendorcancellation_charge", data.vendorcancellation_charge);
                                cmd.Parameters.AddWithValue("p_vendorrefund_amount", data.vendorrefund_amount);
                                cmd.Parameters.AddWithValue("p_vendor_gid", data.vendor_gid);
                                cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnote_gid);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                cmd.Parameters.AddWithValue("p_refund_number", val.refund_number);
                               cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                              cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);



                            mnresult = DBAccess.ExecuteNonQuery(cmd);                                                        
                            
                        }
                        val.status = true;
                        val.message = "Records added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Internal Error Occured";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
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
                    sw.WriteLine("Refund Invoice Number:" + val.refundreference_gid);
                    sw.WriteLine("Error: Error occured while Raise Refund Invoice");
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
            return val;

        }
        public MdlRefundServiceType refundservicetypeupdate(MdlRefundServiceType val, string user_gid)
        {
            MdlRefundServiceType submit = new MdlRefundServiceType();
            try
            {
                if (val.refund_total == null || val.refund_total == "")
                    val.refund_total = "0.000";
                cmd = new MySqlCommand("sp_upt_servicetyperefund");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                   cmd.Parameters.AddWithValue("p_refund_gid", val.refund_gid);
                    cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid); //refund ref_gid passed customerinvoice_gid
                    cmd.Parameters.AddWithValue("p_salesinvoice_refno", val.salesinvoice_refno);
                    cmd.Parameters.AddWithValue("p_refund_type", val.refund_type);
                    cmd.Parameters.AddWithValue("p_refund_number", val.refund_number);
                    cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                    cmd.Parameters.AddWithValue("p_refund_date", val.refund_date);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_refnumber", val.transaction_refnumber);
                    cmd.Parameters.AddWithValue("p_received_amount", val.received_amount);
                    cmd.Parameters.AddWithValue("p_cancellation_charge", val.cancellation_charge);
                    cmd.Parameters.AddWithValue("p_vendorcancellation_charges", val.vendorcancellation_charges);
                    cmd.Parameters.AddWithValue("p_vendorrefund_grandtotal", val.vendorrefund_grandtotal);
                    cmd.Parameters.AddWithValue("p_refund_total", val.refund_total);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                    cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                   cmd.Parameters.AddWithValue("p_paid_status", val.paid_status);
                cmd.Parameters.AddWithValue("p_notes", val.notes);

                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.refundServiceTypeList)
                        {
                            if (data.vendorcancellation_charge == null || data.vendorcancellation_charge == "")
                                data.vendorcancellation_charge = "0.000";
                        if (data.customercancellation_charge == null || data.customercancellation_charge == "")
                                data.customercancellation_charge = "0.000";
                        if (data.customerrefund_amount == null || data.customerrefund_amount == "")
                                data.customerrefund_amount = "0.000";
                            if (data.vendorrefund_amount == null || data.vendorrefund_amount == "")
                                data.vendorrefund_amount = "0.000";
                            cmd = new MySqlCommand("sp_upt_refundservicetypedtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_refunddtl_gid", data.refunddtl_gid);
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                            cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                            cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                            cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);
                            cmd.Parameters.AddWithValue("p_service_type", data.service_type);
                            cmd.Parameters.AddWithValue("p_servicetype_amount", data.total_amount);
                            cmd.Parameters.AddWithValue("p_customercancellation_charges", data.customercancellation_charge);
                            cmd.Parameters.AddWithValue("p_customerrefund_amount", data.customerrefund_amount);
                            cmd.Parameters.AddWithValue("p_vendor_amount", data.vendor_amount);
                            cmd.Parameters.AddWithValue("p_vendorcancellation_charge", data.vendorcancellation_charge);
                            cmd.Parameters.AddWithValue("p_vendorrefund_amount", data.vendorrefund_amount);
                            cmd.Parameters.AddWithValue("p_vendor_gid", data.vendor_gid);
                            cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnote_gid);
                            cmd.Parameters.AddWithValue("p_created_by", user_gid);
                            cmd.Parameters.AddWithValue("p_refund_number", val.refund_number);
                            cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                            cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                          cmd.Parameters.AddWithValue("p_paid_status", val.paid_status);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {

                        }
                        else
                        {
                            string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                            if (!File.Exists(strPath))
                            {
                                File.Create(strPath).Dispose();
                            }
                            using (StreamWriter sw = File.AppendText(strPath))
                            {
                                sw.WriteLine("=============Error Logging ===========");
                                sw.WriteLine("===========Start============= " + DateTime.Now);
                                sw.WriteLine("Refund Invoice Number:" + val.refundreference_gid);
                                sw.WriteLine("Error: Error occured while updating Refund Invoice");
                                sw.WriteLine("===========End============= " + DateTime.Now);

                            }
                            break;
                        }
                    }
                        val.status = true;
                        val.message = "Records added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Internal Error Occured";
                    }
                
               
            }
            catch (Exception ex)
            {
                val.status = false;
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
                    sw.WriteLine("Refund Invoice Number:" + val.refundreference_gid);
                    sw.WriteLine("Error: Error occured while updating Refund Invoice");
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
            return val;

        }
        //daCustomerRefundLedgerReport

        public customerinvoice daRefundCustomerInvoiceSummary(string val)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundcustomerinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<customerinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            customer_gid = rd["customer_gid"].ToString(),
                            invoice_status = rd["invoice_status"].ToString(),
                            paid_amount = rd["paid_amount"].ToString(),
                            balance_amount = rd["balance_amount"].ToString(),
                            pnr_number = rd["pnr_number"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                            company_code = val
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;

                }
                else
                {
                    customerinvoice.status = true;
                    customerinvoice.message = "No Records Found";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured!";
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
        public MdlRefundServiceType refundview(MdlRefundServiceType val, string user_gid)
        {
            MdlRefundServiceType submit = new MdlRefundServiceType();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundview");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);

                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.refund_gid = rd["refund_gid"].ToString();
                    submit.salesinvoice_refno = rd["salesinvoice_refno"].ToString();
                    submit.customerinvoice_gid = rd["customerinvoice_gid"].ToString();
                    submit.refund_number = rd["refund_number"].ToString();
                    submit.refund_date = rd["refund_date"].ToString();
                    submit.invoice_date = rd["invoice_date"].ToString();
                    submit.customer_name = rd["customer_name"].ToString();
                    submit.contact_number = rd["contact_number"].ToString();
                    submit.payment_mode = rd["payment_mode"].ToString();
                    submit.bank_name = rd["bank_name"].ToString();
                    submit.bank_gid = rd["bank_gid"].ToString();
                    submit.transaction_refnumber = rd["transaction_refnumber"].ToString();
                    submit.invoice_amount = rd["invoice_amount"].ToString();
                    submit.refund_amount = rd["refund_amount"].ToString();
                    submit.paid_amount = rd["paid_amount"].ToString();
                    submit.vendorrefund_grandtotal = rd["vendorrefund_grandtotal"].ToString();
                    submit.refund_total = rd["refund_total"].ToString();
                    submit.discount_amount = rd["discount_amount"].ToString();
                    submit.net_amount = rd["net_amount"].ToString();
                    submit.balance_amount = rd["balance_amount"].ToString();
                    submit.notes = rd["notes"].ToString();
                    submit.paid_status = rd["paid_status"].ToString();


                }
                rd.Close();

                var summary = new List<customerinvoicelist>();
                cmd = new MySqlCommand("sp_sel_refundview");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);

                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {

                        summary.Add(new customerinvoicelist
                        {
                            refunddtl_gid = rd1["refunddtl_gid"].ToString(),
                            service_type = rd1["service_type"].ToString(),
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            paymentnote_gid = int.Parse(rd1["paymentnote_gid"].ToString()),
                            reference = rd1["reference"].ToString(),
                            total_amount = double.Parse(rd1["total_amount"].ToString()),
                            customercancellation_charge = rd1["customercancellation_charge"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            customerrefund_amount = rd1["customerrefund_amount"].ToString(),
                            vendor_amount = double.Parse(rd1["vendor_amount"].ToString()),
                            vendorcancellation_charge = rd1["vendorcancellation_charge"].ToString(),
                            vendorrefund_amount = rd1["vendorrefund_amount"].ToString(),



                        });
                    }

                    submit.customerinvoicelist = summary;
                    submit.status = true;
                    submit.message = "Records loaded sucessfully";
                    //        }
                    //        else
                    //        {
                    //            val.status = false;
                    //            val.message = "Internal Error Occured";
                    //        }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                submit.status = false;
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
            return submit;

        }
        public MdlRefundServiceType editrefund(MdlRefundServiceType val, string user_gid)
        {
            MdlRefundServiceType submit = new MdlRefundServiceType();
            try
            {
                cmd = new MySqlCommand("sp_sel_editrefund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);

                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.refund_gid = rd["refund_gid"].ToString();
                    submit.salesinvoice_refno = rd["salesinvoice_refno"].ToString();
                    submit.customerinvoice_gid = rd["customerinvoice_gid"].ToString();
                    submit.refund_number = rd["refund_number"].ToString();
                    submit.refund_date = rd["refund_date"].ToString();
                    submit.invoice_date = rd["invoice_date"].ToString();
                    submit.customer_name = rd["customer_name"].ToString();
                    submit.contact_number = rd["contact_number"].ToString();
                    submit.payment_mode = rd["payment_mode"].ToString();
                    submit.bank_name = rd["bank_name"].ToString();
                    submit.bank_gid = rd["bank_gid"].ToString();
                    submit.transaction_refnumber = rd["transaction_refnumber"].ToString();
                    submit.invoice_amount = rd["invoice_amount"].ToString();
                    submit.refund_amount = rd["refund_amount"].ToString();
                    submit.paid_amount = rd["paid_amount"].ToString();
                    submit.vendorrefund_grandtotal = rd["vendorrefund_grandtotal"].ToString();
                    submit.refund_total = rd["refund_total"].ToString();
                    submit.discount_amount = rd["discount_amount"].ToString();
                    submit.net_amount = rd["net_amount"].ToString();
                    submit.balance_amount = rd["balance_amount"].ToString();
                    submit.notes = rd["notes"].ToString();
                    submit.paid_status = rd["paid_status"].ToString();


                }
                rd.Close();

                var summary = new List<customerinvoicelist>();
                cmd = new MySqlCommand("sp_sel_editrefund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);

                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {

                        summary.Add(new customerinvoicelist
                        {
                            refunddtl_gid = rd1["refunddtl_gid"].ToString(),
                            service_type = rd1["service_type"].ToString(),
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            paymentnote_gid = int.Parse(rd1["paymentnote_gid"].ToString()),
                            reference = rd1["reference"].ToString(),
                            total_amount = double.Parse(rd1["total_amount"].ToString()),
                            customercancellation_charge = rd1["customercancellation_charge"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            customerrefund_amount = rd1["customerrefund_amount"].ToString(),
                            vendor_amount = double.Parse(rd1["vendor_amount"].ToString()),
                            vendorcancellation_charge = rd1["vendorcancellation_charge"].ToString(),
                            vendorrefund_amount = rd1["vendorrefund_amount"].ToString(),



                        });
                    }

                    submit.customerinvoicelist = summary;
                    submit.status = true;
                    submit.message = "Records loaded sucessfully";
                    //        }
                    //        else
                    //        {
                    //            val.status = false;
                    //            val.message = "Internal Error Occured";
                    //        }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                submit.status = false;
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
            return submit;

        }

        public refundledgerdetails daCustomerRefundLedgerReport(refundledgerdetails val)
        {
            refundledgerdetails submit = new refundledgerdetails();
            try
            {
                var summary = new List<Refundlist>();
                cmd = new MySqlCommand("sp_sel_customerrefundledgertransactiondtl");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new Refundlist
                        {
                            service_type = rd1["service_detail"].ToString(),
                            invoice_amount = double.Parse(rd1["servicetype_amount"].ToString()),
                            customer_cancellation = double.Parse(rd1["customercancellation_charge"].ToString()),
                            customer_refund = double.Parse(rd1["customerrefund_amount"].ToString())
                        });
                    }

                    submit.refundlist = summary;
                    submit.status = true;
                    submit.message = "Records loaded sucessfully";
                    //        }
                    //        else
                    //        {
                    //            val.status = false;
                    //            val.message = "Internal Error Occured";
                    //        }
                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                submit.status = false;
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
            return submit;

        }
    }

}