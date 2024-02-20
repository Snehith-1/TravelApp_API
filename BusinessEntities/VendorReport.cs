using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class VendorReport
    {
        public string from_date { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string to_date { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class VendorReportSummary : VendorReport
    {
        public List<VendorReportList> VendorReportList { get; set; }
        public List<vendoroutstandingreport> vendoroutstandingreport { get; set; }
    }

    public class VendorReportSummaryChild : VendorReport
    {
        public List<VendorReportChildList> VendorReportChildList { get; set; }
    }

    public class VendorReportGraph : VendorReport
    {
        public string label { get; set; }
        public string color { get; set; }
        public string series { get; set; }
        public List<VendorReportGraphList> Data { get; set; }
    }

    public class VendorReportList
    {
        public string year { get; set; }
        public string month { get; set; }
        public string invoice_count { get; set; }
        public double vendorinvoice_amount { get; set; }
    }

    public class VendorReportChildList
    {
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public string vendor_name { get; set; }
        public string contact_details { get; set; }
        public double vendorinvoice_amount { get; set; }
        public string invoice_status { get; set; }
    }

    public class VendorReportGraphList
    {
        public string month { get; set; }
        public double total_amount { get; set; }
    }
    public class vendoroutstandingreport
    {
        //public string year { get; set; }
        //public string month { get; set; }
        public double vendorinvoice_amount { get; set; }
        public string invoice_refnumber { get; set; }
        public double payment_amount { get; set; }
        public double outstanding_amount { get; set; }
        public string invoice_date { get; set; }
        public string vendor_name { get; set; }
        public string vendor_company_name { get; set; }
        public string contact_details { get; set; }
        

    }

}