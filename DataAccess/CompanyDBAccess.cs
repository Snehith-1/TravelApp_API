using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Configuration;
using System.IO;

namespace DataAccess
{
    public class CompanyDBAccess
    {
        MySqlCommand cmd = null;
        int mnresult = 0;
        MySqlDataReader rd;
        string error;
        string lspath;
        string lslogo_path;
        public companydetails get(string company_gid, string company_code)
        {
            companydetails company = new companydetails();
         
            try
            {

                using (var client = new HttpClient())
                {
                    var host = HttpContext.Current.Request.Url.Host;
                    var port = Convert.ToString(HttpContext.Current.Request.Url.Port);
                    var uri_builder = "http://" + host + ":" + port;

                    cmd = new MySqlCommand("sp_sel_company");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_company_gid", company_gid);
                    //cmd.Connection = con;
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.Read())
                    {
                        company.company_gid = int.Parse(rd["company_gid"].ToString());
                        company.company_code = rd["company_code"].ToString();
                        company.company_name = rd["company_name"].ToString();
                        company.company_contact_number = rd["company_contact_number"].ToString();
                        company.company_email_address = rd["company_email_address"].ToString();
                        company.contact_person = rd["contact_person"].ToString();
                        company.license = rd["licence"].ToString();
                        company.auth_code = rd["auth_code"].ToString();
                        company.currency_code = rd["currency_code"].ToString();
                        company.company_address = rd["company_address"].ToString();
                        company.smscredits = int.Parse(rd["smscredits"].ToString());
                        company.country_name = rd["country_name"].ToString();
                        company.fax = rd["fax"].ToString();
                        company.fin_yearstart = rd["fin_yearstart"].ToString();
                        company.company_website = rd["company_website"].ToString();
                        company.sequence_reset = rd["sequence_reset"].ToString();
                        company.employer_code = rd["employer_code"].ToString();
                        company.welcomelogo = uri_builder + '/' + company_code + ConfigurationManager.AppSettings["welcome_file_path"].ToString() + rd["welcome_logo_filename"].ToString();
                        company.companylogo = uri_builder + '/' + company_code + ConfigurationManager.AppSettings["company_file_path"].ToString() + rd["company_logo_filename"].ToString();
                        company.letterhead_logo = uri_builder + '/' + company_code + ConfigurationManager.AppSettings["Letterhead_file_path"].ToString() + rd["letterhead_logo_filename"].ToString();
                        //company.welcomelogo = HttpContext.Current.Server.MapPath(rd["welcome_logo"].ToString());
                        //company.companylogo = HttpContext.Current.Server.MapPath(rd["company_logo"].ToString());
                        //company.letterhead_logo= HttpContext.Current.Server.MapPath(rd["letterhead_logo"].ToString());
                        rd.Close();
                        cmd.Connection.Close();
                        var country = new List<CurrencyList>();
                        cmd = new MySqlCommand("sp_sel_currency");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd1.Connection = con;
                        rd = DBAccess.ExecuteReader(cmd);
                        if (rd.HasRows == true)
                        {
                            while (rd.Read())
                            {
                                country.Add(new CurrencyList
                                {
                                    currency_code = rd["currency_code"].ToString(),
                                    currency_name = rd["currency_name"].ToString(),
                                    country_code = rd["country_code"].ToString(),
                                    country_name = rd["country_name"].ToString()
                                });
                            }
                            company.CurrencyList = country;
                            company.status = true;

                        }
                    }
                    else
                    {
                        company.status = false;
                        company.message = "Error while showing records";
                    }
                    rd.Close();

                }


            }
            catch (Exception ex)
            {
                company.status = false;
                company.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return company;
        }

        public companymodel Update(companydetails val, string user_gid)
        {
            companymodel comp = new companymodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_company");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_company_gid", val.company_gid);
                cmd.Parameters.AddWithValue("p_company_code", val.company_code);
                cmd.Parameters.AddWithValue("p_company_name", val.company_name);
                cmd.Parameters.AddWithValue("p_company_address", val.company_address);
                cmd.Parameters.AddWithValue("p_company_contact_number", val.company_contact_number);
                cmd.Parameters.AddWithValue("p_company_email_address", val.company_email_address);
                cmd.Parameters.AddWithValue("p_contact_person", val.contact_person);
                cmd.Parameters.AddWithValue("p_licence", val.license);
                cmd.Parameters.AddWithValue("p_auth_code", val.auth_code);
                cmd.Parameters.AddWithValue("p_currency_code", val.currency_code);
                cmd.Parameters.AddWithValue("p_expiry_date", val.expiry_date);
                cmd.Parameters.AddWithValue("p_sequence_reset", val.sequence_reset);
                cmd.Parameters.AddWithValue("p_company_website", val.company_website);
                cmd.Parameters.AddWithValue("p_country_name", val.country_name);
                cmd.Parameters.AddWithValue("p_fin_yearstart", val.fin_yearstart);
                cmd.Parameters.AddWithValue("p_fax", val.fax);
                cmd.Parameters.AddWithValue("p_smscredits", val.smscredits);
                cmd.Parameters.AddWithValue("p_employer_code", val.employer_code);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    comp.status = true;
                    comp.message = "Records updated succesfully";
                }
                else
                {
                    comp.status = false;
                    comp.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                comp.status = false;
                comp.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return comp;
        }

