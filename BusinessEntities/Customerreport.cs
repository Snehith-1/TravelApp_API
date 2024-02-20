using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class CustomerReport
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class CustomerReportSummary : CustomerReport
    {
        public List<CustomerReportList> CustomerReportList { get; set; }
    }

    public class CustomerReportSummaryChild : CustomerReport
    {
        public List<CustomerReportChildList> CustomerReportChildList { get; set; }
    }

    public class CustomerReportGraph : CustomerReport
    {
        public string label { get; set; }
        public string color { get; set; }
        public string Datas { get; set; }
        public string path { get; set; }
        public string Databars { get; set; }
        public string labels { get; set; }
        public string series { get; set; }
        public List<CustomerReportGraphList> Data { get; set; }
        public List<CustomerReportGraphListbar> Databar { get; set; }
    }

    public class CustomerReportList
    {
        public string year { get; set; }
        public string month { get; set; }
        public string invoice_count { get; set; }
        public double invoice_amount { get; set; }
    }

    public class CustomerReportChildList
    {
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public string customer_name { get; set; }
        public string customer_contactperson { get; set; }
        public double invoice_amount { get; set; }
        public string invoice_status { get; set; }
    }

    public class CustomerReportGraphList
    {
        public string labels { get; set; }
       
    }
    public class CustomerReportGraphListbar
    {
        public string series { get; set; }
    }
}