using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Enquirymodel
    {
        public bool status { get; set; }
        public string message { get; set; }

    }
    public class Enquiry : Enquirymodel
    {
        public List<EnquiryList> EnquiryList { get; set; }
        public List<traveldetails> traveldetails { get; set; }
        public List<Enquiryshowlist> Enquiryshowlist { get; set; }
        public List<Activityloglist> Activityloglist { get; set; }
        public List<quotationdtllist> quotationdtllist { get; set; }
        public List<Unitlist> Unitlist { get; set; }

    }

    public class traveldetails //Enquirymodel
    {
        public string service_name { get; set; }
        public string enquiry_gid { get; set; }
        public string service_details { get; set; }
        public string chk_status { get; set; }
        public int enquirydtl_gid { get; set; }
        public int service_gid { get; set; }

    }
    public class EnquiryList
    {
        public string enquiry_date { get; set; }
        public int enquiry_gid { get; set; }
        public int enquirydtl_gid { get; set; }
        public string enquiry_refnumber { get; set; }
        public string customer_name { get; set; }
        public string contact_number{ get; set; }
        public string enquiry_source { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string enquiry_status { get; set; }
        public string service_details { get; set; }
        public double service_amount { get; set; }
        public string service_gid { get; set; }
        public string branch_name { get; set; }
        public string passport_number { get; set; }

    }
    public class Enquirydetails : Enquirymodel
    {
        public int enquiry_gid { get; set; }
        public int enquirylog_gid { get; set; }
        public string service_details { get; set; }
        public string enquiry_refnumber { get; set; }
        public string customer_type { get; set; }
        public string country_gid { get; set; }
        public string enquiry_source { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string company_name { get; set; }
        public string contact_number { get; set; }
        public string travel_from { get; set; }
        public string travel_to { get; set; }
        public string email_address { get; set; }
        public string remarks { get; set; }
        public string travel_remarks { get; set; }
        public string numberof_peopletravel { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public int infant { get; set; }
        public string enquiry_status { get; set; }
        public string activitylog_remarks { get; set; }
        public string enquiry_date { get; set; }
        public string enquiry { get; set; }
        public string address { get; set; }
        public List<traveldetails> traveldetails { get; set; }
        public List<EnquiryList> EnquiryList { get; set; }
        public List<Activityloglist> Activityloglist { get; set; }
        public List<Unitlist> Unitlist { get; set; }
        public string nextreminder_date { get; set; }
        public string national_id { get; set; }
        public string passport_number { get; set; }

    }
    public class Enquirydelete : Enquirymodel
    {
        public int enquiry_gid { get; set; }

    }

    //public class Enquirydetaillist
    //{
    //    public string enquirygid { get; set; }
    //    public int enquirydtlgid { get; set; }
    //    public string servicedetails { get; set; }

    //}

    public class Enquiryshowlist
    {
        public int service_gid { get; set; }
        public string service_name { get; set; }
        public string service_code { get; set; }
    }


    public class quotationdetail : Enquirymodel
    {
        public int quotation_gid { get; set; }
        public string quotation_refnumber { get; set; }
        public string quotation_date { get; set; }
        public int enquiry_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_type { get; set; }
        public string company_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public int currency_gid { get; set; }
        public double exchange_rate { get; set; }
        public string remarks { get; set; }
        public double quotation_amount { get; set; } //double
        public string quotation_status { get; set; }
        public string customer_type { get; set; }
        //public double tax { get; set; }
        //public double totalwithtax { get; set; }
        public double total_amount { get; set; }
        public string passport_number { get; set; }

        public double discount_amount { get; set; }
        public double addon_charge { get; set; }
        public double net_amount { get; set; }
        public string activitylog_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string terms_conditions { get; set; }
        public string travel_from { get; set; }
        public string travel_to { get; set; }
        public List<EnquiryList> EnquiryList { get; set; }
        public List<quotationdtllist> quotationdtllist { get; set; }//
    }
    public class Activityloglist
    {
        public string enquiry_status { get; set; }
        public string enquiry { get; set; }
        public string nextreminder_date { get; set; }
        public string activitylog_remarks { get; set; }
        public int enquirylog_gid { get; set; }

    }
    public class quotationdtllist
    {
        public int quotationdtl_gid { get; set; }
        public int quotation_gid { get; set; }
        public string service_details { get; set; }
        public string remarks { get; set; }
        public double total_amount { get; set; }
        public string unit { get; set; }
        public double unit_price { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string service_gid { get; set; }
        public string service_name { get; set; } 
        public string unit_name { get; set; }
        public double discount_amount { get; set; }
        public string net_amount { get; set; }
    }

    public class Unitlist
    {
        public string unit_gid { get; set; }
        public string unit_name { get; set; }

    }
}