        public companymodel getcompanylogoupload(string company_gid, HttpRequest httpRequest, string user_gid, string company_code)
        {
            companydetails Getdcompanylogo = new companydetails();
        
            try
            {
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
                        Getdcompanylogo.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["company_file_path"].ToString() + "/" + httpPostedFile.FileName);
                        lslogo_path= ("../../" + company_code + ConfigurationManager.AppSettings["company_file_path"].ToString() +  httpPostedFile.FileName);
                        lspath = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["company_file_path"].ToString());
                        if (!System.IO.Directory.Exists(lspath))
                        {
                            System.IO.Directory.CreateDirectory(lspath);
                        }
                        httpPostedFile.SaveAs(Getdcompanylogo.upload_documents);
                        Getdcompanylogo.company_logo_filename = httpPostedFile.FileName;

                        if (File.Exists(Getdcompanylogo.upload_documents))
                        {
                            cmd = new MySqlCommand("sp_upt_companylogo");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_company_gid", company_gid);
                            cmd.Parameters.AddWithValue("p_company_logo", lslogo_path);
                            cmd.Parameters.AddWithValue("p_company_logo_filename", Getdcompanylogo.company_logo_filename);
                            cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                Getdcompanylogo.status = true;
                                Getdcompanylogo.message = "Company Logo Uploaded Successfully!";
                            }
                            else
                            {
                                Getdcompanylogo.status = false;
                                Getdcompanylogo.message = "Error Occured While uploading Company Logo!";
                            }

                        }
                        else
                        {
                            Getdcompanylogo.status = false;
                            Getdcompanylogo.message = "Error Occured While uploading Company Logo!";
                        }
                    }
                }
                else
                {
                    Getdcompanylogo.status = false;
                    Getdcompanylogo.message = "Error Occured While uploading Company Logo!";
                }
            }
            catch (Exception ex)
            {
                Getdcompanylogo.status = false;
                Getdcompanylogo.message = "Error Occured While uploading Company Logo!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Getdcompanylogo;
        }

        public companymodel getwelcomelogoupload(string company_gid, HttpRequest httpRequest, string user_gid, string company_code)
        {
            companydetails Getwelcomelogo = new companydetails();
            
            try
            {
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
                        Getwelcomelogo.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["welcome_file_path"].ToString() + "/" + httpPostedFile.FileName);
                        lslogo_path = ("../../" + company_code + ConfigurationManager.AppSettings["welcome_file_path"].ToString() + httpPostedFile.FileName);
                        lspath = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["welcome_file_path"].ToString());
                        if (!System.IO.Directory.Exists(lspath))
                        {
                            System.IO.Directory.CreateDirectory(lspath);
                        }
                        httpPostedFile.SaveAs(Getwelcomelogo.upload_documents);
                        Getwelcomelogo.welcome_logo_filename = httpPostedFile.FileName;

                        if (File.Exists(Getwelcomelogo.upload_documents))
                        {
                            cmd = new MySqlCommand("sp_upt_welcomelogo");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_company_gid", company_gid);
                            cmd.Parameters.AddWithValue("p_welcome_logo", lslogo_path);
                            cmd.Parameters.AddWithValue("p_welcome_logo_filename", Getwelcomelogo.welcome_logo_filename);
                            cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                Getwelcomelogo.status = true;
                                Getwelcomelogo.message = "Welcome Logo Uploaded Successfully!";
                            }
                            else
                            {
                                Getwelcomelogo.status = false;
                                Getwelcomelogo.message = "Error Occured While uploading Welcome Logo!";
                            }
                        }
                        else
                        {
                            Getwelcomelogo.status = false;
                            Getwelcomelogo.message = "Error Occured While uploading Welcome Logo!";
                        }
                    }

                }
                else
                {
                    Getwelcomelogo.status = false;
                    Getwelcomelogo.message = "Error Occured While uploading Welcome Logo!";
                }
            }
            catch (Exception ex)
            {
                Getwelcomelogo.status = false;
                Getwelcomelogo.message = "Error Occured While uploading Welcome Logo!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Getwelcomelogo;
        }
        public companymodel getletterheadupload(string company_gid, HttpRequest httpRequest, string user_gid, string company_code)
        {
            companydetails getletterheadupload = new companydetails();
          
            try
            {
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
                        getletterheadupload.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["Letterhead_file_path"].ToString() + "/" + httpPostedFile.FileName);
                        lslogo_path = ("../../" + company_code + ConfigurationManager.AppSettings["Letterhead_file_path"].ToString()  + httpPostedFile.FileName);
                        lspath = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["Letterhead_file_path"].ToString());
                        if (!System.IO.Directory.Exists(lspath))
                        {
                            System.IO.Directory.CreateDirectory(lspath);
                        }

                        httpPostedFile.SaveAs(getletterheadupload.upload_documents);
                        getletterheadupload.letterhead_logo_filename = httpPostedFile.FileName;

                        if (File.Exists(getletterheadupload.upload_documents))
                        {
                            cmd = new MySqlCommand("sp_upt_letterheadlogo");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_company_gid", company_gid);
                            cmd.Parameters.AddWithValue("p_letterhead_logo", lslogo_path);
                            cmd.Parameters.AddWithValue("p_letterhead_logo_filename", getletterheadupload.letterhead_logo_filename);
                            cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                getletterheadupload.status = true;
                                getletterheadupload.message = "LetterHead Logo Uploaded Successfully!";
                            }
                            else
                            {
                                getletterheadupload.status = false;
                                getletterheadupload.message = "Error Occured While uploading Letter head Logo!";
                            }
                        }
                        else
                        {
                            getletterheadupload.status = false;
                            getletterheadupload.message = "Error Occured While uploading Letter Head Logo!";
                        }
                    }

                }
                else
                {
                    getletterheadupload.status = false;
                    getletterheadupload.message = "Error Occured While uploading Letter Head Logo!";
                }
            }
            catch (Exception ex)
            {
                getletterheadupload.status = false;
                getletterheadupload.message = "Error Occured While uploading Letter Head Logo!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return getletterheadupload;
        }
    }
}