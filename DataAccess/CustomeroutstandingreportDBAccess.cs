using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess
{
    public class CustomeroutstandingreportDBAccess
    {
        //MySqlCommand cmd = null;
        DataTable objtb1;
        MySqlDataAdapter sqlad = new MySqlDataAdapter();
        string error;
        
        public customeroutstaindingdetails customeroutstandingreceipt(customeroutstaindingdetails val)
        {
            customeroutstaindingdetails outstaandingdtl = new customeroutstaindingdetails();
            try
            {
               
                sqlad.SelectCommand = new MySqlCommand("sp_sel_outstandingreceipt");
                sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlad.SelectCommand.Parameters.AddWithValue("p_from_date", val.from_date);
                sqlad.SelectCommand.Parameters.AddWithValue("p_to_date", val.to_date);
                objtb1 = DBAccess.GetDataTable(sqlad);
                var outstanding = new List<customeroutstandinglist>();
                double lnoutstanding_amount = 0;
                foreach (DataRow dr in objtb1.Rows)
                {
                    lnoutstanding_amount = double.Parse(dr["outstanding_amount"].ToString());
                    if (lnoutstanding_amount > 0)
                    {
                        outstanding.Add(new customeroutstandinglist
                        {
                            invoice_date = dr["invoice_date"].ToString(),
                            invoice_refnumber = dr["invoice_refnumber"].ToString(),
                            invoice_amount = double.Parse(dr["invoice_amount"].ToString()),
                            received_amount = double.Parse(dr["received_amount"].ToString()),
                            outstanding_amount = double.Parse(dr["outstanding_amount"].ToString()),
                            customer_name = dr["customer_name"].ToString(),
                            contact_details = dr["contact_details"].ToString()

                        });
                        outstaandingdtl.status = true;
                    }
                    else
                    {
                        outstaandingdtl.status = false;
                        outstaandingdtl.message = "No Records Found!";

                    }
                }
                 outstaandingdtl.customeroutstandinglist = outstanding;
               
            }

            catch (Exception ex)
            {
                outstaandingdtl.status = false;
                outstaandingdtl.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (sqlad.SelectCommand.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlad.SelectCommand.Connection.Close();
                }
            }

            return outstaandingdtl;
        }
    }
}