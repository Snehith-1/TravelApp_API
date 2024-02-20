using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;
using System.IO;


namespace DataAccess
{
    public class VendorDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Vendor GetAll()
        {
            Vendor vendor = new Vendor();
            try
             {
                cmd = new MySqlCommand("sp_sel_vendor");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendorlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendorlist
                        {

                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_code = rd["vendor_code"].ToString(),
                            vendor_company_name = rd["vendor_company_name"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_number = rd["vendor_number"].ToString(),

                            vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                            active_flag = rd["active_flag"].ToString ()

                        });
                    }
                    vendor.vendorlist = summary;
                    vendor.status = true;
                    
                }
                else
                {
                    vendor.status = false;
                    
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }
    
            public Vendor activevendorsummary()
            {
                Vendor vendor = new Vendor();
                try
                {
                    cmd = new MySqlCommand("sp_sel_activevendor");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Connection = con;
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<activeVendorlist>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            summary.Add(new activeVendorlist
                            {

                                vendor_gid = rd["vendor_gid"].ToString(),
                                vendor_code = rd["vendor_code"].ToString(),
                                vendor_company_name = rd["vendor_company_name"].ToString(),
                                vendor_name = rd["vendor_name"].ToString(),
                                vendor_number = rd["vendor_number"].ToString(),

                                vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                                active_flag = rd["active_flag"].ToString()

                            });
                        }
                        vendor.activeVendorlist = summary;
                        vendor.status = true;

                    }
                    else
                    {
                        vendor.status = false;

                    }
                    rd.Close();

                }
                catch (Exception ex)
                {
                    vendor.status = false;
                    vendor.message = "Internal Error Occured";
                    error = ex.ToString();
                }
                finally
                {
                    if (cmd.Connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
                return vendor;
            }
            public Vendormodel Add(Vendordetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorcodevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_code", val.vendor_code);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Vendor Code Already Exist!";

                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_vendor");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_vendor_code", val.vendor_code);
                    cmd.Parameters.AddWithValue("p_vendor_company_name", val.vendor_company_name);
                    cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                    cmd.Parameters.AddWithValue("p_tan_number", val.tan_number);
                    cmd.Parameters.AddWithValue("p_number", val.number);
                    cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                    cmd.Parameters.AddWithValue("p_fax", val.fax);
                    cmd.Parameters.AddWithValue("p_vendor_address_line1", val.vendor_address_line1);
                    cmd.Parameters.AddWithValue("p_vendor_address_line2", val.vendor_address_line2);
                    cmd.Parameters.AddWithValue("p_state", val.state);
                    cmd.Parameters.AddWithValue("p_city", val.city);
                    cmd.Parameters.AddWithValue("p_postal_code", val.postal_code);
                    cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                    cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                    cmd.Parameters.AddWithValue("p_tax", val.tax);
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                    cmd.Parameters.AddWithValue("p_ticket_vendor", val.ticket_vendor);
                    cmd.Parameters.AddWithValue("p_vendor_gid","");
                    cmd.Parameters.AddWithValue("p_active_flag", "Active");
                    //cmd.Connection = con;
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Records added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While Inserting Vendor";
                    }


                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Inserting Vendor";
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
        public Vendormodel vendorcode(string user_gid)
        {
            Vendormodel Val = new Vendormodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_vendorcode");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_code", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_vendorcode");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.vendor_code = rd["vendor_code"].ToString();
                    Val.status = true;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                Val.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Val;
        }
        public Vendormodel Delete(int values)
        {
            Vendormodel vendordelete = new Vendormodel();
            try
            {
                 cmd = new MySqlCommand("sp_del_vendor");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", values);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    vendordelete.status = true;
                    vendordelete.message = "Vendor Deleted Successfully";
                }
                else
                {
                    vendordelete.status = false;
                    vendordelete.message = "Error Occured While Deleting Vendor";
                }
            }
            catch (Exception ex)
            {
                vendordelete.status = false;
                vendordelete.message = "Error Occured While Deleting Vendor";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendordelete;
        }
        public Vendordetail Get(string  val)
        {
            Vendordetail Vendordetail = new Vendordetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendoredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val);
                //cmd.Connection = con;
                 rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Vendordetail.vendor_gid = rd["vendor_gid"].ToString();
                    Vendordetail.vendor_name = rd["vendor_name"].ToString();
                    Vendordetail.number = rd["vendor_number"].ToString();
                    Vendordetail.tan_number = rd["tan_number"].ToString();
                    Vendordetail.email_address = rd["email_address"].ToString();
                    Vendordetail.fax = rd["vendor_fax"].ToString();
                    Vendordetail.vendor_address_line1 = rd["vendor_address_line1"].ToString();
                    Vendordetail.vendor_address_line2 = rd["vendor_address_line2"].ToString();
                    Vendordetail.state = rd["state"].ToString();
                    Vendordetail.city = rd["city"].ToString();
                    Vendordetail.postal_code = rd["postal_code"].ToString();
                    Vendordetail.currency_code = rd["currency_code"].ToString();
                    Vendordetail.country_name = rd["country_name"].ToString();
                    Vendordetail.tax = rd["vendor_tax"].ToString();
                    Vendordetail.vendor_company_name = rd["vendor_company_name"].ToString();
                    Vendordetail.service_type = rd["service_type"].ToString();
                    Vendordetail.vendor_code = rd["vendor_code"].ToString();
                    Vendordetail.ticket_vendor = rd["ticket_vendor"].ToString();
                    Vendordetail.status = true;
                    
                }
                else
                {
                    Vendordetail.status = false;
                    Vendordetail.message = "No Records found!";
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                Vendordetail.status = false;
                Vendordetail.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Vendordetail;
        }
        public Vendormodel Update(Vendordetail val, string user_gid)
        {
            Vendormodel Vendor = new Vendormodel();
            try
            {
                 cmd = new MySqlCommand("sp_upt_vendoredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                cmd.Parameters.AddWithValue("p_vendor_number", val.number);
                cmd.Parameters.AddWithValue("p_tan_number", val.tan_number);
                cmd.Parameters.AddWithValue("p_vendor_address_line1", val.vendor_address_line1);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_vendor_fax", val.fax);
                cmd.Parameters.AddWithValue("p_vendor_address_line2", val.vendor_address_line2);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_state", val.state);
                cmd.Parameters.AddWithValue("p_postal_code", val.postal_code);
                cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                cmd.Parameters.AddWithValue("p_vendor_tax", val.tax);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                cmd.Parameters.AddWithValue("p_vendor_company_name",val.vendor_company_name);
                cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                cmd.Parameters.AddWithValue("p_ticket_vendor", val.ticket_vendor);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    Vendor.status = true;
                    Vendor.message = "Vendor updated succesfully";
                }
                else
                {
                    Vendor.status = false;
                    Vendor.message = "Error Occured While Updating Vendor!";
                }
            }
            catch (Exception ex)
            {
                Vendor.status = false;
                Vendor.message = "Error Occured While Updating Vendor!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }            
            return Vendor;
        }
        public Vendormodel Status(Vendordetail val, string user_gid)
        {
            Vendormodel Vendor = new Vendormodel();
            try
            {
                 cmd = new MySqlCommand("sp_upt_vendor_status");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_status", val.vendor_status);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    Vendor.status = true;
                    Vendor.message = "Vendor updated succesfully";
                }
                else
                {
                    Vendor.status = false;
                    Vendor.message = "Error Occured While Updating Vendor!";
                }
            }
            catch (Exception ex)
            {
                Vendor.status = false;
                Vendor.message = "Error Occured While Updating Vendor!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }            
            return Vendor;
        }
        public Vendor paymentvendorsummary()
        {
            Vendor vendor = new Vendor();
            try
            {
                cmd = new MySqlCommand("sp_sel_paymentvendorsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;               
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendorlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendorlist
                        {

                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_code = rd["vendor_code"].ToString(),
                            vendor_company_name = rd["vendor_companyname"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_address_line1 = rd["vendor_address_line1"].ToString(),
                            active_flag = rd["active_flag"].ToString()

                        });
                    }
                    vendor.vendorlist = summary;
                    vendor.status = true;
                    
                }
                else
                {
                    vendor.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                vendor.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }
        public Vendor ticketvendor()
        {
            Vendor vendor = new Vendor();
            try
            {
                cmd = new MySqlCommand("sp_sel_ticketvendorlist");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendorlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendorlist
                        {
                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString()                        
                        });
                    }
                    vendor.vendorlist = summary;
                    vendor.status = true;
                    

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                vendor.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return vendor;
        }

        public Vendormodel vendoradvanceadd(Vendordetail val, string user_gid)
        {
            Vendormodel add = new Vendormodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_vendoradvanceadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                cmd.Parameters.AddWithValue("p_vendor_company_name", val.vendor_company_name);
                cmd.Parameters.AddWithValue("p_advance_date", val.advance_date);
                cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_advance", val.advance);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                cmd.Parameters.AddWithValue("p_bank_name", "");
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

        public Vendormodel submitvendorbudget(Vendordetail val, string user_gid)
        {
            Vendormodel add = new Vendormodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_vendortransaction");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                cmd.Parameters.AddWithValue("p_totalvendor_paymenet", val.totalvendor_paymenet);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_payment_source", val.payment_source);
                cmd.Parameters.AddWithValue("p_service_type", val.service_type);
                cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                cmd.Parameters.AddWithValue("p_transaction_number", val.transaction_number);
                cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                cmd.Parameters.AddWithValue("p_receipt_method", val.receipt_method);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
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
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Vendor Gid:" + val.vendor_gid);
                    sw.WriteLine("Error: Error occured while Add A Vendor Monthly Payment");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public vendorBudget vendorbudgetsummary(vendorBudget val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    if (!String.IsNullOrEmpty(rd["totalvendor_paymenet"].ToString()))
                    {
                        val.totalvendor_paymenet = double.Parse(rd["totalvendor_paymenet"].ToString());
                    }
                    else
                    {
                        val.vendor_payment = 0.000;
                    }
                    rd.NextResult();
                    rd.Read();
                    val.vendor_company_name = rd["vendor_company_name"].ToString();
                    val.vendor_name = rd["vendor_name"].ToString();
                    rd.Close();
                }

                cmd = new MySqlCommand("sp_sel_vendortotaltransactionsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);

                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorBudgetSummary>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        if (double.Parse(rd["debit"].ToString()) == 0.0 && double.Parse(rd["credit"].ToString()) == 0.0)
                        {

                        }
                        else
                        {
                            summary.Add(new vendorBudgetSummary
                            {
                                vendorbudget_gid = rd["vendorbudget_gid"].ToString(),
                                salesorder_gid = rd["salesorder_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                payment_source = rd["payment_source"].ToString(),
                                invoice_refnumber = rd["invoice_refnumber"].ToString(),
                                service_type = rd["service_type"].ToString(),
                                debit = double.Parse(rd["debit"].ToString()),
                                credit = double.Parse(rd["credit"].ToString()),
                                created_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                            });
                        }
                        val.total_debit += double.Parse(rd["debit"].ToString());
                        val.total_credit += double.Parse(rd["credit"].ToString());
        
                    }
                    val.vendorBudgetSummary = summary;
                    val.status = true;
                    rd.Close();
                }
                else
                {
                    rd.Close();
                    val.status = true;
                }
            }
            catch (Exception ex)
            {
                rd.Close();
                val.status = false;
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public vendorBudget monthlypaymentsummary(vendorBudget val)
        {
            try
            {
               
                cmd = new MySqlCommand("sp_sel_monthlypaymentsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);

                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorBudgetSummary>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new vendorBudgetSummary
                        {
                            vendorbudget_gid = rd["vendorbudget_gid"].ToString(),
                            totalvendor_paymenet = double.Parse(rd["totalvendor_paymenet"].ToString()),
                            receipt_method = rd["receipt_method"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            reference = rd["reference"].ToString(),

                            created_date = Convert.ToDateTime(rd["created_date"]).ToString("dd MMM yyyy")
                    });
                      


                    }
                    val.vendorBudgetSummary = summary;
                    val.status = true;
                    rd.Close();
                }
                else
                {
                    rd.Close();
                    val.status = true;
                }
            }
            catch (Exception ex)
            {
                rd.Close();
                val.status = false;
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public vendorBudget vendorbudgetsummarysearch(vendorBudget val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorpayment");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    if (!String.IsNullOrEmpty(rd["totalvendor_paymenet"].ToString()))
                    {
                        val.totalvendor_paymenet = double.Parse(rd["totalvendor_paymenet"].ToString());
                    }
                    else
                    {
                        val.vendor_payment = 0.000;
                    }
                    rd.NextResult();
                    rd.Read();
                    val.vendor_company_name = rd["vendor_company_name"].ToString();
                    val.vendor_name = rd["vendor_name"].ToString();
                    rd.Close();
                }

                cmd = new MySqlCommand("sp_sel_vendortotaltransactionsearch");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);

                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<vendorBudgetSummary>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new vendorBudgetSummary
                        {
                            vendorbudget_gid = rd["vendorbudget_gid"].ToString(),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            payment_source = rd["payment_source"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            debit = double.Parse(rd["debit"].ToString()),
                            credit = double.Parse(rd["credit"].ToString()),
                            created_date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
                        });
                        val.total_debit += double.Parse(rd["debit"].ToString());
                        val.total_credit += double.Parse(rd["credit"].ToString());


                    }
                    val.vendorBudgetSummary = summary;
                    val.status = true;
                    rd.Close();
                }
                else
                {
                    rd.Close();
                    val.status = true;
                }
            }
            catch (Exception ex)
            {
                rd.Close();
                val.status = false;
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public Vendor vendoradvancesummary(string val)
        {
            Vendor Adv = new Vendor();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendoradvancevalue");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if(rd.Read())
                {
                    Adv.vendor_name = rd["vendor_name"].ToString();
                    Adv.vendor_company_name = rd["vendor_company_name"].ToString();

                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_vendoradvancesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_vendor_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendoradvancelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendoradvancelist
                        {
                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            vendor_company_name = rd["vendor_company_name"].ToString(),
                            payment_mode = rd["payment_mode"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            advance_date = rd["advance_date"].ToString(),
                            advance_amount = rd["advance_amount"].ToString(),
                           
                        });
                    }
                    Adv.Vendoradvancelist = summary;
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

        public Vendor vendorledgersummary()
        {
            Vendor Adv = new Vendor();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorledgersummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_vendor_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendorledgersummarylist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendorledgersummarylist
                        {
                            vendor_gid = rd["vendor_gid"].ToString(),
                            vendor_name = rd["vendor_name"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            invoice_amount = double.Parse(rd["invoice_amount"].ToString()),
                            payment_amount = double.Parse(rd["payment_amount"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            advance_amount = double.Parse(rd["advance_amount"].ToString()),
                            refund_amount = double.Parse(rd["refund_amount"].ToString())


                        });
                    }
                    Adv.Vendorledgersummarylist = summary;
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
        public Vendor vendorledgerchildreport(Vendordetail val)
        {
            Vendor Adv = new Vendor();
            try
            {
                if(val.from_date==null)
                {
                    val.from_date = "null";
                }
                if(val.to_date==null)
                {
                    val.to_date = "null";
                }

                cmd = new MySqlCommand("sp_sel_vendorledgerchildreport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Vendorledgerchildreport>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Vendorledgerchildreport
                        {
                            vendor_gid = rd["vendor_gid"].ToString(),
                            reference_number = rd["reference_no"].ToString(),
                            reference_gid = rd["reference_gid"].ToString(),
                            transaction_date = rd["transaction_date"].ToString(),
                            total_debit = double.Parse(rd["total_debit"].ToString()),
                            total_credit = double.Parse(rd["total_credit"].ToString()),
                            outstanding_amount = double.Parse(rd["outstanding_amount"].ToString()),
                            type = rd["type"].ToString(),
                            
                        });
                    }
                    Adv.Vendorledgerchildreport = summary;
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

        //public vendorMonthlyBudget monthlyvendorbudgetsummary(vendorMonthlyBudget val)
        //{
        //    try
        //    {
        //        cmd = new MySqlCommand("sp_sel_vendorpaymentmonthly");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);
        //        cmd.Parameters.AddWithValue("p_month", val.month);
        //        cmd.Parameters.AddWithValue("p_year", val.year);
        //        rd = DBAccess.ExecuteReader(cmd);
        //        if (rd.HasRows == true)
        //        {
        //            rd.Read();
        //            if (!String.IsNullOrEmpty(rd["vendor_payment"].ToString()))
        //            {
        //                val.vendor_payment = double.Parse(rd["vendor_payment"].ToString());
        //            }
        //            else
        //            {
        //                val.vendor_payment = 0.00;
        //            }
        //            rd.Close();
        //        }

        //        cmd = new MySqlCommand("sp_sel_vendormonthlybudgetsummary");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_code);
        //        cmd.Parameters.AddWithValue("p_month", val.month);
        //        cmd.Parameters.AddWithValue("p_year", val.year);
        //        rd = DBAccess.ExecuteReader(cmd);
        //        var summary = new List<vendorBudgetSummary>();
        //        if (rd.HasRows == true)
        //        {
        //            while (rd.Read())
        //            {
        //                summary.Add(new vendorBudgetSummary
        //                {
        //                    reference_number = rd["reference_number"].ToString(),
        //                    salesorder_gid = rd["salesorder_gid"].ToString(),
        //                    process = rd["process"].ToString(),
        //                    debit = double.Parse(rd["total_debit"].ToString()),
        //                    credit = double.Parse(rd["total_credit"].ToString()),
        //                    available_balance = double.Parse(rd["available_balance"].ToString()),
        //                    date = Convert.ToDateTime(rd["created_date"]).ToString("dd/MM/yyyy"),
        //                });
        //                val.total_debit += double.Parse(rd["total_debit"].ToString());
        //            }
        //            val.vendorBudgetSummary = summary;
        //            val.status = true;
        //            rd.Close();
        //        }
        //        else
        //        {
        //            rd.Close();
        //            val.status = true;
        //            val.message = "No records Found!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rd.Close();
        //        val.status = false;
        //        error = ex.ToString();
        //        string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
        //        if (!File.Exists(strPath))
        //        {
        //            File.Create(strPath).Dispose();
        //        }
        //        using (StreamWriter sw = File.AppendText(strPath))
        //        {
        //            sw.WriteLine("=============Error Logging ===========");
        //            sw.WriteLine("===========Start============= " + DateTime.Now);
        //            sw.WriteLine("Error Message: " + ex.Message);
        //            sw.WriteLine("Stack Trace: " + ex.StackTrace);
        //            sw.WriteLine("===========End============= " + DateTime.Now);

        //        }
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return val;
        //}
    }
}