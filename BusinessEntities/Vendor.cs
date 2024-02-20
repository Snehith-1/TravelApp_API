using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Vendormodel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string vendor_code { get; set; }
        public string vendor_gid { get; set; }


    }
    public class Vendor : Vendormodel
    {
        public string vendor_company_name { get; set; }
        public string vendor_name { get; set; }
        public string vendor_gid { get; set; }
        public List<Vendorlist> vendorlist { get; set; }
        public List<activeVendorlist> activeVendorlist { get; set; }
        public List<Vendoradvancelist> Vendoradvancelist { get; set; }
        public List<Vendorledgersummarylist> Vendorledgersummarylist { get; set; }
        public List<Vendorledgerchildreport> Vendorledgerchildreport { get; set; }

    }
    public class Vendorlist
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address_line1 { get; set; }
        public string active_flag { get; set; }
        public string vendor_number { get; set; }


    }
    public class activeVendorlist
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string vendor_name { get; set; }
        public string vendor_address_line1 { get; set; }
        public string active_flag { get; set; }
        public string vendor_number { get; set; }

    }
    public class Vendordetail : Vendormodel
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string contactperson_name { get; set; }
        public string number { get; set; }
        public string tan_number { get; set; }
        public string totalvendor_paymenet { get; set; }
        public string email_address { get; set; }
        public string fax { get; set; }
        public string vendor_address_line1 { get; set; }
        public string vendor_address_line2 { get; set; }
        public string state { get; set; }
        public string vendor_status { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string currency_code { get; set; }
        public string country_name { get; set; }
        public string tax { get; set; }
        public string service_type { get; set; }
        public string ticket_vendor { get; set; }
        public string vendor_name { get; set; }
        public string advance_date { get; set; }
        public string payment_mode { get; set; }
        public string payment_details { get; set; }
        public string advance { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_number { get; set; }
        public string remarks { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string vendor_payment { get; set; }
        public string process { get; set; }
        public string receipt_method { get; set; }
        public string payment_source { get; set; }




        public List<Vendorledgerchildreport> Vendorledgerchildreport { get; set; }
    }
    public class Vendordetails : Vendormodel
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string contactperson_name { get; set; }
        public string number { get; set; }
        public string tan_number { get; set; }
        public string email_address { get; set; }
        public string fax { get; set; }
        public string vendor_address_line1 { get; set; }
        public string vendor_address_line2 { get; set; }
        public string state { get; set; }
        public string vendor_status { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string currency_code { get; set; }
        public string country_name { get; set; }
        public string tax { get; set; }
        public string service_type { get; set; }
        public string ticket_vendor { get; set; }
        public string vendor_name { get; set; }
        public string advance_date { get; set; }
        public string payment_mode { get; set; }
        public string payment_details { get; set; }
        public string advance { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_number { get; set; }
        public string remarks { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public List<Vendordetails> Vendormodel { get; set; }
    }
    public class Vendordelete : Vendormodel
    {
        public int vendor_gid { get; set; }

    }

    public class Vendoradvancelist
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_company_name { get; set; }
        public string vendor_name { get; set; }
        public string advance_date { get; set; }
        public string payment_mode { get; set; }
        public string paymentdetails { get; set; }
        public string advance_amount { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_no { get; set; }
        public string remarks { get; set; }
        public string vendorbudget_gid { get; set; }
        public string budget_amount { get; set; }
        public string available_amount { get; set; }
        public string vendor_details { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string payment_source { get; set; }
        public string process { get; set; }
    }


    public class Vendorledgersummarylist
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_name { get; set; }
        public string vendorinvoice_gid { get; set; }
        public string contact_details { get; set; }
        public string invoice_refnumber { get; set; }
        public double invoice_amount { get; set; }
        public double vendorinvoice_amount { get; set; }
        public double payment_amount { get; set; }
        public double outstanding_amount { get; set; }
        public double refund_amount { get; set; }
        public double advance_amount { get; set; }
    }

    public class Vendorledgerchildreport
    {
        public string vendor_gid { get; set; }
        public string vendor_name { get; set; }
        public string vendorinvoice_gid { get; set; }
        public string invoice_refnumber { get; set; }
        public string reference_number { get; set; }
        public string reference_gid { get; set; }
        public string transaction_date { get; set; }
        public double total_credit { get; set; }
        public double total_debit { get; set; }
        public double vendor_invoicevalue { get; set; }
        public double vendor_paymentvalue { get; set; }
        public double outstanding_amount { get; set; }
        public double vendorinvoice_amount { get; set; }
        public double payment_amount { get; set; }
        public string type { get; set; }
    }

    public class vendorBudget : Vendormodel
    {
        public double vendor_payment { get; set; }
        public double totalvendor_paymenet { get; set; }
        public double total_credit { get; set; }
        public string vendor_company_name { get; set; }
        public string vendor_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public double total_debit { get; set; }
        public List<vendorBudgetSummary> vendorBudgetSummary { get; set; }
    }

    public class vendorMonthlyBudget : Vendormodel
    {
        public double vendor_payment { get; set; }
        public double total_debit { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public List<vendorBudgetSummary> vendorBudgetSummary { get; set; }
    }

    public class vendorBudgetSummary
    {
        public string salesorder_gid { get; set; }
        public string vendorbudget_gid { get; set; }
        public string paymentnote_gid { get; set; }
        public double debit { get; set; }
        public double credit { get; set; }
        public double total_debit { get; set; }
        public double total_credit { get; set; }
        public string service_type { get; set; }
        public string invoice_refnumber { get; set; }
        public string payment_source { get; set; }
        public string created_date { get; set; }
        public double totalvendor_paymenet { get; set; }
        public string receipt_method { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string reference { get; set; }


    }
}