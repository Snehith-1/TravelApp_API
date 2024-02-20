using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class OrderprofitreportDBAccess
    {

        MySqlCommand cmd = null;
        MySqlDataReader rd;
        //int mnresult = 0;
        string error;
        public orderprofitdetails summary(orderprofitdetails val)
        {           
            try
            {
                if (val.from_date == null)
                {
                    val.from_date = "null";
                }
                if (val.to_date == null)
                {
                    val.to_date = "null";
                }
                if (val.branch_gid == null)
                {
                    val.branch_gid = "null";
                }
                if (val.service_name == null || val.service_name == "")
                {
                    val.service_name = "null";
                }
                cmd = new MySqlCommand("sp_sel_saleorderprobabilityreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date",val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
               cmd.Parameters.AddWithValue("p_service_name",val.service_name);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<orderprofitlist>();
                if(rd.HasRows ==true)
                {
                   while( rd.Read())
                    {
                        summary.Add(new orderprofitlist
                        {
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            order_refnumber = rd["order_refnumber"].ToString(),//salesorderreferenceno as 
                            customer_name = rd["customer_name"].ToString(),
                            salesorder_amount = double.Parse(rd["salesorder_amount"].ToString()),
                            income = double.Parse(rd["income"].ToString()),//billing_amount as income
                            expense = double.Parse(rd["expense"].ToString()),//payment_amount as expense
                            profit_amount = double.Parse(rd["profit"].ToString()),
                            branch_name = rd["branch_name"].ToString()
                        });
                        val.orderprofitlist = summary;
                        val.status = true;
                        val.message = "Records Added Successfully";
                    }                   
                }
                else
                {
                    val.status = false;
                    val.message = "No Records Found";
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
                if(cmd.Connection .State ==System .Data .ConnectionState .Open )
                {
                    cmd.Connection.Close();
                }
            }
            return val;
        }
    }
}