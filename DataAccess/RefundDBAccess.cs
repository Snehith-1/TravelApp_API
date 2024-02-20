﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class RefundDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Refund GetAll()
        {
            Refund refund = new Refund();
            try
            {
                cmd = new MySqlCommand("sp_sel_refund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Refundlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Refundlist
                        {
                           
                            refund_gid = int.Parse(rd["refund_gid"].ToString()),
                            refund_number = rd["refund_number"].ToString(),
                            //salesorderrefno = rd["salesorder_refno"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            //customergid = rd["customer_gid"].ToString(),
                            //contactno = rd["mobile_no"].ToString(),
                            refund_type = rd["refund_type"].ToString(),
                            //nationalid = rd["national_id"].ToString(),
                           // refunddate = rd["refund_date"].ToString(),
                            refund_amount = Double.Parse(rd["refund_amount"].ToString()),
                          // receivedamount = Double.Parse(rd["received_amount"].ToString()),
                            //orderamount = Double.Parse(rd["net_value"].ToString()),
                            refund_date = rd["refund_date"].ToString(),
                            cancellation_charge = rd["cancellation_charge"].ToString(),
                            payment_refnumber = rd["payment_refnumber"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),
                            payment_mode = rd["payment_mode"].ToString(),
                            vendor_name=rd["vendor_name"].ToString(),
                            vendorefund_amount=Double.Parse(rd["vendorcancellation_charges"].ToString()),
                            vendor_amount= Double.Parse(rd["vendor_amount"].ToString())

                        });
                    }
                    refund.refundlist = summary;
                    refund.status = true;
                    
                }
                else
                {
                    refund.status = false;  
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                refund.status = false;
                refund.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refund;
        }

       public Refund refundreceiptsummary(Refund val)
        {
            Refund receipt = new Refund();
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
                if (val.service_name == null)
                {
                    val.service_name = "null";
                }
                if(val.ticket_number==null)
                {
                    val.ticket_number = "null";
                }

                cmd = new MySqlCommand("sp_sel_refundreceiptdetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_service_name", val.service_name);
                cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<refundreceiptlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new refundreceiptlist
                        {
                            receipt_gid = rd["customerreceipt_gid"].ToString(),
                            receipt_date = rd["receipt_date"].ToString(),
                            reference_number = rd["reference_number"].ToString(),
                            customer_name = rd["customer_name"].ToString(),
                            receipt_amount = double.Parse(rd["receipt_amount"].ToString()),
                            refund_amount = double.Parse(rd["refund_amount"].ToString()),
                            reference_gid = rd["refundreference_gid"].ToString(),
                            contact_details = rd["contact_details"].ToString(),
                            customer_gid = rd["customer_gid"].ToString(),
                            customerinvoice_gid = rd["customerinvoice_gid"].ToString()

                        });
                    }
                    receipt.refundreceiptlist = summary;
                    receipt.status = true;

                }
                else
                {
                    receipt.status = false;
                }
            rd.Close();
             }
            catch(Exception ex)
            {
                receipt.status = false;
                receipt.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return receipt;
        }
        public Refund refundadvancesummary()
        {
            Refund advance = new Refund();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundadvancedetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<refundadvancelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new refundadvancelist
                        {
                    advance_gid = rd["advance_gid"].ToString(),
                    salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                    customer_name = rd["customer_name"].ToString(),
                    advance_amount = Double.Parse(rd["advance_amount"].ToString()),
                    advancerefund = Double.Parse(rd["refund_amount"].ToString()),
                    //refundref_gid = Double.Parse(rd["refundreference_gid"].ToString()),
                    contact_details = rd["contact_details"].ToString(),
                    customer_gid=rd["customer_gid"].ToString()

                    });
                }
                advance.refundadvancelist = summary;
                    advance.status = true;

                }
                else
                {
                    advance.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                advance.status = false;
                advance.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return advance;
        }
        public Refundlist Get(int val)
        {
            Refundlist refundlist = new Refundlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refundgid", val);            
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    refundlist.refund_gid = int.Parse(rd["refund_gid"].ToString());
                    refundlist.customer_name = rd["customer_name"].ToString();
                    refundlist.refund_number = rd["refund_number"].ToString();
                    refundlist.refund_type = rd["refund_type"].ToString();
                    refundlist.refund_date =rd["refund_date"].ToString();
                    refundlist.refund_amount = Double.Parse(rd["refund_amount"].ToString());
                    refundlist.salesorder_refnumber = rd["salesorder_refnumber"].ToString();
                    refundlist.advance_paid = Double.Parse(rd["advance_paid"].ToString());//receivedamount as advance_paid
                    refundlist.net_amount = Double.Parse(rd["net_amount"].ToString());//orderamount as  net_amount
                    refundlist.status = true;
                    
                }
                else
                {
                    refundlist.status = false;
                    refundlist.message = "No Records found!";
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                refundlist.status = false;
                refundlist.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refundlist;
        }
        public Refundmodel Add(Refunddetails val, string userGid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_refundrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_number", val.refund_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Refund Ref.No Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_refund");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_refund_type", val.refund_type);
                    cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                    cmd.Parameters.AddWithValue("p_refund_refnumber", val.refund_refnumber);
                    cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_refund_date", val.refund_date);
                    cmd.Parameters.AddWithValue("p_created_by", userGid);                                    
                    cmd.Parameters.AddWithValue("p_received_amount", val.received_amount);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_payment_refnumber", val.payment_refnumber);
                    cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_refnumber", val.transaction_refnumber);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
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
                rd.Close();
            }
            catch(Exception ex)
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
        public Refundmodel Update(Refundlist val, string user_gid)
        {
            Refundmodel refund = new Refundmodel();
            try
            {               
                cmd = new MySqlCommand("sp_upt_refund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_refunddate", val.refunddate);
                cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                cmd.Parameters.AddWithValue("p_refund_gid", val.refund_gid);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);             
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    refund.status = true;
                    refund.message = "Refund updated succesfully";
                }
                else
                {
                    refund.status = false;
                    refund.message = "Error Occured While Updating Refund!";
                }
            }
            catch(Exception ex)
            {
                refund.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
           
            return refund;
        }
        public Refundmodel Delete(int values)
        {
            Refundmodel refunddelete = new Refundmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_refund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_gid", values);              
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    refunddelete.status = true;
                    refunddelete.message = "Deleted Successfully";
                }
                else
                {
                    refunddelete.status = false;
                    refunddelete.message = "Error Occured While Deleting Refund!";
                }
            }
            catch(Exception ex)
            {
                refunddelete.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refunddelete;
        }
        public Refunddetails salesdetailrefund(int values)
        {
            Refunddetails refund= new Refunddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesorderrefund");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", values);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {                   
                    refund.salesorder_refnumber = rd["salesorder_refnumber"].ToString();
                    refund.customer_name = rd["customer_name"].ToString();
                    refund.contact_number = rd["contact_number"].ToString();
                    refund.salesorder_gid = int.Parse (rd["salesorder_gid"].ToString());
                    refund.net_amount = Double.Parse(rd["net_amount"].ToString());//orderamount as net_amount
                    refund.created_date =DateTime.Parse(rd["created_date"].ToString());                   
                    refund.status = true;  
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                refund.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refund;
        }
        public Refunddetails refundreferenceno(string usergid)
        {
            Refunddetails Val = new Refunddetails();
            try
            {
                cmd = new MySqlCommand("sp_ins_refundreferencenumber");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_refnumber", "");
                cmd.Parameters.AddWithValue("p_created_by", usergid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_refundreferencenumber");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.refund_refnumber = rd["refund_refnumber"].ToString();
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
        public Refunddetails refundreceiptdetails(Refunddetails values, string usergid)
        {
            Refunddetails refund = new Refunddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundreceiptedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customerreceipt_gid", values.receipt_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    refund.receipt_gid = rd["customerreceipt_gid"].ToString();
                    refund.receipt_date = rd["receipt_date"].ToString();
                    refund.receipt_amount = Double.Parse(rd["receipt_amount"].ToString());
                    refund.contact_number = rd["contact_number"].ToString();
                    //refund.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    refund.customer_name = rd["customer_name"].ToString();
                    refund.reference_number = rd["reference_number"].ToString();
                    refund.status = true;
                }
                rd.Close();

                cmd = new MySqlCommand("sp_sel_vendor_amountdetails ");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_invoice_gid", values.customerinvoice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    refund.vendor_name = rd["vendor_name"].ToString();
                    refund.vendor_amount = Double.Parse(rd["vendor_amount"].ToString());
                    refund.service_details = rd["service_details"].ToString();
                    refund.vendor_gid = rd["vendor_gid"].ToString();
                    refund.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                refund.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refund;
        }
        public Refunddetails refundadvancedetails(string values)
        {
            Refunddetails refund = new Refunddetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_refundadvanceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_advance_gid", values);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    refund.advance_gid = rd["advance_gid"].ToString();
                    refund.advance_date = rd["advance_date"].ToString();
                    refund.advance_amount = Double.Parse(rd["advance_amount"].ToString());
                    refund.contact_number = rd["contact_number"].ToString();
                    //refund.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    refund.customer_name = rd["customer_name"].ToString();
                    //refund.receipt_refno = rd["reference_no"].ToString();
                    refund.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                refund.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return refund;
        }

        public Refundmodel refundreceipadvanceadd(Refunddetails val, string userGid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_refundrefnovalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_refund_number", val.refund_refnumber);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Refund Ref.No Already Exist!";
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_refundsubmit");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_refundreference_gid", val.refundreference_gid); //refund ref_gid for advance and receipt
                    cmd.Parameters.AddWithValue("p_refund_type", val.refund_type);
                    cmd.Parameters.AddWithValue("p_refund_amount", val.refund_amount);
                    cmd.Parameters.AddWithValue("p_refund_number", val.refund_refnumber);
                    //cmd.Parameters.AddWithValue("p_salesordergid", val.salesorder_gid);
                    cmd.Parameters.AddWithValue("p_refund_date", val.refund_date);
                    cmd.Parameters.AddWithValue("p_created_by", userGid);
                    cmd.Parameters.AddWithValue("p_received_amount", val.received_amount);
                    cmd.Parameters.AddWithValue("p_payment_mode", val.payment_mode);
                    cmd.Parameters.AddWithValue("p_payment_refnumber", val.payment_refnumber);
                    cmd.Parameters.AddWithValue("p_bank_gid", val.bank_gid);
                    cmd.Parameters.AddWithValue("p_bank_name", val.bank_name);
                    cmd.Parameters.AddWithValue("p_transaction_refnumber", val.transaction_refnumber);
                    cmd.Parameters.AddWithValue("p_cancellation_charge", val.cancellation_charge);
                    cmd.Parameters.AddWithValue("p_customer_name", val.customer_name);
                    cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                    cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                    cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                    cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                    cmd.Parameters.AddWithValue("p_vendorcancellation_charges", val.vendorcancellation_amount);
                    cmd.Parameters.AddWithValue("p_service_details", val.service_details);
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
                rd.Close();
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

        public Refunddetails refundvendordetails(Refunddetails val)
        {

            return val;
        }
    }
}