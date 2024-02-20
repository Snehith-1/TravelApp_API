using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class AdvanceDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Advance advancesummary(string val)
        {
            Advance Adv = new Advance();
            try
            {
                cmd = new MySqlCommand("sp_sel_advance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Advancelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                     {
                        summary.Add(new Advancelist
                        {
                            advance_gid = int.Parse(rd["advance_gid"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                            payment_mode = rd["payment_mode"].ToString(),
                            payment_details = rd["payment_details"].ToString(),
                            advance_date = rd["advance_date"].ToString(),
                            advance_amount = double.Parse (rd["advance_amount"].ToString()),
                            created_by = rd["created_by"].ToString(),
                            created_date = rd["created_date"].ToString(),
                        });
                    }
                    Adv.Advancelist = summary;
                    Adv.status = true;
                }
                else
                {
                    Adv.status = false;
                }
            }
            catch (Exception ex)
            {
                Adv.status = false;
                error = ex.ToString();           
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Adv;
        }
        public Advancemodel advanceadd(Advancedetail val, string userGid)
        {
            Advancemodel add = new Advancemodel();
            try
            {                                             
                cmd = new MySqlCommand("sp_ins_advance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_advance_date", val.advance_date);
                cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                cmd.Parameters.AddWithValue("p_payment_details", val.payment_details);
                cmd.Parameters.AddWithValue("p_advance_amount", val.advance_amount);             
                cmd.Parameters.AddWithValue("p_created_by", userGid);
                cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    add.status = true;
                    add.message = "Records added sucessfully";
                }
                else
                {
                    add.status = false;                                
                }
            }
            catch (Exception ex)
            {
                add.status = false;
                error = ex.ToString();           
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return add;
        }
    }
}