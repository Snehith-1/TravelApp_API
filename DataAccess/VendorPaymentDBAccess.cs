using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;


namespace DataAccess
{
    public class VendorPaymentDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd, cmd1 = null;
        MySqlDataReader rd;
        string error;
        public VendorPayment vendorpaymentoverallsubmit()
        {
            VendorPayment venpay = new VendorPayment();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentoverallsubmit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            //paymentdate = rd["payment_date"].ToString(),
                            //salesordergid = rd["salesorder_Gid"].ToString(),
                            //vendorname = rd["vendor_name"].ToString(),
                            //paymentmethod = rd["payment_method"].ToString(),
                            //paidamount = rd["paid_amount"].ToString(),
                            //vendorgid = rd["vendor_gid"].ToString(),
                            //paymentvalue = rd["payment_value"].ToString(),
                            //createdby = rd["created_by"].ToString(),
                            //createddate = rd["createddate"].ToString()
                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString(),

                        });
                    }
                    venpay.VendorPaymentlist = summary;
                    venpay.status = true;

                }
                else
                {
                    venpay.status = false;
                    venpay.message = "Internal Error Occur ";
                }
                rd.Close();
            }
            catch (Exception e)
            {
                venpay.status = false;
                venpay.message = "Internal Error Occur ";
                error = e.ToString();

            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return venpay;
        }

        public VendorPaymentmodel vendorpaymentselect(VendorPaymentdetails val, string userGid)
        {
            VendorPayment venpay = new VendorPayment();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentaddselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {

                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            activity_name = rd["activity_name"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            process = rd["process"].ToString()

                        });
                    }
                    val.VendorPaymentlist = summary;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occur ";
                }
                rd.Close();
            }
            catch (Exception e)
            {
                val.status = false;
                val.message = "Internal Error Occur ";
                error = e.ToString();

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
        public VendorPaymentmodel vendorpaymentselectsummary(VendorPaymentdetails val, string userGid)
        {
            VendorPayment venpay = new VendorPayment();
            try
            {
                cmd = new MySqlCommand("sp_sel_rvendorpaymentaddselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            service_type = rd["service_type"].ToString()

                        });
                    }
                    val.VendorPaymentlist = summary;
                    val.status = true;

                }
                else
                {
                    val.status = true;
                    val.message = "No Record Found";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occur ";
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
        public VendorPaymentmodel vendorpaymentsubmit(VendorPaymentdetails val, string usergid)
        {
            VendorPaymentmodel sub = new VendorPaymentmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentaddselect");
                //need to check with sp parameters

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    val.vendor_gid = rd["vendor_gid"].ToString();
                    val.vendor_name = rd["vendor_name"].ToString();
                    val.address = rd["address1"].ToString();
                    val.vendor_amount = rd["vendor_amount"].ToString();

                }
                rd.Close();
                cmd1 = new MySqlCommand("sp_sel_vendorpaymentnote");
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                //cmd1.Connection = con;

                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd1);
                var summary = new List<vendoractivitylist>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new vendoractivitylist
                        {
                            paymentnotemain_gid = rd1["paymentnotemain_gid"].ToString(),
                            salesorder_gid = int.Parse(rd1["salesorder_gid"].ToString()),
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            vendor_amount = Double.Parse(rd1["vendor_amount"].ToString()),
                            //createddate =DateTime.Parse(rd["createddate"].ToString()),
                        });
                        val.vendoractivitylist = summary;
                        val.status = true;
                    }
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occur ";
                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                sub.status = false;
                sub.message = "Internal error occured";
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

        public VendorPayment vendorpaymentmain(VendorPaymentdetails val, string userGid)
        {
            VendorPayment venpay = new VendorPayment();
            try
            {

                cmd = new MySqlCommand("sp_ins_vendorpaymentreceipt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_amount", val.vendor_amount);
                cmd.Parameters.AddWithValue("p_address", val.address);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {

                    //foreach (var data in val.VendorPaymentlist)
                    //{
                    //    cmd = new MySqlCommand("");
                    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //    //cmd.Parameters.AddWithValue("p_vendorpaymentnotereceipt_gid", data.paymentreceiptgid);
                    //    cmd.Parameters.AddWithValue("p_paymentnotemain_gid", data.paymentnotemaingid);
                    //    cmd.Parameters.AddWithValue("p_salesorder_gid", data.salesordergid);
                    //    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    //}
                    venpay.status = true;
                    venpay.message = "Records added successfully!";
                }

                else
                {
                    venpay.status = false;
                    venpay.message = "Internal Error Occur ";
                }
            }
            catch (Exception e)
            {
                venpay.status = false;
                venpay.message = "Internal Error Occur ";
                error = e.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return venpay;
        }
        public VendorPaymentdetails paymentselect(VendorPaymentdetails val)
        {
            VendorPaymentdetails pay = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_paymentselectvendor");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    {
                        pay.vendor_gid = rd["vendor_gid"].ToString();
                        pay.vendor_name = rd["vendor_name"].ToString();
                        pay.vendor_company_name = rd["vendor_company_name"].ToString();
                        pay.vendor_code = rd["vendor_code"].ToString();
                        //pay.currency_name = rd["currency_name"].ToString();
                    }
                }
                var summary = new List<VendorPaymentlist>();
                foreach (var data in val.VendorPaymentlist)
                {
                    cmd = new MySqlCommand("sp_sel_selectvendorpaymentlist");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", data.salesorder_gid);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        while (rd1.Read())
                        {
                            summary.Add(new VendorPaymentlist
                            {
                                vendor_gid = rd1["vendor_gid"].ToString(),
                                vendor_name = rd1["vendor_name"].ToString(),
                                activity_name = rd1["activity_name"].ToString(),
                                vendor_amount = rd1["vendor_amount"].ToString(),
                                paymentnote_gid = rd1["paymentnote_gid"].ToString(),
                                salesorder_gid = rd1["salesorder_gid"].ToString(),
                                invoice_refnumber = rd1["invoice_refnumber"].ToString(),
                                invoice_date = rd1["invoice_date"].ToString(),
                                process = rd1["process"].ToString(),
                         
                            });
                        }
                    }
                    rd1.Close();
                    pay.VendorPaymentlist = summary;
                    pay.status = true;
                    pay.message = "Records loaded successfully!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                pay.status = false;
                pay.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return pay;
        }
        public VendorPaymentdetails salesvendorpaymentselect(VendorPaymentdetails val)
        {
            VendorPaymentdetails pay = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_selectvendorpayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    {
                        pay.vendor_gid = rd["vendor_gid"].ToString();
                        pay.vendor_name = rd["vendor_name"].ToString();
                        pay.vendor_company_name = rd["vendor_company_name"].ToString();
                        pay.vendor_code = rd["vendor_code"].ToString();
                        pay.vendor_address_line1 = rd["vendor_address_line1"].ToString();
                        pay.vendor_number = rd["vendor_number"].ToString();
                    }
                }





                var summary = new List<VendorPaymentlist>();
                    cmd = new MySqlCommand("sp_sel_vendorpaymentlist");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                    if (rd1.HasRows == true)
                    {
                        while (rd1.Read())
                        {
                            summary.Add(new VendorPaymentlist
                            {
                                vendor_gid = rd1["vendor_gid"].ToString(),
                                vendor_name = rd1["vendor_name"].ToString(),
                                //activity_name = rd1["activity_name"].ToString(),
                                vendor_amount = rd1["vendor_amount"].ToString(),
                                paymentnote_gid = rd1["paymentnote_gid"].ToString(),
                                salesorder_gid = rd1["salesorder_gid"].ToString(),
                                salesorder_refnumber = rd1["salesorder_refnumber"].ToString(),
                                //created_date = DateTime.Parse(rd1["created_date"].ToString()),
                                process = rd1["process"].ToString(),
                                  invoice_refnumber = rd1["invoice_refnumber"].ToString(),
                                invoice_date = rd1["invoice_date"].ToString(),
                                vendor_refundamount = rd1["vendor_refundamount"].ToString(),
                                vendorcancellation_charges = double.Parse(rd1["vendorcancellation_charges"].ToString()),
                                ////process = rd1["process"].ToString(),
                                //vendorname = rd1["contactperson_name"].ToString(),
                                //vendorcompanyname = rd1["vendor_companyname"].ToString(),
                                //amount = Double.Parse(rd1["vendor_amt"].ToString()),
                                //status = rd1["status"].ToString(),
                            });
                        }
                    }
                    rd1.Close();
                    pay.VendorPaymentlist = summary;
                    pay.status = true;
                    pay.message = "Records loaded successfully!";

                rd.Close();
            }
            catch (Exception ex)
            {
                pay.status = false;
                pay.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return pay;
        }
        public VendorPaymentmodel paymentoverallsubmit(VendorPaymentdetails val, string user_gid)
        {
            VendorPaymentmodel submit = new VendorPaymentmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorinvoice_refnumbervalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.status = false;
                    submit.message = "Vendor Invoice RefNo Already Exists!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_vendorinvoice");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_vendorinvoice_amount", val.vendorinvoice_amount); //grandtotal changed as  vendorinvoice_amount
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                    cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_currency_name", val.currency_name);
                    cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                    cmd.Parameters.AddWithValue("p_vendorinvoice_gid", "");
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.VendorPaymentlist)
                        {
                            cmd = new MySqlCommand("sp_ins_vendorinvoicedtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_vendorinvoice_gid", "0");
                            cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnote_gid);
                            cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                            cmd.Parameters.AddWithValue("p_created_by", user_gid);
                            cmd.Parameters.AddWithValue("p_total_amount", data.vendor_amount);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            cmd = new MySqlCommand("sp_upt_vendorinvoicedtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnotemain_gid);
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
        public VendorPaymentmodel salesvendoroverallpayment(VendorPaymentdetails val, string user_gid)
        {
            VendorPaymentmodel submit = new VendorPaymentmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_vendorinvoicereferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {

                    cmd = new MySqlCommand("sp_sel_vendorinvoicereferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    val.vendorinvoice_refnumber = rd["vendorinvoice_refnumber"].ToString();
                    val.status = true;

                    int outputparam;
                    cmd = new MySqlCommand("sp_ins_vendorinvoicepayment");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_vendorinvoice_amount", val.vendorinvoice_amount); //grandtotal changed as  vendorinvoice_amount
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", val.vendorinvoice_refnumber);
                    cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_currency_name", val.currency_name);
                    cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                    cmd.Parameters.AddWithValue("p_vendorinvoice_gid", "");
                    cmd.Parameters.AddWithValue("p_vendorpayment_gid", "");
                    cmd.Parameters.AddWithValue("p_reference_number", val.reference_number);
                    cmd.Parameters.AddWithValue("p_receipt_date", val.receipt_date);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);

                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.VendorPaymentlist)
                        {
                            cmd = new MySqlCommand("sp_ins_vendorinvoicedtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_vendorinvoice_gid", "0");
                            cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnote_gid);
                            cmd.Parameters.AddWithValue("p_salesorder_gid", data.salesorder_gid);
                            cmd.Parameters.AddWithValue("p_created_by", user_gid);
                            cmd.Parameters.AddWithValue("p_total_amount", data.vendor_amount);
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
                    sw.WriteLine("Vendor Invoice Number:" + val.vendorinvoice_refnumber);
                    sw.WriteLine("Error: Error occured while Raise Vendor Invoice");
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
        public VendorPaymentdetails vendorinvoicesummary(string val)
        {
            VendorPaymentdetails vendorinvoice = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorinvoicelist>();
                if (rd.HasRows == true)
                {
                    int i = 1;
                    while (rd.Read())
                    {
                        summary.Add(new vendorinvoicelist
                        {
                            ref_no = i,
                            vendorinvoice_gid = rd["vendorinvoice_gid"].ToString(),
                            invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd-MM-yyyy"),
                            vendor_name = rd["vendor_name"].ToString(),
                            salesinvoice_refnumber = rd["salesinvoice_refnumber"].ToString(),
                            vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            net_amount = double.Parse(rd["net_amount"].ToString()),
                            discount_amount = double.Parse(rd["discount_amount"].ToString()),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            company_code = val
                        });
                        i = i + 1;
                    }
                    vendorinvoice.vendorinvoicelist = summary;
                    vendorinvoice.status = true;

                }
                else
                {
                    vendorinvoice.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendorinvoice.status = false;
                vendorinvoice.message = "Internal Error Occured!";
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
            return vendorinvoice;
        }
        public VendorPaymentdetails paymentvendorinvoicesummary()
        {
            VendorPaymentdetails vendorinvoice = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_paymentvendorinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        if (Math.Abs(double.Parse(rd["pending_amount"].ToString())) <= 0.00)
                            {

                        }
                        else
                        {
                            summary.Add(new vendorinvoicelist
                            {
                                vendorinvoice_gid = rd["vendorinvoice_gid"].ToString(),
                                invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd-MM-yyyy"), //invoicedate as created_date
                                vendor_name = rd["vendor_name"].ToString(),
                                vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                                vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString()),
                                created_by = rd["created_by"].ToString(),
                                vendor_gid = rd["vendor_gid"].ToString(),
                                net_amount = double.Parse(rd["net_amount"].ToString()),
                                discount_amount = double.Parse(rd["discount_amount"].ToString()),
                                invoice_refnumber = rd["invoice_refnumber"].ToString(),
                                debit_amount = double.Parse(rd["debit_amount"].ToString()),
                                pending_amount = double.Parse(rd["pending_amount"].ToString()),
                            });
                        }
                        
                    }
                    vendorinvoice.vendorinvoicelist = summary;
                    vendorinvoice.status = true;

                }
                else
                {
                    vendorinvoice.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendorinvoice.status = false;
                vendorinvoice.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendorinvoice;
        }
        public VendorPaymentmodel vendorinvoicedelete(string values)
        {
            VendorPaymentmodel vendorinvoicedelete = new VendorPaymentmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorinvoicepayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_gid", values);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        cmd = new MySqlCommand("sp_upt_vendorinvoicepayment");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_paymentnote_gid", rd["paymentnote_gid"].ToString());
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }
                }
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_del_vendorinvoice");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_invoice_gid", values);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        vendorinvoicedelete.status = true;
                        vendorinvoicedelete.message = "Deleted Successfully";

                    }
                    else
                    {
                        vendorinvoicedelete.status = false;
                        vendorinvoicedelete.message = " Internal Error Occured!";
                    }
                }
            }
            catch (Exception ex)
            {
                vendorinvoicedelete.status = false;
                vendorinvoicedelete.message = " Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendorinvoicedelete;
        }
        public VendorPaymentdetails vendorpaymentaddselect(VendorPaymentdetails val)
        {
            VendorPaymentdetails vendorinvoice = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    vendorinvoice.vendor_gid = rd["vendor_gid"].ToString();
                    vendorinvoice.vendor_name = rd["name"].ToString();
                    vendorinvoice.contact_number = rd["contact_number"].ToString();
                    vendorinvoice.vendor_company_name = rd["vendor_company_name"].ToString();
                    vendorinvoice.vendor_code = rd["vendor_code"].ToString();
                }
                else
                {
                    vendorinvoice.status = false;
                    vendorinvoice.message = "internal error occured";
                }
                rd.Close();

                var summary = new List<vendorinvoicelist>();
                foreach (var data in val.vendorinvoicelist)
                {
                    cmd = new MySqlCommand("sp_sel_vendorpaymentdetailselect");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_vendorinvoice_gid", data.vendorinvoice_gid);
                    //cmd.Parameters.AddWithValue("p_vendor_gid", val.vendorgid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    summary.Add(new vendorinvoicelist
                    {
                        vendorinvoice_gid = rd["vendorinvoice_gid"].ToString(),
                        invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                        vendor_name = rd["vendor_name"].ToString(),
                        vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                        vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString()),
                        created_by = rd["created_by"].ToString(),
                        created_date = DateTime.Parse(rd["created_date"].ToString()),
                        vendor_gid = rd["vendor_gid"].ToString(),
                        debit_amount = double.Parse(rd["debit_amount"].ToString()),
                        pending_amount = double.Parse(rd["pending_amount"].ToString()),
                    });
                }
                vendorinvoice.vendorinvoicelist = summary;
                vendorinvoice.status = true;
            }
            catch (Exception ex)
            {
                vendorinvoice.status = false;
                vendorinvoice.message = "internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendorinvoice;
        }
        public VendorPaymentmodel paymentvendoroverallsubmit(VendorPaymentdetails val, string user_gid)
        {
            VendorPaymentmodel submit = new VendorPaymentmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference_number", val.payment_refnumber); //receiptrefno as reference_no
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.status = false;
                    submit.message = "Vendor Payment RefNo Alreay Exists!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_vendorpayment");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_payment_amount", val.payment_amount);
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_reference_number", val.payment_refnumber);
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
                        foreach (var data in val.vendorinvoicelist)
                        {
                            cmd = new MySqlCommand("sp_ins_vendorpaymentdtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_vendorpayment_gid", "0");
                            cmd.Parameters.AddWithValue("p_vendorinvoice_gid", data.vendorinvoice_gid);
                            cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                            cmd.Parameters.AddWithValue("p_created_by", user_gid);
                            cmd.Parameters.AddWithValue("p_vendorinvoice_amount", data.pending_amount);
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
        public VendorPaymentdetails vendorpaymentsummary()
        {
            VendorPaymentdetails vendor = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorpaymentsummarylist>();
                if (rd.HasRows == true)
                {
                    int i = 1;
                    while (rd.Read())
                    {

                        summary.Add(new vendorpaymentsummarylist
                        {
                            ref_no = i,
                            vendorpayment_gid = rd["vendorpayment_gid"].ToString(),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            payment_amount = rd["payment_amount"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            reference_number = rd["reference_number"].ToString(),
                            payment_date = rd["payment_date"].ToString(),
                        });
                        i = i + 1;
                    }
                    vendor.vendorpaymentsummarylist = summary;
                    vendor.status = true;

                }
                else
                {
                    vendor.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }
        public VendorPaymentdetails paymentlist(string val)
        {
            VendorPaymentdetails vendor = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_paymentlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.Read())
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            process = rd["process"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            activity_name = rd["activity_name"].ToString(),
                        });
                    }
                    vendor.VendorPaymentlist = summary;
                    vendor.status = true;
                    rd.Close();
                }
                else
                {
                    vendor.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }
        public VendorPaymentdetails vendorpaymentprint(string val, string userGid)
        {
            VendorPaymentdetails print = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentprint");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorpayment_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    print.vendorpayment_gid = rd["vendorpayment_gid"].ToString();
                    print.vendor_name = rd["vendor_name"].ToString();
                    print.vendor_company_name = rd["vendor_company_name"].ToString();
                    print.vendor_code = rd["vendor_code"].ToString();
                    print.vendor_gid = rd["vendor_gid"].ToString();
                    print.contact_number = rd["contact_number"].ToString();
                    print.remarks = rd["remarks"].ToString();
                    print.payment_refnumber = rd["reference_number"].ToString();
                    print.payment_date = rd["payment_date"].ToString(); //receiptdate as payment_date
                    print.bank_name = rd["bank_name"].ToString();
                    print.transaction_number = rd["transaction_number"].ToString();
                    print.payment_mode = rd["payment_mode"].ToString();
                }
                rd.Close();
                var summary = new List<vendorinvoicelist>();
                cmd = new MySqlCommand("sp_sel_vendorpaymentprintsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorpayment_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new vendorinvoicelist
                        {
                            vendorpaymentdtl_gid = int.Parse(rd["vendorpaymentdtl_gid"].ToString()),
                            invoice_gid = rd["vendorinvoice_gid"].ToString(),
                            invoice_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),  //invoicedate as   created_date                      
                            created_by = rd["created_by"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString()),
                            debit_amount = double.Parse(rd["debit_amount"].ToString()),
                            pending_amount = double.Parse(rd["pending_amount"].ToString()),
                        });
                    }
                }
                print.vendorinvoicelist = summary;
                print.status = true;
                print.message = "Records loaded successfully!";
                rd.Close();
            }
            catch (Exception ex)
            {
                print.status = false;
                print.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return print;
        }
        public VendorPaymentdetails paymentreferenceno(string user_gid)
        {
            VendorPaymentdetails Val = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_ins_paymentreferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_payment_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_paymentreferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.payment_refnumber = rd["payment_refnumber"].ToString();
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
        public VendorPaymentdetails vendorinvoicereferenceno(string user_gid)
        {
            VendorPaymentdetails Val = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_ins_vendorinvoicereferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_vendorinvoicereferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.vendorinvoice_refnumber = rd["vendorinvoice_refnumber"].ToString();
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
        public VendorPaymentdetails vendorinvoiceview(VendorPaymentdetails val)
        {
            VendorPaymentdetails pay = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorinvoiceview");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    pay.vendor_gid = rd["vendor_gid"].ToString();
                    pay.vendor_name = rd["vendor_name"].ToString();
                    pay.vendor_company_name = rd["vendor_company_name"].ToString();
                    pay.vendor_code = rd["vendor_code"].ToString();
                    pay.currency_name = rd["currency_name"].ToString();
                    pay.exchange_rate = double.Parse(rd["exchange_rate"].ToString());
                    pay.remarks = rd["remarks"].ToString();
                    pay.vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString());
                    pay.discount_amount = double.Parse(rd["discount_amount"].ToString());
                    pay.net_amount = double.Parse(rd["net_amount"].ToString()); //grandtotal as net_amount
                }
                rd.Close();

                var summary = new List<VendorPaymentlist>();
                cmd = new MySqlCommand("sp_sel_vendorinvoicelist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            activity_name = rd1["activity_name"].ToString(),
                            vendor_amount = rd1["vendor_amount"].ToString(),
                            paymentnote_gid = rd1["paymentnote_gid"].ToString(),
                            salesorder_gid = rd1["salesorder_gid"].ToString(),
                            //karthi: commented this salesorder_refno = rd1["salesorder_gid"].ToString(),//salesorderrefno as salesorder_gid
                            process = rd1["process"].ToString(),
                            vendorinvoice_gid = val.vendorinvoice_gid


                        });
                    }

                    pay.VendorPaymentlist = summary;
                    pay.status = true;
                    pay.message = "Recordes loaded successfully!";

                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                pay.status = false;
                pay.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return pay;
        }
        public VendorPaymentdetails salesvendorinvoiceview(VendorPaymentdetails val)
        {
            VendorPaymentdetails pay = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesvendorinvoiceview");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    pay.vendor_gid = rd["vendor_gid"].ToString();
                    pay.vendor_name = rd["vendor_name"].ToString();
                    pay.vendor_company_name = rd["vendor_company_name"].ToString();
                    pay.invoice_refnumber = rd["invoice_refnumber"].ToString();
                    pay.vendor_code = rd["vendor_code"].ToString();
                    pay.vendor_number = rd["vendor_number"].ToString();
                    pay.vendor_address_line1 = rd["vendor_address_line1"].ToString();
                    pay.exchange_rate = double.Parse(rd["exchange_rate"].ToString());
                
                    pay.vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString());
                 
                    pay.net_amount = double.Parse(rd["net_amount"].ToString()); //grandtotal as net_amount
                    pay.reference_number = rd["reference_number"].ToString();
                    pay.payment_date = rd["payment_date"].ToString();
                    pay.payment_mode = rd["payment_mode"].ToString();
                    pay.bank_gid = rd["bank_gid"].ToString();
                    pay.bank_name = rd["bank_name"].ToString();
                    pay.transaction_number = rd["transaction_number"].ToString();
                    pay.terms_conditions = rd["terms_conditions"].ToString();
                }
                rd.Close();

                var summary = new List<VendorPaymentlist>();
                cmd = new MySqlCommand("sp_sel_salesvendorinvoicelist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendorinvoice_gid", val.vendorinvoice_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            activity_name = rd1["activity_name"].ToString(),
                            vendor_amount = rd1["vendor_amount"].ToString(),
                            paymentnote_gid = rd1["paymentnote_gid"].ToString(),
                            salesorder_gid = rd1["salesorder_gid"].ToString(),
                            //karthi: commented this salesorder_refno = rd1["salesorder_gid"].ToString(),//salesorderrefno as salesorder_gid
                            process = rd1["process"].ToString(),
                            invoice_date = rd1["invoice_date"].ToString(),
                            invoice_refnumber = rd1["invoice_refnumber"].ToString(),
                            //vendorinvoice_gid = val.vendorinvoice_gid


                        });
                    }

                    pay.VendorPaymentlist = summary;
                    pay.status = true;
                    pay.message = "Recordes loaded successfully!";

                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                pay.status = false;
                pay.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return pay;
        }
        public VendorPaymentdetails vendorstatementreport(VendorPaymentdetails val)
        {
            VendorPaymentdetails pay = new VendorPaymentdetails();
            try
            {
                var summary = new List<VendorPaymentlist>();
                cmd = new MySqlCommand("sp_sel_vendorinvoicestatement");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        summary.Add(new VendorPaymentlist
                        {
                            vendor_gid = rd1["vendor_gid"].ToString(),
                            vendor_name = rd1["vendor_name"].ToString(),
                            activity_name = rd1["activity_name"].ToString(),
                            vendor_amount = rd1["vendor_amount"].ToString(),
                            paymentnote_gid = rd1["paymentnote_gid"].ToString(),
                            salesorder_gid = rd1["salesorder_gid"].ToString(),
                            //karthi: commented this salesorder_refno = rd1["salesorder_gid"].ToString(),//salesorderrefno as salesorder_gid
                            process = rd1["process"].ToString(),
                            payment_amount = double.Parse(rd1["payment_amount"].ToString()),
                            vendor_invoice_refnumber = rd1["vendor_invoice_refnumber"].ToString(),
                            invoice_date = rd1["invoice_date"].ToString(),
                            invoice_amount = rd1["invoice_amount"].ToString(),
                            invoice_refnumber = rd1["invoice_refnumber"].ToString(),
                            refund_amount = double.Parse(rd1["refund_amount"].ToString()),
                            vendorcancellation_charges = double.Parse(rd1["vendorcancellation_charges"].ToString()),
                            //vendorinvoice_gid = val.vendorinvoice_gid


                        });
                    }

                    pay.VendorPaymentlist = summary;
                    pay.status = true;
                    pay.message = "Recordes loaded successfully!";

                }
                rd1.Close();
            }
            catch (Exception ex)
            {
                pay.status = false;
                pay.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return pay;
        }

        public VendorPaymentdetails daVendorOutstandingSummary(string val)
        {
            VendorPaymentdetails vendor = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendoroutstandingsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        if(rd["vendor_amount"].ToString() != rd["paid_amount"].ToString())
                        {
                            summary.Add(new VendorPaymentlist
                            {
                                vendor_name = rd["vendor_name"].ToString(),
                                vendor_amount = rd["vendor_amount"].ToString(),
                                paid_amount = rd["paid_amount"].ToString(),
                            });
                        }
                    }
                    vendor.VendorPaymentlist = summary;
                    vendor.status = true;
                    rd.Close();
                }
                else
                {
                    vendor.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }
        public VendorPaymentdetails vendorledgeroutstandingsummary(string val)
        {
            VendorPaymentdetails vendor = new VendorPaymentdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorledgeroutstandingsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorPaymentlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                       
                            summary.Add(new VendorPaymentlist
                            {
                                vendor_amount = rd["vendor_amount"].ToString(),
                                invoice_date = rd["invoice_date"].ToString(),
                                invoice_refnumber = rd["invoice_refnumber"].ToString(),
                                payment_amount = double.Parse(rd["payment_amount"].ToString()),
                                outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            });
                       
                    }
                    vendor.VendorPaymentlist = summary;
                    vendor.status = true;
                    rd.Close();
                }
                else
                {
                    vendor.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }


    }

}