using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess
{
    public class CommonFunctions
    {
        string error;
        string[] ls_parametername = null;
        string[] ls_parametervalue = null;

        int count = 0;

        String lsupdateparameter = string.Empty;
        String lsupdateparametervalue = string.Empty;
        string[] ls_inputparamtetername = null;
        string[] ls_inputparametervalue = null;
        string[] ls_outputparametername = null;
        string[] ls_outputparametersize = null;
        string _lsinsertparametername = string.Empty;
        string _lsinsertparametervalue = string.Empty;
        MySqlCommand sqlcmd = new MySqlCommand();


        MySqlDataAdapter sqlad = new MySqlDataAdapter();
        MySqlDataReader sqldr = null;
        int insert = 0;

        public string passwordencryption(string str)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, str);
                return hash;
            }
        }
        
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int SendSMTP(string strFrom, string strTo, string strSubject, string strBody, string strCC, string strAttachments)
        {
            int MailFlag = 1;
            String[] mailsuffixname = null;
            String[] mailtoname = null;
            String lsmailid = String.Empty;
            String[] lsfirstname = null;
            String mailconfigcred = string.Empty;
            String[] mailconfig;
            String port = String.Empty;
            String mailserver = String.Empty;
            String email_credentials = String.Empty;
            String enablessl = String.Empty;
            String pwd = String.Empty;
            MailMessage mMailMessage = new MailMessage();

            mailsuffixname = strTo.Split(',');
            string Toname = mailsuffixname[0];

            mailsuffixname = strCC.Split('@');
            string CCname = mailsuffixname[0];

            mailsuffixname = strFrom.Split('@');
            string Fromname = mailsuffixname[1];
            string[] words = Fromname.Split(new string[] { "." }, StringSplitOptions.None);
            Fromname = words[0].ToUpper();

            mailtoname = strTo.Split(',');

            mailconfigcred = ConfigurationManager.AppSettings["MAILCONFIG"].ToString();
            //mailconfigcred = "zcoreboard.info@gmail.com||smtp.gmail.com||25||Zcoreboard@2017||zcoreboard.info@gmail.com||true";
            mailconfig = mailconfigcred.Split(new string[] { "||" }, StringSplitOptions.None);
            strFrom = mailconfig[0];
            mailserver = mailconfig[1];
            port = mailconfig[2];
            pwd = mailconfig[3];
            email_credentials = mailconfig[4];
            enablessl = mailconfig[5];
            //For sending more than 1 mail id in o field
            for (int i = 0; i <= mailtoname.Length - 1; i++)
            {
                lsmailid = mailtoname[i];
                lsfirstname = lsmailid.Split('@');
                mMailMessage.To.Add(new MailAddress(mailtoname[i], lsfirstname[0]));

                if (!string.IsNullOrEmpty(strCC))
                {
                    String[] lsccmail = null;
                    lsccmail = strCC.Split(',');

                    for (int k = 0; k <= lsccmail.Count() - 1; k++)
                    {
                        mailsuffixname = lsccmail[k].Split('@');
                        CCname = mailsuffixname[0];
                        mMailMessage.CC.Add(new MailAddress(lsccmail[k], CCname));
                    }
                }
                mMailMessage.From = new MailAddress(strFrom, Fromname);

                //mMailMessage.ReplyTo = new MailAddress(strFrom, Fromname);

                mMailMessage.Subject = strSubject;
                //Adding Attachments
                if (!string.IsNullOrEmpty(strAttachments))
                {
                    String[] lsattachmentmail = null;
                    lsattachmentmail = strAttachments.Split(new string[] { "||" }, StringSplitOptions.None);
                    for (int k = 0; k <= lsattachmentmail.Count() - 1; k++)
                    {
                        mMailMessage.Attachments.Add(new Attachment(lsattachmentmail[k]));
                    }
                }

                int mPort = Convert.ToInt32(port);
                mMailMessage.Body = strBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.Priority = MailPriority.Normal;
                SmtpClient mSmtpClient = new SmtpClient();
                mSmtpClient.Host = mailserver;
                mSmtpClient.Port = mPort;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Timeout = 100000;
                mSmtpClient.UseDefaultCredentials = false;
                mSmtpClient.Credentials = new NetworkCredential(email_credentials, pwd);
                try
                {
                    mSmtpClient.Send(mMailMessage);
                }
                catch (Exception ex)
                {
                    MailFlag = 0;
                    error = ex.ToString();
                    
                }
            }
            return MailFlag;
        }

        #region cmnfunctions

        public MySqlDataReader GetDataReader(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)
        {
            MySqlDataReader sqldr = null;
            // initializing ado objects and connectionstring
            try
            {
                //SqlParameter sqlparam = sqlcmd.CreateParameter();

                sqlcmd.CommandText = storedprocedurename;
                sqlcmd.CommandType = CommandType.StoredProcedure;

                // assigning splitted values to string[] array starts here
                ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlcmd.Connection = objcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Clear();
                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= parametercount - 1; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ls_parametername[i], ls_parametervalue[i]);
                    sqlcmd.Parameters[ls_parametername[i]].Direction = ParameterDirection.Input;
                }
                sqldr = sqlcmd.ExecuteReader();
                return sqldr;
            }
            catch (Exception ex)
            {
                return sqldr;
            }
            finally
            {
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }

        public int Getinsert(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)
        {
            // initializing ado objects and connectionstring

            // assigning splitted values to string[] array starts here
            try
            {
                ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlcmd.Connection = objcon;
                sqlcmd.CommandText = storedprocedurename;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Clear();

                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= parametercount - 1; i++)
                {
                    sqlcmd.Parameters.AddWithValue(ls_parametername[i], ls_parametervalue[i]);
                    sqlcmd.Parameters[ls_parametername[i]].Direction = ParameterDirection.Input;

                }

                // try catch finally

                sqlcmd.ExecuteNonQuery();
                return insert = 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }

        // update values starts here 
        public int Getupdate(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)//,bool returnFlag=false)
        {
            // initializing ado objects and connectionstring
            try
            {
                // assigning splitted values to string[] array starts here
                ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlad.SelectCommand = new MySqlCommand();
                sqlad.SelectCommand.Connection = objcon;
                sqlad.SelectCommand.CommandType = CommandType.StoredProcedure;

                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= parametercount - 1; i++)
                {
                    MySqlParameter sqlparam = new MySqlParameter(ls_parametername[i], ls_parametervalue[i]);
                    sqlparam.Direction = ParameterDirection.Input;
                    sqlparam.DbType = DbType.String;
                    sqlad.SelectCommand.Parameters.Add(sqlparam);
                }

                sqlad.SelectCommand.CommandText = storedprocedurename;
                sqlad.SelectCommand.ExecuteNonQuery();
                // try catch finally
                //insert= sqlad.SelectCommand.ExecuteNonQuery();
                // ///If you want to return the number of rows affected then pass a boolean parameter and it returns the exact number of affected rows
                //if (returnFlag)
                //    return insert;
                //else
                return 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {
                sqlad.Dispose();
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }

        public DataTable Getdatatable(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)
        {
            DataTable dt = new DataTable();
            try
            {
                ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);
                sqlad.SelectCommand = new MySqlCommand();
                sqlad.SelectCommand.Connection = objcon;
                sqlad.SelectCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i <= parametercount - 1; i++)
                {
                    MySqlParameter sqlparam = new MySqlParameter(ls_parametername[i], ls_parametervalue[i]);
                    sqlparam.Direction = ParameterDirection.Input;
                    sqlparam.DbType = DbType.String;
                    sqlad.SelectCommand.Parameters.Add(sqlparam);
                }
                sqlad.SelectCommand.CommandText = storedprocedurename;
                sqlad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sqlad.Dispose();
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }

        public string GetExecuteScalar(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)
        {
            // initializing ado objects and connectionstring
            MySqlCommand sqlcmd = new MySqlCommand();
            //SqlParameter sqlparam = sqlcmd.CreateParameter();
            dynamic value = "";

            // assigning splitted values to string[] array starts here
            ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
            ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);

            // assigning sqlcommand to sqldataadpater for stored procedure starts here
            sqlcmd.Connection = objcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;

            // assigning parametername & parametervalue to sqlparameters starts here
            for (int i = 0; i <= parametercount - 1; i++)
            {
                MySqlParameter sqlparam = new MySqlParameter(ls_parametername[i], ls_parametervalue[i]);
                sqlparam.Direction = ParameterDirection.Input;
                sqlparam.DbType = DbType.String;
                sqlcmd.Parameters.Add(sqlparam);
            }

            sqlcmd.CommandText = storedprocedurename;
            try
            {
                value = sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            return value;
        }


        public int sql_insert(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon, MySqlTransaction sql_transaction)
        {

            // initializing ado objects and connectionstring

            // assigning splitted values to string[] array starts here
            ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            // assigning sqlcommand to sqldataadpater for stored procedure starts here

            MySqlCommand sqlCommand = new MySqlCommand(storedprocedurename, objcon, sql_transaction);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            // assigning parametername & parametervalue to sqlparameters starts here
            for (int i = 0; i <= parametercount - 1; i++)
            {
                MySqlParameter sqlparam = new MySqlParameter(ls_parametername[i], ls_parametervalue[i]);
                sqlparam.Direction = ParameterDirection.Input;
                sqlparam.DbType = DbType.String;
                sqlCommand.Parameters.Add(sqlparam);
            }

            // try catch finally
            try
            {
                sqlCommand.ExecuteNonQuery();
                return insert = 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {

                //sqlparam = null;
            }
        }


        public int Getinsert_ExecuteNonQuery(string inputparametername, string inputpatrametervalue, string outputparametername,
                                           string outputparametersize, string storedprocedurename, int inputparametercount,
                                           int outputparametercount, ref string outputparameterresult, MySqlConnection objcon)
        {
            // initializing ado objects and connectionstring
            // try catch finally
            try
            {
                outputparameterresult = string.Empty;
                // assigning splitted values to string[] array starts here
                ls_inputparamtetername = inputparametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_inputparametervalue = inputpatrametervalue.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_outputparametername = outputparametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_outputparametersize = outputparametersize.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlad.SelectCommand = new MySqlCommand();
                sqlad.SelectCommand.Connection = objcon;
                sqlad.SelectCommand.CommandType = CommandType.StoredProcedure;

                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= inputparametercount - 1; i++)
                {
                    MySqlParameter sqlparam = new MySqlParameter(ls_inputparamtetername[i], ls_inputparametervalue[i]);
                    sqlparam.Direction = ParameterDirection.Input;
                    sqlparam.DbType = DbType.String;
                    sqlad.SelectCommand.Parameters.Add(sqlparam);
                }

                for (int i = 0; i <= outputparametercount - 1; i++)
                {
                    MySqlParameter sqlparam1 = new MySqlParameter();
                    sqlparam1.ParameterName = ls_outputparametername[i];
                    sqlparam1.Direction = ParameterDirection.Output;
                    sqlparam1.DbType = DbType.String;
                    sqlparam1.Size = Convert.ToInt32(ls_outputparametersize[i]);
                    sqlad.SelectCommand.Parameters.Add(sqlparam1);
                }

                sqlad.SelectCommand.CommandText = storedprocedurename;


                sqlad.SelectCommand.ExecuteNonQuery();
                for (int i = 0; i <= outputparametercount - 1; i++)
                {
                    outputparameterresult = outputparameterresult + "||" + sqlad.SelectCommand.Parameters[ls_outputparametername[i]].Value;
                }
                if (outputparametercount > 0)
                {
                    outputparameterresult = outputparameterresult.Substring(2, outputparameterresult.Length - 2);
                }

                return insert = 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {
                sqlad.Dispose();
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }
        // insert & output values ends here

        // update & output values starts here
        public int Getupdate_ExecuteNonQuery(string inputparametername, string inputpatrametervalue, string outputparametername,
                                             string outputparametersize, string storedprocedurename, int inputparametercount,
                                             int outputparametercount, ref string outputparameterresult, MySqlConnection objcon)
        {
            // initializing ado objects and connectionstring
            // try catch finally
            try
            {
                outputparameterresult = string.Empty;
                // assigning splitted values to string[] array starts here
                ls_inputparamtetername = inputparametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_inputparametervalue = inputpatrametervalue.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_outputparametername = outputparametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_outputparametersize = outputparametersize.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlad.SelectCommand = new MySqlCommand();
                sqlad.SelectCommand.Connection = objcon;
                sqlad.SelectCommand.CommandType = CommandType.StoredProcedure;

                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= inputparametercount - 1; i++)
                {
                    MySqlParameter sqlparam = new MySqlParameter(ls_inputparamtetername[i], ls_inputparametervalue[i]);
                    sqlparam.Direction = ParameterDirection.Input;
                    sqlparam.DbType = DbType.String;
                    sqlad.SelectCommand.Parameters.Add(sqlparam);
                }

                for (int i = 0; i <= outputparametercount - 1; i++)
                {
                    MySqlParameter sqlparam1 = new MySqlParameter();
                    sqlparam1.ParameterName = ls_outputparametername[i];
                    sqlparam1.Direction = ParameterDirection.Output;
                    sqlparam1.DbType = DbType.String;
                    sqlparam1.Size = Convert.ToInt32(ls_outputparametersize[i]);
                    sqlad.SelectCommand.Parameters.Add(sqlparam1);
                }

                sqlad.SelectCommand.CommandText = storedprocedurename;


                sqlad.SelectCommand.ExecuteNonQuery();
                for (int i = 0; i <= outputparametercount - 1; i++)
                {
                    outputparameterresult = outputparameterresult + "||" + sqlad.SelectCommand.Parameters[ls_outputparametername[i]].Value;
                }
                if (outputparametercount > 0)
                {
                    outputparameterresult = outputparameterresult.Substring(2, outputparameterresult.Length - 2);
                }

                return insert = 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {
                sqlad.Dispose();
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }



        public int Getdelete(string parametername, string parametervalue, string storedprocedurename, int parametercount, MySqlConnection objcon)
        {
            // initializing ado objects and connectionstring
            try
            {

                // assigning splitted values to string[] array starts here
                ls_parametername = parametername.Split(new string[] { "||" }, StringSplitOptions.None);
                ls_parametervalue = parametervalue.Split(new string[] { "||" }, StringSplitOptions.None);

                // assigning sqlcommand to sqldataadpater for stored procedure starts here
                sqlad.SelectCommand = new MySqlCommand();
                sqlad.SelectCommand.Connection = objcon;
                sqlad.SelectCommand.CommandType = CommandType.StoredProcedure;

                // assigning parametername & parametervalue to sqlparameters starts here
                for (int i = 0; i <= parametercount - 1; i++)
                {
                    MySqlParameter sqlparam = new MySqlParameter(ls_parametername[i], ls_parametervalue[i]);
                    sqlparam.Direction = ParameterDirection.Input;
                    sqlparam.DbType = DbType.String;
                    sqlad.SelectCommand.Parameters.Add(sqlparam);
                }

                sqlad.SelectCommand.CommandText = storedprocedurename;

                // try catch finally

                sqlad.SelectCommand.ExecuteNonQuery();
                return insert = 1;
            }
            catch (Exception ex)
            {
                return insert = 0;
            }
            finally
            {
                sqlad.Dispose();
                ls_parametervalue = null;
                ls_parametername = null;
            }
        }

        #endregion

    }
}