using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using BusinessEntities;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.OleDb;
namespace DataAccess
{
    public class DBAccess
    {
        string error;

        private static string connectionString;

        public static MySqlConnection GetConnection()
        {
            //var path1 = HttpContext.Current.Server.MapPath("\\json\\") + HttpContext.Current.Request.Headers.GetValues("company_code")[0] + ".json";
            //var st = File.ReadAllText(path1);
            //var y = JsonConvert.DeserializeObject<object>(st);
            //JObject jObj = JObject.Parse(st);
            //List<table> tablename = new List<table>();
            //foreach (var tableName in jObj["objectname"].Children())
            //{
            //    tablename.Add(new table()
            //    {
            //        id = (string)tableName["id"],
            //        value = (string)tableName["value"]

            //    });
            //    connectionString = (string)tableName["value"];
            //}
            //MySqlConnection Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            //if (Connection == null || Connection.State != System.Data.ConnectionState.Open)
            //{
            //    //Connection = GetConnection();
            //    Connection.Open();
            //}
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        }
        public DataTable ExcelToDataTable(string lsfilePath, string FileName, string range,  string FileExtension)
        {
            DataTable datatable = new DataTable();
            int totalSheet = 1;
            string lsConnectionString = string.Empty;
            string GetMasterGID;
            string lsFirstName, lsLastName, lsEmailaddress, lsContactNo, lsAadharID, lsCustomerType, lsCompanyName, lsAddress, lsCountry,     lsRemarks, lsCreditLimit;
            string fileExtension = Path.GetExtension(FileName);
            string Excel = lsfilePath;
            if (fileExtension == ".xls")
            {
                lsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
            }
            else if (fileExtension == ".xlsx")
            {
                lsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Excel + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
            }

            using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
            {
              
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(); 
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                sheetName = sheetName.Replace("'", "").Trim() + range;
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");

                datatable = ds.Tables["excelData"];
                cmd.Connection.Close();
                objConn.Close();
            }
            return datatable;
        }
        public string uploadFile(string path)
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
                    if (!File.Exists(sPath))
                    {
                        hpf.SaveAs(sPath);
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
        public static MySqlDataReader ExecuteReader(MySqlCommand cmd)
        {
            try
            {

                if (cmd.Connection == null || cmd.Connection.State != System.Data.ConnectionState.Open)
                {
                    cmd.Connection = GetConnection();
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
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
                return null;
            }
            finally
            {

            }
        }

        public static DataTable GetDataTable(MySqlDataAdapter sqlad)
        {
            try
            {
                if (sqlad.SelectCommand.Connection == null || sqlad.SelectCommand.Connection.State != System.Data.ConnectionState.Open)
                {
                    sqlad.SelectCommand.Connection = GetConnection();
                    sqlad.SelectCommand.Connection.Open();
                }

                DataTable dt = new DataTable();
                sqlad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
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
                return null;
            }
        }

        public static int ExecuteNonQuery(MySqlCommand cmd)
        {
            string error;
            cmd.Connection = GetConnection();
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
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
                error = ex.ToString();
                return 0;
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }
    }
}
