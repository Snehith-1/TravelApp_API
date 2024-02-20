using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class DocumentDBAccess
    {
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        int mnresult = 0;
        string error;
        public Document GetAll()
        {
            Document Document = new Document();
            try
            {
                cmd = new MySqlCommand("sp_sel_document");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<DocumentList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new DocumentList
                        {
                            document_gid = rd["document_gid"].ToString(),
                            document_name = rd["document_name"].ToString(),
                            document_type = rd["document_type"].ToString(),
                            next_renewaldate = rd["next_renewaldate"].ToString(),
                            reminder_date = rd["reminder_date"].ToString()
                        });
                    }
                    Document.DocumentList = summary;
                    Document.status = true;
                    
                }
                else
                {
                    Document.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                Document.status = false;
                Document.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Document;
        }

        public Documentmodel GetdocumentUploadExcel(string company_code, HttpRequest httpRequest, string usergid)
        {
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
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
                        GetdocumentImportExcel.upload_documents = HttpContext.Current.Server.MapPath("../../"+company_code+ConfigurationManager.AppSettings["document_file_path"].ToString() + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(GetdocumentImportExcel.upload_documents);
                    }
                    if (File.Exists(GetdocumentImportExcel.upload_documents))
                    {
                        cmd = new MySqlCommand("sp_ins_document");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_document_name", httpRequest.Form["document_name"]);
                        cmd.Parameters.AddWithValue("p_document_type", httpRequest.Form["document_type"]);
                        cmd.Parameters.AddWithValue("p_next_renewaldate", httpRequest.Form["next_renewaldate"]);
                        cmd.Parameters.AddWithValue("p_reminder_date", httpRequest.Form["reminder_date"]);
                        cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
                        cmd.Parameters.AddWithValue("p_uploaded_by", usergid);                       
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            GetdocumentImportExcel.status = true;
                            GetdocumentImportExcel.message = "File Uploaded Successfully!";
                        }
                        else
                        {
                            GetdocumentImportExcel.status = false;
                            GetdocumentImportExcel.message = "Error Occured While uploading file!";
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
                    GetdocumentImportExcel.status = false;
                    GetdocumentImportExcel.message = "Error Occured While uploading file!";
                }
            }
            catch (Exception ex)
            {
                GetdocumentImportExcel.status = false;
                GetdocumentImportExcel.message = "Error Occured While uploading file!";
                error = ex.ToString();
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
        public DocumentDetail Reminder(string val)
        {
            DocumentDetail Document = new DocumentDetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_documentreminder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_documentgid", val);                
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Document.reminder_date = rd["reminder_date"].ToString();
                    Document.next_renewaldate = rd["next_renewaldate"].ToString();
                    Document.status = true;
                    
                }

                else
                {
                    Document.status = false;
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                Document.status = false;
                Document.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Document;
        }
        public Documentmodel Add(DocumentDetail val, string userGid)
        {
            Documentmodel document = new Documentmodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_documentreminder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_document_gid", val.document_gid);
                cmd.Parameters.AddWithValue("p_reminder_type", val.reminder_type);
                cmd.Parameters.AddWithValue("p_reminder_date", val.reminder_date);
                cmd.Parameters.AddWithValue("p_sms", val.sms);
                cmd.Parameters.AddWithValue("p_email_address", val.email_address);
                cmd.Parameters.AddWithValue("p_created_by", userGid);                
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    document.status = true;
                    document.message = "Records added sucessfully";
                }
                else
                {
                    document.status = false;
                    document.message = "Error Occured While Inserting Reminder";
                }
            }
            catch (Exception ex)
            {
                document.status = false;
                document.message = "Error Occured While Inserting Reminder";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return document;
        }
        public DocumentDetail Documentpath(DocumentDetail val)
        {                        
            try
            {
                cmd = new MySqlCommand("sp_sel_documentdwld");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_document_gid", val.document_gid);                               
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.upload_documents = rd["upload_documents"].ToString();
                    val.status = true;
                    
                }
                else
                {
                    val.upload_documents = "Not Detected";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.upload_documents = "Not Detected";
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

        public DocumentDetail edit(DocumentDetail val)
        {
            DocumentDetail document = new DocumentDetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_documentedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_document_gid", val.document_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    document.document_name = rd["document_name"].ToString();
                    document.document_type = rd["document_type"].ToString();
                    document.next_renewaldate = rd["next_renewaldate"].ToString();
                    document.reminder_date = rd["reminder_date"].ToString();
                    document.upload_documents = rd["upload_documents"].ToString();
                    var summary = new List<DocumentList>();
                    summary.Add(new DocumentList
                        {
                            document_gid = val.document_gid,
                            document_name = document.document_name,
                            document_type = document.document_type,
                            next_renewaldate = document.next_renewaldate,
                            reminder_date = document.reminder_date

                    });

                    document.DocumentList = summary;
                    
                }
                else
                {
                    document.status = false;
                    document.message = "No Records found!";
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                document.status = false;
                document.message = "No Records found!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return document;
        }
        public Documentmodel documentupdate(string company_code, HttpRequest httpRequest, string usergid)
        {
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
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
                        GetdocumentImportExcel.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["document_file_path"].ToString() + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(GetdocumentImportExcel.upload_documents);
                    }
                    if (File.Exists(GetdocumentImportExcel.upload_documents))
                    {
                        cmd = new MySqlCommand("sp_upt_document");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_document_gid", httpRequest.Form["document_gid"]);
                        cmd.Parameters.AddWithValue("p_document_name", httpRequest.Form["document_name"]);
                        cmd.Parameters.AddWithValue("p_document_type", httpRequest.Form["document_type"]);
                        cmd.Parameters.AddWithValue("p_next_renewaldate", httpRequest.Form["next_renewaldate"]);
                        cmd.Parameters.AddWithValue("p_reminder_date", httpRequest.Form["reminder_date"]);
                        cmd.Parameters.AddWithValue("p_upload_documents", GetdocumentImportExcel.upload_documents);
                        cmd.Parameters.AddWithValue("p_uploaded_by", usergid);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            GetdocumentImportExcel.status = true;
                            GetdocumentImportExcel.message = "File Uploaded Successfully!";
                        }
                        else
                        {
                            GetdocumentImportExcel.status = false;
                            GetdocumentImportExcel.message = "Error Occured While uploading file!";
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
                    cmd = new MySqlCommand("sp_upt_document");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_document_gid", httpRequest.Form["document_gid"]);
                    cmd.Parameters.AddWithValue("p_document_name", httpRequest.Form["document_name"]);
                    cmd.Parameters.AddWithValue("p_document_type", httpRequest.Form["document_type"]);
                    cmd.Parameters.AddWithValue("p_next_renewaldate", httpRequest.Form["next_renewaldate"]);
                    cmd.Parameters.AddWithValue("p_reminder_date", httpRequest.Form["reminder_date"]);
                    cmd.Parameters.AddWithValue("p_upload_documents", "");
                    cmd.Parameters.AddWithValue("p_uploaded_by", usergid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        GetdocumentImportExcel.status = true;
                        GetdocumentImportExcel.message = "Renewal Updated Successfully Without Document";
                    }
                }
            }
            catch (Exception ex)
            {
                GetdocumentImportExcel.status = false;
                GetdocumentImportExcel.message = "Error Occured While uploading file!";
                error = ex.ToString();
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
        public Documentmodel salesUploadDocument(string company_code, HttpRequest httpRequest, string usergid)
        {
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
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
                        GetdocumentImportExcel.upload_documents = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["document_file_path"].ToString() + httpPostedFile.FileName);
                        httpPostedFile.SaveAs(GetdocumentImportExcel.upload_documents);
                    }
                    if (File.Exists(GetdocumentImportExcel.upload_documents))
                    {
                        cmd = new MySqlCommand("sp_ins_salesdocument");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_document_name", httpRequest.Form["document_name"]);
                        cmd.Parameters.AddWithValue("p_remarks", httpRequest.Form["remarks"]);
                        cmd.Parameters.AddWithValue("p_salesorder_gid", httpRequest.Form["salesorder_gid"]);                       
                        cmd.Parameters.AddWithValue("p_document_path", GetdocumentImportExcel.upload_documents);
                        cmd.Parameters.AddWithValue("p_uploaded_by", usergid);
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        if (mnresult == 1)
                        {
                            GetdocumentImportExcel.status = true;
                            GetdocumentImportExcel.message = "File Uploaded Successfully!";
                        }
                        else
                        {
                            GetdocumentImportExcel.status = false;
                            GetdocumentImportExcel.message = "Error Occured While uploading file!";
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
                    GetdocumentImportExcel.status = false;
                    GetdocumentImportExcel.message = "Error Occured While uploading file!";
                }
            }
            catch (Exception ex)
            {
                GetdocumentImportExcel.status = false;
                GetdocumentImportExcel.message = "Error Occured While uploading file!";
                error = ex.ToString();
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
        public Document salesdocumentsummary(DocumentDetail val)
        {
            Document Document = new Document();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesdocument");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<DocumentList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new DocumentList
                        {
                            document_gid = rd["salesdocument_gid"].ToString(),
                            document_name = rd["document_name"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            upload_documents = rd["document_path"].ToString(),
                            uploaded_by = rd["uploaded_by"].ToString(),
                            uploaded_date =DateTime.Parse( rd["uploaded_date"].ToString()),
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                        });
                    }
                    Document.DocumentList = summary;
                    Document.status = true;
                   
                }
                else
                {
                    Document.status = false;
                    
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                Document.status = false;
                Document.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Document;
        }
        public Documentmodel salesdocumentdelete(string  values)
        {
            Documentmodel delete = new Documentmodel();
            try
            {
                cmd = new MySqlCommand("sp_del_salesdocument");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesdocument_gid", values);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    delete.status = true;                   
                }
                else
                {
                    delete.status = false;               
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
        public DocumentDetail salesDocumentpath(DocumentDetail val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_salesdocumentdwld");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_document_gid", val.document_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.document_path = rd["document_path"].ToString();
                    val.status = true;
                    
                }
                else
                {
                    val.upload_documents = "Not Detected";
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                val.upload_documents = "Not Detected";
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


           
