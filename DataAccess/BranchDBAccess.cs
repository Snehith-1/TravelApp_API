using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace DataAccess
{
    public class BranchDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Branch GetAll()
        {
            Branch branch = new Branch();
            try
            {
                cmd = new MySqlCommand("sp_sel_branch");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Branchlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Branchlist
                        {
                            branch_gid = int.Parse(rd["branch_gid"].ToString()),
                            branch_code = rd["branch_code"].ToString(),
                            branch_name = rd["branch_name"].ToString(),
                        });
                    }
                    branch.branchlist = summary;
                    branch.status = true;
                    //rd.Close();

                }
                else
                {
                    branch.status = false;
                    branch.message = "No Records Fund";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                branch.status = false;
                branch.message = "Error occured while show the record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return branch;
        }

        public Branchmodel branchcode(Branchdetails val, string user_gid)
        {
            Branchdetails branch = new Branchdetails();
            try
            {
                cmd = new MySqlCommand("sp_ins_branchcode");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_branch_code", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_branchcode");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    branch.branch_code = rd["branch_code"].ToString();
                    branch.status = true;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                branch.status = false;
                branch.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return branch;
        }
        public Branchmodel branchadd(Branchdetails val, string userGid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_branchcodevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_branch_code", val.branch_code);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Branch code already exist!";

                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_branchadd");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_branch_code", val.branch_code);
                    cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                    cmd.Parameters.AddWithValue("p_created_by", userGid);
                    cmd.Parameters.AddWithValue("p_created_date", val.created_date);
                    cmd.Parameters.AddWithValue("p_updated_by", val.updated_by);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Branch added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error occured while adding branch";
                    }
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error occured while adding branch";
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
        public Branchmodel branchedit(Branchdetails val, string usergid)
        {
            Branchdetails branchdtls = new Branchdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_branchedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    //branchdtls.branch_code = rd["branch_gid"].ToString();
                    branchdtls.branch_code = rd["branch_code"].ToString();
                    branchdtls.branch_name = rd["branch_name"].ToString();
                    branchdtls.status = true;
                    //rd.Close();
                }
                else
                {
                    branchdtls.status = false;
                    branchdtls.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                branchdtls.status = false;
                branchdtls.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return branchdtls;
        }

        public Branchmodel branchupdate(Branchdetails val, string usergid)
        {
            Branchmodel branch = new Branchmodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_branch");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_branch_gid", val.branch_gid);
                cmd.Parameters.AddWithValue("p_branch_name", val.branch_name);
                cmd.Parameters.AddWithValue("p_branch_code", val.branch_code);
                cmd.Parameters.AddWithValue("p_updated_by", usergid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    branch.status = true;
                    branch.message = "Status updated succesfully";
                }
                else
                {
                    branch.status = false;
                    branch.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                branch.status = false;
                branch.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return branch;
        }
        public Branchmodel Delete(int val)
        {
            Branchmodel deletebranch = new Branchmodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_employeebranchgid_validation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_branch_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    deletebranch.status = false;
                }
                else
                {
                    cmd = new MySqlCommand("sp_del_branch");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_branch_gid", val);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        deletebranch.status = true;
                        deletebranch.message = "Branch deleted successfully";
                    }
                    else
                    {
                        deletebranch.status = false;
                        deletebranch.message = "Error occured while deleting branch!";
                    }
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
            return deletebranch;

        }
        //public subdocument_intelligencelist getsubdocument_intelligence(string prefixtext, string subdocumenttype_gid)
        //{

        //    subdocument_intelligencelist objsubdocument_intelligencelist = new subdocument_intelligencelist();
        //    SqlDataReader objdr;
        //    try
        //    {
        //        var objsubdocument_intelligence = new List<subdocument_intelligence>();

        //        objcon = objdbcon.OpenConn();

        //        _lsselectparameter = "@status||" +
        //                             "@subdocumenttype_gid||" +
        //                             "@subdocumentconfig_name";

        //        _lsselectparametervalue = "success" + "||" +
        //                                   subdocumenttype_gid + "||" +
        //                                   prefixtext;

        //        objdr = objcmnfunctions.GetDataReader(_lsselectparameter, _lsselectparametervalue, "twi_mdir_sel_subdocumentintgnce_new", 3, objcon);

        //        if (objdr.HasRows == true)
        //        {
        //            while (objdr.Read())
        //            {
        //                objsubdocument_intelligence.Add(new subdocument_intelligence
        //                {
        //                    prefixText = objdr["keyvalue_name"].ToString(),
        //                    subdocumenttype_gid = objdr["subdocumenttype_gid"].ToString()
        //                });
        //            }
        //            objsubdocument_intelligencelist.Getsubdocument_intelligencelist = objsubdocument_intelligence;
        //            objsubdocument_intelligencelist.status = true;
        //        }
        //        else
        //        {
        //            objsubdocument_intelligencelist.status = false;
        //            objcmnfunction.Getresponse("ERR_0001", ref msg, ref type, objcon);
        //            objsubdocument_intelligencelist.msg = msg;
        //            objsubdocument_intelligencelist.msg_type = type;
        //        }
        //        objdr.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        objcmnfunction.Auditlog("Country", "Failure", e.ToString(), "Student", objcon);
        //        objsubdocument_intelligencelist.status = false;
        //        objcmnfunction.Getresponse("ERR_0001", ref msg, ref type, objcon);
        //        objsubdocument_intelligencelist.msg = msg;
        //        objsubdocument_intelligencelist.msg_type = type;
        //    }
        //    finally
        //    {
        //        objcon.Close();
        //    }

        //    return objsubdocument_intelligencelist;
        //}


        //public CusIntelligence GetAll()
        //{
        //    CustomerAutoIntelligenceList cusintgnce = new CustomerAutoIntelligenceList();
        //    try
        //    {
        //        cmd = new MySqlCommand("sp_sel_intelligencelist");
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        rd = DBAccess.ExecuteReader(cmd);
        //        var summary = new List<CustomerAutoIntelligenceList>();
        //        if (rd.HasRows == true)
        //        {
        //            while (rd.Read())
        //            {
        //                summary.Add(new CustomerAutoIntelligenceList
        //                {
        //                    prefixtext = rd["customer_name"].ToString(),
        //                    customerid = rd["customer_gid"].ToString()
        //                });
        //            }
        //            cusintgnce.CustomerAutoIntelligenceList = summary;
        //            cusintgnce.status = true;


        //        }
        //        else
        //        {
        //            cusintgnce.status = false;
        //            cusintgnce.message = "No Records Fund";

        //        }
        //        rd.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //objcmnfunction.Auditlog("Country", "Failure", e.ToString(), "Student", objcon);
        //        //objsubdocument_intelligencelist.status = false;
        //        //objcmnfunction.Getresponse("ERR_0001", ref msg, ref type, objcon);
        //        cusintgnce.status = false;
        //        cusintgnce.message = "Error Occured While Show the Record";
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return cusintgnce;
        //}
    }
}