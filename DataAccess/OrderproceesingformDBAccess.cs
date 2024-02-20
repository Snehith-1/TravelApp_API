﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;
namespace DataAccess
{
    public class OrderproceesingformDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Orderprocessingform Getall()
        {
            Orderprocessingform orderprocess = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_orderprocessingsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<orderprocessinglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new orderprocessinglist
                        {

                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            customer_name = rd["customer_name"].ToString(),
                            //customer_gid = int.Parse(rd["customer_gid"].ToString()),
                            contact_number = rd["contact_number"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            net_amount = Double.Parse(rd["net_amount"].ToString()),//customerprice as  net_amount
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString()

                        });
                    }
                    orderprocess.orderprocessinglist = summary;
                    orderprocess.status = true;
                }
                else
                {
                    orderprocess.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                orderprocess.status = false;
                orderprocess.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return orderprocess;
        }
        public Orderprocessingform Orderprocessing()
        {
            Orderprocessingform orderprocess = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_orderprocessingsummaryall");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Orderprocessinglistall>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Orderprocessinglistall
                        {

                            orderprocessing_gid = int.Parse(rd["orderprocessing_gid"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            discount_amount = Double.Parse(rd["discount_amount"].ToString()),
                            net_amount = Double.Parse(rd["net_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            orderprocessing_refnumber = rd["orderprocessing_refnumber"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString())

                        });
                    }
                    orderprocess.Orderprocessinglistall = summary;
                    orderprocess.status = true;

                }
                else
                {
                    orderprocess.status = false;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                orderprocess.status = false;
                orderprocess.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return orderprocess;

        }
        public orderprocessingformmodel pageload(orderprocessinglist val, string usergid)
        {
            orderprocessingformmodel sub = new orderprocessingformmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_orderprocessingformmaintotemp");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        cmd = new MySqlCommand("sp_ins_orderprocessingformmaintotemp");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_reference_gid", rd["reference_gid"].ToString());
                        cmd.Parameters.AddWithValue("p_salesorder_gid", rd["salesorder_gid"].ToString());
                        cmd.Parameters.AddWithValue("p_service_type", rd["service_type"].ToString());
                        cmd.Parameters.AddWithValue("p_reference", rd["reference"].ToString());
                        cmd.Parameters.AddWithValue("p_remarks", rd["remarks"].ToString());
                        cmd.Parameters.AddWithValue("p_total_amount", rd["total_amount"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", usergid);
                        //cmd.Connection = con;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_orderprocessingformpassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        cmd = new MySqlCommand("sp_ins_orderprocessingformpassengertotemp");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", rd["salesorder_gid"].ToString());
                        cmd.Parameters.AddWithValue("p_first_name", rd["first_name"].ToString());
                        cmd.Parameters.AddWithValue("p_last_name", rd["last_name"].ToString());
                        cmd.Parameters.AddWithValue("p_gender", rd["gender"].ToString());
                        cmd.Parameters.AddWithValue("p_dob", rd["dob"].ToString());
                        cmd.Parameters.AddWithValue("p_passport_number", rd["passport_number"].ToString());
                        cmd.Parameters.AddWithValue("p_passport_issueddate", rd["passport_issueddate"].ToString());
                        cmd.Parameters.AddWithValue("p_passport_expirydate", rd["passport_expirydate"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", usergid);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }

                }
                rd.Close();
                if (mnresult == 1)
                {
                    sub.status = true;
                    sub.message = "Records Successfully added!";
                }
                else
                {
                    sub.status = false;
                    sub.message = "Error Occured While Inserting Records!";
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
        public Orderprocessingform opfpassengersummary(opfpassengerlist val)
        {
            Orderprocessingform opf = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_sopassenger");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<opfpassengerlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new opfpassengerlist
                        {
                            passengerservice_gid = rd["passengerservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            gender = rd["gender"].ToString(),
                            dob = rd["dob"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            passport_number = rd["passport_number"].ToString(),
                            passport_expirydate = rd["passport_expirydate"].ToString(),
                        });
                    }
                    opf.opfpassengerlist = summary;
                    opf.status = true;

                }
                else
                {
                    opf.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                opf.status = false;
                opf.message = "Passanger Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return opf;
        }
        public Orderprocessingform activity(opfactivityList val)
        {
            Orderprocessingform opf = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesorderformtoactivitysummary");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<opfactivityList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new opfactivityList
                        {
                            salesactivity_gid = rd["salesactivity_gid"].ToString(),//tmpsalestoactivity_gid as salesactivity_gid
                            //activitygid = int.Parse(rd["reference_gid"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            reference = rd["reference"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            salesactivity_status = rd["salesactivity_status"].ToString(),
                            total_amount = rd["total_amount"].ToString()
                        });
                    }
                    opf.opfactivityList = summary;
                    opf.status = true;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                opf.status = false;
                opf.message = "Activity Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return opf;
        }
        public Orderprocessingformdetails Getcustomer(int val)
        {
            Orderprocessingformdetails orderprocessing = new Orderprocessingformdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_opfcustomer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    orderprocessing.customer_gid = rd["customer_gid"].ToString();
                    orderprocessing.customer_name = rd["customer_name"].ToString();
                    orderprocessing.contact_number = rd["contact_number"].ToString();
                    orderprocessing.email_address = rd["email_address"].ToString();
                    orderprocessing.national_id = rd["national_id"].ToString();
                    orderprocessing.billing_address = rd["billing_address"].ToString();
                    orderprocessing.currency_gid = rd["currency_gid"].ToString();
                }
                orderprocessing.status = true;
                orderprocessing.message = "Customer details loaded successfully";
                rd.Close();
            }
            catch (Exception ex)
            {
                orderprocessing.status = false;
                orderprocessing.message = "Internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return orderprocessing;
        }
        public orderprocessingformmodel oversubmit(orderprocessinglist val, string user_gid)
        {
            orderprocessingformmodel submit = new orderprocessingformmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_checksalesinorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == false)
                {
                    cmd = new MySqlCommand("sp_ins_orderprocessingmain");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                    cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                    cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                    cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_contact_number", "");
                    cmd.Parameters.AddWithValue("p_orderprocessing_refnumber", "");
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
                }
                else
                {
                    cmd = new MySqlCommand("sp_del_allmainorderprocessing");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_orderprocessing_gid", 0);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        cmd = new MySqlCommand("sp_ins_orderprocessingmain");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                        cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                        cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                        cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                        cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                        cmd.Parameters.AddWithValue("p_created_by", user_gid);
                        cmd.Parameters.AddWithValue("p_contact_number", "");
                        cmd.Parameters.AddWithValue("p_orderprocessing_refnumber", "");
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
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                submit.status = false;
                submit.message = "Internal error occured";
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
        //public Activity submit(vendoractivitydetail val, string usergid)
        //{
        //    Activity sub = new Activity();
        //    try
        //    {
        //        Activity activity = new Activity();

        //        cmd = new MySqlCommand("sp_ins_paymentmain");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_salesordergid", val.salesordergid);
        //        cmd.Parameters.AddWithValue("p_vendorgid", val.vendorgid);
        //        cmd.Parameters.AddWithValue("p_vendoramt", val.vendoramt);
        //        //cmd.Parameters.AddWithValue("p_createdby", val.createdby);
        //        mnresult = DBAccess.ExecuteNonQuery(cmd);
        //        if (mnresult == 1)
        //        {
        //            foreach (var data in val.vendoractivitylist)
        //            {
        //                cmd = new MySqlCommand("sp_ins_paymentnotedtl");
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_paymentnotemaingid", val.paymentnotemaingid);
        //                cmd.Parameters.AddWithValue("p_activtygid", data.activtygid);
        //                cmd.Parameters.AddWithValue("p_process", data.process);
        //                mnresult = DBAccess.ExecuteNonQuery(cmd);
        //            }
        //            if (mnresult == 1)
        //            {
        //                sub.status = true;
        //            }
        //            else
        //            {
        //                sub.status = false;
        //                sub.message = "Internal error occured";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sub.status = false;
        //        sub.message = "Internal error occured";
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return sub;
        //}
        public orderprocessingformmodel submit(billing val, string usergid)
        {
            orderprocessingformmodel sub = new orderprocessingformmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_paymentmain");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                cmd.Parameters.AddWithValue("p_createdby", usergid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    foreach (var data in val.billinglist)
                    {
                        cmd = new MySqlCommand("sp_sel_opfsubmit");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_paymentnote_gid", data.paymentnote_gid);
                        rd = DBAccess.ExecuteReader(cmd);
                        if (rd.HasRows == true)
                        {
                            rd.Read();
                            cmd = new MySqlCommand("sp_ins_paymentnotedtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_paymentnotemain_gid", "");
                            cmd.Parameters.AddWithValue("p_activty_name", rd["activity_name"].ToString());
                            cmd.Parameters.AddWithValue("p_process", rd["process"].ToString());
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                        }
                        rd.Close();
                    }

                    if (mnresult == 1)
                    {
                        sub.status = true;
                        sub.message = "Recordes added successfully!";
                    }
                    else
                    {
                        sub.status = false;
                        sub.message = "Internal error occured while add dtltable";
                    }
                }
                else
                {
                    sub.status = false;
                    sub.message = "Internal error occured while add maintable";
                }
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
            return sub;
        }

        public Orderprocessingform vendorpaymentsummary(vendoractivitylist val)
        {
            Orderprocessingform opf = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpaymentnote");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendoractivitylist>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new vendoractivitylist
                        {
                            paymentnotemain_gid = rd["paymentnotemain_gid"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_amount = double.Parse(rd["vendor_amount"].ToString()),
                            //createddate =DateTime.Parse(rd["createddate"].ToString()),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            salesorder_date = DateTime.Parse(rd["salesorder_date"].ToString()),

                        });
                    }
                    opf.vendoractivitylist = summary;
                    opf.status = true;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                opf.status = false;
                opf.message = "vendor Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return opf;
        }
        public Orderprocessingform orderprocessingmainsummary()
        {
            Orderprocessingform opf = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_orderprocessingmainsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Orderprocessinglistall>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Orderprocessinglistall
                        {
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            vendor_amount = double.Parse(rd["vendor_amount"].ToString()),
                        });
                    }
                    opf.Orderprocessinglistall = summary;
                    opf.status = true;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                opf.status = false;
                opf.message = "Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return opf;
        }

        public Orderprocessingform ordersalesselectsummary()
        {
            double total_amount = 0;
            Orderprocessingform OPF = new Orderprocessingform();
            try
            {
                cmd = new MySqlCommand("sp_sel_opfsalessummaryselect");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<orderprocessinglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        if (rd["total_amount"] == System.DBNull.Value)
                        {
                            total_amount = 0;

                        }
                        else
                        {
                            total_amount = double.Parse(rd["total_amount"].ToString());
                        }
                        summary.Add(new orderprocessinglist
                        {
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            customer_name = rd["customer_name"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            total_amount = total_amount,//estimatedprice as total
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            salesorder_status = rd["salesorder_status"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            //created_date=DateTime.Parse(rd["created_date"].ToString())            
                        });
                    }


                    OPF.orderprocessinglist = summary;
                    OPF.status = true;

                }
                rd.Close();
            }

            catch (Exception ex)
            {
                OPF.status = false;
                OPF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return OPF;
        }
        public orderprocessingformmodel opfstatus(Orderprocessingformdetails val)
        {
            orderprocessingformmodel status = new orderprocessingformmodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_salestoactivitystatus");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salestoactivity", val.tmpsalestoactivity_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    status.status = true;
                }
                else
                {
                    status.status = false;
                }
            }
            catch (Exception ex)
            {
                status.status = false;
                status.message = "Internal error occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return status;
        }
        public SalesOrderForm soserviceget(int salesorder_gid)
        {
            SalesOrderForm sof = new SalesOrderForm();

            try
            {

                cmd = new MySqlCommand("sp_sel_sopassport");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var passportsummary = new List<SOPassportList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        passportsummary.Add(new SOPassportList
                        {
                            passportservice_gid = rd["passportservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            id_proof = rd["id_proof"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString())

                        });
                    }
                    rd.Close();
                    cmd.Connection.Close();
                    sof.SOPassportList = passportsummary;
                    sof.status = true;
                }
                else
                {
                    cmd.Connection.Close();

                }
                rd.Close();



                cmd = new MySqlCommand("sp_sel_sovisa");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var visasummary = new List<SOVisaList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        visasummary.Add(new SOVisaList
                        {
                            visaservice_gid = rd["visaservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            country = rd["country"].ToString(),
                            application_date = rd["application_date"].ToString(),
                            expiry_date = rd["expiry_date"].ToString(),
                            visa_period = rd["visa_period"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString())

                        });
                    }
                    rd.Close();
                    cmd.Connection.Close();
                    sof.SOVisaList = visasummary;
                    sof.status = true;
                }

                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();



                cmd = new MySqlCommand("sp_sel_soflight");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var flightsummary = new List<SOFlightList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        flightsummary.Add(new SOFlightList
                        {
                            flightservice_gid = rd["flightservice_gid"].ToString(),
                            flight_number = rd["flight_number"].ToString(),
                            flight_name = rd["flight_name"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            departure_date = rd["departure_date"].ToString(),
                            flight_time = rd["flight_time"].ToString(),
                            flight_from = rd["flight_from"].ToString(),
                            flight_to = rd["flight_to"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            pnr_number = rd["pnr_number"].ToString(),
                            sector_number = rd["sector_number"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),
                            flight_class = rd["flight_class"].ToString(),
                            flight_routing = rd["flight_routing"].ToString(),

                            segment = rd["segment"].ToString()

                        });
                    }
                    rd.Close();
                    cmd.Connection.Close();
                    sof.SOFlightList = flightsummary;
                    sof.status = true;
                }

                else
                {
                    cmd.Connection.Close();

                }
                rd.Close();



                cmd = new MySqlCommand("sp_sel_sohotel");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var hotelsummary = new List<SOHotelList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        hotelsummary.Add(new SOHotelList
                        {
                            hotelservice_gid = rd["hotelservice_gid"].ToString(),
                            hotel_name = rd["hotel_name"].ToString(),
                            category = rd["category"].ToString(),
                            city = rd["city"].ToString(),
                            check_in = rd["check_in"].ToString(),
                            check_out = rd["check_out"].ToString(),
                            total_numberofdays = int.Parse(rd["total_numberofdays"].ToString()),
                            total_numberofpassengers = int.Parse(rd["total_numberofpassengers"].ToString()),
                            total_amount = double.Parse(rd["total_amount"].ToString())
                        });
                    }
                    rd.Close();
                    cmd.Connection.Close();
                    sof.SOHotelList = hotelsummary;
                    sof.status = true;
                }

                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();



                cmd = new MySqlCommand("sp_sel_socar");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var carsummary = new List<SOCarList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        carsummary.Add(new SOCarList
                        {
                            carservice_gid = rd["carservice_gid"].ToString(),
                            car_type = rd["car_type"].ToString(),
                            from_date = rd["from_date"].ToString(),
                            to_date = rd["to_date"].ToString(),
                            pickup_city = rd["pickup_city"].ToString(),
                            drop_city = rd["drop_city"].ToString(),
                            numberof_persons = int.Parse(rd["numberof_persons"].ToString()),
                            total_amount = double.Parse(rd["total_amount"].ToString())

                        });
                    }

                    cmd.Connection.Close();
                    sof.SOCarList = carsummary;
                    sof.status = true;
                }

                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();

                cmd = new MySqlCommand("sp_sel_soforex");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var forexsummary = new List<SOForexList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        forexsummary.Add(new SOForexList
                        {
                            forexservice_gid = rd["forexservice_gid"].ToString(),
                            paidamount_currency = rd["paidamount_currency"].ToString(),
                            customerpaid_amount = double.Parse(rd["customerpaid_amount"].ToString()),
                            total_paidamount = double.Parse(rd["total_paidamount"].ToString()),
                            receivedamount_currency = rd["receivedamount_currency"].ToString(),
                            customerreceived_amount = double.Parse(rd["customerreceived_amount"].ToString()),
                            total_receivedamount = int.Parse(rd["total_receivedamount"].ToString())

                        });
                    }

                    cmd.Connection.Close();
                    sof.SOForexList = forexsummary;
                    sof.status = true;
                }

                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();


                cmd = new MySqlCommand("sp_sel_sopackage");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var packagesummary = new List<SOPackageDetailList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        packagesummary.Add(new SOPackageDetailList
                        {
                            packageservice_gid = rd["packageservice_gid"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            remarks = rd["remarks"].ToString()

                        });
                    }

                    cmd.Connection.Close();
                    sof.SOPackageDetailList = packagesummary;
                    sof.status = true;
                }
                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();

                cmd = new MySqlCommand("sp_sel_soinsurance");
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var insurancesummary = new List<SOInsurenceList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        insurancesummary.Add(new SOInsurenceList
                        {
                            insuranceservice_gid = rd["insuranceservice_gid"].ToString(),
                            name = rd["name"].ToString(),
                            dob = rd["dob"].ToString(),
                            arrival_port = rd["arrival_port"].ToString(),
                            start_date = rd["start_date"].ToString(),
                            end_date = rd["end_date"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString())
                        });
                    }
                    rd.Close();
                    cmd.Connection.Close();
                    sof.SOInsurenceList = insurancesummary;
                    sof.status = true;
                }
                else
                {
                    cmd.Connection.Close();
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Customer Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return sof;
        }
        public orderprocessingformmodel totalvalue(totalvaluedetails values)
        {
            totalvaluedetails total = new totalvaluedetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_salestotalvalue");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    values.total_amount = double.Parse(rd["total_amount"].ToString()); //netvalue as total
                    //values.customervalue = Double.Parse(rd["customer_value"].ToString());
                    //values.vendorvalue = Double.Parse(rd["vendor_value"].ToString());
                    //values.profitvalue = Double.Parse(rd["profit"].ToString());
                    values.status = true;


                }
                else
                {
                    values.status = false;
                    values.message = "Internal error occured!";
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salestotalcustomer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    values.customer_amount = double.Parse(rd["customer_amount"].ToString()); //customer_value as  customer_amount
                    values.status = true;

                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salestotalvendorvalue");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    values.vendor_amount = double.Parse(rd["vendor_amount"].ToString()); //vendor_value as  vendor_amount
                    values.status = true;

                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_salestotalvalueprofit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    values.profit_amount = double.Parse(rd["profit_amount"].ToString());
                    values.status = true;

                }
                else
                {
                    values.status = false;
                    values.message = "Internal error occured!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Internal error occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return values;
        }

        //        cmd = new MySqlCommand("sp_sel_salestotalcustomer");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesordergid);
        //                rd = DBAccess.ExecuteReader(cmd);
        //                if (rd.Read())
        //                {
        //                    values.customervalue = Double.Parse(rd["customer_value"].ToString());
        //                    values.status = true;
        //                }
        //    cmd = new MySqlCommand("sp_sel_salestotalvendorvalue");
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesordergid);
        //                rd = DBAccess.ExecuteReader(cmd);
        //                if (rd.Read())
        //                {
        //                   values.vendorvalue = Double.Parse(rd["vendor_value"].ToString());
        //                    values.status = true;
        //                }
        //cmd = new MySqlCommand("sp_sel_salestotalvalueprofit");
        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_salesorder_gid", values.salesordergid);
        //                rd = DBAccess.ExecuteReader(cmd);
        //                if (rd.Read())
        //                {
        //                    values.profitvalue = Double.Parse(rd["profit"].ToString());
        //                    values.status = true;
        //                }
    }
}