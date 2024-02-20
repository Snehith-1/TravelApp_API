using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    
    public class bankbookDBAccess
    {       
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        
        public Bank bankboodadddetails(string  val)
        {
            Bank bank = new Bank();
            try
            {
                               
                cmd = new MySqlCommand("sp_sel_bankbookdetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_bank_gid", val);
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                if (rd3.Read())
                {
                    bank.bank_gid = rd3["bank_gid"].ToString();
                    bank.bank_name = rd3["bank_name"].ToString();
                    bank.account_number = rd3["account_number"].ToString();
                    bank.ifsc_code = rd3["ifsc_code"].ToString();
                    bank.swift_code = rd3["swift_code"].ToString();
                    bank.neft_code = rd3["neft_code"].ToString();
                    bank.bank_code = rd3["bank_code"].ToString();
                    bank.account_type = rd3["account_type"].ToString();
                    bank.account_gid = rd3["account_gid"].ToString();
                }
                rd3.Close();
                var balance = new List<accountlist>();
                cmd = new MySqlCommand("sp_sel_bankaddaccountlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    balance.Add(new accountlist
                    {
                        account_name = rd["account_name"].ToString(),
                        account_gid = rd["account_gid"].ToString()
                    });
                }
                bank.accountlist = balance;
                bank.status = true;
                rd.Close();
                var asset = new List<accountgrouplist>();
                cmd = new MySqlCommand("sp_sel_bankaddaccountgrouplist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                while (rd1.Read())
                {
                    asset.Add(new accountgrouplist
                    {
                        accountgroup_name = rd1["accountgroup_name"].ToString(),
                        accountgroup_gid = rd1["accountgroup_gid"].ToString()
                    });
                }
                bank.accountgrouplist = asset;
                bank.status = true;
                rd1.Close();
                rd.Close();
                rd3.Close();
            }
            catch (Exception ex)
            {
                bank.status = false;
                bank.message = "Internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return bank;
        }       
        public Bank opgetaccountname(string val)
        {
            Bank bal = new Bank();
            try
            {
            
                cmd = new MySqlCommand("sp_sel_getaccountgroupinbank");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_gid", val);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    bal.accountgroup_name = rd["accountgroup_name"].ToString();
                    bal.accountgroup_gid = rd["accountgroup_gid"].ToString();                  
                }
              
                bal.status = true;
                rd.Close();
                //rd.ReadToend();             
            }
            catch (Exception ex)
            {
                bal.status = false;
                bal.message = "Internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return bal;
        }
        public Bank bankbooksummary(Bankdetails val)
        {
            Bank bal = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_bankbooksummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_gid", val.bank_gid);
                cmd.Parameters.AddWithValue("p_opening_balance", val.opening_balance);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<bankbooklist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new bankbooklist
                        {

                            journal_gid = Convert.ToInt16(rd["journal_gid"].ToString()),
                            journal_refnumber = rd["journal_refnumber"].ToString(),
                            transaction_date = rd["transaction_date"].ToString(),
                            bank_name = rd["bank_name"].ToString(),
                            account_number = rd["account_number"].ToString(),
                            account_desc = rd["account_desc"].ToString(),                            
                            remarks = rd["remarks"].ToString(),
                            credit_amount = Double.Parse(rd["credit_amount"].ToString()),
                            debit_amount = Double.Parse(rd["debit_amount"].ToString()),
                            closing_amount = Double.Parse(rd["closing_amount"].ToString()),
                            account_gid = rd["account_gid"].ToString(),
                            journaldtl_gid = rd["journaldtl_gid"].ToString(),
                            reference_gid = rd["reference_gid"].ToString()                           
                        });
                    }
                    bal.bankbooklist = summary;
                    bal.status = true;
                    //rd.Close();
                }
                else
                {
                    bal.status = false;
                    
                    //rd.Close();
                }

                rd.Close();
            }
            catch (Exception ex)
            {
                bal.status = false;
                bal.message = "Internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return bal;
        }
        public Bankmodel bankbookdelete(journaldetails values)
        {
            Bankmodel delete = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_bankbook");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", values.journal_gid);               
                mnresult = DBAccess.ExecuteNonQuery(cmd);             
                if (mnresult == 1)
                {
                    //cmd = new MySqlCommand("sp_del_bankbookjournal");
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;                 
                    //cmd.Parameters.AddWithValue("p_transaction_gid", values.transactiongid);
                    //mnresult = DBAccess.ExecuteNonQuery(cmd);
                    cmd = new MySqlCommand("sp_del_bankbookjournal");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_journal_gid", values.journal_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult ==1)
                    {
                        //cmd = new MySqlCommand("sp_sel_bankbookdel");
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("p_journal_gid", values.journalgid);
                        //rd = DBAccess.ExecuteReader(cmd);
                        //if(rd.HasRows==false)
                        //{
                        //    cmd = new MySqlCommand("sp_del_bankbookdel");
                        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //    cmd.Parameters.AddWithValue("p_journal_gid", values.journalgid);
                        //    mnresult = DBAccess.ExecuteNonQuery(cmd);
                        //    if(mnresult ==1)
                        //    {
                        //        delete.status = true;                            
                        //    }
                        //    else
                        //    {
                        //        delete.status = false;
                        //    }
                        //}
                        delete.status = true;
                    }
                    else
                    {
                        delete.status = false;
                        delete.message = "Internal error occured";
                    }                   
                }                        
                else
                {
                    delete.status = false;
                    delete.message = "Cannot Delete Team Assigned with the Employee";
                }

            }
            catch (Exception ex)
            {
                delete.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return delete;
        }
    }
}