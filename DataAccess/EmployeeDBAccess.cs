using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;


//Test

namespace DataAccess
{
    public class EmployeeDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Employee employeesummary()
        {
            Employee employee = new Employee();
            try
            {
                cmd = new MySqlCommand("sp_sel_employee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Employeelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Employeelist
                        {
                            user_gid = int.Parse(rd["user_gid"].ToString()),
                            user_code = rd["user_code"].ToString(),
                            employee_name = rd["employee_name"].ToString(),
                            department_name = rd["department_name"].ToString(),
                            active_status = rd["active_status"].ToString(),
                            branch_name = rd["branch_name"].ToString()

                        });
                    }
                    employee.employeelist = summary;
                    employee.status = true;
                    
                }
               
                else
                {
                    employee.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                employee.status = false;
                employee.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }
            
            return employee;
        }
        public Employeedetail employeecode(string user_gid)
        {
            Employeedetail Val = new Employeedetail();
            try
            {
                cmd = new MySqlCommand("sp_ins_employeecode");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_employee_code", "");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_employeecode");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_created_by", user_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    rd.Read();
                    Val.employee_code = rd["employee_code"].ToString();
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
        public Employeedetail EmployeeDocumentpath(Employeedetail values)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_employeedocdownload");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", values.user_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    values.upload_documents = rd["upload_docs"].ToString();
                    if(!String.IsNullOrEmpty(values.upload_documents))
                    values.status = true;
                    else
                    {
                        values.upload_documents = "Not Detected";
                    }
                }
                else
                {
                    values.upload_documents = "Not Detected";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                values.upload_documents = "Not Detected";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return values;
        }

        public Employeedetail employeeedit(string val)
        {
            Employeedetail employeedetail = new Employeedetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_employeeedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", val);
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    employeedetail.user_gid = rd["user_gid"].ToString();
                    employeedetail.user_code = rd["user_code"].ToString();
                    employeedetail.first_name = rd["first_name"].ToString();
                    employeedetail.last_name = rd["last_name"].ToString();
                    employeedetail.department_gid = rd["department_gid"].ToString();
                    employeedetail.active_status = rd["active_status"].ToString();
                    employeedetail.gender = rd["gender"].ToString();
                    employeedetail.email_address = rd["email_address"].ToString();
                    employeedetail.contact_number = rd["contact_number"].ToString();
                    employeedetail.address_line1 = rd["address1"].ToString();
                    employeedetail.address_line2 = rd["address2"].ToString();
                    employeedetail.city = rd["city"].ToString();
                    employeedetail.state = rd["state"].ToString();
                    employeedetail.postal_code = rd["postal_code"].ToString();
                    employeedetail.country = rd["country"].ToString();
                    employeedetail.passport_number = rd["passport_number"].ToString();
                    employeedetail.national_id = rd["national_id"].ToString();
                    employeedetail.branch_gid = rd["branch_gid"].ToString();
                    employeedetail.branch_name = rd["branch_name"].ToString();
                    employeedetail.doj = rd["doj"].ToString();
                    employeedetail.status = true;
                }
                else
                {
                    employeedetail.status = false;
                    employeedetail.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                employeedetail.status = false;
                employeedetail.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return employeedetail;
        }
        public Employeedetail employeeprofile(string val)
        {
            Employeedetail employeedetail = new Employeedetail();

            try
            {

            

                    cmd = new MySqlCommand("sp_sel_employeeprofile");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_user_gid", val);
                    //cmd.Connection = con;
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                    employeedetail.user_gid = rd["user_gid"].ToString();
                    employeedetail.user_code = rd["user_code"].ToString();
                    employeedetail.first_name = rd["first_name"].ToString();
                    employeedetail.department_gid = rd["department_gid"].ToString();
                    employeedetail.email_address = rd["email_address"].ToString();
                    employeedetail.contact_number = rd["contact_number"].ToString();
                  
                       
                    }
                else
                {
                    employeedetail.status = false;
                    employeedetail.message = "Error while showing records";
                }
                rd.Close();




            }
            catch (Exception ex)
            {
                employeedetail.status = false;
                employeedetail.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return employeedetail;
        }






        public Employeemodel Getdocument(string company_code, HttpRequest httpRequest, string usergid)
        {
            string lspath;
            Employeedetail Getdocument = new Employeedetail();
            try
            {
                CmnFunctions objcmnfunctions = new CmnFunctions();
                HttpPostedFile httpPostedFile;
                HttpFileCollection httpFileCollection;
                if (httpRequest.Files.Count > 0)
                {
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lspath= HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["employee_file_path"].ToString());
                        Getdocument.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code+ConfigurationManager.AppSettings["employee_file_path"].ToString() + httpPostedFile.FileName);

                        if(!System.IO.Directory.Exists(lspath))
                        {
                            System.IO.Directory.CreateDirectory(lspath);
                        }
                        httpPostedFile.SaveAs(Getdocument.upload_documents);
                    }
                    if (File.Exists(Getdocument.upload_documents))
                    {
                        cmd = new MySqlCommand("sp_sel_user_codevalidation");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_employeecode", httpRequest.Form["user_code"]);
                        rd = DBAccess.ExecuteReader(cmd);
                        if (rd.Read())
                        {
                            Getdocument.status = false;
                            Getdocument.message = "Employee Code Already Exist!";
                            rd.Close();
                        }
                        else
                        {
                            cmd = new MySqlCommand("sp_ins_useradd");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_employee_code", httpRequest.Form["user_code"]);
                            cmd.Parameters.AddWithValue("p_first_name", httpRequest.Form["first_name"]);
                            cmd.Parameters.AddWithValue("p_last_name", httpRequest.Form["last_name"]);
                            cmd.Parameters.AddWithValue("p_department_gid", httpRequest.Form["department_gid"]);
                            cmd.Parameters.AddWithValue("p_active_flag", httpRequest.Form["active_status"]);
                            cmd.Parameters.AddWithValue("p_gender", httpRequest.Form["gender"]);
                            cmd.Parameters.AddWithValue("p_email_address", httpRequest.Form["email_address"]);
                            cmd.Parameters.AddWithValue("p_contact_number", httpRequest.Form["contact_number"]);
                            cmd.Parameters.AddWithValue("p_address_line1", httpRequest.Form["address_line1"]);
                            cmd.Parameters.AddWithValue("p_address_line2", httpRequest.Form["address_line2"]);
                            cmd.Parameters.AddWithValue("p_city", httpRequest.Form["city"]);
                            cmd.Parameters.AddWithValue("p_state", httpRequest.Form["state"]);
                            cmd.Parameters.AddWithValue("p_postal_code", httpRequest.Form["postal_code"]);
                            cmd.Parameters.AddWithValue("p_country", httpRequest.Form["country"]);
                            cmd.Parameters.AddWithValue("p_created_by", usergid);
                            cmd.Parameters.AddWithValue("p_emp_password", objcmnfunctions.passwordencryption(httpRequest.Form["password"]));
                            cmd.Parameters.AddWithValue("p_address_gid", "0");
                            cmd.Parameters.AddWithValue("p_passport_number", httpRequest.Form["passport_number"]);
                            cmd.Parameters.AddWithValue("p_national_id", httpRequest.Form["national_id"]);
                            cmd.Parameters.AddWithValue("p_doj", httpRequest.Form["doj"]);
                            cmd.Parameters.AddWithValue("p_upload_documents", Getdocument.upload_documents);
                            cmd.Parameters.AddWithValue("p_branch_gid", httpRequest.Form["branch_gid"]);
                            cmd.Parameters.AddWithValue("p_branch_name", "0");
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                Getdocument.status = true;
                                Getdocument.message = "Employee added Successfully!";
                            }
                            else
                            {
                                Getdocument.status = false;
                                Getdocument.message = "Error Occured While adding Employee!";
                            }

                        }

                    }
                    else
                    {
                        Getdocument.status = false;
                        Getdocument.message = "Error Occured While adding Employee!";
                    }
                }
                else
                {
                    cmd = new MySqlCommand("sp_sel_user_codevalidation");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_employeecode", httpRequest.Form["user_code"]);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        Getdocument.status = false ;
                        Getdocument.message = "Employee Code Already Exist!";
                        
                    }
                    else
                    {
                        cmd = new MySqlCommand("sp_ins_useradd");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_employee_code", httpRequest.Form["user_code"]);
                        cmd.Parameters.AddWithValue("p_first_name", httpRequest.Form["first_name"]);
                        cmd.Parameters.AddWithValue("p_last_name", httpRequest.Form["last_name"]);
                        cmd.Parameters.AddWithValue("p_department_gid", httpRequest.Form["department_gid"]);
                        cmd.Parameters.AddWithValue("p_active_flag", httpRequest.Form["active_status"]);
                        cmd.Parameters.AddWithValue("p_gender", httpRequest.Form["gender"]);
                        cmd.Parameters.AddWithValue("p_email_address", httpRequest.Form["email_address"]);
                        cmd.Parameters.AddWithValue("p_contact_number", httpRequest.Form["contact_number"]);
                        cmd.Parameters.AddWithValue("p_address_line1", httpRequest.Form["address_line1"]);
                        cmd.Parameters.AddWithValue("p_address_line2", httpRequest.Form["address_line2"]);
                        cmd.Parameters.AddWithValue("p_city", httpRequest.Form["city"]);
                        cmd.Parameters.AddWithValue("p_state", httpRequest.Form["state"]);
                        cmd.Parameters.AddWithValue("p_postal_code", httpRequest.Form["postal_code"]);
                        cmd.Parameters.AddWithValue("p_country", httpRequest.Form["country_gid"]);
                        cmd.Parameters.AddWithValue("p_created_by", usergid);
                        cmd.Parameters.AddWithValue("p_emp_password", objcmnfunctions.passwordencryption(httpRequest.Form["password"]));
                        cmd.Parameters.AddWithValue("p_address_gid", "0");
                        cmd.Parameters.AddWithValue("p_passport_number", httpRequest.Form["passport_number"]);
                        cmd.Parameters.AddWithValue("p_national_id", httpRequest.Form["national_id"]);
                        cmd.Parameters.AddWithValue("p_doj", httpRequest.Form["doj"]);
                        cmd.Parameters.AddWithValue("p_upload_documents", Getdocument.upload_documents);
                        cmd.Parameters.AddWithValue("p_branch_gid", httpRequest.Form["branch_gid"]);
                        cmd.Parameters.AddWithValue("p_branch_name", "0");
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            Getdocument.status = true;
                            Getdocument.message = "Employee added Successfully!";
                        }
                        else
                        {
                            Getdocument.status = false;
                            Getdocument.message = "Error Occured While adding Employee!";
                        }


                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                Getdocument.status = false;
                Getdocument.message = "Error Occured While adding Employee!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    rd.Close();
                }
            }
            return Getdocument;
        }
        public Employeemodel employeeupdate(string company_code, HttpRequest httpRequest, string user_gid)
        {
            Employeedetail Getdocument = new Employeedetail();
            try
            {
                CmnFunctions objcmnfunctions = new CmnFunctions();

                cmd = new MySqlCommand("sp_upt_employee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", httpRequest.Form["user_gid"]);
                cmd.Parameters.AddWithValue("p_employee_code", httpRequest.Form["user_code"]);
                cmd.Parameters.AddWithValue("p_first_name", httpRequest.Form["first_name"]);
                cmd.Parameters.AddWithValue("p_last_name", httpRequest.Form["last_name"]);
                cmd.Parameters.AddWithValue("p_department_gid", httpRequest.Form["department_gid"]);
                cmd.Parameters.AddWithValue("p_active_flag", httpRequest.Form["activeflag"]);
                cmd.Parameters.AddWithValue("p_gender", httpRequest.Form["gender"]);
                cmd.Parameters.AddWithValue("p_email_address", httpRequest.Form["email_address"]);
                cmd.Parameters.AddWithValue("p_contact_number", httpRequest.Form["contact_number"]);
                cmd.Parameters.AddWithValue("p_address_line1", httpRequest.Form["address_line1"]);
                cmd.Parameters.AddWithValue("p_address_line2", httpRequest.Form["address_line2"]);
                cmd.Parameters.AddWithValue("p_city", httpRequest.Form["city"]);
                cmd.Parameters.AddWithValue("p_state", httpRequest.Form["state"]);
                cmd.Parameters.AddWithValue("p_postal_code", httpRequest.Form["postal_code"]);
                cmd.Parameters.AddWithValue("p_country", httpRequest.Form["country"]);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Parameters.AddWithValue("p_emppassword", "");
                cmd.Parameters.AddWithValue("p_address_gid", "0");
                cmd.Parameters.AddWithValue("p_passport_number", httpRequest.Form["passport_number"]);
                cmd.Parameters.AddWithValue("p_national_id", httpRequest.Form["national_id"]);
                cmd.Parameters.AddWithValue("p_doj", httpRequest.Form["doj"]);
                //cmd.Parameters.AddWithValue("p_upload_documents","");
                cmd.Parameters.AddWithValue("p_branch_gid", httpRequest.Form["branch_gid"]);
                cmd.Parameters.AddWithValue("p_branch_name", httpRequest.Form["branch_name"]);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    Getdocument.status = true;
                    Getdocument.message = "Employee updated Successfully!";
                }
                else
                {
                    Getdocument.status = false;
                    Getdocument.message = "Error Occured While updating Employee!";
                }
            }
            catch (Exception ex)
            {
                Getdocument.status = false;
                Getdocument.message = "Error Occured While updating Employee!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Getdocument;
        }
        public Employeemodel employeestatus(Employeelist val, string user_gid)
        {
            Employeemodel employee = new Employeemodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_employeestatus");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_active_status", val.active_status);
                cmd.Parameters.AddWithValue("p_user_gid", val.user_gid);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                
                //if(con.State != System.Data.ConnectionState.Closed
                //cmd.Connection.Dispose();

                if (mnresult == 1)
                {
                    employee.status = true;
                    employee.message = "Status updated succesfully";
                }
                else
                {
                    employee.status = false;
                    employee.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                employee.status = false;
                employee.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    
                }
            }
            return employee;
        }
        public Employeemodel employeepswreset(Employeedetail val, string user_gid)
        {            
            try
            {
                CmnFunctions objcmnfunctions = new CmnFunctions();
                //cmd = new MySqlCommand("sp_sel_employeepswreset");
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_user_gid", val.userid);
                //cmd.Parameters.AddWithValue("p_password", objcmnfunctions.passwordencryption(val.emppassword));
                //rd = DBAccess.ExecuteReader(cmd);
                //if (rd.Read())
                //{







                    cmd = new MySqlCommand("sp_upt_employeepswreset");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                    cmd.Parameters.AddWithValue("p_password", objcmnfunctions.passwordencryption(val.empchangepassword));
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Employee Password updated succesfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While Update the password";
                    }
                //}
                //else
                //{
                //    val.status = false;
                //    val.message = "Enter Correct Old Password";
                //}
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Reset Password";
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

    }
}