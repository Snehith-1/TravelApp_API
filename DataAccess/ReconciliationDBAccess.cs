using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;
using System.Data.OleDb;
using System.Globalization;

namespace DataAccess
{
    public class ReconciliationDBAccess
    {
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        int mnresult = 0;
        string error;
        OleDbConnection XLDbConnection;
        OleDbCommand XLDbCommand;
        OleDbDataReader XLDbDataReader;
        string Date = string.Empty;
        string DocumentNo = string.Empty;
        string Ticket = string.Empty;
        string Sector = string.Empty;
        string PaxName = string.Empty;
        DateTime Datetime;
        double SellingPrice = 0.0;
        string type = string.Empty;
        int total_records_file;
        int matching_ticket_records;
        int ticket_matches_with_agent;

        public reconciliation GetAll()
        {
            reconciliation reconciliation = new reconciliation();
            try
            {
                cmd = new MySqlCommand("sp_sel_reconciliationsummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<reconciliationlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new reconciliationlist
                        {

                            reconciliation_gid = rd["reconciliation_gid"].ToString(),
                            vendor_gid = rd["vendor_gid"].ToString(),
                            agency_name = rd["vendor_company_name"].ToString(),
                            file_name = rd["file_name"].ToString(),
                            file_type = rd["file_type"].ToString(),
                            created_date = DateTime.Parse(rd["created_date"].ToString()),

                        });
                    }
                    reconciliation.reconciliationlist = summary;
                    reconciliation.status = true;

                }
                else
                {
                    reconciliation.status = false;


                }
                rd.Close();
            }
            catch (Exception ex)
            {
                reconciliation.status = false;
                reconciliation.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();

                }
            }
            return reconciliation;
        }
        public Reconciliation reconciliationcount(reconciliationcountlist values)
        {
            reconciliationcountlist count = new reconciliationcountlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_reconciliationcount");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reconciliation_gid", values.reconciliation_gid);
                cmd.Parameters.AddWithValue("p_vendor_gid", values.vendor_gid);
                cmd.Parameters.AddWithValue("o_total_records_file", "");
                cmd.Parameters["o_total_records_file"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("o_matching_ticket_records", "");
                cmd.Parameters["o_matching_ticket_records"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.AddWithValue("o_ticket_matches_with_agent", "");
                cmd.Parameters["o_ticket_matches_with_agent"].Direction = System.Data.ParameterDirection.Output;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    values.total_records_file = cmd.Parameters["o_total_records_file"].Value.ToString();
                    values.matching_records_file = cmd.Parameters["o_matching_ticket_records"].Value.ToString();
                    values.matchingagent_records = cmd.Parameters["o_ticket_matches_with_agent"].Value.ToString();
                }
                else
                {
                    values.status = false;
                    values.message = "Internal error occured!";
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Internal Error Occured";
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
        public Reconciliation GetdocumentUploadExcel(string company_code, HttpRequest httpRequest, string usergid)
        {
            string lspath;
            reconciliationDetail GetdocumentImportExcel = new reconciliationDetail();
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
                        string filetype = string.Empty;
                        string filename = string.Empty;
                        string renamefile = string.Empty;
                        Double n = 0;
                        n = n + 1;
                        filetype = System.IO.Path.GetExtension(httpPostedFile.FileName);
                        renamefile = "RECN" + DateTime.Now.ToString("yyyyMMddHHmmss") + string.Format(Convert.ToString(n), "0000");
                        filename = renamefile + filetype;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lspath = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["reconciliation_file_path"].ToString());
                        GetdocumentImportExcel.upload_airfile = HttpContext.Current.Server.MapPath("../../" + company_code + ConfigurationManager.AppSettings["reconciliation_file_path"].ToString() + filename);
                        if (!System.IO.Directory.Exists(lspath))
                        {
                            System.IO.Directory.CreateDirectory(lspath);
                        }
                        httpPostedFile.SaveAs(GetdocumentImportExcel.upload_airfile);
                    }
                    string reconciliation_gid = string.Empty;
                    if (File.Exists(GetdocumentImportExcel.upload_airfile))
                    {

                        cmd = new MySqlCommand("sp_ins_reconciliation");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_agency_name", httpRequest.Form["agency_name"]);
                        cmd.Parameters.AddWithValue("p_file_type", httpRequest.Form["file_type"]);
                        cmd.Parameters.AddWithValue("p_file_name", httpRequest.Form["file_name"]);
                        cmd.Parameters.AddWithValue("p_upload_airfile", GetdocumentImportExcel.upload_airfile);
                        cmd.Parameters.AddWithValue("p_created_by", usergid);
                        cmd.Parameters.AddWithValue("o_reconciliation_gid", "");
                        cmd.Parameters["o_reconciliation_gid"].Direction = System.Data.ParameterDirection.Output;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        reconciliation_gid = cmd.Parameters["o_reconciliation_gid"].Value.ToString();
                        if (mnresult == 1)
                        {
                            GetdocumentImportExcel.status = true;

                            String strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + GetdocumentImportExcel.upload_airfile + ";Extended Properties='Excel 8.0;IMEX=1;HDR=YES;'";
                            XLDbConnection = new OleDbConnection(strExcelConn);
                            XLDbConnection.Open();
                            XLDbCommand = new OleDbCommand("select * from [Sheet1$]", XLDbConnection);
                            XLDbDataReader = XLDbCommand.ExecuteReader();
                            if (XLDbDataReader.HasRows == true)
                            {
                                while (XLDbDataReader.Read())
                                {

                                    DocumentNo = XLDbDataReader["Document No"].ToString();

                                    if (DocumentNo != "")
                                    {
                                        Date = XLDbDataReader["Date"].ToString();
                                        Date = DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                                        Datetime = DateTime.Parse(Date);
                                        Ticket = XLDbDataReader["Ticket/Voucher"].ToString();
                                        Sector = XLDbDataReader["Sector"].ToString();
                                        PaxName = XLDbDataReader["Pax Name"].ToString();
                                        SellingPrice = Convert.ToDouble(XLDbDataReader["Selling Price"]);
                                        type = DocumentNo.Substring(0, 2);
                                        if (type == "IN")
                                        {
                                            cmd = new MySqlCommand("sp_ins_reconciliationdtl");
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("p_reconciliation_gid", reconciliation_gid);
                                            cmd.Parameters.AddWithValue("p_reconciliation_date", Date);
                                            cmd.Parameters.AddWithValue("p_document_number", DocumentNo);
                                            cmd.Parameters.AddWithValue("p_ticket_number", Ticket);
                                            cmd.Parameters.AddWithValue("p_sector", Sector);
                                            cmd.Parameters.AddWithValue("p_pax_name", PaxName);
                                            cmd.Parameters.AddWithValue("p_selling_amount", SellingPrice);
                                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        }
                                        else if (type == "RV")
                                        {
                                            if (Ticket == "")
                                            {
                                                Ticket = "null";
                                            }
                                            if (Sector == "")
                                            {
                                                Sector = "null";
                                            }
                                            if (PaxName == "")
                                            {
                                                PaxName = "null";
                                            }
                                            cmd = new MySqlCommand("sp_ins_reconciliationrvdtl");
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("p_reconciliation_gid", reconciliation_gid);
                                            cmd.Parameters.AddWithValue("p_reconciliation_date", Date);
                                            cmd.Parameters.AddWithValue("p_document_number", DocumentNo);
                                            cmd.Parameters.AddWithValue("p_ticket_number", Ticket);
                                            cmd.Parameters.AddWithValue("p_sector", Sector);
                                            cmd.Parameters.AddWithValue("p_pax_name", PaxName);
                                            cmd.Parameters.AddWithValue("p_selling_amount", SellingPrice);
                                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        }
                                        else if (type == "RF")
                                        {
                                            cmd = new MySqlCommand("sp_ins_reconciliationrfdtl");
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("p_reconciliation_gid", reconciliation_gid);
                                            cmd.Parameters.AddWithValue("p_reconciliation_date", Date);
                                            cmd.Parameters.AddWithValue("p_document_number", DocumentNo);
                                            cmd.Parameters.AddWithValue("p_ticket_number", Ticket);
                                            cmd.Parameters.AddWithValue("p_sector", Sector);
                                            cmd.Parameters.AddWithValue("p_pax_name", PaxName);
                                            cmd.Parameters.AddWithValue("p_selling_amount", SellingPrice);
                                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        }
                                    }

                                };
                            }
                            XLDbDataReader.Close();
                            XLDbConnection.Close();

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
        public Reconciliation reconciliationpath(reconciliationlist val)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_reconciliationdownloadpath");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_reconciliation_gid", val.reconciliation_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.document_path = rd["upload_airfile"].ToString();
                    val.status = true;
                }
                else
                {
                    val.document_path = "Not Detected";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.document_path = "Not Detected";
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
        public Reconciliation reconciliationmatchingcount(matchingcountlist val)
        {
            matchingcountlist count = new matchingcountlist();
            try
            {
                cmd = new MySqlCommand("sp_sel_reconciliationmatchingwithticket");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_reconciliation_gid", val.reconciliation_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<matchingcountlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new matchingcountlist
                        {

                            date = Convert.ToDateTime(rd["reconciliation_date"].ToString()),
                            sector = rd["sector"].ToString(),
                            document_number = rd["document_number"].ToString(),
                            pax_name = rd["pax_name"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),
                            selling_amount = Convert.ToDouble(rd["selling_amount"].ToString())

                        });
                    }
                    val.matchingcount = summary;
                    val.status = true;

                }
                else
                {
                    val.status = false;


                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public Reconciliation reconciliationmatchingagentcount(matchingwithagentcount val)
        {
            matchingwithagentcount count = new matchingwithagentcount();
            try
            {
                cmd = new MySqlCommand("sp_sel_reconciliationmatchingwithagent");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_reconciliation_gid", val.reconciliation_gid);
                cmd.Parameters.AddWithValue("p_vendor_gid", val.vendor_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<matchingwithagentcount>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new matchingwithagentcount
                        {

                            date = Convert.ToDateTime(rd["reconciliation_date"].ToString()),
                            sector = rd["sector"].ToString(),
                            document_number = rd["document_number"].ToString(),
                            pax_name = rd["pax_name"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),
                            selling_amount = Convert.ToDouble(rd["selling_amount"].ToString())

                        });
                    }
                    val.matchingwithagent = summary;
                    val.status = true;

                }
                else
                {
                    val.status = false;


                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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