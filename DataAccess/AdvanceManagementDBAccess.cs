using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class AdvanceManagementDBAccess
    {
        
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public advancemanagement advancemanagementsummary()
        {
            advancemanagement dtl = new advancemanagement();
            try
            {
                cmd = new MySqlCommand("sp_sel_advancesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Advancemanagementlistitem>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {  
                         summary.Add(new Advancemanagementlistitem
                        {

                             contact_details = rd["contact_details"].ToString(),
                             salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                             customer_name = rd["customer_name"].ToString(),
                             customer_gid = rd["customer_gid"].ToString(),
                             advance_amount = double.Parse(rd["advance_amount"].ToString()),
                             paid_amount = double.Parse(rd["paid_amount"].ToString()),
                             outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                             national_id = rd["national_id"].ToString()        

                           
                        });
                    }
                    dtl.Advancemanagementlistitem = summary;
                    dtl.status = true;
                    rd.Close();

                }

                else
                {
                    dtl.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                dtl.status = false;
                dtl.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return dtl;
        }
        public Advancecustomerdetails advancemanagementinvoicesummary(Advancecustomerdetails dtl)
        {
            Advancecustomerdetails val = new Advancecustomerdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_advanceinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid",dtl.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Advancemanagementlistitem>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Advancemanagementlistitem
                        {

                            contact_details = rd["contact_details"].ToString(),
                            //salesorder_refno = rd["salesorder_refno"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            advance_amount = double.Parse(rd["advance_amount"].ToString()),
                            paidadvance_amount = double.Parse(rd["paidadvance_amount"].ToString()),
                            advanceoutstanding_amount = double.Parse(rd["advanceoutstanding_amount"].ToString()),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString()


                        });
                    }
                    val.Advancemanagementlistitem = summary;
                    val.status = true;
                    rd.Close();

                }

                else
                {
                    val.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return val;//
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
                            invoice_date = rd["created_date"].ToString(), //invoicedate changed as created_date
                            customer_name = rd["customer_name"].ToString(),
                            //contactdetils = rd["contact_details"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),//invoiceid changed as customerinvoice_gid
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),//invoiceamountwithtax
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            //customerid=int.Parse(rd["customer_gid"].ToString())
                            // company_code=val
                          
                           
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
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.national_id = rd["national_id"].ToString();
                    customerinvoice.advance_amount = 0.0;
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
                //        invoiceamountwithtax = Double.Parse(rd["invoice_value"].ToString()),
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
    }
}