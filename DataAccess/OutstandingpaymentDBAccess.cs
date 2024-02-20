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
    public class OutstandingpaymentDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public customerinvoicedetail outstandingpaymentaddselect(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_outstandingpaymentaddselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);

                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //var summary = new List<customerinvoicelist>();               
                if (rd.Read())
                {
                    customerinvoice.customerinvoice_gid = rd["customerinvoice_gid"].ToString();

                    customerinvoice.customer_gid = rd["customer_gid"].ToString();
                    customerinvoice.invoice_amount = double.Parse(rd["invoice_amount"].ToString());
                    customerinvoice.customer_name = rd["customer_name"].ToString();
                    customerinvoice.vendor_name = rd["vendor_name"].ToString();
                    //customerinvoice.vendor_amount = double.Parse(rd["vendor_amount"].ToString());
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.national_id = rd["national_id"].ToString();
                    customerinvoice.paid_amount = double.Parse(rd["paid_amount"].ToString());
                    customerinvoice.balance_amount = double.Parse(rd["balance_amount"].ToString());
                    //customerinvoice.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
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


     

        public customerinvoicedetail airinvoiceaddselect(customerinvoicedetail val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_airsalesinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);

    
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //var summary = new List<customerinvoicelist>();               
                if (rd.Read())
                {
                    customerinvoice.air_gid = rd["air_gid"].ToString();
                    customerinvoice.epax_name = rd["epax_name"].ToString();
                    customerinvoice.eticket_number = rd["eticket_number"].ToString();
                    customerinvoice.epnr_no = rd["epnr_no"].ToString();
                    //customerinvoice.etrip_type = rd["etrip_type"].ToString();
                    customerinvoice.eflag = rd["eflag"].ToString();
                    customerinvoice.eagent_gid = rd["eagent_gid"].ToString();
                    customerinvoice.efirstplace_from =rd["efirstplace_from"].ToString();
                    customerinvoice.efirstplace_to = rd["efirstplace_to"].ToString();
                    customerinvoice.efirststart_time =rd["efirststart_time"].ToString();
                    customerinvoice.efirstend_time = rd["efirstend_time"].ToString();
                    customerinvoice.esecondplace_from = rd["esecondplace_from"].ToString();
                    customerinvoice.esecondplace_to = rd["esecondplace_to"].ToString();
                    customerinvoice.esecondstart_time = rd["esecondstart_time"].ToString();
                    customerinvoice.esecondend_time =rd["esecondend_time"].ToString();
                    customerinvoice.ecustomer_camount = rd["ecustomer_camount"].ToString();
                    //customerinvoice.evendor_name =rd["evendor_name"].ToString();
                    //customerinvoice.evendor_vamount = rd["evendor_vamount"].ToString();
                   
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


        //public customerinvoicedetail salesinvoiceupdate(customerinvoicedetail val)
        //{
        //    try
        //    {
        //        cmd = new MySqlCommand("sp_upt_salesinvoiceupdate");

        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gidd);
        //        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
        //        cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);  // changes made
        //        cmd.Parameters.AddWithValue("p_passenger_name", "");
        //        cmd.Parameters.AddWithValue("p_id_proof", val.id_proof);
        //        cmd.Parameters.AddWithValue("p_additional_proof", val.additional_proof);
        //        cmd.Parameters.AddWithValue("p_photo", val.photo);
        //        cmd.Parameters.AddWithValue("p_anygovt_document", val.anygovt_document);
        //        cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
        //        //cmd.Parameters.AddWithValue("p_upload_documents", val.insamount);
        //        cmd.Parameters.AddWithValue("p_created_by", user_gid);
        //        mnrestult = DBAccess.ExecuteNonQuery(cmd);
        //        if (mnrestult == 1)
        //        {
        //            val.status = true;
        //            val.message = "Updated Successfully!";
        //        }
        //        else
        //        {
        //            val.status = false;
        //            val.message = "Internal Error Occured";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        val.status = true;
        //        val.message = "Internal Error Occured";
        //        error = ex.ToString();
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return val;
        //}

        public customerinvoicedetail outstandingpaymentoverallsubmit(customerinvoicedetail val, string user_gid)
        {
            customerinvoicedetail submit = new customerinvoicedetail();
            try
            {

                cmd = new MySqlCommand("sp_ins_outstandingamountpayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_outstandingpayment_gid", val.outstandingpayment_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", val.customerinvoice_gid);
                cmd.Parameters.AddWithValue("p_customer_firstname", val.customer_name);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                //cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                //cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);
                cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                cmd.Parameters.AddWithValue("p_payment_amount", val.payment_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);

                cmd.Parameters.AddWithValue("p_grandtotal_amount", val.grandtotal_amount);
                //cmd.Parameters.AddWithValue("p_created_by", "");
                //cmd.Parameters.AddWithValue("p_created_date", "");




                //cmd.Parameters.AddWithValue("p_customerreceipt_gid", "");//1
                //changes made in sql

                //cmd.Parameters.AddWithValue("p_journal_from",val.journal_from);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    
                    submit.status = true;
                    submit.message = "Records added successfully!";
                }
                else
                {
                    submit.status = false;
                    submit.message = "Internal error occured";
                }

                //rd.Close();
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
                    sw.WriteLine("Outstanding(customer Invoice Gid):" + val.customerinvoice_gid);
                    sw.WriteLine("Error: Error occured while Outstanding Payment Process");
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