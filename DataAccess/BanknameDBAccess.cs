using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class BanknameDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader DataReader;
        public bankname GetAll()
        {
            bankname bankname = new bankname();
            try
            {
                cmd = new MySqlCommand("sp_sel_bankname");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                DataReader = DBAccess.ExecuteReader(cmd);
                var banksummary = new List<banknamelist>();
                if (DataReader.HasRows == true)
                {
                    while (DataReader.Read())
                    {
                        banksummary.Add(new banknamelist
                        {
                            //bankgid = DataReader["bank_gid"].ToString(),
                            bankcode = DataReader["bank_code"].ToString(),
                          bankname = DataReader["bank_name"].ToString()
                        });
                    }
                    bankname.banknamelist = banksummary;
                    bankname.status = true;
                }

                else
                {
                    bankname.status = false;
                    bankname.message = "No Records Found";
                }
                DataReader.Close();
            }
            catch (Exception ex)
            {
                bankname.status = false;
                bankname.message = "Error occurred while show record";
            }
            finally
            {
                if(cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return bankname;
           
        }
       public banknamemodel addbankname(banknamedetails val, string usergid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_bankcodevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_bank_code", val.bankcode);
                MySqlDataReader DataReader = DBAccess.ExecuteReader(cmd);
                if (DataReader.Read())
                {
                    val.status = false;
                    val.message = "This bank code already exist";
                }
                else
                {
                    //while (DataReader.Read())
                    //{
                        cmd = new MySqlCommand("sp_ins_bankname");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_bank_code", val.bankcode);
                        cmd.Parameters.AddWithValue("p_bank_name", val.bankname);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Bank name added successfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error occurred while adding bankname";
                    }
                }

            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error occurred while adding bankname";
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