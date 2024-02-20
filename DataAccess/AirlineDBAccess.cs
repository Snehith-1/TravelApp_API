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
    public class AirlineDBAccess
    {
        DBAccess objcmnfunctions = new DBAccess();
        HttpPostedFile httpPostedFile;
        HttpRequest httpRequest;
        SqlDataReader objDataReader;
        int mnresult = 0;
        MySqlCommand cmd = null;
        MySqlDataReader rd;
        DateTime airlinecreated_Date ;
        string msSQL, msGIDRs, msGIDRq, msGIDStsEnqDD, msSQLlog, msGIDRslog, msGIDStRqlog;
        int  mnresultResp, rowCount, columnCount, StsEnqDtlCount = 0, mnresultStsEnqDtl, mnresultSecondary = 0, mnresultlog, mnresultupdate, mnresultL;
        string lspath, endRange;
        int insertCount = 0;
        string lsAirport, lscity, lsCountry, lsIATADesignator, lsICAODesignator, lsfaadesignator;
        string error;
        public airlinedetails airlinesummary()
        {
            airlinedetails air = new airlinedetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_airlinesummary");
                cmd.CommandType  = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Airlinelist>();               
                if(rd.HasRows ==true )
                {
                    while (rd.Read())
                    {
                        if (rd["created_date"] != System.DBNull.Value)
                        {
                            airlinecreated_Date = DateTime.Parse(rd["created_date"].ToString());
                        }
                        summary.Add(new Airlinelist
                        {
                            airline_gid = rd["airline_gid"].ToString(),
                            airline_name = rd["airline_name"].ToString(),
                            city = rd["city"].ToString(),
                            airline_country = rd["airline_country"].ToString(),
                            iata_designator = rd["iata_designator"].ToString(),
                            icao_designator = rd["icao_designator"].ToString(),
                            faa_designator = rd["faa_designator"].ToString(),
                            created_date = airlinecreated_Date,
                        });
                    }
                   // string lobj = summary.ToString("");
                    air.Airlinelist = summary;
                    air.status = true;
                    air.message = "Airline added successfully!";
                }
                else
                {
                    air.status = true;
                    air.message = "No Records Found!";
                }
                rd.Close();              
            }
            catch (Exception ex)
            {
                air.status = false;
                air.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return air;
        }
        public Airlinemodel airlineradd(airlinedetails val, string userGid)
        {
            Airlinemodel air = new Airlinemodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_airline");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_airline_name", val.airline_name);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_airline_country", val.airline_country);             
                cmd.Parameters.AddWithValue("p_iata_designator", val.iata_designator);
                cmd.Parameters.AddWithValue("p_icao_designator", val.icao_designator);
                cmd.Parameters.AddWithValue("p_faa_designator", val.faa_designator);
                cmd.Parameters.AddWithValue("p_created_by", userGid);

                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    air.status = true;
                    air.message = "Airline added sucessfully";
                }
                else
                {
                    air.status = false;
                    air.message = "Error occured while adding airline";
                }
            }
            catch (Exception ex)
            {
                air.status = false;
                air.message = "Error occured while adding airline";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return air;
        }
        public Airlinemodel airlinedelete (string val)
        {
            Airlinemodel air = new Airlinemodel();
            try
            {
                cmd = new MySqlCommand("sp_del_airline");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_airline_gid", val);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if(mnresult ==1)
                {
                    air.status = true;
                    air.message = "Airline Deleted Successfully!";
                }
                else
                {
                    air.status = false;
                    air.message = "Error Occured While Deleting Airline!";
                }
            }
            catch (Exception ex)
            {
                air.status = false;
                air.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return air;
        }
        public Airlinemodel Excel(string lscompany_code, HttpRequest httpreq, airlinedetails val, string user_gid)
        {
            Airlinemodel air = new Airlinemodel();

            string lsfilePath = string.Empty;


            HttpFileCollection httpFileCollection; DataTable dt = null;
            //Customerdetails GetdocumentImportExcel = new Customerdetails();
            try
            {
               
                lsfilePath = HttpContext.Current.Server.MapPath("../../erp_documents" + "/" + lscompany_code + "/TTS/AirlineImportExcel/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                        lsAirport = row["Airport"].ToString();
                        lscity = row["City"].ToString();
                        lsCountry = row["Country"].ToString();
                       
                        lsIATADesignator = row["IATA"].ToString();

                       lsICAODesignator = row["icao"].ToString();
                       lsfaadesignator = row["FAA"].ToString();
                        

                        cmd = new MySqlCommand("sp_ins_airlineimportexcel");

                        cmd.Parameters.AddWithValue("p_airline_name", lsAirport);
                        cmd.Parameters.AddWithValue("p_city", lscity);
                        cmd.Parameters.AddWithValue("p_airline_country", lsCountry);
                        cmd.Parameters.AddWithValue("p_iata_designator", lsIATADesignator);
                        cmd.Parameters.AddWithValue("p_icao_designator", lsICAODesignator);
                        cmd.Parameters.AddWithValue("p_faa_designator", lsfaadesignator);
                        cmd.Parameters.AddWithValue("p_created_by", "");
                        cmd.Parameters.AddWithValue("p_airline_gid", "");
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
            return air;
        }
        public Airlinemodel othersericesadd(otherservicedetails val, string userGid)
        {
            Airlinemodel othersericesadd = new Airlinemodel();
            try
            {
                cmd = new MySqlCommand("sp_ins_otherservicesadd");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_servicetype_name", val.servicetype_name);
                cmd.Parameters.AddWithValue("p_customer_amount", val.customer_amount);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                cmd.Parameters.AddWithValue("p_otherservice_details", val.otherservice_details);
                cmd.Parameters.AddWithValue("p_created_by", userGid);

                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    othersericesadd.status = true;
                    othersericesadd.message = "Other Services added sucessfully";
                }
                else
                {
                    othersericesadd.status = false;
                    othersericesadd.message = "Error occured while adding Other Services";
                }
            }
            catch (Exception ex)
            {
                othersericesadd.status = false;
                othersericesadd.message = "Error occured while adding Other Services";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return othersericesadd;
        }
        public otherservicedetails otherservicesummary()
        {
            otherservicedetails otherservice = new otherservicedetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_otherservicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<otherservicelist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        if (rd["created_date"] != System.DBNull.Value)
                        {
                            airlinecreated_Date = DateTime.Parse(rd["created_date"].ToString());
                        }
                        summary.Add(new otherservicelist
                        {
                            otherservice_gid = rd["otherservice_gid"].ToString(),
                            servicetype_name = rd["servicetype_name"].ToString(),
                            customer_amount = rd["customer_amount"].ToString(),
                            vendor_name = rd["vendor_company_name"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString(),
                            otherservice_details = rd["otherservice_details"].ToString(),
                        });
                    }
                    // string lobj = summary.ToString("");
                    otherservice.otherservicelist = summary;
                    otherservice.status = true;
                    otherservice.message = "Other Service Loaded    successfully!";
                }
                else
                {
                    otherservice.status = true;
                    otherservice.message = "No Records Found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                otherservice.status = false;
                otherservice.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return otherservice;
        }
        public otherservicedetails otherservicetype()
        {
            otherservicedetails otherservice = new otherservicedetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_otherservicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<otherservicetype>();
                if (rd.HasRows == true)
                {

                    while (rd.Read())
                    {
                        summary.Add(new otherservicetype
                        {
                            otherservice_gid = rd["otherservice_gid"].ToString(),
                            servicetype_name = rd["servicetype_name"].ToString(),
                            
                        });
                    }
                    otherservice.otherservicetype = summary;
                    otherservice.status = true;
                    otherservice.message = "Other Service Loaded    successfully!";
                }
                else
                {
                    otherservice.status = true;
                    otherservice.message = "No Records Found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                otherservice.status = false;
                otherservice.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return otherservice;
        }
        public Airlinemodel otherserviceedit(otherservicelist val, string user_gid)
        {
            otherservicedetails otherservice = new otherservicedetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_otherserviceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_otherservice_gid", val.otherservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    otherservice.servicetype_name = rd["servicetype_name"].ToString();
                    otherservice.customer_amount = rd["customer_amount"].ToString();
                    otherservice.vendor_name = rd["vendor_name"].ToString();
                    otherservice.vendor_amount = rd["vendor_amount"].ToString();
                    otherservice.otherservice_details = rd["otherservice_details"].ToString();

                    otherservice.status = true;
                    //rd.Close();
                }
                else
                {
                    otherservice.status = false;
                    otherservice.message = "No Records found!";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                otherservice.status = false;
                otherservice.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return otherservice;
        }
        public Airlinemodel otherserviceupdate(otherservicelist val, string user_gid)
        {
            otherservicedetails otherservice = new otherservicedetails();
            try
            {
                cmd = new MySqlCommand("sp_upt_otherservice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_servicetype_name", val.servicetype_name);
                cmd.Parameters.AddWithValue("p_customer_amount", val.customer_amount);
                cmd.Parameters.AddWithValue("p_vendor_name", val.vendor_name);
                cmd.Parameters.AddWithValue("p_vendor_amount", val.vendor_amount);
                cmd.Parameters.AddWithValue("p_otherservice_gid", val.otherservice_gid);
                cmd.Parameters.AddWithValue("p_otherservice_details", val.otherservice_details);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    otherservice.status = true;
                    otherservice.message = "Status updated succesfully";
                }
                else
                {
                    otherservice.status = false;
                    otherservice.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                otherservice.status = false;
                otherservice.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return otherservice;
        }
        public Airlinemodel choosesubservicetype(otherservicelist val)
        {
            otherservicedetails sof = new otherservicedetails();
            string otherservice_gid = string.Empty;
            
            int flagCustomerExists = 0;

            try
            {
                cmd = new MySqlCommand("sp_sel_otherservice_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_servicetype_name",val.servicetype_name);
                //cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    flagCustomerExists = 1;
                    otherservice_gid = rd["otherservice_gid"].ToString();
                }
                else
                {
                    val.status = false;
                    val.message = "ERR078";
                }

                if (flagCustomerExists == 1)
                {

                    cmd = new MySqlCommand("sp_sel_otherserviceid");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_otherservice_gid", otherservice_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        sof.otherservice_gid = rd["otherservice_gid"].ToString();
                        sof.servicetype_name = rd["servicetype_name"].ToString();
                        sof.customer_amount = rd["customer_amount"].ToString();
                        sof.vendor_name = rd["vendor_name"].ToString();
                        sof.vendor_amount = rd["vendor_amount"].ToString();
                        sof.otherservice_details = rd["otherservice_details"].ToString();
             
                    }

                    sof.status = true;
                    sof.message = "Customer Details Loaded Successfully";
                    rd.Close();

                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Customer Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sof;
        }
    }
}