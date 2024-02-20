using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{

    public class companymodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class companydetails : companymodel
    {
        public int company_gid { get; set; }
        public string company_code { get; set; }
        public string currency_code { get; set; }
        public string company_name { get; set; }
        public string company_contact_number { get; set; }
        public string company_email_address { get; set; }
        public string contact_person { get; set; }
        public string license { get; set; }
        public string auth_code { get; set; }
        public string base_currency { get; set; }
        public string company_address { get; set; }
        public int smscredits { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string fax { get; set; }
        public string sequence_reset { get; set; }
        public string fin_yearstart { get; set; }
        public DateTime expiry_date { get; set; }
        public string company_website { get; set; }
        public string sequenceon { get; set; }
        public string employer_code { get; set; }
        public string country_code { get; set; }
        //public int smscredit { get; set; }
        public string employeecode { get; set; }
        public string companylogo { get; set; }
        public string welcomelogo { get; set; }
        public string company_logo_filename { get; set; }
        public string welcome_logo_filename { get; set; }
        public string letterhead_logo_filename { get; set; }
        public string upload_documents { get; set; }
        public string letterhead_logo { get; set; }
        public List<CurrencyList> CurrencyList { get; set; }
    }
}