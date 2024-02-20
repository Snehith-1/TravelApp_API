using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    //public class VendorPaymentmodel
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; } 
    //}
    //public class VendorPayment: VendorPaymentmodel
    //{
    //    public List<VendorPaymentlist> VendorPaymentlist { get; set; }
    //}
    //public class VendorPaymentlist
    //{
    //    public int vendorpaymentgid { get; set; }
    //    public string vendor_gid { get; set; }
    //    public string paymentvalue { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //}
    //public class VendorPaymentdetails : VendorPaymentmodel
    //{

    //    public int vendorpaymentgid { get; set; }
    //    public string vendorgid { get; set; }
    //    public string paymentvalue { get; set; }
    //    public string createdby { get; set; }
    //    public string createddate { get; set; }
    //}
    public class VendorPaymentmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class VendorPayment : VendorPaymentmodel
    {
        public string salesorder_gid { get; set; }
        public List<VendorPaymentlist> VendorPaymentlist { get; set; }
    }
    public class VendorPaymentlist
    {
        public string salesactivity_gid { get; set; }
        public string invoice_date { get; set; }
        public string customer_gid { get; set; }
        public string invoice_refnumber { get; set; }
        public string vendor_invoice_refnumber { get; set; }
        public string vendor_gid { get; set; }
        public string customerinvoice_gid { get; set; }
        public string invoice_status { get; set; }
        public string invoice_amount { get; set; }
        public string paid_amount { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address_line1 { get; set; }
        public string vendor_amount { get; set; }
        public string vendor_code { get; set; }
        public string vendor_companyname { get; set; }
        public string remarks { get; set; }
        public string paymentnotemain_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string paymentreceipt_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public string process { get; set; }
        public string service_type { get; set; }
        public string contact_number { get; set; }
        public string activity_name { get; set; }
        public string status { get; set; }
        public double total_amount { get; set; }
        public string salesorder_refnumber { get; set; }
        public DateTime created_date { get; set; }
        public string invoice_gid { get; set; }
        public string vendorinvoice_gid { get; set; }
        public double vendorinvoice_amount { get; set; }
        public double payment_amount { get; set; }
        public double outstanding_amount { get; set; }
        public double refund_amount { get; set; }
        public double vendorcancellation_charges { get; set; }
        public string vendor_refundamount { get; set; }


    }
    public class VendorPaymentdetails : VendorPaymentmodel
    {
        public string  vendorpayment_gid { get; set; }
        public string customerinvoice_gid { get; set; }
        public string paymentnotemain_gid { get; set; }
        public string vendor_gid { get; set; }
        public string vendor_name { get; set; }
        public string address { get; set; }
        public string vendor_address_line1 { get; set; }
        public string vendor_number { get; set; }
        public string vendor_amount { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string contact_number { get; set; }
        public string remarks { get; set; }
        public double amount { get; set; }
        public string salesorder_gid { get; set; }
        public double net_amount { get; set; }
        public double discount_amount { get; set; }
        public string invoice_date { get; set; }
        public string vendorinvoice_refnumber { get; set; }
        public string invoice_refnumber { get; set; }
        public string payment_date { get; set; }
        public List<vendoractivitylist> vendoractivitylist { get; set; }
        public List<VendorPaymentlist> VendorPaymentlist { get; set; }
        public List<vendorinvoicelist> vendorinvoicelist { get; set; }
        public List<vendorpaymentsummarylist> vendorpaymentsummarylist { get; set; }
        public List<salesorderlist> salesorderlist { get; set; }
        public string vendorinvoice_gid { get; set; }
        public string currency_name { get; set; }
        public double exchange_rate { get; set; }
        public string payment_mode { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string transaction_number { get; set; }
        public string payment_refnumber{ get; set; }
        public string debit_amount { get; set; }
        public string created_by { get; set; }
        public double vendorinvoice_amount { get; set; }
        public string terms_conditions { get; set; }
        public string payment_amount { get; set; }
        public string receipt_date { get; set; }
        public string reference_number { get; set; }
        public string invoice_gid { get; set; }
        public string debit_date { get; set; }
    }
    public class vendorinvoicelist
    {
        public int ref_no { get; set; }
        public string vendorinvoice_gid { get; set; }
        public string invoice_date { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address_line1 { get; set; }
        public double payment_amount { get; set; }
        public double discount_amount { get; set; }
        public double net_amount { get; set; }
        public string invoice_refnumber { get; set; }
       // public string created_date { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public string vendor_gid { get; set; }
        public int vendorpaymentdtl_gid { get; set; }
        public string remarks { get; set; }
        public double vendorinvoice_amount { get; set; }
        public string invoice_gid { get; set; }
        public double debit_amount { get; set; }
        public double pending_amount { get; set; }
        public string salesinvoice_refnumber { get; set; }
        public string company_code { get; set; }
    }
    public class vendorpaymentsummarylist
    {
        public int ref_no { get; set; }
        public string vendorpayment_gid { get; set; }
        public string vendor_gid { get; set; }  // 
        public string payment_amount { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address_line1 { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public string reference_number { get; set; }
        public string payment_date { get; set; }
    }
    public class salesorderlist
    {
        public string salesorder_gid { get; set; }

    }

}                   
                                                                                                               