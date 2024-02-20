using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
  
        public class Partnermodel
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class Partner: Partnermodel
        {
            public List<Partnerlist> Partnerlist { get; set; }
        }
        public class Partnerdetails : Partnermodel
        {
            public int partner_gid { get; set; }
            public string partner_code { get; set; }
            public string partner_name { get; set; }
            public string national_id { get; set; }
            public string contact_number { get; set; }
            public string email_address{ get; set; }
            public string partner_address { get; set; }
            public string capitalshare_percent { get; set; }
            public string revenueshare_percent { get; set; }
            public string sharepaid_captial { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string updated_by { get; set; }
            public string updated_date { get; set; }
            public string partner_country { get; set; }
            public string country_code { get; set; }
        
    }
        public class Partnerlist
    {

            public int partner_gid { get; set; }
            public string partner_code { get; set; }
            public string partner_name { get; set; }
            public string national_id { get; set; }
            public string contact_number { get; set; }
            public string email_address { get; set; }
            public string partner_address { get; set; }
            public string capitalshare_percent { get; set; }
            public string revenueshare_percent { get; set; }
            public string sharepaid_captial { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string updated_by { get; set; }
            public string updated_date { get; set; }
            public string partner_country { get; set; }
            public string country_code { get; set; }
    }
    }