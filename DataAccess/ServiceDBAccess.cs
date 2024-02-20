﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class ServiceDBAccess
    {
        //int mnresult= 0;
        MySqlCommand cmd = null;
        string error;  
        public Service GetAll()
        {
            Service service = new Service();
            try
            {
                cmd = new MySqlCommand("sp_sel_service");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Servicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Servicelist
                        {

                            service_gid = int.Parse(rd["service_gid"].ToString()),
                            service_code = rd["service_code"].ToString(),
                            service_name = rd["service_name"].ToString()
                          
                        });
                    }
                    service.servicelist = summary;
                    service.status = true;
                    
                }
                else
                {
                    service.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                service.status = false;
                service.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return service;
        }

        public Service servicereportsummary(Servicedetails val)
        {
            Service service = new Service();
            try
            {
                cmd = new MySqlCommand("sp_sel_servicereport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Servicereportlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Servicereportlist
                        {
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            service_amount = Double.Parse(rd["service_amount"].ToString()),
                            invoice_date = rd["invoice_date"].ToString(),
                            passenger_firstname=rd["passenger_firstname"].ToString(),
                            created_by = rd["created_by"].ToString(),

                        });
                    }
                    service.Servicereportlist = summary;
                    service.status = true;

                }
                else
                {
                    service.status = false;
                    service.message = "No Records Found!";

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                service.status = false;
                service.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return service;
        }
    }
}