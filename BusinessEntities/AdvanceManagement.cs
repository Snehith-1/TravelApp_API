using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class AdvanceManagementmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class advancemanagement: AdvanceManagementmodel
    {
        public List<Advancemanagementlistitem> Advancemanagementlistitem { get; set; }
    }

    //public class advanceinvoicedetails:AdvanceManagement
    //{ 
    //    public List<Advanceinvoielist> advanceinvoielist { get; set; }
     
    //}
    public class Advancemanagementlistitem
    {
        public DateTime advance_date { get; set; }
        public double advance_amount { get; set; }
        public string payment_details { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_no { get; set; }
        public string payment_mode { get; set; }
        public string salesorder_refnumber { get; set; }
        public string salesorder_gid { get; set; }
        public double paidadvance_amount { get; set; }
        public double advanceoutstanding_amount { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string national_id { get; set; }
        public int phone_no { get; set; }
        public string remarks { get; set; }
        public string contact_details { get; set; }
         public string customerinvoice_gid { get; set; }
        public double paid_amount { get; set; }
        public string invoicevalue { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public double receipt_amount { get; set; }
        public double invoice_amount { get; set; }
        public double outstanding_amount { get; set; }


    }
    public class Advanceinvoielist
    {
        public string invoice_refnumber { get; set; }
        public DateTime invoice_date { get; set; }
        public double invoice_amount { get; set; }
        public double receiptpaid_amount { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public double invoiceoutstanding_amount { get; set; }

    }
    public class Advancecustomerdetails : AdvanceManagementmodel
    {

        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string national_id { get; set; }
        public int phone_no { get; set; }
        public string remarks { get; set; }
        public string contact_details { get; set; }
       public List<Advancemanagementlistitem> Advancemanagementlistitem { get; set; }
    }
}