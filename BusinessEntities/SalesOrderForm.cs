using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessEntities
{
    public class SalesOrderFormModel
    {

        public bool status { get; set; }
        public string message { get; set; }
        public int salesorder_gid { get; set; } 
        public int service_gid { get; set; }
    }
    public class SalesOrderForm : SalesOrderFormModel
    {
        public List<SalesOrderFormList> SalesOrderList { get; set; }
        public List<SOPassengerList> SOPassengerList { get; set; }
        public List<SOVisaList> SOVisaList { get; set; }
        public List<SOPassportList> SOPassportList { get; set; }
        public List<SOFlightList> SOFlightList { get; set; }
        public List<SOHotelList> SOHotelList { get; set; }
        public List<SOCarList> SOCarList { get; set; }
        public List<SOForexList> SOForexList { get; set; }
        public List<SOPackageDetailList> SOPackageDetailList { get; set; }
        public List<SOOtherServiceDetailList> SOOtherServiceDetailList { get; set; }
        public List<SOInsurenceList> SOInsurenceList { get; set; }
        public List<Customerlist> customerList { get; set; }
        public List<ActivityList> ActivityList { get; set; }
        public List<CurrencyList> CurrencyList { get; set; }
        public List<SOActivityList> SOActivityList { get; set; }        
        public string customergid { get; set; }
    }
    public class SalesOrderFormList
    {
        public double total_amount { get; set; }
        public double invoice_amount { get; set; }
        public string currency_gid { get; set; }
        public string net_amount{ get; set; }
        public string discount_amount { get; set; }
        public string advance_paid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string national_id { get; set; }
        public string billing_address { get; set; }
        public int salesorder_gid { get; set; }
        public string passport_no { get; set; }
        public string salesorder_refnumber { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public int salesteam_gid { get; set; }
        public int salesteamemployee_gid { get; set; }
        public string contact_details { get; set; }
        public string service_type { get; set; }
        public string billing_companyname { get; set; }
        public string branch_name { get; set; }
        public string customer_type { get; set; }
        public List<ActivityList> ActivityList { get; set; }
        public string salesorder_status { get; set; }
        public double receipt_amount { get; set; }
        public double outstanding_amount { get; set; }
        public double refund_amount { get; set; }
        public double advance_amount { get; set; }
        public string billing_status { get; set; }

    }
    public class SalesOrderFormDetail : SalesOrderFormModel
    {        
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
        public string contact_number { get; set; }
        public int total { get; set; }
    }
    public class SOPassengerDetail : SalesOrderFormModel
    {
        public string passengerservice_gid { get; set; }
        public string passenger_firstname { get; set; }       
        public string passenger_lastname { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string passport_number { get; set; }
        public string passport_expirydate { get; set; }
        public string passport_issueddate { get; set; }
        public int salesactivity_gid { get; set; }
        public string tmppassengerservice_gid { get; set; }
    }
    public class SOPassengerList : SalesOrderFormModel
    {
        public string passengerservice_gid { get; set; }
        public string passenger_name { get; set; }   
        public string passenger_firstname { get; set; }
        public string passenger_lastname { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string passport_number { get; set; }
        public string passport_expirydate { get; set; }
        public string passport_issueddate { get; set; }
        public string tmppassengerservice_gid { get; set; }
        public string name { get; set; }
        public string epax_name { get; set; }
        public string eticket_number { get; set; }
        public string epnr_no { get; set; }
    }

    public class SOActivityList
    {
        public string salesactivity_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string service_type { get; set; }
        public string reference { get; set; }
        public string remarks { get; set; }
        public string salesactivity_status { get; set; }
        public string total_amount { get; set; }
        public string vendor_company_name { get; set; }
        public string vendor_amount { get; set; }

    }
    public class SOPassportDetail : SalesOrderFormModel
    {
        public string tmppassengerservice_gid { get; set; }
        public string passengerservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string passportservice_gid { get; set; }
        public string passenger_name { get; set; }        
        public string id_proof { get; set; }
        public string additional_proof { get; set; }
        public string anygovt_document { get; set; }
        public string upload_document { get; set; }

        public string passvendor_name { get; set; }
        public double pass_vamount { get; set; }
        public double pass_vpaidamount { get; set; }

        public int photo{ get; set; }
        public double total_amount { get; set; }
    }
    public class SOPassportList : SalesOrderFormModel
    {
        public string passportservice_gid { get; set; }
        public string upload_document { get; set; }
        public string passenger_name { get; set; }        
        public string id_proof { get; set; }
        public string additional_proof { get; set; }
        public string anygovt_document { get; set; }
        public string paymentnote_gid { get; set; }
        public string submit_flag { get; set; }
        public int photo { get; set; }
        public double total_amount { get; set; }
        public string passvendor_name { get; set; }
        public double pass_vamount { get; set; }
        public double pass_vpaidamount { get; set; }

    }
    public class SOVisaDetail : SalesOrderFormModel
    {
        public string visaservice_gid { get; set; }
        public string passenger_name { get; set; }
        public string passengerservice_gid { get; set; }
       // public string visapassportno { get; set; }       
        public string invoice_refnumber { get; set; }
        public string customerinvoice_gid { get; set; }
        public string country { get; set; }
        public string application_date { get; set; }
        public string expiry_date { get; set; }
        public int visa_period { get; set; }
        public string currency_gid { get; set; }
        public double total_amount { get; set; }
        public string visavendor_name { get; set; }
        public string paymentnote_gid { get; set; }
        public double visa_vamount { get; set; }

    }
    public class SOVisaList : SalesOrderFormModel
    {
        public string visaservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string submit_flag { get; set; }
        public string visapassportno { get; set; }
        public string passenger_name { get; set; }
        public string passengerservice_gid { get; set; }
        public string country { get; set; }
        public string application_date { get; set; }
        public string expiry_date { get; set; }
        public string visa_period { get; set; }
        public int currency { get; set; }
        public double total_amount { get; set; }
        public string visavendor_name { get; set; }

        public double visa_vamount { get; set; }
    }
    public class SOFlightDetail : SalesOrderFormModel
    {
        public List<SOPassengerList> SOPassengerList { get; set; }
        public string flightservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string passport_number { get; set; }
        public string passenger_name { get; set; }

        public string flight_name { get; set; }
        public string flight_from { get; set; }
        public string flight_to { get; set; }
        public string departure_date { get; set; }
        public string flight_time { get; set; }
        public string flight_number { get; set; }
        public int currency_gid { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string pnr_number { get; set; }
        public string ticket_number { get; set; }
        public string sector_number { get; set; }
        public string flight_class { get; set; }
        public string segment { get; set; }
        public string flight_routing { get; set; }
        public string flight_airline { get; set; }
        public string passengerservice_gid { get; set; }
        public double ticket_vamount { get; set; }
        public string ticketvendor_name { get; set; }
        public string air_gid { get; set; }
        public string eairline { get; set; }
        public string esystem_use { get; set; }
        public string epax_name { get; set; }
        public string eticket_number { get; set; }
        public string epnr_no { get; set; }
        public string etrip_type { get; set; }
        public string esegment { get; set; }
        public string flighttrip_type { get; set; }

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
        public double evendor_vamount { get; set; }

        public string flight_fare { get; set; }
        public string flight_comm { get; set; }
        public string flight_sc { get; set; }
        public string flight_xt { get; set; }
        public double flight_totalcalcamount { get; set; }

    }
    public class SOFlightList : SalesOrderFormModel
    {
        public string flightservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string passenger_details { get; set; }
        public string flight_name { get; set; }
        public string flight_from { get; set; }
        public string flight_to { get; set; }
        public string departure_date { get; set; }
        public string flight_time { get; set; }
        public string flight_number { get; set; }
        public string submit_flag { get; set; }
        public string passenger_name { get; set; }
        public string flight_currency { get; set; }
        public double total_amount { get; set; }
        public string flighttrip_type { get; set; }

        public string pnr_number { get; set; }
        public string flight_remarks { get; set; }
        
        public string ticket_number { get; set; }
        public string sector_number { get; set; }
        public string flight_class { get; set; }
        public string segment { get; set; }
        public string flight_routing { get; set; }
        public string flightairline { get; set; }

        public string air_gid { get; set; }
        public string epax_name { get; set; }

        public string eticket_number { get; set; }
        public string epnr_no { get; set; }
        public string eflag { get; set; }
        public string eagent_gid { get; set; }
        public double ecustomer_camount { get; set; }
    }
    public class SOHotelDetail : SalesOrderFormModel
    {
        public string hotelservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string passenger_name { get; set; }
        public string passengerservice_gid { get; set; }
        public string remarks { get; set; }
        public string hotel_name { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string destination { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
        public int total_numberofdays { get; set; }
        public int total_numberofpassengers { get; set; }
        public double total_amount { get; set; }
        public int currency_gid { get; set; }
        public string hotelvendor_name { get; set; }
        public double hotel_vamount { get; set; }
    }
    public class SOHotelList : SalesOrderFormModel
    {
        public string remarks { get; set; }
        public string passenger_name { get; set; }
        public string hotelservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string hotel_name { get; set; }
        public string category { get; set; }
        public string hotelcategory { get; set; }
        public string city { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
        public int total_numberofdays { get; set; }
        public int total_numberofpassengers { get; set; }
        public double total_amount { get; set; }
        public string hotelcurrency { get; set; }
        public string hotelvendor_name { get; set; }
        public double hotel_vamount { get; set; }
        public string submit_flag { get; set; }
    }
    public class SOCarDetail : SalesOrderFormModel
    {
        public string carservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string car_type { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string pickup_city { get; set; }
        public string drop_city { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string remarks { get; set; }
        public int numberof_persons { get; set; }
        public int currency_gid { get; set; }
        public double total_amount { get; set; }
        public string carvendor_name { get; set; }
        public double car_vamount { get; set; }
    }
    public class SOCarList
    {
        public string submit_flag { get; set; }
        public int salesorder_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string carservice_gid { get; set; }
        public string car_type { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string pickup_city { get; set; }
        public string drop_city { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string remarks { get; set; }
        public int numberof_persons { get; set; }
        public string currency { get; set; }
        public double total_amount { get; set; }
        public bool status { get; set; }
    }
    public class SOForexDetail : SalesOrderFormModel
    {
        public string forexservice_gid { get; set; }        
        public double customerpaid_amount { get; set; }
        public string paymentnote_gid { get; set; }
        public string paidamount_currency { get; set; }
        public string paidamount_exchangerate { get; set; }
        public double total_paidamount { get; set; }
        public double customerreceived_amount { get; set; }
        public string receivedamount_exchangerate { get; set; }
        public double total_receivedamount { get; set; }
        public string receivedamount_currency { get; set; }
        public string remarks { get; set; }
    }
    public class SOForexList : SalesOrderFormModel
    {
        public string forexservice_gid { get; set; }        
        public string paidamount_currency { get; set; }
        public double customerpaid_amount { get; set; }
        public double customerpaidexchangerate { get; set; }
        public double total_paidamount { get; set; }
        public string receivedamount_currency { get; set; }
        public double customerreceived_amount { get; set; }
        public double customerreceivedexchangerate { get; set; }
        public double total_receivedamount { get; set; }
        public string forexremarks { get; set; }

    }
    public class SOPackageDetail : SalesOrderFormModel
    {
        public string packageservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string package_uploaddocs { get; set; }
        public string packagevendor_name { get; set; }
        public string package_name { get; set; }
        public string package_category { get; set; }
        public string totalnoPassenger { get; set; }
        public double package_vamount { get; set; }
        public string period { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string country { get; set; }



    }
    public class SOPackageDetailList : SalesOrderFormModel
    {
        public string packageservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string package_uploaddocs { get; set; }
        public string submit_flag { get; set; }
        public string package_name { get; set; }
        public string package_category { get; set; }


    }
    public class SOOtherServiceDetail : SalesOrderFormModel
    {
        public string otherservice_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string otherservicevendor_name { get; set; }
        public string service_name { get; set; }
        public double otherServices_vamount { get; set; }
        public string passengerservice_gid { get; set; }
        public string passenger_name { get; set; }






    }
    public class SOOtherServiceDetailList : SalesOrderFormModel
    {
        public string paymentnote_gid { get; set; }
        public string otherservice_gid { get; set; }
        public string passenger_name { get; set; }

        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string service_name { get; set; }
        public string submit_flag { get; set; }
   

    }
    public class SOInsurenceDetail : SalesOrderFormModel
    {
        public string paymentnote_gid { get; set; }
        public string insuranceservice_gid { get; set; }        
        public string name { get; set; }
        public string dob { get; set; }
        public string arrival_port { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int currency_gid { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string passengerservice_gid { get; set; }

        public double insurance_vamount { get; set; }
        public string insvendor_name { get; set; }
        public string insurance_type { get; set; }
    }
    public class SOInsurenceList : SalesOrderFormModel
    {
        public string paymentnote_gid { get; set; }
        public string insuranceservice_gid { get; set; }        
        public string name { get; set; }
        public string dob { get; set; }
        public string arrival_port { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string currency { get; set; }
        public double total_amount { get; set; }
        public string remarks { get; set; }
        public string submit_flag { get; set; }
    }

    public class soservicedelete : SalesOrderFormModel
    {
        public string passengerservice_gid { get; set; }
        public string passportservice_gid { get; set; }
        public string visaservice_gid { get; set; }
        public string flightservice_gid { get; set; }
        public string hotelservice_gid { get; set; }
        public string carservice_gid { get; set; }
        public string forexservice_gid { get; set; }
        public string packageservice_gid { get; set; }
        public string insuranceservice_gid { get; set; }
        public string air_gid { get; set; }

    } 
    public class customer
    {
        public string customer_name { get; set; }
    }
    public class sotempactivity:SalesOrderFormModel
    {
        public string created_by { get; set; }
        public string activity_source { get; set; }

    }
}