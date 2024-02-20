using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class billingmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class billing : billingmodel
    {
        public List<billinglist> billinglist { get; set; }
        public double vendor_amount { get; set; }
        public int salesorder_gid { get; set; }
        public string vendor_gid { get; set; }
    }
    public class Billingdetail : billingmodel
    {
        public object currency;

        public List<billinggetlist> billinggetlist { get; set; }
        public List<billinglist> billinglist { get; set; }
        public int salesorder_gid { get; set; }
        public int billing_gid { get; set; }
        public string customer_gid { get; set; }
        public string service_name { get; set; }
        public string customer_name { get; set; }
        public string billingcontactdetail { get; set; }
        public string description { get; set; }// billingactivityname changed as description
        public string billable { get; set; }
        public string vendor_gid { get; set; }
        public double unit_price { get; set; }
        public double balance_amount { get; set; }
        public double paid_amount { get; set; }
        public double total { get; set; }
        public double sub_total1 { get; set; }
        public string customer_type { get; set; }
        public string reference_gid { get; set; }
        public string salesorder_refnumber { get; set; }
        public string customer_code { get; set; }
        public int currency_gid { get; set; }
        public int branch_gid { get; set; }

        public int branch_name { get; set; }
        public string passenger_name { get; set; }
        public string id_proof { get; set; }
        public string photo { get; set; }
        public double pass_camount { get; set; }
        public string passvendor_name { get; set; }
        public double pass_vamount { get; set; }
        public string application_date { get; set; }
        public string expiry_date { get; set; }
        public string visa_period { get; set; }
        public string country { get; set; }
        public double visa_camount { get; set; }
        public string visavendor_name { get; set; }
        public double visa_vamount { get; set; }
        public string flightno { get; set; }
        public string ticket_number { get; set; }
        public string sector_number { get; set; }
        public string flight_class { get; set; }
        public string flight_routing { get; set; }
        public string departure_date { get; set; }
        public string flight_time { get; set; }
        public string flight_from { get; set; }
        public string flight_to { get; set; }
        public string twodeparture_date { get; set; }
        public string oneflight_number { get; set; }
        public string twoflight_from { get; set; }
        public string twoflight_to { get; set; }
        public string twoflight_time { get; set; }
        public string twoflight_number { get; set; }
        public string segment { get; set; }
        public double ticket_camount { get; set; }
        public string ticketvendor_name { get; set; }
        public double ticket_vamount { get; set; }
        public string receipt_method { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string transaction_number { get; set; }
        public string hotelname { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string destination { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
        public string totalnodays { get; set; }
        public string totalnoPassenger { get; set; }
        public string hotelvendor_name { get; set; }
        public double hotel_vamount { get; set; }
        public double hotel_camount { get; set; }
        public string car_type { get; set; }
        public string noofperson { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string pickup_city { get; set; }
        public string dropcity { get; set; }
        public string ctotalnodays { get; set; }
        public string carvendor_name { get; set; }
        public double car_vamount { get; set; }
        public double car_camount { get; set; }
        public string customerpaid_currency { get; set; }
        public string customreceived_currency { get; set; }
        public double cpaidamount { get; set; }
        public double creceivedamount { get; set; }
        public string packages_remarks { get; set; }
        public double packages_amount { get; set; }
        public string insuranceperson_name { get; set; }
        public string insurance_type { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string insvendor_name { get; set; }
        public double insurance_vamount { get; set; }
        public double insurance_camount { get; set; }
        public string epax_name { get; set; }
        public string eticket_number { get; set; }
        public string epnr_no { get; set; }
        public string etrip_type { get; set; }
        public string esegment { get; set; }
        public string eflag { get; set; }
        public string eagent_gid { get; set; }
        public string efirstplace_from { get; set; }
        public string efirstplace_to { get; set; }
        public string efirststart_time { get; set; }
        public string efirstend_time { get; set; }
        public string esecondplace_from { get; set; }
        public string esecondplace_to { get; set; }
        public string esecondstart_time { get; set; }
        public string esecondend_time { get; set; }
        public string ethirdplace_from { get; set; }
        public string ethirdplace_to { get; set; }
        public string ethirdstart_time { get; set; }
        public string ethirdend_time { get; set; }
        public string efourthplace_from { get; set; }
        public string efourthplace_to { get; set; }
        public string efourthstart_time { get; set; }
        public string efourthend_time { get; set; }
        public string ecustomer_camount { get; set; }
        public string eoneflight_number { get; set; }
        public string esecondflight_number { get; set; }
        public string ethirdflight_number { get; set; }
        public string efourthflight_number { get; set; }
        public string eticket_camount { get; set; }
        public string evendor_name { get; set; }
        public string evendor_vamount { get; set; }
        //public int currency{ get; set; }
        public double advance_amount { get; set; }
        public double net_amount { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public double addon_charge { get; set; }
        public double discount_amount { get; set; }
        public double grand_total { get; set; }
        public double pax { get; set; }
        public string billing_address { get; set; }
        public double total_ticket_price { get; set; }
        public int air_gid { get; set; }
        public string currency_code { get; set; }
        public double exchange_rate { get; set; }
        public string invoice_gid { get; set; }
        public string terms_conditions { get; set; }
        public string notes { get; set; }
        public string client_notes { get; set; }
        public double vendor_amount { get; set; }
        public string vendor_refnumber { get; set; }
        public string process { get; set; }
        public string paymentnote_status { get; set; }
        public string contact_details { get; set; }
        public string national_id { get; set; }
        public string activity_name { get; set; }
        public double billing_amount { get; set; }
        public double customer_amount { get; set; }
        public double service_amount { get; set; }
        public double invoice_amount { get; set; }
        public string customerinvoice_gid { get; set; }
        public double total_withtax { get; set; }
        public double ticket_price { get; set; }
        public double tvendorreferencenumber { get; set; }
        public string service_type { get; set; }
        public string pnr_number { get; set; }
        //public string flight_number { get; set; }
        public double servicetype_totalamount { get; set; }




    }

    public class billinggetlist
    {
        public int salesorder_gid { get; set; }
        public int billing_gid { get; set; }
        public string service_name { get; set; }
        public string activity_name { get; set; }
        public string process { get; set; }
        public string contact_number { get; set; }
        public double billing_amount { get; set; }
        public string description { get; set; }
        public string billable { get; set; }
        public string vendor_gid { get; set; }
        public double unit_price { get; set; }
        public string reference_gid { get; set; }
        public string billing_status { get; set; }
        public string remarks { get; set; }
        public double service_amount { get; set; }
        public double customer_amount { get; set; }
        public double net_amount { get; set; }
        public double total_amount { get; set; }
        public string unit { get; set; }


    }

    public class billinglist
    {
        public int salesorder_gid { get; set; }
        public int billing_gid { get; set; }
        public string service_name { get; set; }
        public string billingcontactdetail { get; set; }
        public string description { get; set; }
        public string billable { get; set; }
        public double unit_price { get; set; }
        public string vendor_gid { get; set; }
        public int paymentnote_gid { get; set; }
        public string date { get; set; }
        public string remarks { get; set; }
        public string vendor_name { get; set; }
        public double service_amount { get; set; }
        public string quantity { get; set; }
        public double discount_amount { get; set; }
        public double total_amount { get; set; }
        public string unit { get; set; }
        public double net_amount { get; set; }
        public string process { get; set; }
        public string contact_number { get; set; }
        public string activity_name { get; set; }
        public string billing_amount { get; set; }
        public string vendor_companyname { get; set; }
        public double vendor_amount { get; set; }
        public double customer_amount { get; set; }
        public string customerinvoice_gid { get; set; }
        public string service_details { get; set; }
        public string reference_number { get; set; }
        public string tvendorreferencenumber { get; set; }
    }
    ////public class Unitlist
    ////{
    ////    public string unit_gid { get; set; }
    ////    public string unit_name { get; set; }

    ////}
}
