using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class SmsManagementmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class SmsManagement : SmsManagementmodel
    {
        public List<SmsManagementList> smsManagementList { get; set; }
    }
    public class SmsManagementList
    { 
        public string smsmanagement_gid { get; set; }
        public string smsmanagement_code { get; set; }
        public string smsmanagement_name { get; set; }
        public string smsmanagement_message { get; set; } 
    }
    public class SmsManagementdetail : SmsManagementmodel
    {
        public string smsmanagement_gid { get; set; }
        public string smsmanagement_code { get; set; }
        public string smsmanagement_name { get; set; }
        public string smsmanagement_message { get; set; }
    }
    public class smsPushdetail : MailManagementmodel
    {
        public int smsmanagement_gid { get; set; }
        public List<customerlist> customerList { get; set; }

    } 
    public class SmsManagementdelete : SmsManagementmodel
    {
     public int smsmanagement_gid { get; set; }

    }
    
}