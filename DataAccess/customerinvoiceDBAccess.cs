using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
namespace DataAccess
{
    public class customerinvoiceDBAccess
    {
        int mnresult, mnresult1 = 0;
        DataTable objtb1;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        MySqlDataAdapter sqlad = new MySqlDataAdapter();

        public customerinvoicedetail customerinvoiceaddselect(int val)
        {
            customerinvoicedetail customerinvoice = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoiceadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);               
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    customerinvoice.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    customerinvoice.customer_name = rd["customer_name"].ToString();
                    customerinvoice.billing_companyname = rd["billing_companyname"].ToString();
                    customerinvoice.contact_number = rd["contact_number"].ToString();
                    customerinvoice.email_address = rd["email_address"].ToString();
                    customerinvoice.billing_address = rd["billing_address"].ToString();
                    customerinvoice.billing_country = rd["billing_country"].ToString();
                    rd.Close();
                    cmd = new MySqlCommand("sp_sel_customerinvoicesalestoactivity");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val);                    
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    var summary = new List<customerinvoicelist>();
                    if (rd1.HasRows == true)
                    {
                        while (rd1.Read())
                        {
                            summary.Add(new customerinvoicelist
                            {
                                service_type = rd1["service_type"].ToString(),
                                remarks = rd1["remarks"].ToString(),
                                salesorder_refnumber =rd1["salesorder_refnumber"].ToString (),
                                total_amount = Double.Parse(rd1["total_amount"].ToString()),
                                salesorder_gid = int.Parse(rd1["salesorder_gid"].ToString()),
                                salesactivity_gid =int.Parse (rd1["salesactivity_gid"].ToString ())
                            });
                        }
                        customerinvoice.customerinvoicelist = summary;
                        customerinvoice.status = true;
                        //rd1.Close();
                    }
                    else
                    {
                        customerinvoice.status = false;
                        customerinvoice.message = "No Record found in details!";
                        //rd1.Close();
                    }
                    customerinvoice.status = true;
                    rd1.Close();
                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "No Records found!";
                }
                rd.Close();
                
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal error Occured!";
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
        public customerinvoicemodel Add(customerinvoicedetail val, string userGid)
        {
            customerinvoicemodel customerinvoice = new customerinvoicemodel();
            try
            {                
                cmd = new MySqlCommand("sp_ins_customerinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_created_by", userGid);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                cmd.Parameters.AddWithValue("p_tax", val.tax);
                cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                cmd.Parameters.AddWithValue("p_contact_type", val.contact_type);
                cmd.Parameters.AddWithValue("p_address", val.address);
                cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                cmd.Parameters.AddWithValue("p_invoice_gid", "");
               
               

                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_customerinvoice");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        val.invoice_gid = int.Parse(rd["invoice_gid"].ToString());
                        rd.Close();
                        cmd= new MySqlCommand("sp_sel_customerinvoicesalestoactivity");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);                                          
                        MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                        var summary = new List<customerinvoicelist>();
                        if (rd1.HasRows == true)
                        {
                            while (rd1.Read())
                            {
                                cmd = new MySqlCommand("sp_ins_customerinvoicedtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_invoice_gid", val.invoice_gid);
                                cmd.Parameters.AddWithValue("p_salesorder_refnumber", val.salesorder_refnumber);
                                cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                                cmd.Parameters.AddWithValue("p_remarks", val.remarks);

                                mnresult1 = DBAccess.ExecuteNonQuery(cmd);
                            }
                            if (mnresult1 == 1)
                            {
                                customerinvoice.status = true;
                                customerinvoice.message = "records added successfully!";

                            }                         
                        }
                    }
                    rd.Close();
                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "Internal Error Occured";
                }
            }
            catch(Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured";
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
        public customerinvoice Getall(string val, string user_gid)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_usercustomerinvoicesummary");
                cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;               
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<customerinvoicelist>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {
                            invoice_refnumber= rd["invoice_refnumber"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount =rd["invoice_amount"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            customer_gid = rd["customer_gid"].ToString(),
                            invoice_status = rd["invoice_status"].ToString(),
                            paid_amount = rd["paid_amount"].ToString(),
                            balance_amount = rd["balance_amount"].ToString(),
                            pnr_number = rd["pnr_number"].ToString(),
                            passenger_firstname = rd["passenger_firstname"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                            company_code =val
                    });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;
                    
                }
                else
                {
                    customerinvoice.status = true;
                    customerinvoice.message = "No Records Found";
                    //rd.Close();
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
        public customerinvoice salesinvoicesummarysearch(customerinvoicedetail values, string user_gid)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesinvoicesummarysearch");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                cmd.Parameters.AddWithValue("p_user_gid", user_gid);


                rd = DBAccess.ExecuteReader(cmd);
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
                            branch_name = rd["branch_name"].ToString()
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;

                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "No Records Found!";
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured";
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
        public customerinvoice salesinvoicesummarysearchfunction(customerinvoicedetail values, string user_gid)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesinvoicesummarysearchfunction");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
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
                            branch_name = rd["branch_name"].ToString()
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;

                }
                else
                {
                    customerinvoice.status = false;
                    customerinvoice.message = "No Records Found!";
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                customerinvoice.status = false;
                customerinvoice.message = "Internal Error Occured";
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
        public customerinvoice showallinvoicesummary(string val, string user_gid)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoicesummary");
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
                            passenger_firstname = rd["passenger_firstname"].ToString(),
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
                    //rd.Close();
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
        public customerinvoice customerstatement(string val)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerstatement");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<customerinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customerinvoicelist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            email_address = rd["email_address"].ToString(),
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
                    //rd.Close();
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


        public customerinvoice receiptcustomerinvoicesummary(string val)
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_receiptcustomerinvoicesummary");
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
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),//invoiceamountwithtax changed as invoice_amount<-> invoice_value
                            created_by = rd["created_by"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            customer_gid = rd["customer_gid"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                            outstanding_amount =rd["outstanding_amount"].ToString(), //paidamt changed outstanding_amount
                            //receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            paid_amount = rd["paid_amount"].ToString(),


                            company_code = val
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;
                    //rd.Close();
                }
                else
                {
                    customerinvoice.status = true;
                    customerinvoice.message = "No Records Found";
                    //rd.Close();
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
        public outstandingCustomerInvoiceList outstandingcustomerinvoicesummary(outstandingCustomerInvoiceList val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_oustandinginvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
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
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),//invoice_amount<->and totalwith_tax are same value
                            customer_gid = rd["customer_gid"].ToString(),
                            balance_amount = rd["balance_amount"].ToString(), // outstanding_amount
                            paid_amount = rd["paid_amount"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                        });
                        val.total_outstanding += double.Parse(rd["balance_amount"].ToString());
                        val.total_invoice_amount += double.Parse(rd["invoice_amount"].ToString());
                        val.total_paid_amount += double.Parse(rd["paid_amount"].ToString());
                    }
                    val.customerinvoicelist = summary;
                    val.status = true;
                }
                else
                {
                    val.status = true;
                    val.message = "No Records Found";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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
        public customerinvoicemodel Delete(string values)
        {
            customerinvoicemodel customerinvoicedelete = new customerinvoicemodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_customerinvoicebilling");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);                                 
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_del_customerinvoice");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_invoice_gid", values);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        customerinvoicedelete.status = true;
                        customerinvoicedelete.message = "Deleted Successfully";

                    }
                    else
                    {
                        customerinvoicedelete.status = false;
                        customerinvoicedelete.message = " Internal Error Occured!";
                    }
                }
            }
            catch (Exception ex)
            {
                customerinvoicedelete.status = false;
                customerinvoicedelete.message = " Internal Error Occured!";
                ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }                        
            return customerinvoicedelete;
        }
        
        public SalesOrderForm CustomerReport()
        {
            SalesOrderForm SOF = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_rcustomerreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesOrderFormList>();
                while (rd.Read())
                {
                    summary.Add(new SalesOrderFormList
                    {
                        salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                        customer_name = rd["customer_name"].ToString(),
                        customer_gid = rd["customer_gid"].ToString(),
                        contact_details = rd["contact_details"].ToString(),
                        total_amount = double.Parse(rd["total_amount"].ToString()),//service value, service type need
                        salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                        created_by = rd["created_by"].ToString(),
                        created_date = rd["created_date"].ToString(),
                        branch_name = rd["branch_name"].ToString(),
                        invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                        receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                        outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                        refund_amount= double.Parse(rd["refund_amount"].ToString()),
                        advance_amount= double.Parse(rd["advance_amount"].ToString())
                      
                     });
                }
                
                SOF.SalesOrderList = summary;
                SOF.status = true;
                rd.Close();
            }
           

            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error=ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }
       
        public customerinvoicedetail CustomerLedgerReport(customerinvoicedetail val)
        {
            //customerinvoicedetail val = new customerinvoicedetail();
            try
            {

                //customer sales order:
                cmd = new MySqlCommand("sp_sel_rcustomersalesordeerreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid); 
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesOrderFormList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SalesOrderFormList
                        {
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            customer_name = rd["customer_name"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),//receipt value from customerreceipt table
                            salesorder_refnumber= rd["salesorder_refnumber"].ToString(),
                            billing_companyname = rd["billing_companyname"].ToString(),
                            customer_type = rd["customer_type"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = rd["created_date"].ToString()
                            
                        });
                    }
                    
                    val.SalesOrderList = summary;
                    val.status = true;
                    
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured customersalesreport";
                    
                }

                rd.Close();
                //customer invoice:

                cmd = new MySqlCommand("sp_sel_rcustomerinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary1 = new List<customerinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary1.Add(new customerinvoicelist
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            pnr_number = rd["pnr_number"].ToString(),

                            //branch_name = rd["branch_name"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                           

                            customer_gid = rd["customer_gid"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            //nationalid = rd["national_id"].ToString(),
                            billing_address = rd["billing_address"].ToString(),
                            paid_amount = rd["paid_amount"].ToString(),
                            balance_amount = rd["balance_amount"].ToString(),
                            invoice_status = rd["invoice_status"].ToString(),
                            customer_type = rd["customer_type"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),
                        });
                    }
                    val.customerinvoicelist = summary1;
                    val.status = true;
                    
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while customer invoice ";
                    //rd.Close();
                }

                rd.Close();
                //customer receipt:

                cmd = new MySqlCommand("sp_sel_rcustomerreceipt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary2 = new List<customerreceiptreporlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary2.Add(new customerreceiptreporlist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            customer_type = rd["customer_type"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            receipt_method = rd["receipt_method"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            created_by = rd["first_name"].ToString()
                        });
                    }
                    val.customerreceiptreporlist = summary2;
                    val.status = true;
                }
                else
                {
                    val.status = false;
                }
                rd.Close();

                //outstanding report:
                cmd = new MySqlCommand("sp_sel_rcustomeroutstandingrpt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary3 = new List<customeroutstandingreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary3.Add(new customeroutstandingreport
                        {
                            //salesordergid = int.Parse(rd["salesorder_gid"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            paid_amount = double.Parse(rd["paid_amount"].ToString()), //paid amount  shows from customerinvoicedtl
                            //createdby = rd["created_by"].ToString(),
                            //createddate = rd["created_date"].ToString(),
                            invoice_date=rd["invoice_date"].ToString(),
                            customer_gid =rd["customer_gid"].ToString()
                        });
                    }
                    val.customeroutstandingreport = summary3;
                    val.status = true;
                    
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while outstanding report";
                    //rd.Close();
                }
                rd.Close();
                //customer dashboard report:
                cmd = new MySqlCommand("sp_sel_rcustomerdashboardrpt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary4 = new List<customerdashboardreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary4.Add(new customerdashboardreport
                        {
                            salesorder_count = rd["salesorder_count"].ToString(),
                            invoice_count = rd["invoice_count"].ToString(),
                            invoice_amount =double.Parse(rd["invoice_amount"].ToString()), //paid amount  shows from customerinvoicedtl
                            paid_amount = double.Parse(rd["paid_amount"].ToString()),
                            //createddate = DateTime.Parse(rd["created_date"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString())
                        });
                    }
                    val.customerdashboardreport = summary4;
                    val.status = true;
                    
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while outstanding report";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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
        public customerinvoicedetail Customerstatementreport(customerinvoicedetail val)
        {
            //customerinvoicedetail val = new customerinvoicedetail();
            try
            {



                cmd = new MySqlCommand("sp_sel_customerstatementreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary1 = new List<customerinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary1.Add(new customerinvoicelist
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            pnr_number = rd["pnr_number"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            paid_amount = rd["paid_amount"].ToString(),
                            balance_amount = rd["balance_amount"].ToString(),
                            invoice_status = rd["invoice_status"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),
                            refund_amount = Double.Parse(rd["refund_amount"].ToString())
                        });
                    }
                    val.customerinvoicelist = summary1;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while customer invoice ";
                    //rd.Close();
                }
            }





            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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
     
        public customerinvoicedetail customerinvoiceprint(int values)
        {
            customerinvoicedetail customerprint = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoicemainprint");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", values);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows==true)
                {
                    rd.Read();
                    customerprint.invoice_refnumber = rd["invoice_refnumber"].ToString();
                    customerprint.invoice_date = rd["invoice_date"].ToString();
                    customerprint.customer_name = rd["customer_name"].ToString();
                    customerprint.billing_companyname = rd["billing_companyname"].ToString();
                    customerprint.billing_address = rd["billing_address"].ToString();
                    customerprint.billing_country = rd["billing_country"].ToString();
                    customerprint.net_amount = double.Parse(rd["net_amount"].ToString());
                    customerprint.addon_charge = double.Parse(rd["addon_charge"].ToString());
                    customerprint.discount_amount = double.Parse(rd["discount_amount"].ToString());
                    customerprint.total_withtax = double.Parse(rd["total_withtax"].ToString());
                    customerprint.status = true;
                    
                }
                else
                {
                    customerprint.status = true;
                    customerprint.message = "No Record Found";
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_customerinvoicedtlprint");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", values);
                rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<customeroutstandingreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new customeroutstandingreport
                        {
                            service_name = rd["service_name"].ToString(),
                            description = rd["description"].ToString(),
                            quantity = int.Parse(rd["quantity"].ToString()),
                            price = Double.Parse(rd["price"].ToString()),
                            discount_amount = double.Parse(rd["discount_amount"].ToString()),
                            net_amount = double.Parse(rd["net_amount"].ToString()),
                            grand_total = double.Parse(rd["grand_total"].ToString())
                        });
                    }
                    customerprint.customeroutstandingreport = summary;
                    customerprint.status = true;
                    
                }
                else
                {
                    customerprint.status = true;
                    customerprint.message = "No Record Found";
                    
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                customerprint.status = false;
                customerprint.message = "Error Occured While Showing Invoice Record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return customerprint;

        }

        public customerinvoicedetail ivnvoicereferenceno(string user_gid)
        {
            customerinvoicedetail Val = new customerinvoicedetail();
            try
            {
                cmd = new MySqlCommand("sp_ins_invoicereferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if(mnresult ==1)
                {
                    cmd = new MySqlCommand("sp_sel_invoicereferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.invoice_refnumber= rd["invoice_refnumber"].ToString();
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
        public Billingdetail airfileinvoiceedit(customerinvoicedetail val)
        {
            Billingdetail edit = new Billingdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_airfileinvoiceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference", val.invoice_gid);
                cmd.Parameters.AddWithValue("p_customerinvoice_gid","0");
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {

                    rd.Read();
                    edit.customer_name = rd["customer_name"].ToString();
                    edit.invoice_date = rd["invoice_date"].ToString();
                    edit.invoice_refnumber = rd["invoice_refnumber"].ToString();                  
                    edit.remarks = rd["remarks"].ToString();
                    edit.email_address = rd["email_address"].ToString();
                    edit.billing_address = rd["billing_address"].ToString();
                    edit.contact_number = rd["contact_number"].ToString();
                    edit.net_amount =double .Parse ( rd["net_amount"].ToString());
                    edit.discount_amount = Double.Parse(rd["discount_amount"].ToString());
                    edit.total_withtax = Double.Parse(rd["total_withtax"].ToString());
                    edit.addon_charge = Double.Parse(rd["addon_charge"].ToString());
                    edit.status = true;
                   
                }
                else
                {
                    edit.status = true;
                    edit.message = "No Record Found";
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_airfileinvoicedetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reference", val.invoice_gid);
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<billinglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new billinglist
                        {
                            service_name = rd["service_name"].ToString(), //billing_Process as service_name
                            description = rd["description"].ToString(),
                            quantity = rd["quantity"].ToString(),
                            service_amount = Double.Parse(rd["service_amount"].ToString()),
                            discount_amount = Double.Parse(rd["discount_amount"].ToString()),
                            net_amount = Double.Parse(rd["net_amount"].ToString()),
                            unit = rd["unit"].ToString(),
                            unit_price =Double.Parse(rd["unit_price"].ToString()),
                        });
                    }
                    edit.billinglist = summary;
                    edit.status = true;
                    
                }
                else
                {
                    edit.status = true;
                    edit.message = "No Record Found";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                edit.status = false;
                edit.message = "Error Occured While Showing Invoice Record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return edit;

        }
        public billingmodel airfileinvoiceupdate(Billingdetail val, string userGid)
        {
            //customerinvoicemodel customerinvoice = new customerinvoicemodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_airfileinvoiceupt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", userGid);
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);//p_adddiscount as discount
                cmd.Parameters.AddWithValue("p_grand_total", val.grand_total);//totalwithtax as
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                cmd.Parameters.AddWithValue("p_ar_gid", val.invoice_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_airfileinvoicedelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_ar_gid", val.invoice_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();

                    cmd = new MySqlCommand("sp_del_airfileinvoicedelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid",rd["customerinvoice_gid"].ToString ());
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        foreach (var data in val.billinglist)
                        {
                            cmd = new MySqlCommand("sp_ins_billingactivityselectmaindtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_customerinvoice_gid",rd["customerinvoice_gid"].ToString());
                            cmd.Parameters.AddWithValue("p_service_name", data.service_name);
                            cmd.Parameters.AddWithValue("p_service_amount", data.service_amount);
                            cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                            cmd.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                            cmd.Parameters.AddWithValue("p_net_amount", data.net_amount);
                            cmd.Parameters.AddWithValue("p_description", data.description);
                            cmd.Parameters.AddWithValue("p_unit", data.unit);
                            cmd.Parameters.AddWithValue("p_unit_price", data.unit_price);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                        }
                        val.status = true;
                        val.message = "Updated Successful!";
                    }
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
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
            return val;
        }
        public customerinvoicemodel ivnvoicereferencenocancel(string val)
        {
            customerinvoicemodel cancel = new customerinvoicemodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_sequencecode");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("p_referencecode", val);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                cancel.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return cancel;
        }

        public customertransactiondetails customertransctions(customertransactiondetails val)
        {
            customertransactiondetails transactions = new customertransactiondetails();
            try
            {
                sqlad.SelectCommand = new MySqlCommand("sp_sel_customertransactions");
                sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlad.SelectCommand.Parameters.AddWithValue("p_from_date", val.from_date);
                sqlad.SelectCommand.Parameters.AddWithValue("p_to_date", val.to_date);
                sqlad.SelectCommand.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                DataTable objtb1 = DBAccess.GetDataTable(sqlad);
                var dtls = new List<customertransactionsdtl>();
                foreach (DataRow dr in objtb1.Rows)
                {
                    dtls.Add(new customertransactionsdtl
                    {
                        transaction_date = dr["transaction_date"].ToString(),
                        reference_number = dr["reference_number"].ToString(),
                        total_credit = double.Parse(dr["total_credit"].ToString()),
                        total_debit = double.Parse(dr["total_debit"].ToString()),
                        outstanding_amount = double.Parse(dr["outstanding_amount"].ToString()),
                        type = dr["type"].ToString()

                    });
                }
                transactions.customertransactionsdtl = dtls;
                transactions.status = true;

            }
            catch(Exception ex)
            {

                transactions.status = false;
                transactions.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (sqlad.SelectCommand.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlad.SelectCommand.Connection.Close();
                }
            }

            return transactions;
        }

        public customerinvoice daVendorPaymentPendingInvoiceSummary()
        {
            customerinvoice customerinvoice = new customerinvoice();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentpendinginvoicesummary");
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
                            customer_name = rd["customer_name"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            invoice_date = rd["invoice_date"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            created_by = rd["created_by"].ToString()
                        });
                    }
                    customerinvoice.customerinvoicelist = summary;
                    customerinvoice.status = true;

                }
                else
                {
                    customerinvoice.status = true;
                    customerinvoice.message = "No Records Found";
                    //rd.Close();
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

        public SalesOrderForm daCustomerLedgerSummary()
        {
            SalesOrderForm SOF = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerledgersummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesOrderFormList>();
                while (rd.Read())
                {
                    summary.Add(new SalesOrderFormList
                    {
                        //salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                        customer_name = rd["customer_name"].ToString(),
                        customer_gid = rd["customer_gid"].ToString(),
                        //contact_details = rd["contact_details"].ToString(),
                        //total_amount = double.Parse(rd["total_amount"].ToString()),//service value, service type need
                        //salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                        created_by = rd["created_by"].ToString(),
                        //created_date = rd["created_date"].ToString(),
                        branch_name = rd["branch_name"].ToString(),
                        invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                        receipt_amount = double.Parse(rd["paid_amount"].ToString()),
                        outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                        refund_amount = double.Parse(rd["refund_amount"].ToString()),
                        //advance_amount = double.Parse(rd["advance_amount"].ToString())

                    });
                }

                SOF.SalesOrderList = summary;
                SOF.status = true;
                rd.Close();
            }


            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }

        public customerinvoicedetail daCustomerLedgerReport(customerinvoicedetail val)
        {
            //customerinvoicedetail val = new customerinvoicedetail();
            try
            {
                //customer invoice:

                cmd = new MySqlCommand("sp_sel_ledgercustomerinvoicedtl");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary1 = new List<customerinvoicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {

                        val.customer_name = rd["customer_name"].ToString();
                        val.contact_number = rd["contact_number"].ToString();
                        summary1.Add(new customerinvoicelist
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            invoice_status = rd["invoice_status"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            invoice_amount = rd["invoice_amount"].ToString(),
                        });
                    }
                    val.customerinvoicelist = summary1;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while customer invoice ";
                }
                rd.Close();

                //customer receipt:

                cmd = new MySqlCommand("sp_sel_rcustomerreceipt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary2 = new List<customerreceiptreporlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary2.Add(new customerreceiptreporlist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            customer_type = rd["customer_type"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            receipt_method = rd["receipt_method"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            created_by = rd["first_name"].ToString()
                        });
                    }
                    val.customerreceiptreporlist = summary2;
                    val.status = true;
                }
                else
                {
                    val.status = false;
                }
                rd.Close();

                //outstanding report:

                cmd = new MySqlCommand("sp_sel_customeroutstandingrpt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary3 = new List<customeroutstandingreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary3.Add(new customeroutstandingreport
                        {
                            outstanding_amount = double.Parse(rd["balance_amount"].ToString()),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            paid_amount = double.Parse(rd["paid_amount"].ToString()), //paid amount  shows from customerinvoicedtl
                            invoice_date = rd["invoice_date"].ToString(),
                            invoice_refno = rd["invoice_refnumber"].ToString()
                        });
                    }
                    val.customeroutstandingreport = summary3;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while outstanding report";
                }
                rd.Close();
                //customer dashboard report:

                cmd = new MySqlCommand("sp_sel_customerrefundrpt");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);

                var summary4 = new List<customerrefundreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary4.Add(new customerrefundreport
                        {
                            refund_date = rd["refund_date"].ToString(),
                            refund_amount = rd["refund_amount"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            refund_number = rd["refund_number"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString()
                        });
                    }
                    val.customerrefundreport = summary4;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured while outstanding report";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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

    }
}