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


namespace DataAccess
{
    public class DashboardDBAccess
    {        
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;

        public Dashboarddetails dashboardcounts()
        {
            Dashboarddetails dashboard = new Dashboarddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_dashboardmaincount");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<serviceList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new serviceList
                        {
                            service_count = rd["activitycount"].ToString(),
                            service_type = rd["service_type"].ToString()                           
                        });
                        dashboard.serviceList = summary;
                        dashboard.status = true;
                    }
                   
                }
                else
                {
                    dashboard.status = false;
                    dashboard.message = "No Records found!";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                dashboard.status = false;
                dashboard.message = "Error Occured While Showing Dashboard ";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                   
                }
            }
            return dashboard;
        }

        public Dashboarddetails dashboardsttransaction()
        {
            Dashboarddetails dsbtransaction = new Dashboarddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesteamorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<TransactionList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new TransactionList
                        {
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            invoice_date = rd["invoice_date"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()

                           )
                        });
                        dsbtransaction.TransactionList = summary;
                        dsbtransaction.status = true;
                    } 
                }
                else
                {
                    dsbtransaction.status = false;
                    dsbtransaction.message = "No Records found!";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                dsbtransaction.status = false;
                dsbtransaction.message = "Error Occured While Showing Dashboard Sales Transaction ";
                error = ex.ToString();
                    
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                   
                }
            }            

            return dsbtransaction;
        }
        public Dashboarddetails dashlistinvoicecount(string user_gid)
        {
            Dashboarddetails dsbtransaction = new Dashboarddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_dashlistinvoicecount");
                cmd.Parameters.AddWithValue("p_user_gid", user_gid);
          
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<invoicecountlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new invoicecountlist
                        {
                            invoice_count = rd["invoice_count"].ToString(),
                            user_code = rd["user_code"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            total_amount = rd["total_amount"].ToString()

                     
                        });
                        dsbtransaction.invoicecountlist = summary;
                        dsbtransaction.status = true;
                    }
                }
                else
                {
                    dsbtransaction.status = false;
                    dsbtransaction.message = "No Records found!";

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                dsbtransaction.status = false;
                dsbtransaction.message = "Error Occured While Showing Dashboard Sales Transaction ";
                error = ex.ToString();

            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();

                }
            }

            return dsbtransaction;
        }
        public Dashboardservicesales dashboardservicesales(string company_code)       
        {            
            Dashboardservicesales dsbservicesale = new Dashboardservicesales();
            try
            {
                double value=0;
                cmd = new MySqlCommand("sp_sel_salesvalueofyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);                
                if (rd.HasRows == true)
                {
                    rd.Read();
                    value = Double.Parse(rd["total_amount"].ToString());
                    rd.Close();
                    cmd = new MySqlCommand("sp_sel_servicesalesvalue");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<servicesalesList>();
                    if (rd.HasRows== true)
                    {
                        while (rd.Read())
                        {
                            summary.Add(new servicesalesList
                            {
                                highlight = rd["color_name"].ToString(),                         
                                label = rd["service_type"].ToString(),
                                color = rd["color_name"].ToString(),
                                value  = Double.Parse(rd["service_amount"].ToString()

                               )
                            });
                            dsbservicesale.servicesalesList = summary;                            
                        }
                       // rd.Close();

                        using (var client = new HttpClient())
                        {
                            var host = HttpContext.Current.Request.Url.Host;
                            var port = Convert.ToString(HttpContext.Current.Request.Url.Port);
                            var uri_builder = "http://" + host + ":" + port;
                            //var path = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["dashboard_file_path"].ToString() + "Salesservice" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json");
                            var path = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["dashboard_file_path"].ToString() + "Salesservice" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json");
                            //var path = uri_builder + company_code + ConfigurationManager.AppSettings["dashboard_file_path"].ToString() + "Salesservice" + ".json";
                            var json =JsonConvert.SerializeObject(summary);                            
                            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);                           
                            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);                            
                            File.WriteAllText(path, output);
                            var path1 = uri_builder+"/" + company_code + ConfigurationManager.AppSettings["dashboard_file_path"].ToString() + "Salesservice" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json";                           
                            //dsbservicesale.file_path = path;
                            dsbservicesale.file_path = output;
                            dsbservicesale.path = path1;
                        }                        
                    }
                    
                    else
                    {
                        
                    }
                }
                else
                {
                    
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }            
            return dsbservicesale;
        }
        public Dashboardservicesales dashboardservicesalesbar(string company_code)
        {
            Dashboardservicesales dsbservicesale = new Dashboardservicesales();
            try
            {
                double value = 0;
                cmd = new MySqlCommand("sp_sel_salesvalueofyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    value = Double.Parse(rd["total_amount"].ToString());
                    rd.Close();

                    cmd = new MySqlCommand("sp_sel_servicesalesvaluelist");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary1 = new List<servicesalesList1>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            summary1.Add(new servicesalesList1
                            {

                                label = rd["service_name"].ToString()

                            });
                            dsbservicesale.servicesalesList1 = summary1;

                        }
                        rd.Close();



                        cmd = new MySqlCommand("sp_sel_servicesalesvaluebar");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        rd = DBAccess.ExecuteReader(cmd);
                        var summary = new List<servicesalesbarList>();
                        if (rd.HasRows == true)
                        {
                            while (rd.Read())
                            {
                                summary.Add(new servicesalesbarList
                                {
                                    //highlightFill = rd["color_name"].ToString(),
                                    //fillColor = rd["color_name"].ToString(),
                                    ////label = rd["service_type"].ToString(),
                                    //strokeColor = rd["color_name"].ToString(),
                                    //highlightStroke = rd["color_name"].ToString(),
                                    data = rd["service_amount"].ToString()

                                });
                                dsbservicesale.servicesalesbarList = summary;

                            }
                            //rd.Close();

                            using (var client = new HttpClient())
                            {                                
                                var json = JsonConvert.SerializeObject(summary);
                                var json1 = JsonConvert.SerializeObject(summary1);
                                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                                dynamic jsonObj1 = Newtonsoft.Json.JsonConvert.DeserializeObject(json1);
                                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                                string output1 = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj1, Newtonsoft.Json.Formatting.Indented);                      
                                dsbservicesale.file_path = output;
                                dsbservicesale.file_path1 = output1;                               
                            }
                        }
                        else
                        {
                            
                        }
                        rd.Close();
                    }
                    else
                    {
                        
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }
            return dsbservicesale;
        }

        //Dim sw As StreamWriter = File.AppendText(ConfigurationManager.AppSettings("LOG_FILENAME").ToString)
        //    sw.WriteLine(Now() & " : " & strVal)
        //    sw.Close()
            //File.AppendAllText(@"c:\path\file.txt", "text content" + Environment.NewLine);
            //objcmnfunctions.LogforAudit("Error Occured ", cmd)

           // StreamWriter sw = new System.IO.StreamWriter();

    }
}