using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Customermodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class Customer : Customermodel
    {
        public List<Customerlist> customerList { get; set; }

    }
    public class Customerlist
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string national_id { get; set; }
        public string customer_type { get; set; }
        public string billing_address { get; set; }
        public string customer_status { get; set; }

    }

    public class Customerdetails:Customermodel
    {
        
        public string customer_gid { get; set; }

       // public string epax_name { get; set; }
        public string customer_status { get; set; }
        public string customer_name { get; set; }
        public string customer_firstname { get; set; }
        public string customer_lastname { get; set; }        
        public string email_address{ get; set; }
        public string contact_number { get; set; }
        public string national_id { get; set; }
        public string customer_type { get; set; }
        public string company_name { get; set; }
        public string billing_address { get; set; }
        public string billing_country { get; set; }
        public int credit_limit { get; set; }
        public string billing_remarks { get; set; }
        public string mailing_companyname { get; set; }
        public string mailing_address { get; set; }
        public string mailing_country { get; set; }
        public string mailing_remarks { get; set; }
        public string currency { get; set; }
        public string billing_companyname { get; set; }
        public string upload_documents { get; set; }
        public string companycode { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string passport_number { get; set; }
        public string epax_name { get; set; }
        public string air_gid { get; set; }
        public string epnr_no { get; set; }
        public string eticket_number { get; set; }
        public string eflag { get; set; }
        public string eagent_gid { get; set; }
        public string e_ticketnumber { get; set; }
        public string esecondflight_number { get; set; }
        public string efirstplace_from { get; set; }
        public string efirstplace_to { get; set; }
        public string efirststart_time { get; set; }
        public string efirstend_time { get; set; }
        public string esecondplace_from { get; set; }
        public string esecondplace_to { get; set; }
        public string esecondstart_time { get; set; }
        public string esecondend_time { get; set; }
        public string eticket_camount { get; set; }
        public string eoneflight_number { get; set; }
        public string ethirdflight_number { get; set; }
        public string efourthflight_number { get; set; }
        public string ethirdplace_from { get; set; }
        public string ethirdplace_to { get; set; }
        public string ethirdstart_time { get; set; }
        public string ethirdend_time { get; set; }
        public string efourthplace_from { get; set; }
        public string efourthplace_to { get; set; }
        public string efourthstart_time { get; set; }
        public string efourthend_time { get; set; }
        public string air_Line { get; set; }
        public double total_debit { get; set; }
        public double total_credit { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public List<CustomerstatementSummary> CustomerstatementSummary { get; set; }


    }
    public class CustomerstatementSummary
    {
        public string salesorder_gid { get; set; }
        public string vendorbudget_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }
        public double total_debit { get; set; }
        public double total_credit { get; set; }
        public string service_type { get; set; }
        public string invoice_refnumber { get; set; }
        public string payment_source { get; set; }
        public string created_date { get; set; }


    }

}