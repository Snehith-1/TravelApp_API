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
    public class LoginDBAccess
    {
        int mnresult = 0;
              

        public LoginValidateUser Add(Login login)
        {            
            LoginValidateUser ValidateUser = null;
            MySqlCommand cmd;
            MySqlDataReader rd;
            try
            {
                
                CmnFunctions objcmnfunctions = new CmnFunctions();
                cmd = new MySqlCommand("sp_sel_login");
                cmd.Parameters.AddWithValue("p_company_code", login.company_code);
                cmd.Parameters.AddWithValue("p_user_code", login.user_code);
                cmd.Parameters.AddWithValue("p_password", objcmnfunctions.passwordencryption(login.password));
              
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);

                ValidateUser = new LoginValidateUser();
                if (rd.Read())
                {
                    var tokenresponse = GetToken(login.company_code, login.user_code, objcmnfunctions.passwordencryption(login.password) + "||" + login.company_code);
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    string token_value = newobj.access_token;
                    token_value = "Bearer " + token_value;

                     cmd = new MySqlCommand("sp_ins_login");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_company_code", login.company_code);
                    cmd.Parameters.AddWithValue("p_user_code", login.user_code);
                    cmd.Parameters.AddWithValue("p_token_value", token_value);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);

                    ValidateUser.status = true;
                    ValidateUser.user_code = rd["user_code"].ToString();
                    ValidateUser.token_value = token_value;
                    ValidateUser.redirect = "Dashboard";
                    ValidateUser.user_gid = rd["user_gid"].ToString();
                    ValidateUser.department_name = rd["department_name"].ToString();
                    ValidateUser.country_gid = rd["country_gid"].ToString(); 

                    //rd.Close();
                }
                else
                {
                    ValidateUser.message = " Invalid credentials";
                    
                }
                rd.Close();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                ValidateUser.status = false;
                ValidateUser.message = "Internal Error Occured";
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
            return ValidateUser;

        }

        static string GetToken(string company_code,string user_code, string password)
        {
            try
            {

                var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type", "password" ),
                            new KeyValuePair<string, string>("company_code", company_code ),
                            new KeyValuePair<string, string>("username", user_code ),                            
                            new KeyValuePair<string, string>("Password", password )
                        };
                var content = new FormUrlEncodedContent(pairs);
                using (var client = new HttpClient())
                {
                    var scheme = HttpContext.Current.Request.Url.Scheme;
                    var host = HttpContext.Current.Request.Url.Host;
                    var port = HttpContext.Current.Request.Url.Port;
                    var uri_builder = new UriBuilder();
                    uri_builder = new UriBuilder(scheme, host);
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

                  var appURL = ConfigurationManager.AppSettings["URL"].ToString();
                    var response =
                           client.PostAsync(appURL, new FormUrlEncodedContent(pairs)).Result;
                    return response.Content.ReadAsStringAsync().Result;
                    string strPath = HttpContext.Current.Server.MapPath("../Error_Log1.txt");
                    if (!File.Exists(strPath))
                    {
                        File.Create(strPath).Dispose();
                    }
                    using (StreamWriter sw = File.AppendText(strPath))
                    {
                        sw.WriteLine("=============Error Logging ===========");
                        sw.WriteLine("===========Start============= " + DateTime.Now);
                        sw.WriteLine("Error Message: " + "local");
                        sw.WriteLine("Stack Trace: " + response.Content.ReadAsStringAsync().Result);
                        sw.WriteLine("===========End============= " + DateTime.Now);
                    }

                    /*      if (ConfigurationManager.AppSettings["Debug"].ToString() == "on")
                          {
                              var response =
                              return response.Content.ReadAsStringAsync().Result;
                              string strPath = HttpContext.Current.Server.MapPath("../Error_Log1.txt");
                              if (!File.Exists(strPath))
                              {
                                  File.Create(strPath).Dispose();
                              }
                              using (StreamWriter sw = File.AppendText(strPath))
                              {
                                  sw.WriteLine("=============Error Logging ===========");
                                  sw.WriteLine("===========Start============= " + DateTime.Now);
                                  sw.WriteLine("Error Message: " + "local");
                                  sw.WriteLine("Stack Trace: " + response.Content.ReadAsStringAsync().Result);
                                  sw.WriteLine("===========End============= " + DateTime.Now);


                              }
                          }
                      else if (ConfigurationManager.AppSettings["Debug1"].ToString() == "on")
                          {
                              var response =
                              return response.Content.ReadAsStringAsync().Result;
                              string strPath = HttpContext.Current.Server.MapPath("../Error_Log1.txt");
                              if (!File.Exists(strPath))
                              {
                                  File.Create(strPath).Dispose();
                              }
                              using (StreamWriter sw = File.AppendText(strPath))
                              {
                                  sw.WriteLine("=============Error Logging ===========");
                                  sw.WriteLine("===========Start============= " + DateTime.Now);
                                  sw.WriteLine("Error Message: " + "local");
                                  sw.WriteLine("Stack Trace: " + response.Content.ReadAsStringAsync().Result);
                                  sw.WriteLine("===========End============= " + DateTime.Now);


                              }
                          }


                          else
                          {
                              var response =
                                  client.PostAsync(uri_builder + "saforsol/token", new FormUrlEncodedContent(pairs)).Result;
                              return response.Content.ReadAsStringAsync().Result;
                              string strPath = HttpContext.Current.Server.MapPath("../Error_Log1.txt");
                              if (!File.Exists(strPath))
                              {
                                  File.Create(strPath).Dispose();
                              }
                              using (StreamWriter sw = File.AppendText(strPath))
                              {
                                  sw.WriteLine("=============Error Logging ===========");
                                  sw.WriteLine("===========Start============= " + DateTime.Now);
                                  sw.WriteLine("Error Message: " + "local");
                                  sw.WriteLine("Stack Trace: " + response.Content.ReadAsStringAsync().Result);
                                  sw.WriteLine("===========End============= " + DateTime.Now);




                              }
                          }
                          */

                }
            }
            catch (Exception ex)
            {
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log1.txt");
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
                return "";
            }

        }
    }
}