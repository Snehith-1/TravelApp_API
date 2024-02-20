using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class customerinvoicemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
        //public List<customerinvoiceselectlist> customerinvoiceselectlist { get; set; }
    }
  
    public class customerinvoice : customerinvoicemodel
    {
        public List<customerinvoicelist> customerinvoicelist { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }

    public class MdlRefund
    {
        public string bank_gid { get; set; }
        public string cancellation_charge { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string payment_mode { get; set; }
        public string payment_refnumber { get; set; }
        public string refundServiceTypeList { get; set; }
        public string refund_number { get; set; }
        public string refund_refnumber { get; set; }
        public string refunddate { get; set; }
        public string salesinvoice_refno { get; set; }
        public string salesorder_gid { get; set; }
        public string transaction_refnumber { get; set; }
    }

    public class MdlRefundServiceType : customerinvoicemodel
    {
        public List<customerinvoicelist> refundServiceTypeList { get; set; }
        public List<customerinvoicelist> customerinvoicelist { get; set; }

        public string salesorder_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string paid_status { get; set; }
        public string refund_refnumber { get; set; }
        public string refundreference_gid { get; set; }
        public string refund_type { get; set; }
        public string refund_amount { get; set; }
        public string payment_mode { get; set; }
        public string transaction_refnumber { get; set; }
        public string refund_date { get; set; }
        public string customer_gid { get; set; }
        public string refund_gid { get; set; }
        public string invoice_date { get; set; }
        public string contact_number { get; set; }
        public string customer_name { get; set; }
        public string refunddate { get; set; }
        public string salesinvoice_refno { get; set; }
        public string refund_number { get; set; }
        public string receipt_method { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string transaction_number { get; set; }
        public string received_amount { get; set; }
        public string vendorcancellation_charges { get; set; }
        public string cancellation_charge { get; set; }
        public string customerinvoice_gid { get; set; }
        public string invoice_amount { get; set; }
        public string vendorrefund_grandtotal { get; set; }
        public string paid_amount { get; set; }
        public string refund_total { get; set; }
        public string discount_amount { get; set; }
        public string net_amount { get; set; }
        public string balance_amount { get; set; }
        public string notes { get; set; }
        public string refunddtl_gid { get; set; }





    }

    public class customerRefundDetails
    {
        public customerRefundDetails()
        {
            customerinvoicelist[] customerinvoicelist = new customerinvoicelist[] { };
        }
        public customerinvoicelist[] customerinvoicelist { get; set; }
    }

    public class customerinvoicelist
    {
     
        public string customerinvoice_gid { get; set; }
        public string email_address { get; set; }
        public string passenger_firstname { get; set; }
        public string reference { get; set; }
        public string invoice_date { get; set; }
        public string contact_number { get; set; }
        public string invoice_refnumber { get; set; }
        public string customer_name{ get; set; }
        public string invoice_status { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string contact_details { get; set; }
        public int salesactivity_gid { get; set; }
        public int paymentnote_gid { get; set; }
        public string salesorder_refnumber { get; set; }
        public int salesorder_gid { get; set; }
        public string service_type { get; set; }
        public string receipt_method { get; set; }
        public string remarks { get; set; }
        public double total_amount { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public string invoice_amount { get; set; }
        public string customer_gid { get; set; }//vendor_amount
        public string national_id { get; set; }
        public double vendor_amount { get; set; }
        public string billing_companyname { get; set; }
        public string billing_address { get; set; }
        public string customer_type { get; set; }
        public string customerreceipt_gid { get; set; }
        public string company_code { get; set; }
        public string reference_number { get; set; }
        public string receipt_date { get; set; }
        public string branch_name { get; set; }
        public double receipt_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string invoice_gid { get; set; }
        public string paid_amount { get; set; }
        public string balance_amount { get; set; }
        public string ticket_number { get; set; }
        public string pnr_number { get; set; }
        public string vendor_gid { get; set; }
        public string vendor_name { get; set; }
        public string vendorinvoice_gid { get; set; }
        public double vendorinvoice_amount { get; set; }
        public double payment_amount { get; set; }
        public string customercancellation_charge { get; set; }
        public string customerrefund_amount { get; set; }
        public string vendorcancellation_charge { get; set; }
        public string vendorrefund_amount { get; set; }
        public double refund_amount { get; set; }
        public string vendorrefund_grandtotal { get; set; }
        public string refunddtl_gid { get; set; }




    }
    public class customerinvoicedetail : customerinvoicemodel
    {
        // public int invoice_gid { get; set; }


        public int salesactivity_gid { get; set; }
        public int paymentnote_gid { get; set; }
        public string vendor_gid { get; set; }
        public double vendor_amount { get; set; }

        public int invoice_gid { get; set; }
        public int customerrrefund_gid { get; set; }
        public int outstandingpayment_gid { get; set; }
        public DateTime created_date { get; set; }
        public string userGid { get; set; }

        // changes made int to string
        public int salesorder_gid { get; set; }
        public string invoice_date { get; set; }
        public string invoice_refnumber { get; set; }
        public string total { get; set; }
        public string sub_total1 { get; set; }
        public string salesorder_refnumber { get; set; }
        public string reference_number { get; set; }
        public string refund_refnumber { get; set; }
        public string terms_conditions { get; set; }
        public string client_notes { get; set; }
        public string notes { get; set; }
        public double sub_total { get; set; }
        public string customer_type { get; set; }
        public string contact_type { get; set; }
        public string customer_name{ get; set; }
        public string billing_companyname { get; set; }
        public string contact_number { get; set; }
        public string email_address{get; set; }
        public string remarks { get; set; }
        public string receipt_method { get; set; }
        public string created_by { get; set; }
        public double invoice_amount { get; set; }
        public double payment_amount { get; set; }
        public double paid_amount { get; set; }
        public double grandtotal_amount { get; set; }
        public string vendor_name { get; set; }
        public string invoice_status { get; set; }
        public string currency_code { get; set; }
        public double exchange_rate{ get; set; }
        public double customer_cancellation { get; set; }
        public double customer_refund { get; set; }
        public double vendor_cancellation { get; set; }
        public double vendor_refund { get; set; }
        public double tax { get; set; }
        public double total_withtax { get; set; }
        public double service_amount { get; set; }
        public double addon_charge { get; set; }
        public string billing_address { get; set; }
        public string billing_country { get; set; }
        public string salesorder_refnumner { get; set; }
        public string service_type { get; set; }
        public double total_amount { get; set; }
        public double discount_amount { get; set; }
        public string national_id { get; set; }
        public string customer_gid { get; set; }
        public string customerreceipt_gid { get; set; }
        public string customerinvoice_gid { get; set; }
        public string receipt_refnumber { get; set; }
        public string receipt_date { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public double net_amount { get; set; }
        public double unit { get; set; }
        public string reference_code { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string transaction_number { get; set; }
        public string journal_from { get; set; }
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
        public string oneflight_number { get; set; }
        public string twoflight_from { get; set; }
        public string twoflight_to { get; set; }
        public string twodeparture_date { get; set; }
        public string twoflight_time { get; set; }
        public string twoflight_number { get; set; }
        public string segment { get; set; }
        public string pnr_number { get; set; }
        public double ticket_camount { get; set; }
        public string ticketvendor_name { get; set; }
        public double ticket_vamount { get; set; }

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
        public double servicetype_totalamount { get; set; }

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

        //AIR INVOICE
        public string air_gid { get; set; }
        public string epax_name { get; set; }
        public string eticket_number { get; set; }
        public string epnr_no { get; set; }
        public string etrip_type { get; set; }
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
        public string ecustomer_camount { get; set; }
        public string evendor_name { get; set; }
        public string evendor_vamount   { get; set; }

        public string esegment { get; set; }
        public string ethirdplace_from  { get; set; }
        public string ethirdplace_to { get; set; }
        public string ethirdstart_time { get; set; }
        public string ethirdend_time { get; set; }
        public string efourthplace_from { get; set; }
        public string efourthplace_to { get; set; }
        public string efourthstart_time { get; set; }
        public string efourthend_time { get; set; }
        public string eoneflight_number { get; set; }
        public string esecondflight_number { get; set; }
        public string ethirdflight_number { get; set; }
        public string efourthflight_number { get; set; }
        public string eticket_camount { get; set; }

        // public double invoice_amount{ get; set; } //invoiceamountwithtax for invoice_value in DBA
        //public string created_date { get; set; }
        public string refund_date { get; set; }
        public double balance_amount { get; set; }
        public double receipt_amount { get; set; }
        public double advance_amount { get; set; }

        public List<customerinvoicelist> customerinvoicelist { get; set; }
        public List<SalesOrderFormList> SalesOrderList { get; set; }
        public List<customerreceiptreporlist> customerreceiptreporlist { get; set; }
        public List<customeroutstandingreport> customeroutstandingreport { get; set; }
        public List<customerdashboardreport> customerdashboardreport { get; set; }
        public List<customerinvoiceselectlist> customerinvoiceselectlist { get; set; }
        public List<customerrefundreport> customerrefundreport { get; set; }
        public List<servicetypeList> servicetypeList { get; set; }

        public string company_name { get; set; }
        public string address { get; set; }
        public string country_name { get; set; }



    }
    public class servicetypeList
    {
        public string service_type { get; set; }
        public string service_count { get; set; }
    }
    public class customerreceiptreporlist
    {
        public int invoice_gid { get; set; }
        public string invoice_date { get; set; }
        public string invoice_refno { get; set; }
        public string customer_name{ get; set; }
        public int salesactivity_gid { get; set; }
        public string salesorder_refnumber { get; set; }
        public int salesorder_gid { get; set; }
        public string service_type { get; set; }
        public string receipt_method { get; set; }
        public string remarks { get; set; }
        public double total_amount { get; set; }
        public string created_date { get; set; } // string to datetime
        public string created_by { get; set; }
        public double invoice_amount{ get; set; }
        public string customer_gid{ get; set; }
        public string national_id { get; set; }
        public string company_name { get; set; }
        public string address { get; set; }
        public string customer_type { get; set; }
        public string customerreceipt_gid { get; set; }
        public string contact_details { get; set; }
        public double receipt_amount { get; set; }
    }
    public class customeroutstandingreport
    {
        public int invoice_gid { get; set; }
        public string invoice_date { get; set; }
        public string invoice_refno { get; set; }
        public string customer_name{ get; set; }
        public string contact_detils { get; set; }
        public int salesactivity_gid { get; set; }
        public string salesorder_refno { get; set; }
        public int salesorder_gid { get; set; }
        public double outstanding_amount { get; set; }
        public string service_name { get; set; }
        public string receipt_method { get; set; }
        public string remarks { get; set; }
        public double paid_amount { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public double invoice_amount { get; set; }
        public string customer_gid { get; set; }
        public string national_id { get; set; }
        public string company_name { get; set; }
        public string address { get; set; }
        public string customer_type { get; set; }
        public double price { get; set; }
        public double discount_amount { get; set; }
        public double net_amount { get; set; }
        public double grand_total { get; set; }
        public string description { get; set; }
        public string customerreceipt_gid { get; set; }
        public int quantity { get; set; }
    }

    public class customerdashboardreport
    {
        public string salesorder_count { get; set; }
        public string invoice_count { get; set; }
        public double invoice_amount { get; set; }
        public double paid_amount { get; set; }
        public double outstanding_amount { get; set; }
    }

    public class customerrefundreport
    {
        public string refund_date { get; set; }
        public string refund_amount { get; set; }
        public string invoice_refnumber { get; set; }
        public string salesorder_gid { get; set; }
        public string refund_number { get; set; }
        public string customerinvoice_gid { get; set; }
    }

    //invoice_date = rd["created_date"].ToString(),
    //                    customer = rd["customer_name"].ToString(),
    //                    //contactdetils = rd["contact_details"].ToString(),
    //                    salesorderrefno = rd["salesorder_refno"].ToString(),
    //                    invoicegid = rd["customerinvoice_gid"].ToString(),
    //                    invoiceamountwithtax = Double.Parse(rd["invoice_value"].ToString()),
    //                    createdby = rd["created_by"].ToString(),
    //                    createddate = DateTime.Parse(rd["created_date"].ToString()),
    //                    paidamt = rd["receipt_amount"].ToString(),
    //                    receiptamt = rd["outstanding"].ToString(),

    public class customerinvoiceselectlist : customerinvoicemodel
    {
        public string customerinvoice_gid { get; set; }
        public string invoice_date { get; set; }
        public string customer_name { get; set; }
        public string salesorder_refnumber { get; set; }       
        public double invoice_amount { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public double paid_amount { get; set; }
        public double outstanding_amount { get; set; }
        public double advance_amount { get; set; }
        public double receipt_amount { get; set; }
    }

    public class customertransactionsdtl
    {
        public string transaction_date { get; set; }
        public string reference_number { get; set; }
        public double total_credit { get; set; }
        public double total_debit { get; set; }
        public double outstanding_amount{ get; set; }
        public string type { get; set; }
    }
    public class customertransactiondetails:customerinvoicemodel
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string customer_gid { get; set; }
        public List<customertransactionsdtl> customertransactionsdtl { get; set; }
    }

    public class outstandingCustomerInvoiceList : customerinvoicemodel
    {
        public string customer_gid { get; set; }
        public double total_outstanding { get; set; }
        public double total_paid_amount { get; set; }
        public double total_invoice_amount { get; set; }
        public List<customerinvoicelist> customerinvoicelist { get; set; }
    }
}
