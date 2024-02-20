﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
namespace DataAccess
{
    public class CashbookDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        MySqlDataAdapter sqlad = new MySqlDataAdapter();
        string error;
        
        public Bank cashboodadddetails(string company_code)
        {
            Bank bank = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_cashcompanydetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("p_company_gid", company_code);
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                if (rd3.Read())
                {
                    bank.company_gid = int.Parse(rd3["company_gid"].ToString());
                    bank.company_name = rd3["company_name"].ToString();
                    bank.company_code = rd3["company_code"].ToString();
                    bank.company_address = rd3["company_address"].ToString();
                    bank.company_contact_number = rd3["company_contact_number"].ToString();
                    bank.company_email_address = rd3["company_email_address"].ToString();
                    bank.contact_person = rd3["contact_person"].ToString();                   
                }
                rd3.Close();
                //var balance = new List<accountlist>();
                //cmd = new MySqlCommand("sp_sel_cashaddaccountlist");
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                //while (rd.Read())
                //{
                //    balance.Add(new accountlist
                //    {
                //        accountname = rd["account_name"].ToString(),
                //        accountgid = rd["account_gid"].ToString()
                //    });
                //}
                //bank.accountlist = balance;

                sqlad.SelectCommand = new MySqlCommand("sp_sel_cashaddaccountlist");
                sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable dt = DBAccess.GetDataTable(sqlad);
                var balance = new List<accountlist>();
                foreach (DataRow dr in dt.Rows)
                {
                    balance.Add(new accountlist
                    {
                        account_name = dr["account_name"].ToString(),
                        account_gid = dr["account_gid"].ToString()
                    });
                }
                bank.accountlist = balance;
                bank.status = true;
                //var asset = new List<accountgrouplist>();
                //cmd = new MySqlCommand("sp_sel_cashaddaccountgrouplist");
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                //while (rd1.Read())
                //{
                //    asset.Add(new accountgrouplist
                //    {
                //        accountgroupname = rd1["accountgroup_name"].ToString(),
                //        accountgroupgid = rd1["accountgroup_gid"].ToString()
                //    });
                //}
                sqlad.SelectCommand = new MySqlCommand("sp_sel_cashaddaccountgrouplist");
                sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable dt1 = DBAccess.GetDataTable(sqlad);
                var asset = new List<accountgrouplist>();
                foreach (DataRow dr in dt1.Rows)
                {
                    asset.Add(new accountgrouplist
                    {
                        accountgroup_name = dr["accountgroup_name"].ToString(),
                        accountgroup_gid = dr["accountgroup_gid"].ToString()
                    });
                }
               bank.accountgrouplist = asset;
               bank.status = true;
               
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

                cmd = new MySqlCommand("sp_sel_getaccountgroupincash");
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
        public Bank cashbooksummary(Bankdetails val,string company_code)
        {
            Bank bal = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_cashbooksummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_account_gid", company_code);
                //cmd.Parameters.AddWithValue("p_year", val.year);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<cashbooklist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new cashbooklist
                        {

                            journal_gid = int.Parse(rd["journal_gid"].ToString()),
                            journal_refnumber = rd["journal_refnumber"].ToString(),
                            transaction_date = rd["transaction_date"].ToString(),
                            //companyname = rd["company_name"].ToString(),                            
                            account_desc = rd["account_desc"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            credit_amount = double.Parse(rd["credit_amount"].ToString()),
                            debit_amount = double.Parse(rd["debit_amount"].ToString()),
                            closing_amount = double.Parse(rd["closing_amount"].ToString()),
                            account_gid = rd["account_gid"].ToString(),
                            journaldtl_gid = rd["journaldtl_gid"].ToString(),
                            reference_gid = rd["reference_gid"].ToString(),
                            branch_name = rd["branch_name"].ToString()  
                            //transactiongid =rd["transaction_gid"].ToString(),
                        });
                    }
                    bal.cashbooklist = summary;
                    bal.status = true;
                }
                else
                {
                    bal.status = false;
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
        public Bankmodel cashbookdelete(journaldetails values)
        {
            Bankmodel delete = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_bankbook");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journaldtl_gid", values.journaldtl_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_del_bankbookjournal");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_transaction_gid", values.transaction_gid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        cmd = new MySqlCommand("sp_sel_bankbookdel");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", values.journal_gid);
                        rd = DBAccess.ExecuteReader(cmd);
                        if (rd.HasRows == false)
                        {
                            cmd = new MySqlCommand("sp_del_bankbookdel");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_journal_gid", values.journal_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                delete.status = true;
                            }
                            else
                            {
                                delete.status = false;
                            }
                        }
                        rd.Close();
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
                ex.ToString();
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

        public Bankmodel cashbookentry(journaldetails val, string usergid, string comapnycode)
        {
            Bankmodel bnk = new Bankmodel();
            try
            {

                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                rd.Read();
                {
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.transaction_date);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    rd1.Read();
                    {
                        foreach (var data in val.journallist)
                        {
                            cmd = new MySqlCommand("sp_ins_cashbookjournalentry");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_transaction_date", val.transaction_date);
                            cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                            cmd.Parameters.AddWithValue("p_transaction_type", val.transaction_type);
                            cmd.Parameters.AddWithValue("p_reference_type", val.reference_type);
                            cmd.Parameters.AddWithValue("p_reference_gid", val.reference_gid);
                            cmd.Parameters.AddWithValue("p_transaction_code", val.transaction_code);
                            cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                            cmd.Parameters.AddWithValue("p_journal_from", val.journal_from);
                            cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                            cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                            cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                            cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                            cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                            cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                            cmd.Parameters.AddWithValue("p_created_By", usergid);
                            cmd.Parameters.AddWithValue("p_account_gid", data.account_gid);
                            cmd.Parameters.AddWithValue("p_company_gid", val.company_name);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {

                                cmd = new MySqlCommand("sp_ins_journalentrydtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                                cmd.Parameters.AddWithValue("p_journal_type", data.journal_type);
                                cmd.Parameters.AddWithValue("p_journal_type1", "");
                                cmd.Parameters.AddWithValue("p_transaction_amount", data.transaction_amount);
                                cmd.Parameters.AddWithValue("p_account_gid", data.account_gid);
                                cmd.Parameters.AddWithValue("p_remarks", data.remarks);
                                cmd.Parameters.AddWithValue("p_created_By", usergid);
                                cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                                cmd.Parameters.AddWithValue("p_journal_from", val.journal_from);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                            }
                        }
                        
                    }
                    rd1.Close();
                }
                rd.Close();
                             
               if (mnresult == 1)
                    {
                        bnk.status = true;
                        bnk.message = "Records added sucessfully";
                    }
                
                else
                {
                    bnk.status = false;
                    bnk.message = "Error Occured While Adding journalentry";
                }
                //rd.Close();

            }
            catch (Exception ex)
            {
                bnk.status = false;
                bnk.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return bnk;
        }
    }
}