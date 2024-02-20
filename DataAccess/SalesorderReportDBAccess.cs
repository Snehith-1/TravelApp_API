using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class SalesorderReportDBAccess
    {
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public SalesorderReportSummary GetAll(SalesorderReport values)
        {
            SalesorderReportSummary SalesorderReportSummary = new SalesorderReportSummary();
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
                cmd = new MySqlCommand("sp_sel_salesreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", values.from_date);
                cmd.Parameters.AddWithValue("p_to_date", values.to_date);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesorderReportList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SalesorderReportList
                        {

                            year = rd["year"].ToString(),
                            month = rd["month"].ToString(),
                            salescount = rd["salescount"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString())

                           
                        });
                    }
                    SalesorderReportSummary.SalesorderReportList = summary;
                    SalesorderReportSummary.status = true;
                    
                }
                else
                {
                    SalesorderReportSummary.status = false;
                    SalesorderReportSummary.message = "No Records Found!";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                SalesorderReportSummary.status = false;
                SalesorderReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SalesorderReportSummary;
        }

        public SalesorderReportGraph GetAllgraph(SalesorderReport values)
        {
            SalesorderReportGraph SalesorderReportSummary = new SalesorderReportGraph();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesreport");
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

                        if (result == "")
                        {
                            result = rd["month"].ToString();
                        }
                        else
                        {
                            result = result + "," + rd["month"].ToString();
                        }
                        if (resultamt == "")
                        {
                            resultamt = rd["total_amount"].ToString();
                        }
                        else
                        {
                            resultamt = resultamt + "," + rd["total_amount"].ToString();
                        }
                    }
                    SalesorderReportSummary.color = "#9cd159";
                    SalesorderReportSummary.label = "Sales";
                    SalesorderReportSummary.label = result;
                    SalesorderReportSummary.series = resultamt;
                  SalesorderReportSummary.status = true;
                    //rd.Close();
                }
                else
                {
                    SalesorderReportSummary.status = false;
                    
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                SalesorderReportSummary.status = false;
                SalesorderReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SalesorderReportSummary;
        }

        public SalesorderReportSummaryChild GetAllChild(SalesorderReport values)
        {
            SalesorderReportSummaryChild SalesorderReportSummary = new SalesorderReportSummaryChild();
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
                
                cmd = new MySqlCommand("sp_sel_salesdtlreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from", values.from_date);
                cmd.Parameters.AddWithValue("p_to", values.to_date);
                cmd.Parameters.AddWithValue("p_month", values.month);
                cmd.Parameters.AddWithValue("p_year", values.year);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesorderReportChildList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SalesorderReportChildList
                        {

                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            invoice_date = Convert.ToDateTime(rd["invoice_date"]).ToString("dd/MM/yyyy"),
                            customer_name = rd["customer_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            net_amount = double.Parse(rd["net_amount"].ToString()

                           )
                        });
                    }
                    SalesorderReportSummary.SalesorderReportChildList = summary;
                    SalesorderReportSummary.status = true;
                    
                }
                else
                {
                    SalesorderReportSummary.status = false;
                    SalesorderReportSummary.message ="No Records Found!";
                    
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                SalesorderReportSummary.status = false;
                SalesorderReportSummary.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SalesorderReportSummary;
        }
    }
}