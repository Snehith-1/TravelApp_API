﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class VendorReportDBAccess
    {
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public VendorReportSummary GetAll(VendorReport values)
        {
            VendorReportSummary VendorReportSummary = new VendorReportSummary();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorReportList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorReportList
                        {

                            year = rd["year1"].ToString(),
                            month = rd["month1"].ToString(),
                            invoice_count = rd["inv_count"].ToString(),
                            vendorinvoice_amount = Double.Parse(rd["vendorinvoice_amount"].ToString()

                           )
                        });
                    }
                    VendorReportSummary.VendorReportList = summary;
                    VendorReportSummary.status = true;
                    
                }
                else
                {
                    VendorReportSummary.status = false;
                    VendorReportSummary.message = "No Records Found!";
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                VendorReportSummary.status = false;
                VendorReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return VendorReportSummary;
        }

        public VendorReportGraph GetAllgraph(VendorReport values)
        {
            VendorReportGraph VendorReportSummary = new VendorReportGraph();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                string result = "";
                string resultamt = "";
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        if(result=="")
                        {
                            result = rd["month1"].ToString();
                        }
                        else
                        {
                            result = result + "," + rd["month1"].ToString();
                        }
                        if(resultamt=="")
                        {
                            resultamt = rd["vendorinvoice_amount"].ToString();

                        }
                        else
                        {
                            resultamt = resultamt + "," + rd["vendorinvoice_amount"].ToString();
                        }
                            
                            
                    }
                    VendorReportSummary.color = "#9cd159";
                    VendorReportSummary.label = "Sales";
                    VendorReportSummary.label = result;
                    VendorReportSummary.series = resultamt;
                    VendorReportSummary.status = true;
                    rd.Close();
                }
                else
                {
                    VendorReportSummary.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                VendorReportSummary.status = false;
                VendorReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return VendorReportSummary;
        }

        public VendorReportSummaryChild GetAllChild(VendorReport values )
        {
            VendorReportSummaryChild VendorReportSummary = new VendorReportSummaryChild();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendordtlreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //karthi commented the belwo two lines
				//cmd.Parameters.AddWithValue("p_from", values.from_date);
                //cmd.Parameters.AddWithValue("p_to", values.to_date);
                cmd.Parameters.AddWithValue("p_month", values.month);
                cmd.Parameters.AddWithValue("p_year", values.year);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<VendorReportChildList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new VendorReportChildList
                        {

                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            invoice_date = Convert.ToDateTime(rd["invoice_date"]).ToString("dd/MM/yyyy"),
                            vendor_name = rd["vendor_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            vendorinvoice_amount = Double.Parse(rd["vendorinvoice_amount"].ToString()

                           )
                        });
                    }
                    VendorReportSummary.VendorReportChildList = summary;
                    VendorReportSummary.status = true;
                    
                }
                else
                {
                    VendorReportSummary.status = false;
                    VendorReportSummary.message = "No Records Found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                VendorReportSummary.status = false;
                VendorReportSummary.message = "Internal Error Occured";
                error=ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return VendorReportSummary;
        }

        public VendorReportSummary vendoroutstandingreport(VendorReport values)
        {
            VendorReportSummary lsoutstanding = new VendorReportSummary();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendoroutstandingpayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendoroutstandingreport>();
                double lnoutstanding_amount = 0;
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        lnoutstanding_amount = Double.Parse(rd["outstanding_amount"].ToString());
                        if (lnoutstanding_amount > 0)
                        {
                            summary.Add(new vendoroutstandingreport
                            {

                                //year = rd["year1"].ToString(),
                                //month = rd["month1"].ToString(),
                                //Invoice_count = rd["inv_count"].ToString(),
                                vendor_name = rd["vendor_name"].ToString(),
                                vendor_company_name = rd["vendor_company_name"].ToString(),
                                vendorinvoice_amount = double.Parse(rd["vendorinvoice_amount"].ToString()),
                                invoice_refnumber = rd["invoice_refnumber"].ToString(),
                                payment_amount = double.Parse(rd["payment_amount"].ToString()),
                                invoice_date = rd["invoice_date"].ToString(),
                                contact_details = rd["contact_details"].ToString(),
                                outstanding_amount = double.Parse(rd["outstanding_amount"].ToString())
                            });
                         }
                        else
                        {
                            lsoutstanding.status = false;
                            lsoutstanding.message = " No Records Found!";
                        }
                    }

                    lsoutstanding.vendoroutstandingreport = summary;
                    lsoutstanding.status = true;
                }
                else
                {
                    lsoutstanding.status = false;
                    lsoutstanding.message = "No Records Found!";
                }
                //lsoutstanding.status = false;
                //lsoutstanding.message = "No Records Found!";
                rd.Close();
            }
            catch (Exception ex)
            {
                lsoutstanding.status = false;
                lsoutstanding.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return lsoutstanding;
        }
    }
}