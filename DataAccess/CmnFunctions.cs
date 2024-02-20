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
using System.IO;

namespace DataAccess
{
    public class CmnFunctions
    {
        string error;
       
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
        public int SendSMS(string strTo, string strMessage)
        {   
            string mobile_no = strTo;
            string message = strMessage;
            string lstoken = ConfigurationManager.AppSettings["token"].ToString();
            string lscredit = ConfigurationManager.AppSettings["credit"].ToString();
            string lssender = ConfigurationManager.AppSettings["sender"].ToString();
            var url = "http://pay4sms.in/sendsms/?token=" + lstoken + "&credit=" + lscredit + "&sender=" + lssender + "&message=" + strMessage + "&number=" + strTo;
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseText = reader.ReadToEnd();
            //if your response is in json format just uncomment below line  
            //Response.AddHeader("Content-type", "text/json");    
            return 1;
        }
        public int SendSMTP(string strFrom, string strTo, string strSubject, string strBody, string strCC, string strAttachments)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
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
                mMailMessage.BodyEncoding = Encoding.UTF8;
                mMailMessage.Priority = MailPriority.High;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;

                SmtpClient mSmtpClient = new SmtpClient();
                mSmtpClient.Host = mailserver;
                mSmtpClient.Port = mPort;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Timeout = 100000;
                mSmtpClient.UseDefaultCredentials = true;
                mSmtpClient.Credentials = new NetworkCredential(email_credentials, pwd);
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
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

        //var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "rcsearch");
        //var request = new RestRequest(Method.POST);
        //request.AddHeader("content-type", "application/json");
        //request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());                
        //        request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"engine_no\" : \"" + Values.engine_no + "\",  \"chassis_no\" : \"" + Values.chassis_no + "\", \"state\" : \"" + Values.state + "\" }", ParameterType.RequestBody);
        //        IRestResponse response = client.Execute(request);
        //ObjVehicleRCSearchResponse = JsonConvert.DeserializeObject<VehicleRCSearchResponse>(response.Content);
        //        ObjVehicleRCSearchResponse.status = true;

    }
}