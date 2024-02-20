using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelApp_API.Models;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using DataAccess;
using BusinessEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace TravelApp_API
{
    public class ValidateUserFromDatabase
    {
        private static string connectionString;
        //Cmnfunctions objcmnfunctions = new Cmnfunctions();
        public static DataTable IsValidUser(string usercode, string password, string companycode)
        {
            MySqlCommand cmd;
            MySqlDataReader rd;
            MySqlConnection con;
            DataTable userinfo = new DataTable();
            try
            {
                Login GetLogin = new Login();
                //var path1 = HttpContext.Current.Server.MapPath("\\json\\") + companycode + ".json";
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
                cmd = new MySqlCommand();
                con = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " select email_address,user_code,first_name from sys_mst_tuser where user_code='" + usercode + "' and password='" + password + "'";
                cmd.Connection = con;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                
                try
                {
                    adapter.Fill(userinfo);
                }
                catch (Exception ex)
                {
                    //string error = "Invalid User";
                    //error = ex.Message;
                }


            }
            catch (Exception ex)
            {

            }


            //if (LoginType == "Student")
            //{
            //    cmd.CommandText = "Select student_name as ID, user_id as Mail, 'student login request' as collegecode  from ins_mst_student where user_id = '" + userName + "' and password = '" + password + "' and student_status = 'Active'";
            //}
            //else
            //{
            //    cmd.CommandText = "select email_gid as ID,email_gid as Mail,college_code as collegecode  from adm_mst_login where email_gid='" + userName + "' and password='" + password + "' ";
            //}

            return userinfo;
        }
        public static MySqlConnection GetConnection()
        {
            
            return new MySqlConnection(connectionString);

        }
    }
}