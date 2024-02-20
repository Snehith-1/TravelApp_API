using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Refundmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class Refund : Refundmodel
    {

        public string from_date { get; set; }
        public string to_date { get; set; }
        public string ticket_number { get; set; }
        public string service_name { get; set; }
        public List<Refundlist> refundlist { get; set; }
        public List<refundreceiptlist> refundreceiptlist { get; set; }
        public List<refundadvancelist> refundadvancelist { get; set; }

    }
    public class Refundlist:Refundmodel
    {
        public int refund_gid { get; set; }
        public int customerrrefund_gid { get; set; }
        public string customerinvoice_gid { get; set; }
        public string paid_status { get; set; }
        public string refundreference_gid { get; set; }
        public string service_type { get; set; }
        public string receipt_method { get; set; }
        public string salesinvoice_refno { get; set; }
        public double received_amount { get; set; }
        public double customer_refund { get; set; }
        public double vendor_refund { get; set; }
        public double customer_cancellation { get; set; }
        public double vendor_cancellation { get; set; }
        public double invoice_amount { get; set; }
        public string paid_amount { get; set; }
        public string company_code { get; set; }
        public string refund_number { get; set; }
        public string invoice_refnumber { get; set; }
        public string refund_date { get; set; }
        public string salesorder_refnumber { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string refund_type { get; set; }
        public string national_id { get; set; }
        public int salesorder_gid { get; set; } //salesorder form have salesorder_gid
        public double refund_amount { get; set; }
        public double advance_paid { get; set; }
        public double net_amount { get; set; }
        public string created_by { get; set; }
        public string customer_gid { get; set; }
        public DateTime created_date { get; set; }
        public string cancellation_charge { get; set; }
        public string payment_refnumber { get; set; }
        public string vendor_name { get; set; }
        public double vendor_amount { get; set; }
        public double vendorefund_amount { get; set; }
        public string payment_mode { get; set; }
        
                            
    }
    public class Refunddetails:Refundmodel
    {
         
        public int refund_gid { get; set; }
        public string invoice_date { get; set; }
        public string refund_refnumber { get; set; }
        public DateTime refund_date { get; set; }
        public string salesorder_refnumber { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string refund_type { get; set; }
        public string national_id { get; set; }
        public int salesorder_gid { get; set; }  //salesorder form have salesorder_gid
        public double refund_amount { get; set; }
        public double received_amount { get; set; }
        public double net_amount { get; set; }
        public string created_by { get; set; }
        public string customer_gid { get; set; }
        public string payment_mode { get; set; }
        public string payment_refnumber { get; set; }
        public DateTime created_date { get; set; }
        public string refundreference_gid { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_refnumber { get; set; }
        public string receipt_date { get; set; }
        public string reference_number { get; set; }
        public string invoice_refno { get; set; }
        public double receipt_amount { get; set; }
        public double invoice_amount { get; set; }

        //public string created_by { get; set; }
        public string contact_details { get; set; }
        public string receipt_gid { get; set; }
        public double advance_amount { get; set; }
        public double advance_refund { get; set; }
        public string advance_gid { get; set; }
        public string cancellation_charge { get; set; }
        public string advance_date { get; set; }
        public string vendor_name { get; set; }
        public double vendor_amount { get; set; }
        public double vendorcancellation_amount { get; set; }
        public string service_details { get; set; }
        public string vendor_gid { get; set; }
        public string invoice_gid { get; set; }
        public string customerinvoice_gid { get; set; }

    }
    public class refundreceiptlist
    {

        public string receipt_date { get; set; }
        public string reference_number { get; set; }
        public string reference_gid { get; set; }
        public string customer_name { get; set; }
        public double refund_amount { get; set; }
        public double receipt_amount { get; set; }
        public string created_by { get; set; }
        public string contact_details { get; set; }
        public string receipt_gid { get; set; }
        public string customer_gid { get; set; }
        public string customerinvoice_gid { get; set; }
    }
    public class refundadvancelist
    {
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string salesorder_refnumber { get; set; }  
        public double advance_amount { get; set; }
        public double advancerefund { get; set; }
        public string created_by { get; set; }
        public string advance_gid { get; set; }
        public string customer_gid { get; set; }
    }

    public class refundledgerdetails : Refundmodel
    {
        public string salesorder_gid { get; set; }
        public string customerinvoice_gid { get; set; }
        public string refund_number { get; set; }
        public List<Refundlist> refundlist { get; set; }
    }
}