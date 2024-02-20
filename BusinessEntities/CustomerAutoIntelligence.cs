using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class CustomerAutoIntelligenceModel
    {
        
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class CustomerAutoIntelligence: CustomerAutoIntelligenceModel
    {

        public List<CustomerAutoIntelligenceList> CustomerAutoIntelligenceList { get; set; }
    }

    public class CustomerAutoIntelligenceList 
    {
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
        public string contact_number { get; set; }
        public string air_gid { get; set; }
        public string epax_name { get; set; }


    }
    public class CustomerAutoIntelligenceDetails :CustomerAutoIntelligenceModel
    {
        public string prefixtext { get; set; }
        public string customer_gid { get; set; }
        public string customer_type { get; set; }
        public string contact_number { get; set; }

        public List<CustomerAutoIntelligenceList> customerAutoIntelligenceList { get; set; }

    }
    public class journalaccountlist 
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }

    }

    public class journalaccountlistdetails: CustomerAutoIntelligenceModel
    {
           public string account_name { get; set; }
        public string account_gid { get; set; }
        public string prefixtext { get; set; }
        public List<journalaccountlist> journalaccountAutoIntelligence { get; set; }
    }

    public class journalaccountAutoIntelligence : CustomerAutoIntelligenceModel
    {

        public List<journalaccountlist> journalaccountAutoIntelligencelist { get; set; }
    }



    public class journalaccountgrouplist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }

    }

    public class journalaccountgrouplistdetails : CustomerAutoIntelligenceModel
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string prefixtext { get; set; }
        public List<journalaccountgrouplist> journalaccountgroupAutoIntelligence { get; set; }
    }

    public class journalaccountgrouplistAutoIntelligence : CustomerAutoIntelligenceModel
    {

        public List<journalaccountgrouplist> journalaccountgroupAutoIntelligencelist { get; set; }
    }
}



