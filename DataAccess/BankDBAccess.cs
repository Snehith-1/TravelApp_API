using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace DataAccess
{
    public class BankDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd,cmd1 = null;
        MySqlDataReader rd;
        string error;      
        public Bank banksummary()
        {
            Bank bank = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_bank");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Banklist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Banklist
                        {
                            bank_gid = rd["bank_gid"].ToString(),
                            bank_code = rd["bank_code"].ToString(),
                            bank_name = rd["bank_name"].ToString(),
                            account_number = rd["account_number"].ToString(),
                            account_type = rd["account_type"].ToString(),
                            ifsc_code = rd["ifsc_code"].ToString(),
                            neft_code = rd["neft_code"].ToString(),
                            swift_code = rd["swift_code"].ToString(),
                            //accountgroup=rd["account_group"].ToString(),
                            opening_balance = rd["opening_balance"].ToString(),
                            account_gid = rd["account_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            closing_amount = Double.Parse(rd["closing_amount"].ToString()),
                            branch_name = rd["branch_name"].ToString(),
                            //branchgid=rd["branch_gid"].ToString(),
                            remarks =rd["remarks"].ToString()
                           
                        });
                    }
                    bank.Banklist = summary;
                    bank.status = true;
                    //rd.Close();
                    //rd.Dispose();
                    
                }
                else
                {
                    bank.status = false;
                    bank.message = "No Records Fund";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                bank.status = false;
                bank.message = "Error Occured While Show the Record";
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
        
        public Bankmodel bankadd(journaldetails val, string user_gid)
        {
            Bankmodel bnk = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_bank_codevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_bank_code", val.bank_code);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    bnk.status = false;
                    bnk.message = "Bank Code Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_bank");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_bank_code", val.bank_code);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_account_number", val.account_number);
                    cmd.Parameters.AddWithValue("p_account_type", val.account_type);
                    cmd.Parameters.AddWithValue("p_ifsc_code", val.ifsc_code);
                    cmd.Parameters.AddWithValue("p_neft_code", val.neft_code);
                    cmd.Parameters.AddWithValue("p_swift_code", val.swift_code);
                    cmd.Parameters.AddWithValue("p_opening_balance", val.opening_balance);
                    cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                    cmd.Parameters.AddWithValue("p_account_name", "");
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    cmd.Parameters.AddWithValue("p_date", val.date);
                    cmd.Parameters.AddWithValue("p_created_By", user_gid);
                    cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                    cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        cmd = new MySqlCommand("sp_ins_chartofaccounts");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_account_gid", "");
                        cmd.Parameters.AddWithValue("p_account_name", val.account_name);
                        cmd.Parameters.AddWithValue("p_gl_code", val.gl_code);
                        cmd.Parameters.AddWithValue("p_ledger_type", "Y");
                        cmd.Parameters.AddWithValue("p_display_type", "A");
                        cmd.Parameters.AddWithValue("p_created_By", user_gid);
                        cmd.Parameters.AddWithValue("p_accountgroup_gid", val.accountgroup_gid);
                        cmd.Parameters.AddWithValue("p_accountgroup_name", val.accountgroup_name);
                        cmd.Parameters.AddWithValue("p_has_child", "N");
                        cmd.Parameters.AddWithValue("p_ledger", "BS");
                        cmd.Parameters.AddWithValue("p_account_code", val.account_code);
                        rd = DBAccess.ExecuteReader(cmd);
                        if (mnresult == 1)
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
                                    cmd = new MySqlCommand("sp_ins_journalentrybank");
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("p_transaction_date", val.transaction_date);
                                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                                    cmd.Parameters.AddWithValue("p_transaction_type", val.transaction_type);
                                    cmd.Parameters.AddWithValue("p_reference_type", "");
                                    cmd.Parameters.AddWithValue("p_reference_gid", val.reference_gid);
                                    cmd.Parameters.AddWithValue("p_transaction_code", val.transaction_code);
                                    cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                                    cmd.Parameters.AddWithValue("p_journal_from", val.journal_from);
                                    cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                                    cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                                    cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                                    cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                                    cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                                    //cmd.Parameters.AddWithValue("p_journal_gid", "0");
                                    cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                                    cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                                }
                            }
                            if (mnresult == 1)
                            {
                                cmd = new MySqlCommand("sp_ins_journalentrybankdtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                                cmd.Parameters.AddWithValue("p_journal_type", val.journal_type);
                                cmd.Parameters.AddWithValue("p_transaction_amount", val.transaction_amount);
                                cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                                cmd.Parameters.AddWithValue("p_bank_gid", "");
                                mnresult = DBAccess.ExecuteNonQuery(cmd);

                                if (mnresult == 1)
                                {
                                    bnk.status = true;
                                    bnk.message = "Records added sucessfully";
                                }
                            }
                            else
                            {
                                bnk.status = false;
                                bnk.message = "Error Occured While Adding journalentry";
                            }
                        }
                        else
                        {
                            bnk.status = false;
                            bnk.message = "Error Occured While Adding Bank in Char of Accounts";
                        }
                    }
                    else
                    {
                        bnk.status = false;
                        bnk.message = "Error Occured While Adding Partner";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                bnk.status = false;
                bnk.message = "Error Occured While Adding Partner";
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
        public Bankmodel journaladd(journaldetails val, string user_gid, string comapnycode)
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
                    cmd.Parameters.AddWithValue("p_date",val.transaction_date);
                    MySqlDataReader rd1= DBAccess.ExecuteReader(cmd);
                    rd1.Read();
                    {
                        cmd = new MySqlCommand("sp_ins_journalentry");
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
                        cmd.Parameters.AddWithValue("p_created_By", user_gid);
                        cmd.Parameters.AddWithValue("p_branch_gid",val.branch_gid);
                        cmd.Parameters.AddWithValue("p_branch_name", "");
                        mnresult = DBAccess.ExecuteNonQuery(cmd);

                    }
                    rd1.Close();
                }
                if (mnresult == 1)
                {
                    if(val.type== "Opening Balance")
                    {
                        cmd = new MySqlCommand("sp_ins_journalentryobdtl");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid",val.journal_gid);
                        cmd.Parameters.AddWithValue("p_journal_type", val.journal_type);                       
                        cmd.Parameters.AddWithValue("p_transaction_amount", val.transaction_amount);
                        cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                        cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                        cmd.Parameters.AddWithValue("p_created_By", user_gid);                       
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }
                    else if(val.journal_from== "Bank Book")
                    {
                        foreach (var data in val.journallist)
                        {
                            cmd = new MySqlCommand("sp_ins_journalentrydtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_journal_gid", data.journal_gid);
                            cmd.Parameters.AddWithValue("p_journal_type", data.journal_type);
                            cmd.Parameters.AddWithValue("p_journal_type1", "");
                            cmd.Parameters.AddWithValue("p_transaction_amount", data.transaction_amount);
                            cmd.Parameters.AddWithValue("p_account_gid", data.account_gid);
                            cmd.Parameters.AddWithValue("p_remarks", data.remarks);
                            cmd.Parameters.AddWithValue("p_created_By", user_gid);
                            cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                            cmd.Parameters.AddWithValue("p_journal_from", val.journal_from);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                        }

                    }
                    else { 
                    foreach (var data in val.journallist)
                    { 
                        cmd = new MySqlCommand("sp_ins_invoicejournalentrydtl");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid",data.journal_gid);
                        cmd.Parameters.AddWithValue("p_journal_type", data.journal_type);
                        cmd.Parameters.AddWithValue("p_journal_from",val.journal_from);
                        cmd.Parameters.AddWithValue("p_transaction_amount", data.transaction_amount);
                        cmd.Parameters.AddWithValue("p_account_gid", data.account_gid);
                        cmd.Parameters.AddWithValue("p_remarks", data.remarks);
                        cmd.Parameters.AddWithValue("p_created_By", user_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                    }
                    }
                    if (mnresult ==1)
                    {
                        bnk.status = true;
                        bnk.message = "Records added sucessfully";
                    }                
                }
                else
                {
                    bnk.status = false;
                    bnk.message = "Error Occured While Adding journalentry";
                }
                rd.Close();
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
        public Bankmodel chartofaccountadd(chartofaccountdetails val, string user_gid)
        {
            Bankmodel chart = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_masterchartofaccount");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_name", val.account_name);
                cmd.Parameters.AddWithValue("p_gl_code", val.gl_code);
                cmd.Parameters.AddWithValue("p_account_code", val.account_code);
                cmd.Parameters.AddWithValue("p_ledger_type", val.ledger_type);
                cmd.Parameters.AddWithValue("p_accountgroup_name", val.accountgroup_name);
                cmd.Parameters.AddWithValue("p_accountgroup_gid", val.accountgroup_gid);
                cmd.Parameters.AddWithValue("p_display_type", val.display_type);
                cmd.Parameters.AddWithValue("p_has_child", val.has_child);
                cmd.Parameters.AddWithValue("p_created_by",user_gid);
                cmd.Parameters.AddWithValue("p_ledger", val.ledger);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    chart.status = true;
                    chart.message = "Records added sucessfully";
                }
            }
            catch (Exception ex)
            {
                chart.status = false;
                chart.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chart;
        }
        public Bank getaccountgroupname()
        {
            Bank chart = new Bank();
            try
            {              
                var country = new List<ledgergrouplist>();
                cmd = new MySqlCommand("sp_sel_ledgergrouplist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;    
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    country.Add(new ledgergrouplist
                    {
                        account_name = rd["account_name"].ToString(),
                        account_gid = rd["account_gid"].ToString()
                    });
                }
                chart.ledgergrouplist = country;
                chart.status = true;
                rd.Close();
                var account = new List<accountgrouplist>();
                cmd1 = new MySqlCommand("sp_sel_accountgrouplist");
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd1);
                while (rd1.Read())
                {
                    account.Add(new accountgrouplist
                    {
                        account_name = rd1["account_name"].ToString(),
                        account_gid = rd1["account_gid"].ToString()
                    });
                }
                chart.accountgrouplist = account;
                chart.status = true;
                rd1.Close();
                
            }
            catch (Exception ex)
            {
                chart.status = false;
                chart.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chart;
        }
        public Bank chartofaccountsummary()
        {
            Bank chart = new Bank();
            try
            {
                var ledgerincome = new List<ledgerincomelist>();
                cmd = new MySqlCommand("sp_sel_ledgerincomelist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    ledgerincome.Add(new ledgerincomelist
                    {
                        account_name = rd["account_name"].ToString(),
                        accountgroup_name = rd["accountgroup_name"].ToString(),
                        account_gid = rd["account_gid"].ToString(),
                        accountgroup_gid = rd["accountgroup_gid"].ToString(),
                    });
                }
                chart.ledgerincomelist = ledgerincome;
                chart.status = true;
                rd.Close();
                var ledgerexpense = new List<ledgerexpenselist>();
                cmd1 = new MySqlCommand("sp_sel_ledgerexpenselist");
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd1);
                while (rd1.Read())
                {
                    ledgerexpense.Add(new ledgerexpenselist
                    {
                        account_name = rd1["account_name"].ToString(),
                        accountgroup_name = rd1["accountgroup_name"].ToString(),
                        account_gid = rd1["account_gid"].ToString(),
                        accountgroup_gid = rd1["accountgroup_gid"].ToString(),
                    });
                }
                chart.ledgerexpenselist = ledgerexpense;
                chart.status = true;
                rd1.Close();
                var accoundasset = new List<accountassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    accoundasset.Add(new accountassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                    });
                }
                chart.accountassetlist = accoundasset;
                chart.status = true;
                rd2.Close();
                var accountliability = new List<accountliabilitylist>();
                cmd1 = new MySqlCommand("sp_sel_accountliabilitylist");
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd1);
                while (rd3.Read())
                {
                    accountliability.Add(new accountliabilitylist
                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                    });
                }
                chart.accountliabilitylist = accountliability;
                chart.status = true;
                rd.Close();
                rd1.Close();
                rd2.Close();
                rd3.Close();
            }
            catch (Exception ex)
            {
                chart.status = false;
                chart.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chart;
        }
        public Bank bankinsert()
        {
            Bank chart = new Bank();
            try
            {
                var country = new List<accountgrouplist>();
                cmd = new MySqlCommand("sp_sel_bankadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    country.Add(new accountgrouplist
                    {
                        account_name = rd["account_name"].ToString(),
                        account_gid = rd["account_gid"].ToString(),
                    });
                }
                chart.accountgrouplist = country;
                chart.status = true;
                rd.Close();               
            }
            catch (Exception ex)
            {
                chart.status = false;
                chart.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chart;
        }
        public Bank journaladdsummary()
        {
            Bank bal = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_journaladdsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;               
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<journallist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new journallist
                        {
                            journal_gid = Convert.ToInt16(rd["journal_gid"].ToString()),
                            transaction_type = rd["transaction_type"].ToString(),
                            date = rd["date"].ToString(),
                            journal_refnumber = rd["journal_refnumber"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                        });                       
                    }
                    bal.journallist = summary;
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
        public Bank journaladddtlsummary(int values)
        {
            Bank bal = new Bank();
            try
            {
                cmd = new MySqlCommand("sp_sel_journaladddtlsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", values);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<journaldtllist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new journaldtllist
                        {
                            journal_gid = int.Parse(rd["journal_gid"].ToString()),
                            voucher_type = rd["voucher_type"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            credit_amount = double.Parse(rd["credit_amount"].ToString()),
                            debit_amount = double.Parse (rd["debit_amount"].ToString()),
                            journaldtl_gid = int.Parse(rd["journaldtl_gid"].ToString())
                        });
                    }
                    bal.journaldtllist = summary;
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
        public Bank chartofaccountchildsummary(string val)
        {
            Bank chart = new Bank();
            try
            {
                var ledgerincome = new List<ledgerincomelist>();
                cmd = new MySqlCommand("sp_sel_ledgerincomechildlist");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    ledgerincome.Add(new ledgerincomelist
                    {
                        account_name = rd["account_name"].ToString(),
                        accountgroup_name = rd["accountgroup_name"].ToString(),
                        account_gid = rd["account_gid"].ToString(),
                        accountgroup_gid = rd["accountgroup_gid"].ToString(),
                    });
                }
                chart.ledgerincomelist = ledgerincome;
                chart.status = true;
                rd.Close();
                var ledgerexpense = new List<ledgerexpenselist>();
                cmd = new MySqlCommand("sp_sel_ledgerexpensechildlist");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                while (rd1.Read())
                {
                    ledgerexpense.Add(new ledgerexpenselist
                    {
                        account_name = rd1["account_name"].ToString(),
                        accountgroup_name = rd1["accountgroup_name"].ToString(),
                        account_gid = rd1["account_gid"].ToString(),
                        accountgroup_gid = rd1["accountgroup_gid"].ToString(),
                    });
                }
                chart.ledgerexpenselist = ledgerexpense;
                chart.status = true;
                rd1.Close();
                var accoundasset = new List<accountassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetchildlistca");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    accoundasset.Add(new accountassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                    });
                }
                chart.accountassetlist = accoundasset;
                chart.status = true;
                rd2.Close();
                var accountliability = new List<accountliabilitylist>();
                cmd = new MySqlCommand("sp_sel_accountliabilitychildlistca");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                while (rd3.Read())
                {
                    accountliability.Add(new accountliabilitylist
                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                    });
                }
                chart.accountliabilitylist = accountliability;
                chart.status = true;
                rd3.Close();
                rd.Close();
                rd1.Close();
                rd2.Close();
            }
            catch (Exception ex)
            {
                chart.status = false;
                chart.message = "Internal Error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chart;
        }
        public Bankmodel journaldelete(int  values)
        {
            Bankmodel delete = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_journaldetail");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult ==1)
                {                  
                    cmd = new MySqlCommand("sp_del_journal");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_journal_gid", values);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        delete.status = true;
                        delete.message = "Deleted Successfully";                          
                    }                  
                    else
                    {
                        delete.status = false;
                        delete.message = "Error Occured While Deleting Sales Team";
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
        public Bankmodel chartsofaccountdelete(string values)
        {
            Bankmodel delete = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_chartsofaccountdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_gid", values);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    if(rd["has_child"].ToString()=="N")
                    {
                    cmd = new MySqlCommand("sp_sel_chartsofaccountaccountdelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_account_gid", values);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == false)
                        {
                            rd1.Read();
                            cmd = new MySqlCommand("sp_del_chartsofaccount");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_account_gid", values);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if(mnresult ==1)
                            {
                                delete.status = true;
                                delete.message = "Account Successfully Deleted";
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
                            delete.message = "Account has Sub Account or Account has Ledger,Account Can't Be Deleted";
                        }
                        rd1.Close();
                    }
                    else
                    {
                    cmd = new MySqlCommand("sp_sel_chartsofaccountaccountgroupdelete");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_account_gid", values);
                    MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                     if (rd1.HasRows == false)
                        {
                            rd1.Read();
                            cmd = new MySqlCommand("sp_del_chartsofaccount");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_account_gid", values);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                delete.status = true;
                                delete.message = "Account Successfully Deleted";
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
                            delete.message = "Account has Sub Account or Account has Ledger,Account Can't Be Deleted";
                        }
                        rd1.Close();
                    }
                }
                else
                {
                    delete.status = false;
                    delete.message = "Account has Sub Account or Account has Ledger,Account Can't Be Deleted";
                }
                rd.Close();
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
        public journaldetails journalentryedit(int val)
        {
            journaldetails details = new journaldetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_journaledit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if(rd.HasRows==true)
                {
                    rd.Read();
                    details.journal_gid = Convert.ToInt16(rd["journal_gid"].ToString());
                    details.journal_refnumber = rd["journal_refnumber"].ToString();
                    details.transaction_type = rd["transaction_type"].ToString();
                    details.transaction_date = rd["transaction_date"].ToString();
                    details.remarks = rd["remarks"].ToString();
                    details.company_name = rd["company_name"].ToString();                  
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_journaldtledit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<journallist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new journallist
                        {
                            journal_gid = Convert.ToInt16(rd["journal_gid"].ToString()),
                            journaldtl_gid = rd["journaldtl_gid"].ToString(),
                            account_gid = rd["account_gid"].ToString(),
                            accountgroup_gid = rd["accountgroup_gid"].ToString(),
                            account_name = rd["account_name"].ToString(),
                            accountgroup_name = rd["accountgroup_name"].ToString(),
                            transaction_amount = Double.Parse( rd["transaction_amount"].ToString()),
                            journal_type = rd["journal_type"].ToString(),
                            remarks = rd["remarks"].ToString(),
                        });
                    }
                }
                details.journallist = summary;
                details.status = true;
                rd.Close();
            }
            catch (Exception ex)
            {
                details.status = false ;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return details;
        }
        public Bankmodel journalentryeditdel(string val)
        {
            Bankmodel del = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_journaleditdel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journaldtl_gid", val);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if(mnresult ==1)
                {
                    del.status = true;
                }
                else
                {
                    del.status = false;
                }
            }
            catch (Exception ex)
            {
                del.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return del;
        }
        public Bankmodel journalentryeditadd(journaldetails val)
        {
            Bankmodel add = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_journaldtleditadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                rd = DBAccess.ExecuteReader(cmd);                
                rd.Read();           
                if (rd.HasRows ==true) 
                {
                    cmd = new MySqlCommand("sp_upt_journaldtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_jpournaldtl_gid", rd["journaldtl_gid"].ToString());
                    cmd.Parameters.AddWithValue("p_transaction_amount", val.transaction_amount);
                    cmd.Parameters.AddWithValue("p_journal_type", val.journal_type);
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if(mnresult==1)
                    {
                        add.status = true;
                    }
                    else
                    {
                        add.status = false;
                    }
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_journaledit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                    cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                    cmd.Parameters.AddWithValue("p_transaction_amount", val.transaction_amount);
                    cmd.Parameters.AddWithValue("p_journal_type", val.journal_type);
                    cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if(mnresult ==1)
                    {
                        add.status = true;
                    }
                    else
                    {
                        add.status = false;
                    }
                }
                rd.Close();                                  
            }
            catch(Exception ex)
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

        public chartofaccountdetails chartofaccountdetails(chartofaccountdetails val)
        {
            chartofaccountdetails chartledgers = new chartofaccountdetails();

            try
            {
                var ledgers = new List<accountgrouplist>();
                cmd = new MySqlCommand("sp_sel_chartpfaccountledger");
                cmd.Parameters.AddWithValue("p_accountgroup_gid", val.accountgroup_gid);
                cmd.Parameters.AddWithValue("p_ledger_type", val.ledger_type);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    ledgers.Add(new accountgrouplist
                    {
                        account_name = rd["account_name"].ToString(),
                        accountgroup_name = rd["accountgroup_name"].ToString()
                    });
                }
                chartledgers.accountgrouplist = ledgers;
                chartledgers.status = true;
                rd.Close();
            }
            catch (Exception ex)
            {
                chartledgers.status = false;
                chartledgers.message = "Internal Error occured";
                error = ex.ToString();
            }
           finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return chartledgers;
        }

        public Bankmodel chartofaccountdelete(string val)
        {
            Bankmodel delete = new Bankmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_chartofaccountdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows==false)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_del_chartofaccount");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_account_gid", val);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if  (mnresult==1)
                    {
                        delete.status = true;
                     }
                    else
                    {
                       
                        delete.status = false;
                        delete.message = " Error Occured While Deleting Chart of Account";

                    }
                    
                }
               
                else
                {
                    delete.status = false;
                    delete.message = "Cannot Delete This Account, Because the account has transactions";
                }
                rd.Close();
            }
            catch(Exception ex)
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