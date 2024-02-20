using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class VoucherDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Voucher vouchersummary(string val)
        {

            Voucher voucher = new Voucher();
            try
            {
                cmd = new MySqlCommand("sp_sel_voucher");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Voucherlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Voucherlist
                        {
                            voucher_gid = int.Parse(rd["voucher_gid"].ToString()),
                            guest_name = rd["guest_name"].ToString(),
                            property = rd["property"].ToString(),
                            check_in_date = rd["check_in_date"].ToString(),
                            check_out_date = rd["check_out_date"].ToString(),

                            check_in_time = rd["check_in_time"].ToString(),
                            check_out_time = rd["check_out_time"].ToString(),
                            total_numberofdays = rd["total_numberofdays"].ToString(),
                            total_numberofpaxs = rd["total_numberofpaxs"].ToString(),
                            meal_plan = rd["meal_plan"].ToString(),
                            extras = rd["extras"].ToString(),
                            bookings_doneby = rd["bookings_doneby"].ToString(),
                            created_by = rd["created_by"].ToString(),
                            created_date = rd["created_date"].ToString(),
                            updated_by = rd["updated_by"].ToString(),
                            updated_date = rd["updated_date"].ToString(),
                            company_code = val


                        });
                    }
                    voucher.Voucherlist = summary;
                    voucher.status = true;

                }
                else
                {
                    voucher.status = false;
                    voucher.message = "No Records Fund";

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                voucher.status = false;
                voucher.message = "Error occured while show the record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return voucher;
        }
        public Vouchermodel voucheradd(Voucherdetails val, string usergid)
        {
            Vouchermodel part = new Vouchermodel();

            try
            {
                cmd = new MySqlCommand("sp_sel_vouchervalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_guest_name", val.guest_name);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Guest Name Already Exist!";

                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_voucher");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_guest_name", val.guest_name);
                    cmd.Parameters.AddWithValue("p_property", val.property);
                    cmd.Parameters.AddWithValue("p_check_in_date", val.check_in_date);
                    cmd.Parameters.AddWithValue("p_check_out_date", val.check_out_date);
                    cmd.Parameters.AddWithValue("p_Check_in_time", val.check_in_time);
                    cmd.Parameters.AddWithValue("p_Check_out_time", val.check_out_time);
                    cmd.Parameters.AddWithValue("p_total_numberofdays", val.total_numberofdays);
                    cmd.Parameters.AddWithValue("p_total_numberofpaxs", val.total_numberofpaxs);
                    cmd.Parameters.AddWithValue("p_meal_plan", val.meal_plan);
                    cmd.Parameters.AddWithValue("p_extras", val.extras);
                    cmd.Parameters.AddWithValue("p_bookings_doneby", val.bookings_doneby);
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    cmd.Parameters.AddWithValue("p_updated_by", usergid);
                    cmd.Parameters.AddWithValue("p_voucher_gid", "");
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Voucher added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error occured while adding voucher";
                    }

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error occured while adding voucher";
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

        ////public voucherdetails Get(int val)
        ////{
        ////    voucherdetails voucherlist = new voucherdetails();
        ////    try
        ////    {

        ////        cmd = new MySqlCommand("sp_sel_voucheredit");
        ////        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        ////        cmd.Parameters.AddWithValue("p_voucher_gid", val);
        ////        MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

        ////        if (rd.Read())
        ////        {
        ////            voucherlist.voucher_gid = int.Parse(rd["voucher_gid"].ToString());
        ////            voucherlist.voucher_code = rd["voucher_code"].ToString();
        ////            voucherlist.voucher_name = rd["voucher_name"].ToString();
        ////            voucherlist.national_id = rd["national_id"].ToString();
        ////            voucherlist.contact_number = rd["contact_number"].ToString();
        ////            voucherlist.email_address = rd["email_address"].ToString();
        ////            voucherlist.voucher_country = rd["country_name"].ToString();
        ////            voucherlist.voucher_address = rd["voucher_address"].ToString();
        ////            voucherlist.capitalshare_percent = rd["capitalshare_percent"].ToString();
        ////            voucherlist.revenueshare_percent = rd["revenueshare_percent"].ToString();
        ////            voucherlist.sharepaid_captial = rd["sharepaid_captial"].ToString();
        ////            voucherlist.status = true;

        ////        }
        ////        else
        ////        {
        ////            voucherlist.status = false;
        ////            voucherlist.message = " No Records Found!";
        ////        }
        ////        rd.Close();

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        voucherlist.status = false;
        ////        voucherlist.message = "Internal Error Occured!";
        ////        error = ex.ToString();
        ////    }
        ////    finally
        ////    {
        ////        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        ////        {
        ////            cmd.Connection.Close();
        ////        }

        ////    }
        ////    return voucherlist;


        ////}

        ////public vouchermodel Update(voucherdetails values, string userGid)
        ////{
        ////    vouchermodel voucher = new vouchermodel();
        ////    try
        ////    {
        ////        cmd = new MySqlCommand("sp_upt_voucher");
        ////        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        ////        cmd.Parameters.AddWithValue("p_voucher_gid", values.voucher_gid);
        ////        cmd.Parameters.AddWithValue("p_voucher_code", values.voucher_code);
        ////        cmd.Parameters.AddWithValue("p_voucher_name", values.voucher_name);
        ////        cmd.Parameters.AddWithValue("p_national_id", values.national_id);
        ////        cmd.Parameters.AddWithValue("p_contact_number", values.contact_number);
        ////        cmd.Parameters.AddWithValue("p_email_address", values.email_address);
        ////        cmd.Parameters.AddWithValue("p_voucher_address", values.voucher_address);
        ////        cmd.Parameters.AddWithValue("p_capitalshare_percent", values.capitalshare_percent);
        ////        cmd.Parameters.AddWithValue("p_revenueshare_percent", values.revenueshare_percent);
        ////        cmd.Parameters.AddWithValue("p_sharepaid_captial", values.sharepaid_captial);
        ////        cmd.Parameters.AddWithValue("p_updated_by", values.updated_by);
        ////        cmd.Parameters.AddWithValue("p_voucher_country", values.voucher_country);
        ////        cmd.Parameters.AddWithValue("p_updated_date", values.updated_date);
        ////        mnresult = DBAccess.ExecuteNonQuery(cmd);
        ////        if (mnresult == 1)
        ////        {
        ////            voucher.status = true;
        ////            voucher.message = "voucher updated successfully";
        ////        }
        ////        else
        ////        {
        ////            voucher.status = false;
        ////            voucher.message = "Internal Error Occured";
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        voucher.status = false;
        ////        voucher.message = "Internal Error Occured";
        ////        error = ex.ToString();
        ////    }
        ////    finally
        ////    {
        ////        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        ////        {
        ////            cmd.Connection.Close();
        ////        }

        ////    }
        ////    return voucher;
        ////}

        public Vouchermodel Delete(int values)
        {
            Vouchermodel deletevoucher = new Vouchermodel();
            try
            {
                cmd = new MySqlCommand("sp_del_voucher");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_voucher_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    deletevoucher.status = true;
                    deletevoucher.message = "Voucher deleted successfully";
                }
                else
                {
                    deletevoucher.status = false;
                    deletevoucher.message = "Error occured while deleting voucher!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return deletevoucher;

        }
    }
}






