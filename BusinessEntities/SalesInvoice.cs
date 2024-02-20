using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    
        public class SalesInvoicemodel
        {
            public bool status { get; set; }
            public string message { get; set; }
            //public List<customerinvoiceselectlist> customerinvoiceselectlist { get; set; }
        }
        public class SalesInvoice : SalesInvoicemodel
    {
            public List<salesinvoicelist> salesinvoicelist { get; set; }
        public string customerinvoice_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string customer_gid { get; set; }


    }
    public class salesinvoicelist
        {
            public string customerinvoice_gid { get; set; }
            public string invoice_date { get; set; }
            public string invoice_refnumber { get; set; }
            public string customer_name { get; set; }
            public string vendor_gid { get; set; }
            public string vendor_name { get; set; }
            public string contact_details { get; set; }
            public int salesactivity_gid { get; set; }
            public string salesorder_refnumber { get; set; }
            public int salesorder_gid { get; set; }
            public string service_type { get; set; }
            public string receipt_method { get; set; }
            public string remarks { get; set; }
            public double total_amount { get; set; }
            public DateTime created_date { get; set; }
            public string created_by { get; set; }
            public double invoice_amount { get; set; }
            public string customer_gid { get; set; }
            public string national_id { get; set; }
            public string billing_companyname { get; set; }
            public string billing_address { get; set; }
            public string customer_type { get; set; }
            public string customerreceipt_gid { get; set; }
            public string company_code { get; set; }
            public string reference_number { get; set; }
            public string receipt_date { get; set; }
            public string branch_name { get; set; }//vendor_amount
        public string vendor_amount { get; set; }

        public string receipt_amount { get; set; }
            public string outstanding_amount { get; set; }
            public string invoice_gid { get; set; }
            public string paid_amount { get; set; }
        }
        public class salesinvoicedetail : customerinvoicemodel
        {
            // public int invoice_gid { get; set; }
            public int invoice_gid { get; set; } // changes made int to string
            public int salesorder_gid { get; set; }
            public string invoice_date { get; set; }
            public string invoice_refnumber { get; set; }
            public string salesorder_refnumber { get; set; }
            public string reference_number { get; set; }
            public string customer_type { get; set; }
            public string contact_type { get; set; }
            public string customer_name { get; set; }
            public string billing_companyname { get; set; }
            public string contact_number { get; set; }
            public string email_address { get; set; }
            public string remarks { get; set; }
            public string receipt_method { get; set; }
            public string created_by { get; set; }
            public double invoice_amount { get; set; }
            public string invoice_status { get; set; }
            public string currency_code { get; set; }
            public double exchange_rate { get; set; }
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
            // public double invoice_amount{ get; set; } //invoiceamountwithtax for invoice_value in DBA
            public string created_date { get; set; }
            public string paid_amount { get; set; }
            public string receipt_amount { get; set; }
            public double advance_amount { get; set; }
            public List<customerinvoicelist> customerinvoicelist { get; set; }
            public List<SalesOrderFormList> SalesOrderList { get; set; }
            public List<customerreceiptreporlist> customerreceiptreporlist { get; set; }
            public List<customeroutstandingreport> customeroutstandingreport { get; set; }
            public List<customerdashboardreport> customerdashboardreport { get; set; }
            public List<customerinvoiceselectlist> customerinvoiceselectlist { get; set; }
            public string company_name { get; set; }
            public string address { get; set; }
            public string country_name { get; set; }



        }

        //public class customerreceiptreporlist
        //{
        //    public int invoice_gid { get; set; }
        //    public string invoice_date { get; set; }
        //    public string invoice_refno { get; set; }
        //    public string customer_name { get; set; }
        //    public int salesactivity_gid { get; set; }
        //    public string salesorder_refnumber { get; set; }
        //    public int salesorder_gid { get; set; }
        //    public string service_type { get; set; }
        //    public string receipt_method { get; set; }
        //    public string remarks { get; set; }
        //    public double total_amount { get; set; }
        //    public string created_date { get; set; } // string to datetime
        //    public string created_by { get; set; }
        //    public double invoice_amount { get; set; }
        //    public string customer_gid { get; set; }
        //    public string national_id { get; set; }
        //    public string company_name { get; set; }
        //    public string address { get; set; }
        //    public string customer_type { get; set; }
        //    public string customerreceipt_gid { get; set; }
        //    public string contact_details { get; set; }
        //    public string receipt_amount { get; set; }
        //}
        //public class customeroutstandingreport
        //{
        //    public int invoice_gid { get; set; }
        //    public string invoice_date { get; set; }
        //    public string invoice_refno { get; set; }
        //    public string customer_name { get; set; }
        //    public string contact_detils { get; set; }
        //    public int salesactivity_gid { get; set; }
        //    public string salesorder_refno { get; set; }
        //    public int salesorder_gid { get; set; }
        //    public string outstanding_amount { get; set; }
        //    public string service_name { get; set; }
        //    public string receipt_method { get; set; }
        //    public string remarks { get; set; }
        //    public double paid_amount { get; set; }
        //    public string created_date { get; set; }
        //    public string created_by { get; set; }
        //    public double invoice_amount { get; set; }
        //    public string customer_gid { get; set; }
        //    public string national_id { get; set; }
        //    public string company_name { get; set; }
        //    public string address { get; set; }
        //    public string customer_type { get; set; }
        //    public double price { get; set; }
        //    public double discount_amount { get; set; }
        //    public double net_amount { get; set; }
        //    public double grand_total { get; set; }
        //    public string description { get; set; }
        //    public string customerreceipt_gid { get; set; }
        //    public int quantity { get; set; }
        //}

        //public class customerdashboardreport
        //{
        //    public string salesorder_count { get; set; }
        //    public string invoice_count { get; set; }
        //    public double invoice_amount { get; set; }
        //    public double paid_amount { get; set; }
        //    public double outstanding_amount { get; set; }
        //}

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

        //public class customerinvoiceselectlist : customerinvoicemodel
        //{
        //    public string customerinvoice_gid { get; set; }
        //    public string invoice_date { get; set; }
        //    public string customer_name { get; set; }
        //    public string salesorder_refnumber { get; set; }
        //    public double invoice_amount { get; set; }
        //    public string created_by { get; set; }
        //    public DateTime created_date { get; set; }
        //    public string paid_amount { get; set; }
        //    public string outstanding_amount { get; set; }
        //    public double advance_amount { get; set; }
        //    public string receipt_amount { get; set; }
        //}

        //public class customertransactionsdtl
        //{
        //    public string transaction_date { get; set; }
        //    public string reference_number { get; set; }
        //    public double total_credit { get; set; }
        //    public double total_debit { get; set; }
        //    public double outstanding_amount { get; set; }
        //    public string type { get; set; }
        //}
        //public class customertransactiondetails : customerinvoicemodel
        //{
        //    public string from_date { get; set; }
        //    public string to_date { get; set; }
        //    public string customer_gid { get; set; }
        //    public List<customertransactionsdtl> customertransactionsdtl { get; set; }
        //}
    }
