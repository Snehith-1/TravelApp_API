﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class BillingDBAccess
    {
        MySqlCommand cmd = new MySqlCommand();
        int mnresult = 0;
        MySqlDataReader rd,rd1,rd2;
        string error;

        public Billingdetail billingGetall(int val)
        {

            Billingdetail blng = new Billingdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_opbilling_all");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<billinggetlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new billinggetlist
                        {
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            activity_name = rd["activity_name"].ToString(),
                            process = rd["process"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            billable = rd["billable"].ToString(),
                            billing_gid = int.Parse(rd["billing_gid"].ToString()),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            billing_amount = double.Parse(rd["billing_amount"].ToString()),
                            billing_status = rd["billing_status"].ToString(),

                        });
                    }

                    blng.billinggetlist = summary;
                    blng.status = true;
                    //rd.Close();

                }
                else
                {
                    blng.status = false;
                    //rd.Close();
                }
                rd.Close();

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

        public billingmodel billingadd(Billingdetail values, string user_gid)
        {
            billingmodel sub = new billingmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_opbilling");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                cmd.Parameters.AddWithValue("p_process", values.process);
                cmd.Parameters.AddWithValue("p_contact_number", values.contact_number);
                cmd.Parameters.AddWithValue("p_activity_name", values.activity_name);
                cmd.Parameters.AddWithValue("p_billable", values.billable);
                cmd.Parameters.AddWithValue("p_customer_amount", values.billing_amount);
                cmd.Parameters.AddWithValue("p_reference_gid", values.reference_gid);
                cmd.Parameters.AddWithValue("p_vendor_gid", values.vendor_gid);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_remarks", values.remarks);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    sub.status = true;
                }
            }
            catch (Exception ex)
            {
                sub.status = false;
                sub.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sub;
        }

        public billingmodel paymentadd(Billingdetail values, string user_gid)
        {
            billingmodel sub = new billingmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_oppaymentnote");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                cmd.Parameters.AddWithValue("p_process", values.process);
                cmd.Parameters.AddWithValue("p_contact_number", values.contact_number);
                cmd.Parameters.AddWithValue("p_activity_name", values.activity_name);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_vendor_gid", values.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_amount", values.vendor_amount);
                cmd.Parameters.AddWithValue("p_paymentnote_status", values.paymentnote_status);
                cmd.Parameters.AddWithValue("p_remarks", values.remarks);
                cmd.Parameters.AddWithValue("p_billable", values.billable);
                cmd.Parameters.AddWithValue("p_tvendorreferencenumber", values.tvendorreferencenumber);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    sub.status = true;
                }
            }
            catch (Exception ex)
            {
                sub.status = false;
                sub.message = "Internal Error Occured!";
                error = ex.ToString();

            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return sub;
        }

        public Billingdetail billingGet(int val)
        {
            Billingdetail blng = new Billingdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_opbilling");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<billinggetlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new billinggetlist
                        {
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            activity_name = rd["activity_name"].ToString(),
                            process = rd["process"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            billable = rd["billable"].ToString(),
                            billing_gid = int.Parse(rd["billing_gid"].ToString()),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            billing_amount = Double.Parse(rd["billingamount"].ToString())

                        });
                    }

                    blng.billinggetlist = summary;
                    blng.status = true;
                    //rd.Close();
                }
                else
                {
                    blng.status = false;
                    //rd.Close();
                }
                rd.Close();
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



        public Billingdetail billingdelete(int val)
        {
            Billingdetail blng = new Billingdetail();
            try
            {
                cmd = new MySqlCommand("sp_del_opbilling");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    blng.status = true;
                }
                else
                {
                    blng.status = false;
                    blng.message = "Internal Error Occured!";
                }
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
        public billing activitylist(int val)
        {
            billing billing = new billing();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentactivitylist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<billinglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new billinglist
                        {
                            activity_name = rd["activity_name"].ToString(),
                            process = rd["process"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            paymentnote_gid = int.Parse(rd["paymentnote_gid"].ToString()),
                            vendor_companyname = rd["vendor_company_name"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            vendor_amount = double.Parse(rd["vendor_amount"].ToString()), //billingamount changed as vendor_amount
                            reference_number = rd["tvendorreferencenumber"].ToString(),
                        });
                    }
                    billing.billinglist = summary;
                    billing.message = "Record found successfully";
                    billing.status = true;
                    //rd.Close();
                }
                else
                {
                    billing.status = false;
                    billing.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                billing.status = false;
                billing.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return billing;
        }
        public Billingdetail billingselect(Billingdetail val)
        {
            Billingdetail blng = new Billingdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_opbillingactivityselectso");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    blng.salesorder_refnumber = rd["salesorder_refnumber"].ToString();
                    blng.customer_name = rd["customer_name"].ToString();
                    //blng.advance =Double.Parse(rd["advance_paid"].ToString());
                    //blng.net = Double.Parse(rd["net_value"].ToString());
                    blng.customer_gid = rd["customer_gid"].ToString();
                    blng.contact_number = rd["contact_number"].ToString();
                    blng.email_address = rd["email_address"].ToString();
                    blng.currency_code = rd["currency_code"].ToString();
                }
                rd.Close();

                var summary = new List<billinggetlist>();
                foreach (var data in val.billinggetlist)
                {
                    cmd = new MySqlCommand("sp_sel_opbillingactivityselect");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_billing_gid", data.billing_gid);

                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);

                    if (rd1.HasRows == true)
                    {
                        while (rd1.Read())
                        {
                            summary.Add(new billinggetlist
                            {
                                salesorder_gid = int.Parse(rd1["salesorder_gid"].ToString()),
                                activity_name = rd1["activity_name"].ToString(),
                                process = rd1["process"].ToString(),
                                contact_number = rd1["contact_number"].ToString(),
                                billable = rd1["billable"].ToString(),
                                billing_gid = int.Parse(rd1["billing_gid"].ToString()),
                                vendor_gid = rd1["vendor_gid"].ToString(),
                                billing_amount = double.Parse(rd1["customer_amount"].ToString()),
                                billing_status = rd1["billing_status"].ToString()
                            });
                        }

                    }
                    blng.billinggetlist = summary;
                    blng.message = "Recordes loaded successfully!";
                    rd1.Close();
                }
                cmd = new MySqlCommand("sp_del_tempcustomerinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    foreach (var data in val.billinggetlist)
                    {
                        cmd = new MySqlCommand("sp_ins_tempcustomerinvoice");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_billing_gid", data.billing_gid);
                        cmd.Parameters.AddWithValue("p_process", data.process);
                        cmd.Parameters.AddWithValue("p_total_amount", data.total_amount);
                        cmd.Parameters.AddWithValue("p_salesorder_gid", data.salesorder_gid);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }
                    cmd = new MySqlCommand("sp_sel_tempcustomerinvoice");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                    var sum = new List<billinglist>();
                    if (rd2.HasRows == true)
                    {
                        while (rd2.Read())
                        {
                            sum.Add(new billinglist
                            {
                                process = rd2["process"].ToString(),
                                billing_gid = int.Parse(rd2["billing_gid"].ToString()),
                                total_amount = double.Parse(rd2["total_amount"].ToString()),
                            });
                        }
                    }
                    blng.billinglist = sum;
                    blng.message = "Recordes loaded successfully!";
                    rd2.Close();
                }
                else
                {
                    blng.status = false;
                    blng.message = "Internal error occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                blng.status = false;
                blng.message = "Internal error occured";
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
        public billingmodel billingoverallsubmit(Billingdetail val, string user_gid)
        {
            billingmodel submit = new billingmodel();
            try
            {

                cmd = new MySqlCommand("sp_sel_customerinvoice_refnumbervalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    submit.status = false;
                    submit.message = "Invoice Ref.No Already Exists!";
                }
                else
                {

                    cmd = new MySqlCommand("sp_ins_billingactivityselectmainovrsub");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);//
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                    cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                    cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                    cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                    cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                    cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                    cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                    cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                    cmd.Parameters.AddWithValue("p_customerinvoice_gid", "");
                    cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);
                    cmd.Parameters.AddWithValue("p_branch_gid", user_gid);
                    cmd.Parameters.AddWithValue("p_branch_name", "");
                    mnresult = DBAccess.ExecuteNonQuery(cmd);



                    if (mnresult == 1)
                    {
                        foreach (var data in val.billinggetlist)
                        {
                            cmd = new MySqlCommand("sp_ins_billingactivityselectmaindtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                            cmd.Parameters.AddWithValue("p_service_name", data.process);
                            cmd.Parameters.AddWithValue("p_service_amount", data.billing_amount);
                            //cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                            cmd.Parameters.AddWithValue("p_quantity", "1");
                            cmd.Parameters.AddWithValue("p_discount_amount", "0");
                            cmd.Parameters.AddWithValue("p_net_amount", data.billing_amount);
                            cmd.Parameters.AddWithValue("p_description", data.activity_name);
                            cmd.Parameters.AddWithValue("p_unit", 3);
                            cmd.Parameters.AddWithValue("p_unit_price", data.billing_amount);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            //if(mnresult==1)
                            //{
                            //    foreach(var locdata in val.billinglist)
                            //    {
                            //        cmd = new MySqlCommand("sp_ins_");
                            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //        cmd.Parameters.AddWithValue("p", locdata.vendorname);
                            //        cmd.Parameters.AddWithValue("p", locdata.vendoramount);
                            //        mnresult = DBAccess.ExecuteNonQuery(cmd);
                            //    }
                            //}
                        }
                        foreach (var data in val.billinggetlist)
                        {
                            cmd = new MySqlCommand("sp_upt_billingactivityselectmaindtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_billing_gid", data.billing_gid);
                            cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
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
        public billingmodel billingoverallsubmitairfile(Billingdetail val, string user_gid)
        {
            billingmodel submit = new billingmodel();
            string customer_gid = string.Empty;
            string branch_gid = string.Empty;

            string branch_name = string.Empty;
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
                    cmd = new MySqlCommand("sp_sel_customerinvoice_refnumbervalidation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        submit.status = false;
                        submit.message = "Invoice Ref.No Already Exists!";
                    }
                    else
                    {
                        //cmd = new MySqlCommand("sp_sel_branch");
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                        //cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                        //rd = DBAccess.ExecuteReader(cmd);
                        //if (rd.Read())
                        //{
                        //    // Karthi: Need to proceed only when customer gid is a valid one
                        //    branch_gid = rd["branch_gid"].ToString();

                        //    branch_name = rd["branch_name"].ToString();
                        //}
                        int outputparam;
                        cmd = new MySqlCommand("sp_ins_billingdirectinvoice");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gidtemp", "");
                        cmd.Parameters["p_salesorder_gidtemp"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                        cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                        cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_currency_code", val.currency);
                        cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                        cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                        cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);

                        //cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                        //cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                        ////cmd.Parameters.AddWithValue("p_flight_number", val.flight_number);

                        //cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                        //cmd.Parameters.AddWithValue("p_departure_date", val.departure_date);
                        //cmd.Parameters.AddWithValue("p_flight_class", val.flight_class);
                        //cmd.Parameters.AddWithValue("p_flight_time", val.flight_time);
                        //cmd.Parameters.AddWithValue("p_flight_from", val.flight_from);
                        //cmd.Parameters.AddWithValue("p_flight_to", val.flight_to);
                        //cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                        cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                        cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                        cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);
                        cmd.Parameters.AddWithValue("p_branch_name", "");
                       
                        mnresult = DBAccess.ExecuteNonQuery(cmd);

                     ;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        outputparam = Convert.ToInt32(cmd.Parameters["p_salesorder_gidtemp"].Value.ToString());

                        cmd = new MySqlCommand("sp_ins_invoicepaymentadd");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", outputparam);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                        // cmd.Parameters.AddWithValue("p_customer_gid", val.customergid);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);  // val.customergid
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_contact_details", "");
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_orderprocessing_refnumber", "");
                        cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                        cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                        cmd.Parameters.AddWithValue("p_branch_name", branch_name);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            foreach (var data in val.billinglist)
                            {
                                cmd = new MySqlCommand("sp_ins_billingactivityselectmaindtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_customerinvoice_gid", data.customerinvoice_gid);
                                cmd.Parameters.AddWithValue("p_service_name", data.process);
                                cmd.Parameters.AddWithValue("p_service_amount", data.service_amount);
                                cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                                cmd.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                                cmd.Parameters.AddWithValue("p_net_amount", data.net_amount);
                                cmd.Parameters.AddWithValue("p_description", data.description);
                                cmd.Parameters.AddWithValue("p_unit", data.unit);
                                cmd.Parameters.AddWithValue("p_unit_price", data.unit_price);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);


                                cmd = new MySqlCommand("sp_ins_directinvoiceserivedetails");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_service_details", data.process);
                                cmd.Parameters.AddWithValue("p_total_amount", data.total_amount);
                                cmd.Parameters.AddWithValue("p_reference_gid", outputparam);
                                cmd.Parameters.AddWithValue("p_salesorder_gid", outputparam);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);

                                cmd = new MySqlCommand("sp_ins_directinvoicepaymentadd");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_salesorder_gid", outputparam);   //changes made "0"
                                cmd.Parameters.AddWithValue("p_service_details", data.process);
                                cmd.Parameters.AddWithValue("p_process", data.process);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                cmd.Parameters.AddWithValue("p_vendor_gid", data.vendor_gid);
                                cmd.Parameters.AddWithValue("p_vendor_amount", data.vendor_amount);
                                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                                cmd.Parameters.AddWithValue("p_customer_amount", data.service_amount);
                                cmd.Parameters.AddWithValue("p_customerinvoice_gid", "");
                                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                                cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);// val.customergid
                                cmd.Parameters.AddWithValue("p_customer_name", "");
                                cmd.Parameters.AddWithValue("p_email_address", "");
                                cmd.Parameters.AddWithValue("p_national_id", "");
                                cmd.Parameters.AddWithValue("p_reference_gid", outputparam);
                                cmd.Parameters.AddWithValue("p_activity", "0");
                                cmd.Parameters.AddWithValue("p_tvendorreferencenumber", data.tvendorreferencenumber);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);



                            }
                            //if (mnresult == 1)
                            //{
                            //    cmd = new MySqlCommand("sp_ins_invoicetopayment");
                            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendorid);
                            //    cmd.Parameters.AddWithValue("p_vendorreferenceno", val.vendorreference_no);
                            //    cmd.Parameters.AddWithValue("p_vendoramount", val.vendoramount);
                            //    cmd.Parameters.AddWithValue("p_created_by", usergid);
                            //    cmd.Parameters.AddWithValue("p_customer_gid", val.customergid);
                            //    cmd.Parameters.AddWithValue("p_netamount", val.netamount);
                            //    cmd.Parameters.AddWithValue("p_currency", val.currency);
                            //    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                            //    cmd.Parameters.AddWithValue("p_grandtotal", val.totalwithtax);
                            //    cmd.Parameters.AddWithValue("p_addoncharge", val.addoncharge);
                            //    cmd.Parameters.AddWithValue("p_adddiscount", val.discount_amount);
                            //    cmd.Parameters.AddWithValue("p_salesorder_gid", "0");
                            //    cmd.Parameters.AddWithValue("p_customer_name", "");
                            //    cmd.Parameters.AddWithValue("p_mobile_no", "");
                            //    cmd.Parameters.AddWithValue("p_national_id", "");
                            //    cmd.Parameters.AddWithValue("p_email_id", "");
                            //    cmd.Parameters.AddWithValue("p_contact_details", "");
                            //    cmd.Parameters.AddWithValue("p_orderprocessingref_no", "");
                            //    cmd.Parameters.AddWithValue("p_process", "Ticket");
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
        public billingmodel billingoverallsubmitairfileinvoice(Billingdetail val, string user_gid)
        {
            billingmodel submit = new billingmodel();
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
                    submit.status = false;
                    submit.message = "ERR078";
                }
                if (flagCustomerExists == 1)
                {
                    cmd = new MySqlCommand("sp_sel_customerinvoice_refnumbervalidation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        submit.status = false;
                        submit.message = "Invoice Ref.No Already Exists!";
                    }
                    else
                    {
                        int outputparam;
                        cmd = new MySqlCommand("sp_ins_billingactivityselectmainairfiles");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gidtemp", "");
                        cmd.Parameters["p_salesorder_gidtemp"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                        cmd.Parameters.AddWithValue("p_invoice_amount", val.invoice_amount);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                        cmd.Parameters.AddWithValue("p_invoice_date", val.invoice_date);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_total_withtax", val.total_withtax);
                        cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                        cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                        cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                        cmd.Parameters.AddWithValue("p_exchange_rate", val.exchange_rate);
                        cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                        cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                        cmd.Parameters.AddWithValue("p_ticket_price", val.ticket_price);
                        cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                        cmd.Parameters.AddWithValue("p_paid_amount", val.paid_amount);
                        cmd.Parameters.AddWithValue("p_balance_amount", val.balance_amount);
                        cmd.Parameters.AddWithValue("p_customerinvoice_gid", "");
                        cmd.Parameters.AddWithValue("p_terms_conditions", val.terms_conditions);
                        cmd.Parameters.AddWithValue("p_branch_gid", "");
           

                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        outputparam = Convert.ToInt32(cmd.Parameters["p_salesorder_gidtemp"].Value.ToString());
                        if (mnresult == 1)
                        {
                            foreach (var data in val.billinglist)
                            {
                                cmd = new MySqlCommand("sp_ins_billingactivityselectmaindtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_customerinvoice_gid", "0");
                                cmd.Parameters.AddWithValue("p_service_name", data.service_name);
                                cmd.Parameters.AddWithValue("p_service_amount", data.service_amount);
                                cmd.Parameters.AddWithValue("p_quantity", data.quantity);
                                cmd.Parameters.AddWithValue("p_discount_amount", data.discount_amount);
                                cmd.Parameters.AddWithValue("p_net_amount", data.net_amount);
                                cmd.Parameters.AddWithValue("p_description", data.description);
                                cmd.Parameters.AddWithValue("p_unit", data.unit);
                                cmd.Parameters.AddWithValue("p_unit_price", data.unit_price);

                                mnresult = DBAccess.ExecuteNonQuery(cmd);

                                cmd = new MySqlCommand("sp_ins_airfileinvoiceservicedetails");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_service_details", data.service_name);
                                cmd.Parameters.AddWithValue("p_total_amount", data.service_amount);
                                cmd.Parameters.AddWithValue("p_reference_gid", outputparam);
                                cmd.Parameters.AddWithValue("p_salesorder_gid", outputparam);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                            }
                            if (mnresult == 1)
                            {
                                cmd = new MySqlCommand("sp_sel_tvendorrefnumber");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                                rd = DBAccess.ExecuteReader(cmd);
                                if (rd.HasRows == true)
                                {
                                    string e_ticketnumber = string.Empty;
                                    while (rd.Read())
                                    {
                                        e_ticketnumber = rd["e_ticketnumber"].ToString();
                                        cmd = new MySqlCommand("sp_ins_invoicetopayment");
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                                        cmd.Parameters.AddWithValue("p_vendorref_number", val.vendor_refnumber);
                                        cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                        cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                                        cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                                        cmd.Parameters.AddWithValue("p_total_amount", val.grand_total);
                                        cmd.Parameters.AddWithValue("p_addon_charge", val.addon_charge);
                                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                                        cmd.Parameters.AddWithValue("p_salesorder_gid", "0");
                                        cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                                        cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                                        cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                                        cmd.Parameters.AddWithValue("p_email_address", "");
                                        cmd.Parameters.AddWithValue("p_contact_details", "");
                                        cmd.Parameters.AddWithValue("p_orderprocessing_refnumber", "");
                                        cmd.Parameters.AddWithValue("p_process", "Ticket");
                                        cmd.Parameters.AddWithValue("p_branch_name", "");
                                        cmd.Parameters.AddWithValue("p_tvendorreferencenumber", e_ticketnumber);
                                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                                    }
                                }
                                rd.Close();
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
        public billingmodel opfbillingdelete(billinglist val, string usergid)
        {
            billingmodel del = new billingmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_billingnotesdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_billing_gid", val.billing_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    {
                        if (rd["customerinvoice_gid"].ToString() == "")
                        {
                            cmd = new MySqlCommand("sp_del_billingnotesdelete");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_billing_gid", val.billing_gid);
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
                        else
                        {
                            del.status = false;
                            del.message = "Invoice Raised for this billing notes";
                        }
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                del.status = false;
                del.message = "Error Occured While deleteing billingnotes";
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
        public billingmodel opfpaymentdelete(VendorPaymentlist val, string usergid)
        {
            billingmodel del = new billingmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_paymentnotesdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == false)
                {
                    cmd = new MySqlCommand("sp_del_paymentnotesdelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
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
                else
                {
                    del.status = false;
                    del.message = "Invoice Raised for this payment notes";
                }
            }
            catch (Exception ex)
            {
                del.status = false;
                del.message = "Error Occured While deleteing billingnotes";
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
    }
}