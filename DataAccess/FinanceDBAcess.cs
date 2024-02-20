using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;

namespace DataAccess
{
       
    public class FinanceDBAcess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd,rd1,rd2,rd3;
        string lsaccoungroup_gid;
        string LSLedgerType;
        string LSDisplayType;
        string lsreferenceType;
        string journalfrom;
        string journaltype;
        string vendor_journaltype;
        string journaldtltype;
        string lstransaction_code;
        string lstransaction_type;
        string lsaccount_gid;
        string lsreference_type;
        string advanceaccount_gid;
        string error;
       

        public Financemodel financemasteradd(Financedetail val, string userGid)
        {
            
            
            try
            {    
                if (val.reference_type=="Customer")
                {
                    lsreferenceType = "Sundry Debtors";
                    lsaccoungroup_gid = "FCOA000022";
                    LSLedgerType = "Y";
                    LSDisplayType = "A";
                }
                else if(val.reference_type == "Vendor")
                {
                    lsreferenceType = "Sundry Creditors";
                    lsaccoungroup_gid = "FCOA000021";
                    LSLedgerType = "Y";
                    LSDisplayType = "L";

                }
                else
                {
                    lsreferenceType = "";
                    lsaccoungroup_gid = "";
                    LSLedgerType = "";
                    LSDisplayType = "";
                }

                cmd = new MySqlCommand("sp_ins_financemasteradd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_gid", "");
                cmd.Parameters.AddWithValue("p_account_name", val.account_name);
                cmd.Parameters.AddWithValue("p_account_code", val.account_code);
                cmd.Parameters.AddWithValue("p_gl_code", val.account_code);
                cmd.Parameters.AddWithValue("p_ledger_type", LSLedgerType);
                cmd.Parameters.AddWithValue("p_display_type", LSDisplayType);
                cmd.Parameters.AddWithValue("p_created_By", userGid);
                cmd.Parameters.AddWithValue("p_accountgroup_gid", lsaccoungroup_gid);
                cmd.Parameters.AddWithValue("p_accountgroup_name", lsreferenceType);
                cmd.Parameters.AddWithValue("p_has_child","N");
                cmd.Parameters.AddWithValue("p_ledger", "BS");
                cmd.Parameters.AddWithValue("p_gid", val.gid);
                cmd.Parameters.AddWithValue("p_reference_type", val.reference_type);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "Records added sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch(Exception ex)
            {
                val.status = false;
                val.message = "Error Occured Finance Common Function!";
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

        public Financemodel financemasterupdate(Financedetail val, string userGid)
        {
            try
            {   
                cmd = new MySqlCommand("sp_upt_chartofaccounts");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_account_name", val.account_name);
                cmd.Parameters.AddWithValue("p_accountcode", val.account_code);
                cmd.Parameters.AddWithValue("p_gl_code", val.account_code);
                cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                cmd.Parameters.AddWithValue("p_created_By", userGid);
                cmd.Parameters.AddWithValue("p_id",val.gid);
                cmd.Parameters.AddWithValue("p_reference_type",val.reference_type);

                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "Records Updated sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "No Record Function On Edit!";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured Finance Common Function On Edit!";
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

        public Financemodel financeinvoice(Financedetail val, string userGid)
        {
            string strNationalID; 
            try
            {
                if (val.reference_type == "Customer")
                {
                    lsreferenceType = "Sundry Debtors";
                    lsaccoungroup_gid = "FCOA000022";
                    LSLedgerType = "Y";
                    LSDisplayType = "A";
                    journalfrom = "sales";
                    journaltype = "dr";
                    journaldtltype = "cr";

                    string customer_gid = string.Empty;
                    string[] arrCustomerData = val.account_name.Split('|');
                    int sizeCustomerData = arrCustomerData.Length;
                    string strCustomerName = arrCustomerData[0].Trim();
                    val.account_name = strCustomerName; // For reuse purpose
                    if (sizeCustomerData == 2)
                    {
                        strNationalID = arrCustomerData[1].Trim();
                    }
                    else
                        strNationalID = "";
                    cmd = new MySqlCommand("sp_sel_customer_gid");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                    cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        customer_gid = rd["customer_gid"].ToString();
                    }
                    rd.Close();
                    val.account_gid = customer_gid;
                }
                else if (val.reference_type == "Vendor")
                {
                    lsreferenceType = "Sundry Creditors";
                    lsaccoungroup_gid = "FCOA000021";
                    LSLedgerType = "Y";
                    LSDisplayType = "L";
                    journalfrom = "purchase";
                    journaltype = "cr";
                    journaldtltype = "dr";
                }
                else
                {
                    lsreferenceType = "";
                    lsaccoungroup_gid = "";
                    LSLedgerType = "";
                    LSDisplayType = "";
                }

                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if(rd.HasRows==true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.journal_date);
                    rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        rd1.Read();
                      
                        cmd = new MySqlCommand("sp_ins_financejournalentry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", "0");
                        cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                        cmd.Parameters.AddWithValue("p_transaction_date", val.journal_date);
                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                        cmd.Parameters.AddWithValue("p_transaction_type", "Journal");
                        cmd.Parameters.AddWithValue("p_reference_type", val.account_name);
                        cmd.Parameters.AddWithValue("p_reference_gid", val.account_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        cmd.Parameters.AddWithValue("p_transaction_code", val.journal_refnumber);                        
                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);                        
                        cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult==1)
                        {
                            cmd = new MySqlCommand("sp_ins_invoicejournalentrydtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_journal_gid", val.journal_gid);
                            cmd.Parameters.AddWithValue("p_journal_type",journaltype);                            
                            cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                            cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                            cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);                            
                            cmd.Parameters.AddWithValue("p_created_by", userGid);
                            cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult==1)
                            {
                                                              
                                cmd = new MySqlCommand("sp_sel_accountmapping");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                                cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                                rd2 = DBAccess.ExecuteReader(cmd);
                                if (rd2.HasRows == true)
                                {
                                    while (rd2.Read())
                                    {
                                       
                                        cmd = new MySqlCommand("sp_ins_invoicejournalentrydtl");
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_journal_gid",val.journal_gid);
                                        cmd.Parameters.AddWithValue("p_journal_from", "");
                                        if (rd2["field_name"].ToString() == "COGS")
                                        {
                                            cmd.Parameters.AddWithValue("p_transaction_amount", val.cogs_amount);
                                            cmd.Parameters.AddWithValue("p_journal_type", journaldtltype);
                                        }
                                        else if (rd2["field_name"].ToString() == "Addon Amount")
                                        {
                                            cmd.Parameters.AddWithValue("p_transaction_amount", val.addon_amount);
                                            cmd.Parameters.AddWithValue("p_journal_type", journaldtltype);
                                        }
                                        else if(rd2["field_name"].ToString()== "Additional Discount")
                                        {
                                            cmd.Parameters.AddWithValue("p_transaction_amount", val.discount_amount);
                                            cmd.Parameters.AddWithValue("p_journal_type", journaltype);

                                        }
                                        cmd.Parameters.AddWithValue("p_account_gid", rd2["account_gid"].ToString());
                                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                        cmd.Parameters.AddWithValue("p_created_By", userGid);
                                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                                   }
                                    
                                }
                                
                                if (mnresult==1)
                                {
                                    val.status = true;
                                    val.message = "Record Inserted sucessfully";
                                }
                                else
                                {
                                    val.status = false;
                                    val.message = "Error Occured While Adding Finance Journal Entry Details";
                                }

                            }
                            rd2.Close();
                        }
                        else
                        {
                            val.status = false;
                            val.message = "Error Occured While Adding Finance Journal Entry.";
                            
                        }
                    }
                    rd1.Close();
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Adding Finance Sales Order.";
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

        public Financemodel financepayment(Financedetail val, string userGid)
        {
            try
            {

                if (val.reference_type == "Customer")
                {
                    journalfrom = "receipt";
                    journaltype = "cr";
                }
                else if (val.reference_type == "Vendor")
                {
                    journalfrom = "payment";
                    journaltype = "dr";
                }
                else
                {
                    lsreferenceType = "";
                    lsaccoungroup_gid = "";
                }

                if (val.payment_mode== "Cash")
                {
                    lstransaction_code = "CC001";
                    lstransaction_type = "Cash Book";
                    lsaccount_gid = "FCOA1404070080";
                    lsreference_type = "CASH ON HAND";
                }
                else if(val.payment_mode == "Bank")
                {
                    lstransaction_type = "Bank Book";
                    cmd = new MySqlCommand("sp_sel_bankdetails");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows==true)
                    {
                        rd.Read();
                        lsreference_type = rd["bank_name"].ToString();
                        lstransaction_code = rd["bank_code"].ToString();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                   else if(val.payment_mode=="Advance")
                {
                    lstransaction_type = "Advance Receipt";
                    journalfrom = "advance";
                    lstransaction_code = "SADV001";
                    lsreference_type = "Loan and Advance Receipt";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", "Advance");
                    cmd.Parameters.AddWithValue("p_module_name", "sales");
                    rd = DBAccess.ExecuteReader(cmd);
                    if(rd.HasRows==true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }


                else if (val.payment_mode == "Credit Note")
                {
                    lstransaction_type = "Credit Note";
                    journalfrom = "creditnote";
                    lstransaction_code = "CRN001";
                    lsreference_type = "Sales Discounts";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                    cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                else if (val.payment_mode == "Debit Note")
                {
                    lstransaction_type = "Debit Note";
                    journalfrom = "debitnote";
                    lstransaction_code = "DBN001";
                    lsreference_type = "Other Income";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                    cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.journal_date);
                    rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        rd1.Read();
                        cmd = new MySqlCommand("sp_ins_paymentjournalentry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", "0");
                        cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                        cmd.Parameters.AddWithValue("p_transaction_date", val.journal_date);
                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                        cmd.Parameters.AddWithValue("p_transaction_type", lstransaction_type);
                        cmd.Parameters.AddWithValue("p_reference_type", lsreference_type);
                        cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        cmd.Parameters.AddWithValue("p_transaction_code", lstransaction_code);
                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                        cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                        cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                                cmd = new MySqlCommand("sp_ins_paymentjournalentrydtl");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_journal_gid", "");
                                cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                                cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                                cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                                cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                cmd.Parameters.AddWithValue("p_created_by", userGid);
                                cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);                          
                                if (mnresult == 1)
                                {
                                    val.status = true;
                                    val.message = "Record Inserted sucessfully";
                                }
                                else
                                {
                                    val.status = false;
                                    val.message = "Error Occured While Adding Finance Journal Entry Details";
                                }

                            }
                        }
                        else
                        {
                            val.status = false;
                            val.message = "Error Occured While Adding Finance Journal Entry.";
                        }
                    }
                    rd1.Close();
                    rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                error = ex.ToString();
            }
            return val;
        }

        public Financemodel financeadvance(Financedetail val,string userGid)
        {
          
            try
            {
                if (val.screen_name == "Advance")
                 {
                    journalfrom = "Sales Advance";
                    journaltype = "cr";
                }
                else if (val.screen_name == "Vendor Advance")
                {
                    journalfrom = "Vendor Advance";
                    journaltype = "dr";
                    val.journal_refnumber = "VADV001";
                }
                    if (val.payment_mode == "Cash")
                {
                    lstransaction_code = "CC001";
                    lstransaction_type = "Cash Book";
                    lsaccount_gid = "FCOA1404070080";
                    lsreference_type = "CASH ON HAND";
                }
                else if (val.payment_mode == "Bank")
                {
                    lstransaction_type = "Bank Book";
                    cmd = new MySqlCommand("sp_sel_bankdetails");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsreference_type = rd["bank_name"].ToString();
                        lstransaction_code = rd["bank_code"].ToString();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }


                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.journal_date);
                    rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        rd1.Read();

                        cmd = new MySqlCommand("sp_ins_paymentjournalentry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", "0");
                        cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                        cmd.Parameters.AddWithValue("p_transaction_date", val.journal_date);
                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                        cmd.Parameters.AddWithValue("p_transaction_type", lstransaction_type);
                        cmd.Parameters.AddWithValue("p_reference_type", lsreference_type);
                        cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        cmd.Parameters.AddWithValue("p_transaction_code", lstransaction_code);
                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                        cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                        cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                                             
                               cmd = new MySqlCommand("sp_sel_accountmapping");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                                cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                                rd2 = DBAccess.ExecuteReader(cmd);
                                if (rd2.HasRows == true)
                                {
                                    while (rd2.Read())
                                    {

                                        advanceaccount_gid = rd2["account_gid"].ToString();
                                        cmd = new MySqlCommand("sp_ins_paymentjournalentrydtl");
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_journal_gid", "");
                                        cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                        cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                                        cmd.Parameters.AddWithValue("p_account_gid", rd2["account_gid"].ToString());
                                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                                        cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                                       mnresult = DBAccess.ExecuteNonQuery(cmd);
                                    }

                                }
                               if (mnresult == 1)
                                {
                                    val.status = true;
                                    val.message = "Record Inserted sucessfully";
                                }
                                else
                                {
                                    val.status = false;
                                    val.message = "Error Occured While Adding Finance Journal Entry Details";
                                }

                            
                            rd2.Close();
                        }
                        else
                        {
                            val.status = false;
                            val.message = "Error Occured While Adding Finance Journal Entry.";
                        }
                    }
                    rd1.Close();
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                val.status = false;
                error = ex.ToString();
            }
            
            return val;
        }

        public Financemodel invoicedelete(string val)
        {
            Financemodel delete = new Financemodel();
            try
            {
                cmd = new MySqlCommand("sp_del_invoicedelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_gid", val);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    delete.status = true;
               }
                else
                {
                    delete.status = false;
                    delete.message = " Error Occured While Deleting Journals";

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

        public Financemodel financerefund(Financedetail val,string userGid)
        {
            string strNationalID;
            try
            {
                if (val.reference_type == "Refund Receipt")
                {
                    journalfrom = "Receipt Refund";
                    journaltype = "cr";
                    vendor_journaltype = "dr";
               }
                if (val.reference_type == "Refund Advance")
                {
                    journalfrom = "Advance Refund";
                    journaltype = "cr";
                }
                string customer_gid = string.Empty;
                string[] arrCustomerData = val.account_name.Split('|');
                int sizeCustomerData = arrCustomerData.Length;
                string strCustomerName = arrCustomerData[0].Trim();
                val.account_name = strCustomerName;
                if (sizeCustomerData == 2)
                {
                    strNationalID = arrCustomerData[1].Trim();
                }
                else
                    strNationalID = "";

                cmd = new MySqlCommand("sp_sel_customer_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", val.account_name);
                cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    customer_gid = rd["customer_gid"].ToString();
                }
                rd.Close();
                val.account_gid = customer_gid;
                if (val.payment_mode == "Cash")
                {
                    lstransaction_code = "CC001";
                    lstransaction_type = "Cash Book";
                    lsaccount_gid = "FCOA1404070080";
                    lsreference_type = "CASH ON HAND";
                }
                else if (val.payment_mode == "Bank")
                {
                    lstransaction_type = "Bank Book";
                    cmd = new MySqlCommand("sp_sel_bankdetails");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsreference_type = rd["bank_name"].ToString();
                        lstransaction_code = rd["bank_code"].ToString();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.journal_date);
                    rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        rd1.Read();
                        cmd = new MySqlCommand("sp_ins_paymentjournalentry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", "0");
                        cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                        cmd.Parameters.AddWithValue("p_transaction_date", val.journal_date);
                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                        cmd.Parameters.AddWithValue("p_transaction_type", lstransaction_type);
                        cmd.Parameters.AddWithValue("p_reference_type", lsreference_type);
                        cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        cmd.Parameters.AddWithValue("p_transaction_code", lstransaction_code);
                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                        cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"]);
                        cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                        cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            cmd = new MySqlCommand("sp_ins_refundjournalentry");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_journal_gid", "");
                            cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                            cmd.Parameters.AddWithValue("p_transaction_amount", val.cogs_amount);
                            cmd.Parameters.AddWithValue("p_account_gid", lsaccount_gid);
                            cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                            cmd.Parameters.AddWithValue("p_created_by", userGid);
                            cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                            cmd.Parameters.AddWithValue("p_transaction_gid", val.account_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                                                                 
                            if (mnresult == 1)
                            {

                                cmd = new MySqlCommand("sp_sel_accountmapping");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                                cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                                rd3 = DBAccess.ExecuteReader(cmd);
                                if (rd3.HasRows == true)
                                {
                                    while (rd3.Read())
                                    {
                                        if (val.reference_type == "Refund Receipt")
                                        {
                                            cmd = new MySqlCommand("sp_ins_refundjournalentry");
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("p_journal_gid", "");
                                            if (rd3["field_name"].ToString() == "Refund Cancellation")
                                            {

                                                cmd.Parameters.AddWithValue("p_transaction_amount", val.addon_amount);
                                                cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                                            }
                                            else if (rd3["field_name"].ToString() == "Refund Receipt")
                                            {

                                                journaldtltype = "dr";
                                                cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                                                cmd.Parameters.AddWithValue("p_journal_type", journaldtltype);
                                            }
                                            cmd.Parameters.AddWithValue("p_account_gid", rd3["account_gid"].ToString());
                                            cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                            cmd.Parameters.AddWithValue("p_created_by", userGid);
                                            cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                            cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        }
                                        if (val.reference_type == "Refund Advance")
                                        {
                                            cmd = new MySqlCommand("sp_ins_refundjournalentry");
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("p_journal_gid", "");
                                            if (rd3["field_name"].ToString() == "Refund Advance")
                                            {

                                                journaldtltype = "dr";
                                                cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                                                cmd.Parameters.AddWithValue("p_journal_type", journaldtltype);
                                            }
                                            else if (rd3["field_name"].ToString() == "Refund Advance Cancellation")
                                            {

                                                cmd.Parameters.AddWithValue("p_transaction_amount", val.addon_amount);
                                                cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                                            }
                                            cmd.Parameters.AddWithValue("p_account_gid", rd3["account_gid"].ToString());
                                            cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                            cmd.Parameters.AddWithValue("p_created_by", userGid);
                                            cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                            cmd.Parameters.AddWithValue("p_transaction_gid", val.account_gid);
                                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        }


                                    }

                                }
                                string vendor_gid = string.Empty;
                                cmd = new MySqlCommand("sp_sel_vendoraccount_gid");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_vendor_gid", val.account_code);
                                rd = DBAccess.ExecuteReader(cmd);
                                if (rd.Read())
                                {
                                    vendor_gid = rd["account_gid"].ToString();
                                }
                                rd.Close();
                                val.account_gid = vendor_gid;
                                cmd = new MySqlCommand("sp_ins_refundjournalentry");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_journal_gid", "");
                                cmd.Parameters.AddWithValue("p_journal_type", vendor_journaltype);
                                cmd.Parameters.AddWithValue("p_transaction_amount", val.basic_amount);
                                cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                                cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                                cmd.Parameters.AddWithValue("p_created_by", userGid);
                                cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);

                                if (mnresult == 1)
                                {
                                    val.status = true;
                                    val.message = "Record Inserted sucessfully";
                                }
                                else
                                {
                                    val.status = false;
                                    val.message = "Error Occured While Adding Finance Journal Entry Details";
                                }
                                rd3.Close();

                            }
                        }
                        else
                        {
                            val.status = false;
                            val.message = "Error Occured While Adding Finance Journal Entry.";
                        }
                    }
                    rd1.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                error = ex.ToString();
            }
            return val;
        }

        public Financemodel financenewrefund(Financerefunddetail val, string userGid)
        {
            try
            {

                if (val.reference_type == "Customer")
                {
                    journalfrom = "refund";
                    journaltype = "cr";
                }
                else if (val.reference_type == "Vendor")
                {
                    journalfrom = "refund";
                    journaltype = "dr";
                }
                else
                {
                    lsreferenceType = "";
                    lsaccoungroup_gid = "";
                }

                if (val.payment_mode == "Cash")
                {
                    lstransaction_code = "CC001";
                    lstransaction_type = "Cash Book";
                    lsaccount_gid = "FCOA1404070080";
                    lsreference_type = "CASH ON HAND";
                }
                else if (val.payment_mode == "Bank")
                {
                    lstransaction_type = "Bank Book";
                    cmd = new MySqlCommand("sp_sel_bankdetails");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsreference_type = rd["bank_name"].ToString();
                        lstransaction_code = rd["bank_code"].ToString();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                else if (val.payment_mode == "Advance")
                {
                    lstransaction_type = "Advance Receipt";
                    journalfrom = "advance";
                    lstransaction_code = "SADV001";
                    lsreference_type = "Loan and Advance Receipt";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", "Advance");
                    cmd.Parameters.AddWithValue("p_module_name", "sales");
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }


                else if (val.payment_mode == "Credit Note")
                {
                    lstransaction_type = "Credit Note";
                    journalfrom = "creditnote";
                    lstransaction_code = "CRN001";
                    lsreference_type = "Sales Discounts";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                    cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                else if (val.payment_mode == "Debit Note")
                {
                    lstransaction_type = "Debit Note";
                    journalfrom = "debitnote";
                    lstransaction_code = "DBN001";
                    lsreference_type = "Other Income";
                    cmd = new MySqlCommand("sp_sel_accountmapping");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                    cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        lsaccount_gid = rd["account_gid"].ToString();
                    }
                    rd.Close();
                }
                cmd = new MySqlCommand("sp_sel_financeyear");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    cmd = new MySqlCommand("sp_sel_jmonthday");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_fin_yearstart", rd["fin_yearstart"].ToString());
                    cmd.Parameters.AddWithValue("p_date", val.journal_date);
                    rd1 = DBAccess.ExecuteReader(cmd);
                    if (rd1.HasRows == true)
                    {
                        rd1.Read();
                        cmd = new MySqlCommand("sp_ins_paymentjournalentry");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_journal_gid", "0");
                        cmd.Parameters.AddWithValue("p_journal_refnumber", val.journal_refnumber);
                        cmd.Parameters.AddWithValue("p_transaction_date", val.journal_date);
                        cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                        cmd.Parameters.AddWithValue("p_transaction_type", lstransaction_type);
                        cmd.Parameters.AddWithValue("p_reference_type", lsreference_type);
                        cmd.Parameters.AddWithValue("p_reference_gid", val.payment_gid);
                        cmd.Parameters.AddWithValue("p_transaction_gid", val.transaction_gid);
                        cmd.Parameters.AddWithValue("p_transaction_code", lstransaction_code);
                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                        cmd.Parameters.AddWithValue("p_journal_year", rd1["finyear"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_month", rd1["month"].ToString());
                        cmd.Parameters.AddWithValue("p_journal_day", rd1["day"].ToString());
                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                        cmd.Parameters.AddWithValue("p_yearendactivity_flag", "Y");
                        cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            cmd = new MySqlCommand("sp_ins_paymentjournalentrydtl");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_journal_gid", "");
                            cmd.Parameters.AddWithValue("p_journal_type", journaltype);
                            cmd.Parameters.AddWithValue("p_transaction_amount", val.invoice_amount);
                            cmd.Parameters.AddWithValue("p_account_gid", val.account_gid);
                            cmd.Parameters.AddWithValue("p_remarks", val.fin_remarks);
                            cmd.Parameters.AddWithValue("p_created_by", userGid);
                            cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                            cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                val.screen_name = "Debit Note";
                                val.module_name = "purchase";
                                lstransaction_type = "Debit Note";
                                journalfrom = "debitnote";
                                lstransaction_code = "DBN001";
                                lsreference_type = "Other Income";
                                cmd = new MySqlCommand("sp_sel_accountmapping");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_screen_name", val.screen_name);
                                cmd.Parameters.AddWithValue("p_module_name", val.module_name);
                                rd = DBAccess.ExecuteReader(cmd);
                                if (rd.HasRows == true)
                                {
                                    rd.Read();
                                    lsaccount_gid = rd["account_gid"].ToString();
                                }
                                rd.Close();
                                foreach (var item in val.refundServiceTypeList)
                                {
                                    if (item.vendorrefund_amount == null || item.vendorrefund_amount == "0")
                                    {

                                    }
                                    else
                                    {
                                        cmd = new MySqlCommand("sp_ins_paymentjournalentrydtl");
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_journal_gid", "");
                                        cmd.Parameters.AddWithValue("p_journal_type", "dr");
                                        cmd.Parameters.AddWithValue("p_transaction_amount", item.vendorrefund_amount);
                                        cmd.Parameters.AddWithValue("p_account_gid", item.vendor_gid);
                                        cmd.Parameters.AddWithValue("p_remarks", "");
                                        cmd.Parameters.AddWithValue("p_created_by", userGid);
                                        cmd.Parameters.AddWithValue("p_journal_from", journalfrom);
                                        cmd.Parameters.AddWithValue("p_transaction_gid", lsaccount_gid);
                                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                                    }                                   
                                }
                                val.status = true;
                                val.message = "Record Inserted sucessfully";
                            }
                            else
                            {
                                val.status = false;
                                val.message = "Error Occured While Adding Finance Journal Entry Details";
                            }

                        }
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While Adding Finance Journal Entry.";
                    }
                }
                rd1.Close();
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                error = ex.ToString();
            }
            return val;
        }

    }
}