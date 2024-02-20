using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Creditnote
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class creditnotereceiptlist : Creditnote
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string remarks { get; set; }
        public string receipt_method { get; set; }
        public string receipt_date { get; set; }
        public string created_by { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string transaction_number { get; set; }
        public string journal_from { get; set; }
        public int currency_gid { get; set; }
        public double exchange_rate { get; set; }
        public double credit_amount { get; set; }
        public double paid_amount { get; set; }
        public double receipt_amount { get; set; }
        public double discount_amount { get; set; }
        public string national_id { get; set; }
        public string invoice_gid { get; set; } // changes made int to string
        public int salesorder_gid { get; set; }
        public string customerreceipt_gid { get; set; }
        public string receipt_refnumber { get; set; }
        public string credit_date { get; set; }
        public string reference_number { get; set; }
        public List<creditnotereceipt> creditnotereceipt { get; set; }
        public List<debitnotereceipt> debitnotereceipt { get; set; }
        public string customerinvoice_gid { get; set; }

    }
    public class creditnotereceipt
    {
        public string invoice_date { get; set; }
        public string invoice_refnumber { get; set; }
        public double invoice_amount { get; set; }
        public string invoice_status { get; set; }
        public double receipt_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string salesorder_refnumber { get; set; }
        public double total_amount { get; set; }
        public string customer_name { get; set; }
        public string invoice_gid { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string credit_amount { get; set; }
        public string total_withtax { get; set; }
        public string credit_date { get; set; }
        public string customerinvoice_gid { get; set; }

    }
    public class debitnotereceipt
    {
        public string invoice_date { get; set; }
        public string invoice_refnumber { get; set; }
        public string net_amount { get; set; }
        public string vendor_name{ get; set; }
        public string invoice_gid { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string debit_amount { get; set; }
        public string total_withtax { get; set; }
        public string debit_date { get; set; }

    }
}