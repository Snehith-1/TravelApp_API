using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class openingbalanceDBAccess
    {
        //int mnresult = 0;
        MySqlCommand cmd, cmd1 = null;
        MySqlDataReader rd;
        string error;      
        public openingbalance openingbalance()
        {
            openingbalance bal = new openingbalance();
            try
            {
                var balance = new List<parentliabilitylist>();
                cmd = new MySqlCommand("sp_sel_opparentliabilitylist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    balance.Add(new parentliabilitylist
                    {
                        account_name = rd["account_name"].ToString(),
                        account_gid = rd["account_gid"].ToString()
                    });
                }
                bal.parentliabilitylist = balance;
                bal.status = true;
                rd.Close();
                var asset = new List<parentassetlist>();
                cmd = new MySqlCommand("sp_sel_opparentassetlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                while (rd1.Read())
                {
                    asset.Add(new parentassetlist
                    {
                        account_name = rd1["account_name"].ToString(),
                        account_gid = rd1["account_gid"].ToString()
                    });
                }
                bal.parentassetlist = asset;
                bal.status = true;
                rd1.Close();
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
        public openingbalancedetail opgetaccountname(string val)
        {
            openingbalancedetail bal = new openingbalancedetail();
            try
            {
                var balance = new List<accountnanmeliabilitylist>();
                cmd = new MySqlCommand("sp_sel_account_nameliabilitylist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_accountgroup_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                while (rd.Read())
                {
                    balance.Add(new accountnanmeliabilitylist
                    {
                        account_name = rd["account_name"].ToString(),
                        account_gid = rd["account_gid"].ToString()
                    });
                }
                bal.accountnanmeliabilitylist = balance;
                bal.status = true;
                rd.Close();
                var asset = new List<accountnanmeassetlist>();
                cmd = new MySqlCommand("sp_sel_account_nameassetlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_accountgroup_gid", val);
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                while (rd1.Read())
                {
                    asset.Add(new accountnanmeassetlist
                    {
                        account_name = rd1["account_name"].ToString(),
                        account_gid = rd1["account_gid"].ToString()
                    });
                }
                bal.accountnanmeassetlist = asset;
                bal.status = true;
                rd1.Close();
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
        public openingbalance openingbalanceassetsummary()
        {
            openingbalance asset = new openingbalance();
            try
            {
                var assetlist = new List<parentassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    assetlist.Add(new parentassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentassetlist = assetlist;
                asset.status = true;
                rd2.Close();
                var liability = new List<parentliabilitylist>();
                cmd1 = new MySqlCommand("sp_sel_accountliabilitylist");
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd1);
                while (rd3.Read())
                {
                    liability.Add(new parentliabilitylist
                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentliabilitylist = liability;
                asset.status = true;
                rd3.Close();
            }
            catch (Exception ex)
            {
                asset.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return asset;
        }
        public openingbalance openingbalancechildsummary(string val)
        {
            openingbalance asset = new openingbalance();
            try
            {
                var assetlist = new List<parentassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetchildlist");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    assetlist.Add(new parentassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentassetlist = assetlist;
                asset.status = true;
                rd2.Close();
                var liability = new List<parentliabilitylist>();
                cmd = new MySqlCommand("sp_sel_accountliabilitychildlist");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                while (rd3.Read())
                {
                    liability.Add(new parentliabilitylist
                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentliabilitylist = liability;
                asset.status = true;
                rd3.Close();
            }
            catch (Exception ex)
            {
                asset.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return asset;
        }
        public openingbalance openingbalancechild1summary(string val)
        {
            openingbalance asset = new openingbalance();
            try
            {
                var assetlist = new List<parentassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetchild1list");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    assetlist.Add(new parentassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentassetlist = assetlist;
                asset.status = true;
                rd2.Close();
                var liability = new List<parentliabilitylist>();
                cmd = new MySqlCommand("sp_sel_accountliabilitychild1list");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                while (rd3.Read())
                {
                    liability.Add(new parentliabilitylist
                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                    });
                }
                asset.parentliabilitylist = liability;
                asset.status = true;
                rd3.Close();
            }
            catch (Exception ex)
            {
                asset.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return asset;
        }
        public openingbalance openingbalancechild2summary(string val)
        {
            double amount = 0.0;
            openingbalance asset = new openingbalance();
            try
            {
                var assetlist = new List<parentassetlist>();
                cmd = new MySqlCommand("sp_sel_accountassetchild2list");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd2 = DBAccess.ExecuteReader(cmd);
                while (rd2.Read())
                {
                    if (rd2["transaction_amount"].ToString() != "")
                    {
                       amount =double.Parse(rd2["transaction_amount"].ToString());
                    }
                   assetlist.Add(new parentassetlist
                    {
                        account_name = rd2["account_name"].ToString(),
                        accountgroup_name = rd2["accountgroup_name"].ToString(),
                        account_gid = rd2["account_gid"].ToString(),
                        accountgroup_gid = rd2["accountgroup_gid"].ToString(),
                        transaction_date = rd2["transaction_date"].ToString(),
                        remarks = rd2["remarks"].ToString(),
                        transaction_type = rd2["journal_type"].ToString(),
                        transaction_amount= amount,
                        journal_refnumber = rd2["journal_refnumber"].ToString(),
                    });
                }
                asset.parentassetlist = assetlist;
                asset.status = true;
                rd2.Close();
                var liability = new List<parentliabilitylist>();
                cmd = new MySqlCommand("sp_sel_accountliabilitychild2list");
                cmd.Parameters.AddWithValue("p_account_gid", val);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd3 = DBAccess.ExecuteReader(cmd);
                while (rd3.Read())
                {
                    if (rd3["transaction_amount"].ToString() != "")
                    {
                        amount = Double.Parse(rd3["transaction_amount"].ToString());
                    }
                    liability.Add(new parentliabilitylist

                    {
                        account_name = rd3["account_name"].ToString(),
                        accountgroup_name = rd3["accountgroup_name"].ToString(),
                        account_gid = rd3["account_gid"].ToString(),
                        accountgroup_gid = rd3["accountgroup_gid"].ToString(),
                        transaction_date = rd3["transaction_date"].ToString(),
                        remarks = rd3["remarks"].ToString(),
                        transaction_type = rd3["journal_type"].ToString(),
                        transaction_amount = amount,
                        journal_refnumber = rd3["journal_refnumber"].ToString(),

                    });
                }
                asset.parentliabilitylist = liability;
                asset.status = true;
                rd3.Close();
            }
            catch (Exception ex)
            {
                asset.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return asset;
        }
    }
}