using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataAccess
{
    public class MailManagementDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        CmnFunctions objcmnfunction = new CmnFunctions();
        public MailManagement GetAll()
        {
            MailManagement Mailmanagement = new MailManagement();
            try
            {
                cmd = new MySqlCommand("sp_sel_mailmanagement");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<MailManagementList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new MailManagementList
                        {

                            mailmanagement_gid = Convert.ToInt16(rd["mailmanagement_gid"].ToString()),
                            mailmanagement_code = rd["mailmanagement_code"].ToString(),
                            mailmanagement_name = rd["mailmanagement_name"].ToString(),
                            mailmanagement_message = rd["mailmanagement_message"].ToString(),

                        });
                    }
                    Mailmanagement.MailManagementList = summary;
                    Mailmanagement.status = true;
                    rd.Close();

                }

                else
                {
                    Mailmanagement.status = false;

                }
                rd.Close();
            }
            catch (Exception)
            {
                Mailmanagement.status = false;
                Mailmanagement.message = "Internal error occured";
                //Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Mailmanagement;
        }

        public MailManagement Mailservice(List<customerlist> customer_gid, int mailmanagement_gid)
        {
            string mailmanagement_name = "";
            string mailmanagement_message = "";
            string upload_documents = "";
            int mailSendResult = 0;

            string customer_email = "";
            MailManagement Mailmanagement = new MailManagement();
            try
            {
                cmd = new MySqlCommand("sp_sel_Mailmanagementedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_gid", mailmanagement_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    mailmanagement_name = rd["mailmanagement_name"].ToString();
                    mailmanagement_message = rd["mailmanagement_message"].ToString();
                    upload_documents = rd["upload_documents"].ToString();

                }
                rd.Close();
                for (int i = 0; i < customer_gid.Count; i++)
                {

                    cmd = new MySqlCommand("sp_sel_getcustomermailid");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("customers_gid", customer_gid[i].flag);
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<MailManagementList>();
                    if (rd.HasRows == true)
                    {
                        if (rd.Read())
                        {
                            customer_email = rd["email_address"].ToString();
                            //System.Net.Mail.Attachment attachment;
                            //attachment = new System.Net.Mail.Attachment(upload_documents);
                            //attachment.Name = upload_documents;
                            //mailmanagement_message.Attachments.Add(attachment);
                            mailSendResult =  objcmnfunction.SendSMTP("no-reply@vcidex.com", customer_email, mailmanagement_name, mailmanagement_message, "", upload_documents);
                        }//Info@vcidex.com
                        if(mailSendResult == 1)
                        {
                            //Insert Log SP
                            cmd = new MySqlCommand("sp_ins_mailmanagelog");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_mailmanagement_name", mailmanagement_name);
                            cmd.Parameters.AddWithValue("p_mailmanagement_message", mailmanagement_message);
                            cmd.Parameters.AddWithValue("p_customer_email", customer_email);
                            cmd.Parameters.AddWithValue("p_upload_documents", upload_documents);
                            rd = DBAccess.ExecuteReader(cmd);

                            Mailmanagement.status = true;
                        }
                        else
                        {
                            Mailmanagement.status = false;
                        }
                       
                    }
                    else
                    {
                        Mailmanagement.status = false;

                    }
                    rd.Close();
                }
            }



            catch (Exception e)
            {
                Mailmanagement.status = false;
                Mailmanagement.message = "Internal Error Occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Mailmanagement;
        }
        //public MailManagementmodel Mailservice(string company_code, HttpRequest httpRequest, string mailmanagement_gid)
        //public void Mailservice(List<customerlist> customer_gid, int mailmanagement_gid, string company_code, HttpRequest httpRequest)
        //{
        //    string mailmanagement_name = "";
        //    string mailmanagement_message = "";

        //    string customer_email = "";
        //    MailManagement Mailmanagement = new MailManagement();
        //    MailManagementdetail GetdocumentImportExcel = new MailManagementdetail();
        //    try
        //    {

        //        HttpPostedFile httpPostedFile;
        //        HttpFileCollection httpFileCollection;
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            httpFileCollection = httpRequest.Files;
        //            for (int i = 0; i < httpFileCollection.Count; i++)
        //            {
        //                httpPostedFile = httpFileCollection[i];
        //                string FileExtension = httpPostedFile.FileName;
        //                FileExtension = Path.GetExtension(FileExtension).ToLower();
        //                GetdocumentImportExcel.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["document_file_path"].ToString() + httpPostedFile.FileName);
        //                httpPostedFile.SaveAs(GetdocumentImportExcel.upload_documents);
        //            }
        //            for (int i = 0; i < customer_gid.Count; i++)
        ////        {

        ////            cmd = new MySqlCommand("sp_sel_getcustomermailid");
        ////            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        ////            cmd.Parameters.AddWithValue("customers_gid", customer_gid[0].flag);
        ////            rd = DBAccess.ExecuteReader(cmd);
        ////            var summary = new List<MailManagementList>();
        ////            if (rd.HasRows == true)
        ////            {
        ////                if (rd.Read())
        ////                {
        ////                    customer_email = rd["email_address"].ToString();
        ////                }//Info@vcidex.com
        ////                Mailmanagement.status = true;
        ////            }
        ////            else
        ////            {
        ////                Mailmanagement.status = false;

        ////            }
        ////            rd.Close();
        ////        }
        ////    }
        //                cmd = new MySqlCommand("sp_sel_Mailmanagementedit");
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_activity_gid", mailmanagement_gid);
        //                rd = DBAccess.ExecuteReader(cmd);
        //                if (rd.Read())
        //                {
        //                    cmd = new MySqlCommand("sp_ins_mailmanage");
        //                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                    cmd.Parameters.AddWithValue("p_mailmanagement_code", httpRequest.Form["mailmanagement_code"]);
        //                    cmd.Parameters.AddWithValue("p_mailmanagement_name", httpRequest.Form["mailmanagement_name"]);
        //                    cmd.Parameters.AddWithValue("p_mailmanagement_message", httpRequest.Form["mailmanagement_message"]);
        //                    cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
        //                    //cmd.Parameters.AddWithValue("p_mailmanagement_code", httpRequest.Form.mailmanagement_code);
        //                    //cmd.Parameters.AddWithValue("p_mailmanagement_name", val.mailmanagement_name);
        //                    //cmd.Parameters.AddWithValue("p_mailmanagement_message", val.mailmanagement_message);
        //                    //cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
        //                    //cmd.Connection = con;
        //                    mnresult = DBAccess.ExecuteNonQuery(cmd);
        //                    if (mnresult == 1)
        //                    {
        //                        GetdocumentImportExcel.status = true;
        //                        GetdocumentImportExcel.message = "Mail added sucessfully";
        //                    }
        //                    else
        //                    {
        //                        GetdocumentImportExcel.status = false;
        //                        GetdocumentImportExcel.message = "Internal error occured";
        //                    }
        //                }
        //                else
        //                {
        //                    GetdocumentImportExcel.status = false;
        //                    GetdocumentImportExcel.message = "Error Occured While uploading file!";
        //                }

        //        }   }


        //    catch (Exception e)
        //    {
        //        GetdocumentImportExcel.status = false;
        //        GetdocumentImportExcel.message = "Internal error occured";
        //        Console.WriteLine("Error: {0}", e);
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    //return GetdocumentImportExcel;
        //}


        public MailManagementdetail Get(int val)
        {
            MailManagementdetail Mailmanagementdetail = new MailManagementdetail();

            try
            {
                cmd = new MySqlCommand("sp_sel_Mailmanagementedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Mailmanagementdetail.mailmanagement_gid = Convert.ToInt16(rd["mailmanagement_gid"].ToString());
                    Mailmanagementdetail.mailmanagement_code = rd["mailmanagement_code"].ToString();
                    Mailmanagementdetail.mailmanagement_name = rd["mailmanagement_name"].ToString();
                    Mailmanagementdetail.mailmanagement_message = rd["mailmanagement_message"].ToString();
                    Mailmanagementdetail.status = true;

                }
                else
                {
                    Mailmanagementdetail.status = false;
                    Mailmanagementdetail.message = "No Records found!";

                }
                rd.Close();
            }

            catch
            {
                Mailmanagementdetail.status = false;
                Mailmanagementdetail.message = "Internal Error Occured!";
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Mailmanagementdetail;
        }
        public MailManagementmodel Add(string company_code, HttpRequest httpRequest, string userGid)
        {
            MailManagementdetail GetdocumentImportExcel = new MailManagementdetail();
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
                        var uri_builder = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["document_file_path"].ToString() + httpPostedFile.FileName);
                        string strPath1 = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                        if (!File.Exists(strPath1))
                        {
                            File.Create(strPath1).Dispose();
                        }
                        using (StreamWriter sw = File.AppendText(strPath1))
                        {
                            sw.WriteLine("=============Error Logging ===========");
                            sw.WriteLine("===========Start============= " + DateTime.Now);
                            sw.WriteLine("Error Message: " + "local");
                            sw.WriteLine("Stack Trace: " + uri_builder);
                            sw.WriteLine("===========End============= " + DateTime.Now);

                        }

                        GetdocumentImportExcel.upload_documents = uri_builder;
                         

                        httpPostedFile.SaveAs(GetdocumentImportExcel.upload_documents);
                    }

                    if (File.Exists(GetdocumentImportExcel.upload_documents))
                    {
                        cmd = new MySqlCommand("sp_ins_mailmanage");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_mailmanagement_code", httpRequest.Form["mailmanagement_code"]);
                        cmd.Parameters.AddWithValue("p_mailmanagement_name", httpRequest.Form["mailmanagement_name"]);
                        cmd.Parameters.AddWithValue("p_mailmanagement_message", httpRequest.Form["mailmanagement_message"]);
                        cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
                        //cmd.Parameters.AddWithValue("p_mailmanagement_code", httpRequest.Form.mailmanagement_code);
                        //cmd.Parameters.AddWithValue("p_mailmanagement_name", val.mailmanagement_name);
                        //cmd.Parameters.AddWithValue("p_mailmanagement_message", val.mailmanagement_message);
                        //cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
                        //cmd.Connection = con;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            GetdocumentImportExcel.status = true;
                            GetdocumentImportExcel.message = "Mail added sucessfully";
                        }
                        else
                        {
                            GetdocumentImportExcel.status = false;
                            GetdocumentImportExcel.message = "Internal error occured";
                        }
                    }
                    else
                    {
                        GetdocumentImportExcel.status = false;
                        GetdocumentImportExcel.message = "Error Occured While uploading file!";
                    }
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_mailmanage");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_mailmanagement_code", httpRequest.Form["mailmanagement_code"]);
                    cmd.Parameters.AddWithValue("p_mailmanagement_name", httpRequest.Form["mailmanagement_name"]);
                    cmd.Parameters.AddWithValue("p_mailmanagement_message", httpRequest.Form["mailmanagement_message"]);
                    cmd.Parameters.AddWithValue("p_upload_documents", "");
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        GetdocumentImportExcel.status = true;
                        GetdocumentImportExcel.message = "Mail added sucessfully";
                    }
                    else
                    {
                        GetdocumentImportExcel.status = false;
                        GetdocumentImportExcel.message = "Internal error occured";
                    }

                }
            }
            catch (Exception e)
            {
                GetdocumentImportExcel.status = false;
                GetdocumentImportExcel.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return GetdocumentImportExcel;
        }
        public MailManagementmodel Update(MailManagementdetail val, string usergid)
        {
            MailManagementmodel Mailmanagement = new MailManagementmodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_mailmanage");
                cmd.Parameters.AddWithValue("p_mailmanagement_gid", Convert.ToInt32(val.mailmanagement_gid));
                cmd.Parameters.AddWithValue("p_mailmanagement_code", val.mailmanagement_code);
                cmd.Parameters.AddWithValue("p_mailmanagement_name", val.mailmanagement_name);
                cmd.Parameters.AddWithValue("p_mailmanagement_message", val.mailmanagement_message);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    Mailmanagement.status = true;
                    Mailmanagement.message = "Mail updated succesfully";
                }
                else
                {
                    Mailmanagement.status = false;
                    Mailmanagement.message = "Internal error occured";
                }
            }
            catch (Exception e)
            {
                Mailmanagement.status = false;
                Mailmanagement.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Mailmanagement;
        }
        public MailManagementmodel Delete(int values)
        {
            MailManagementmodel MailManagementdelete = new MailManagementmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_mailmanage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_mailmanagement_gid", values);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    MailManagementdelete.status = true;
                    MailManagementdelete.message = "Mail deleted successfully";

                }
                else
                {
                    MailManagementdelete.status = false;
                    MailManagementdelete.message = " Internal error occured!";
                }
            }
            catch (Exception e)
            {
                MailManagementdelete.status = false;
                MailManagementdelete.message = " Internal error occured!";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return MailManagementdelete;
        }
    }
}