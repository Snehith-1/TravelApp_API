using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class MailManagementmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MailManagement : MailManagementmodel
    {
        public List<MailManagementList> MailManagementList { get; set; }


    }
    public class MailManagementList
    {
        public int mailmanagement_gid { get; set; }
        public string mailmanagement_code { get; set; }
        public string mailmanagement_name { get; set; }
        public string mailmanagement_message { get; set; }
        public string customerList { get; set; }
    }
    public class MailManagementdetail : MailManagementmodel
    {
        public int mailmanagement_gid { get; set; }
        public string mailmanagement_code { get; set; }
        public string mailmanagement_name { get; set; }
        public string mailmanagement_message { get; set; }
        public string upload_documents { get; set; }
    }

    public class MailPushdetail : MailManagementmodel
    {
        public List<customerlist> customer_gid;

        public int mailmanagement_gid { get; set; }
        public List<customerlist> customerList { get; set; }


    }
    public class customerlist
    {
        public string name { get; set; }
        public string flag { get; set; }
        public string upload_documents { get; set; }
    }
    public class MailManagementdelete : MailManagementmodel
    {
        public int mailmanagement_gid { get; set; }

    }

}