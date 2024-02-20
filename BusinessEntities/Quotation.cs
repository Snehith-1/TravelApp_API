using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Quotationmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string salesorder_gid { get; set; }
    }

    public class Quotation:Quotationmodel
    {
        public List<QuotationList> quotationlist { get; set; }
        
    }

    public class QuotationList :Quotationmodel
    {
        public int quotation_gid { get; set; }
        public string quotation_date { get; set; }
        public string quotation_refnumber { get; set; }
        public string enquiry_refnumber { get; set; }
        public string customer_name { get; set; }
        public string created_by { get; set; }
        public double net_amount { get; set; }//qunamount changed as net_amount
        public string quotation_status { get; set; }
        public string service_details { get; set; }
        public int enquirydtl_gid { get; set; }
        public int enquiry_gid { get; set; }
        public string company_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string remarks { get; set; }
        public string currency { get; set; }
        public double exchange_rate { get; set; }
        public double tax { get; set; }
        public double total_withtax { get; set; }
        public double discount_amount { get; set; }
        public double total_amount { get; set; }
        public double addon_charge { get; set; }
        public int quotationdtlgid { get; set; }
        public string customer_type { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string contact_type { get; set; }
        public double unit_price { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string service_gid { get; set; }
        public string service_name { get; set; }
        public int unit_gid { get; set; }
        public string unit_name { get; set; }
        public string company_code { get; set; }
        public string branch_name { get; set; }
        public int quotationdtl_gid { get; set; }
        public string customer_gid { get; set; }
    }
    public class Quotationdetail :Quotationmodel
    {
        public string service_details { get; set; }
        public int quotationdtl_gid { get; set; }
        public int quotation_gid { get; set; }
        public int enquiry_gid { get; set; }
        public string quotation_date { get; set; }
        public string quotation_refnumber { get; set; }
        public string enquiry_refnumber{ get; set; }
        public string customer_type { get; set; }
        public string contact_type { get; set; }
        public string customer { get; set; }
        public string customer_name { get; set; } // changes made quotationedit.
        public string company_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public double quotation_amount { get; set; }
        public string quotation_status { get; set; }
        public int currency_gid { get; set; }
        public double exchange_rate { get; set; }
        public double tax { get; set; }
        public double total_amount { get; set; }
        public double discount_amount { get; set; }
        public double net_amount { get; set; }
        public double service_amount { get; set; }
        public double addon_charge { get; set; }
        public string dtlremarks { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
        public string customer_gid { get; set; }
        public List<QuotationList> quotationlist { get; set; }
        public string terms_conditions { get; set; }

    }
}