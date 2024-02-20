using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OfficeOpenXml;


namespace DataAccess
{
    public class CustomerDBAccess
    {
        DBAccess objcmnfunctions = new DBAccess();
        HttpPostedFile httpPostedFile;
        HttpRequest httpRequest;
        SqlDataReader objDataReader;
        string msSQL, msGIDRs, msGIDRq, msGIDStsEnqDD, msSQLlog, msGIDRslog, msGIDStRqlog;
        int mnresult, mnresultResp, rowCount, columnCount, StsEnqDtlCount = 0, mnresultStsEnqDtl, mnresultSecondary = 0, mnresultlog, mnresultupdate, mnresultL;
        string lspath, endRange, GetMasterGID;
        int insertCount = 0;
        string lsFirstName, lsLastName, lsEmailaddress, lsContactNo, lsAadharID,lsgender,lsdob,lspassportnumber, lsCustomerType, lsCompanyName, lsAddress, lsCountry, lsRemarks, lsVAT;
        DataTable table = new DataTable();
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        string error;
        public Customer GetAll()
        {
            Customer customer = new Customer();
            try
            {
                cmd = new MySqlCommand("sp_sel_customer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Customerlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Customerlist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["cutomer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            email_address = rd["email_address"].ToString(),
                            // national_id = rd["national_id"].ToString(),
                            customer_type = rd["customer_type"].ToString(),
                            billing_address = rd["billing_address"].ToString(),
                            customer_status = rd["customer_status"].ToString()

                        });
                    }
                    customer.customerList = summary;
                    customer.status = true;
                    //rd.Close();

                }
                else
                {
                    customer.status = false;
                    customer.message = "No Records Fund";
                    //rd.Close();
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                customer.status = false;
                customer.message = "Error Occured While Show the Record";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return customer;
        }
        public Customermodel customerstatementsummary(Customerdetails val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_customerdetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    val.customer_firstname = rd["customer_firstname"].ToString();
                    val.contact_number = rd["contact_number"].ToString();
                    rd.Close();
                }

