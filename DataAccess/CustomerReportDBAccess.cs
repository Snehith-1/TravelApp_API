﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DataAccess
{
    public class CustomerReportDBAccess
    {
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public CustomerReportSummary GetAll(CustomerReport values)
        {
            CustomerReportSummary CustomerReportSummary = new CustomerReportSummary();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoicereport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from", values.from_date);
                cmd.Parameters.AddWithValue("p_to", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CustomerReportList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new CustomerReportList
                        {

                            year = rd["year"].ToString(),
                            month = rd["month"].ToString(),
                            invoice_count = rd["invoice_count"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()

                           )
                        });
                    }
                    CustomerReportSummary.CustomerReportList = summary;
                    CustomerReportSummary.status = true;
                    
                }
                else
                {
                    CustomerReportSummary.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                CustomerReportSummary.status = false;
                CustomerReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return CustomerReportSummary;
        }

        public CustomerReportGraph GetAllgraph(CustomerReport values, string companycode)
        {
            CustomerReportGraph CustomerReportSummary = new CustomerReportGraph();
            try
            {
                cmd = new MySqlCommand("sp_sel_customerinvoicereport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from", values.from_date);
                cmd.Parameters.AddWithValue("p_to", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CustomerReportGraphList>();
                var list = new List<CustomerReportGraphListbar>();
                StringBuilder sb = new StringBuilder();
                string result = "";
                string result1 = "";
                if (rd.HasRows == true)
                {

                    while (rd.Read())
                    {
                        if (result == "")
                        {                         
                            result = rd["month"].ToString();
                        }
                        else
                        {                          
                           result = result + "," + rd["month"].ToString();

                        }
                        if (result1 == "")
                        {
                            result1 = rd["invoice_amount"].ToString();

                        }
                        else
                        {
                            result1 = result1 + "," + Double.Parse(rd["invoice_amount"].ToString());
                        }                      
                    }                  
                    CustomerReportSummary.color = "#9cd159";
                    CustomerReportSummary.label = "Sales";                
                    CustomerReportSummary.status = true;
                    //rd.Close();
                    CustomerReportSummary.labels = result;
                    CustomerReportSummary.series = result1;
                    //rd.Close();
                }
                else
                {
                    CustomerReportSummary.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                CustomerReportSummary.status = false;
                CustomerReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return CustomerReportSummary;
        }

        public CustomerReportSummaryChild GetAllChild(CustomerReport values)
        {
            CustomerReportSummaryChild CustomerReportSummary = new CustomerReportSummaryChild();
            try
            {
                if (values.from_date == null)
                {
                    values.from_date = "null";
                }
                if (values.to_date == null)
                {
                    values.to_date = "null";
                }
                cmd = new MySqlCommand("sp_sel_customerinvoicedtlreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from", values.from_date);
                cmd.Parameters.AddWithValue("p_to", values.to_date);
                cmd.Parameters.AddWithValue("p_month", values.month);
                cmd.Parameters.AddWithValue("p_year", values.year);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CustomerReportChildList>();
                if (rd.HasRows == true)
                {

                    while (rd.Read())
                    {
                        summary.Add(new CustomerReportChildList
                        {
                                
                            invoice_refnumber= rd["invoice_refnumber"].ToString(),
                            invoice_date = Convert.ToDateTime(rd["invoice_date"]).ToString("dd-MM-yyyy"),
                            customer_name = rd["customer_name"].ToString(),
                            customer_contactperson = rd["customer_contactperson"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()

                           )
                        });
                    }
                    CustomerReportSummary.CustomerReportChildList = summary;
                    CustomerReportSummary.status = true;
                    
                }
                else
                {
                    CustomerReportSummary.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                CustomerReportSummary.status = false;
                CustomerReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return CustomerReportSummary;
        }
    }
}