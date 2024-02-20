using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class IncomeandexpenseDBAccess
    {
        //int mnResult = 0;
        MySqlCommand cmd;
        MySqlDataReader rd;
        string error;
       public Incomeandexpense incomeandexpensesummary(IEdetails val)
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
                cmd = new MySqlCommand("sp_sel_expensesummary");
                cmd.Parameters.AddWithValue("p_transaction_fromdate", val.from_date);
                cmd.Parameters.AddWithValue("p_transaction_todate", val.to_date);
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var expensesummary = new List<expenselist>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        expensesummary.Add(new expenselist
                        {
                            account_gid = rd["account_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            accountgroup_name=rd["accountgroup_name"].ToString(),
                            debit_amount=Double.Parse(rd["debit_amount"].ToString()),
                            branch_name = rd["branch_name"].ToString(),
                            credit_amount = Double.Parse(rd["credit_amount"].ToString())

                        });
                      }

                    }
                val.expenselist = expensesummary;
                val.status = true;
                rd.Close();

                cmd = new MySqlCommand("sp_sel_incomesummary");
                cmd.Parameters.AddWithValue("p_transaction_fromdate", val.from_date);
                cmd.Parameters.AddWithValue("p_transaction_todate", val.to_date);
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var incomesummary = new List<incomelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        incomesummary.Add(new incomelist
                        {
                            account_gid = rd["account_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            accountgroup_name = rd["accountgroup_name"].ToString(),
                            debit_amount = Double.Parse(rd["debit_amount"].ToString()),
                            branch_name = rd["branch_name"].ToString(),
                            credit_amount = Double.Parse(rd["credit_amount"].ToString())
                        });
                    }

                }
                val.incomelist = incomesummary;
                val.status = true;
                
         }
            catch (Exception ex)
            {
                val.status = false;
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


        public Incomeandexpense balancesheetsummary(IEdetails val,string value)
        {
            
            try
            {
                //if (val.from_date == null)
                //{
                //    val.from_date = "null";
                //}
                //if (val.to_date == null)
                //{
                //    val.to_date = "null";
                //}
                if (val.branch_gid == null)
                {
                    val.branch_gid = "null";
                } 

                cmd = new MySqlCommand("sp_Sel_bsexpenseclosing");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    val.expense_closing = Convert.ToDouble(rd["expense_closing"].ToString());
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_bsincomeclosing");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    val.income_closing = Convert.ToDouble(rd["income_closing"].ToString());
                }
                rd.Close();

                cmd = new MySqlCommand("sp_sel_liabilitysummary");
                //cmd.Parameters.AddWithValue("p_transaction_fromdate", val.from_date);
                //cmd.Parameters.AddWithValue("p_transaction_todate", val.to_date);
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var expensesummary = new List<expenselist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        expensesummary.Add(new expenselist
                        {
                            account_gid = rd["account_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            accountgroup_name = rd["accountgroup_name"].ToString(),
                            debit_amount = Double.Parse(rd["debit_amount"].ToString()),
                            branch_name = rd["branch_name"].ToString(),
                            credit_amount = Double.Parse(rd["credit_amount"].ToString()),
                            company_code = value,
                        });
                    }

                }
                val.expenselist = expensesummary;
                val.status = true;
               
                rd.Close();

                cmd = new MySqlCommand("sp_sel_assetsummary");
                //cmd.Parameters.AddWithValue("p_transaction_fromdate", val.from_date);
                //cmd.Parameters.AddWithValue("p_transaction_todate", val.to_date);
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var incomesummary = new List<incomelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        incomesummary.Add(new incomelist
                        {
                            account_gid = rd["account_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            accountgroup_name = rd["accountgroup_name"].ToString(),
                            debit_amount = Double.Parse(rd["debit_amount"].ToString()),
                            branch_name = rd["branch_name"].ToString(),
                            credit_amount = Double.Parse(rd["credit_amount"].ToString()),
                            company_code = value,
                        });
                    }

                }
                val.incomelist = incomesummary;
                val.status = true;

            }
            catch (Exception ex)
            {
                val.status = false;
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