                cmd = new MySqlCommand("sp_sel_customertotaltransactionsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);

                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CustomerstatementSummary>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new CustomerstatementSummary
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
                    val.CustomerstatementSummary = summary;
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
        public Customermodel customerstatementsummarysearch(Customerdetails val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_customerdetails");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    val.customer_firstname = rd["customer_firstname"].ToString();
                    val.contact_number = rd["contact_number"].ToString();
                    rd.Close();
                }

                cmd = new MySqlCommand("sp_sel_customertotaltransactionsearch");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);

                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CustomerstatementSummary>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new CustomerstatementSummary
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
                    val.CustomerstatementSummary = summary;
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
        public Customermodel Add(Customerdetails val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_ins_customer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_firstname", val.customer_firstname);
                cmd.Parameters.AddWithValue("p_customer_lastname", val.customer_lastname);


                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_national_id", val.national_id);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                cmd.Parameters.AddWithValue("p_billing_companyname", val.billing_companyname);
                cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                cmd.Parameters.AddWithValue("p_billing_country", val.billing_country);
                cmd.Parameters.AddWithValue("p_billing_remarks", val.billing_remarks);
                cmd.Parameters.AddWithValue("p_credit_limit", val.credit_limit);
                cmd.Parameters.AddWithValue("p_mailing_companyname", val.mailing_companyname);
                cmd.Parameters.AddWithValue("p_mailing_address", val.mailing_address);
                cmd.Parameters.AddWithValue("p_mailing_country", val.mailing_country);
                cmd.Parameters.AddWithValue("p_mailing_remarks", val.mailing_remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_customer_gid", "");
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
                    val.message = "Error Occured While Adding Customer";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Adding Customer";
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
        public Customermodel Status(Customerdetails val, string user_gid)
        {
            Customermodel customer = new Customermodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_customerstatus");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_customer_status", val.customer_status);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    customer.status = true;
                    customer.message = "Customer updated succesfully";
                }
                else
                {
                    customer.status = false;
                    customer.message = "Error Occured While Updating Customer!";
                }
            }
            catch (Exception ex)
            {
                customer.status = false;
                customer.message = "Error Occured While Updating Customer!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return customer;
        }
        public Customerdetails Get(string val)
        {
            Customerdetails Customer = new Customerdetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_customeredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Customer.customer_firstname = rd["customer_firstname"].ToString();
                    Customer.customer_lastname = rd["customer_lastname"].ToString();
                    Customer.email_address = rd["email_address"].ToString();
                    Customer.contact_number = rd["contact_number"].ToString();
                    Customer.national_id = rd["national_id"].ToString();
                    Customer.gender = rd["gender"].ToString();
                    Customer.dob = rd["dob"].ToString();
                    Customer.passport_number = rd["passport_number"].ToString();

                    Customer.customer_type = rd["customer_type"].ToString();
                    Customer.billing_companyname = rd["billing_companyname"].ToString();
                    Customer.billing_address = rd["billing_address"].ToString();
                    Customer.billing_country = rd["billing_country"].ToString();
                    Customer.credit_limit = int.Parse(rd["credit_limit"].ToString());
                    Customer.billing_remarks = rd["billing_remarks"].ToString();
                    Customer.mailing_companyname = rd["mailing_companyname"].ToString();
                    Customer.mailing_address = rd["mailing_address"].ToString();
                    Customer.mailing_companyname = rd["mailing_companyname"].ToString();
                    Customer.mailing_country = rd["mailing_country"].ToString();
                    Customer.mailing_remarks = rd["mailing_remarks"].ToString();
                    Customer.status = true;
                    //rd.Close();
                }
                else
                {
                    Customer.status = false;
                    Customer.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                Customer.status = false;
                Customer.message = "No Records found!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Customer;
        }
        public Customermodel Update(Customerdetails val, string user_gid)
        {
            Customermodel customer = new Customermodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_customeredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_customer_firstname", val.customer_firstname);
                cmd.Parameters.AddWithValue("p_customer_lastname", val.customer_lastname);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_contact_number", val.contact_number);
                cmd.Parameters.AddWithValue("p_national_id", val.national_id);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                cmd.Parameters.AddWithValue("p_billing_companyname", val.billing_companyname);
                cmd.Parameters.AddWithValue("p_billing_address", val.billing_address);
                cmd.Parameters.AddWithValue("p_billing_country", val.billing_country);
                cmd.Parameters.AddWithValue("p_credit_limit", val.credit_limit);
                cmd.Parameters.AddWithValue("p_billing_remarks", val.billing_remarks);
                cmd.Parameters.AddWithValue("p_mailing_companyname", val.mailing_companyname);
                cmd.Parameters.AddWithValue("p_mailing_address", val.mailing_address);
                cmd.Parameters.AddWithValue("p_mailing_country", val.mailing_country);
                cmd.Parameters.AddWithValue("p_mailing_remarks", val.mailing_remarks);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    customer.status = true;
                    customer.message = "Customer updated succesfully";
                }
                else
                {
                    customer.status = false;
                    customer.message = "Error Occured While Updating Customer!";
                }
            }
            catch (Exception ex)
            {
                customer.status = false;
                customer.message = "Error Occured While Updating Customer!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return customer;
        }
        public string uploadFile(string path, string file_name)
        {
            int iUploadedCnt = 0;
            string sPath = "";
            //    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/locker/");
            sPath = path;
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (!File.Exists(sPath + file_name))
                    {
                        hpf.SaveAs(sPath + file_name);
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }
        }
        //Excel Upload Do Debit Request  
        //Excel Upload Do Debit Request  
        public Customermodel Excel(string lscompany_code,HttpRequest httpreq, Customerdetails val, string user_gid)
        {
            Customermodel customer = new Customermodel();
           
            string lsfilePath = string.Empty;


            HttpFileCollection httpFileCollection; DataTable dt = null;
            //Customerdetails GetdocumentImportExcel = new Customerdetails();
            try
            {
                //cmd = " select company_code from sys_mst_tcompany";
                //lscompany_code = DBAccess.GetExecuteScalar(cmd);
                lsfilePath = HttpContext.Current.Server.MapPath("../../erp_documents" + "/" + lscompany_code + "/TTS/CustomerImportExcel/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                
                    if ((!System.IO.Directory.Exists(lsfilePath)))
                        System.IO.Directory.CreateDirectory(lsfilePath);
             
                // Create Directory
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                httpFileCollection = httpreq.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                
                string lsfile_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "\\" + httpPostedFile.FileName;
                //FileStream file = new FileStream(FileMode.Create, FileAccess.Write);
               // ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {

                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["Sheet1"];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }
                //file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath);

                //Excel To DataTable

                lsfilePath = @"" + lspath.Replace("/", "\\") + "\\";
               
                var excelRange = "A1:" + endRange;
                try
                {
                    dt = objcmnfunctions.ExcelToDataTable(lspath, httpPostedFile.FileName, excelRange, FileExtension);

                    foreach (DataRow row in dt.Rows)
                    {
                        lsFirstName = row["First Name"].ToString();
                        lsLastName = row["Last Name"].ToString();
                        lsEmailaddress = row["Email address"].ToString();
                        lsContactNo = row["Contact No"].ToString();
                        lsAadharID = row["National ID"].ToString();
                        lsgender = row["Gender"].ToString();
                        lsdob = row["Date of Birth"].ToString();
                        lspassportnumber = row["Passport No"].ToString();

                        lsCustomerType = row["Customer Type"].ToString();

                        lsCompanyName = row["Company Name"].ToString();
                        lsAddress = row["Address"].ToString();
                        lsCountry = row["Country"].ToString();
                        lsRemarks = row["Remarks"].ToString();
                        lsVAT = row["VAT"].ToString();
                        
                        cmd = new MySqlCommand("sp_ins_customerimportexcel");

                        cmd.Parameters.AddWithValue("p_customer_firstname", lsFirstName);
                        cmd.Parameters.AddWithValue("p_customer_lastname", lsLastName);
                        cmd.Parameters.AddWithValue("p_email_address", lsEmailaddress);
                        cmd.Parameters.AddWithValue("p_contact_number", lsContactNo);
                        cmd.Parameters.AddWithValue("p_national_id", lsAadharID);
                        cmd.Parameters.AddWithValue("p_gender", lsgender);
                        cmd.Parameters.AddWithValue("p_dob", lsdob);
                        cmd.Parameters.AddWithValue("p_passport_number", lspassportnumber);
                        cmd.Parameters.AddWithValue("p_customer_type", lsCustomerType);
                        cmd.Parameters.AddWithValue("p_billing_companyname", lsCompanyName);
                        cmd.Parameters.AddWithValue("p_billing_address", lsAddress);
                        cmd.Parameters.AddWithValue("p_billing_country", lsCountry);
                        cmd.Parameters.AddWithValue("p_credit_limit", lsVAT);
                        cmd.Parameters.AddWithValue("p_billing_remarks", lsRemarks);
                        cmd.Parameters.AddWithValue("p_mailing_companyname", lsCompanyName);
                        cmd.Parameters.AddWithValue("p_mailing_address", lsAddress);
                        cmd.Parameters.AddWithValue("p_mailing_country", lsCountry);
                        cmd.Parameters.AddWithValue("p_mailing_remarks", lsRemarks);
                        cmd.Parameters.AddWithValue("p_created_by", "");
                        cmd.Parameters.AddWithValue("p_customer_gid", "");





                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                       

                        if (mnresult != 0)
                        {
                            insertCount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    val.message = "DataTable Conversion" + ex.ToString();
                    val.message = "DataTable Conversion" + ex.ToString();
                }
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "Excel Uploaded Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Error Occured While uploading Excel!";
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = ex.ToString();
            }
            return customer;
        }
    }
}

