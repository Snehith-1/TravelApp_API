using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class SalesorderReport
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class SalesorderReportSummary : SalesorderReport
    {
        public List<SalesorderReportList> SalesorderReportList { get; set; }
    }

    public class SalesorderReportSummaryChild : SalesorderReport
    {
        public List<SalesorderReportChildList> SalesorderReportChildList { get; set; }
    }

    public class SalesorderReportGraph : SalesorderReport
    {
        public string label { get; set; }
              public string series { get; set; }
        public string color { get; set; }
        public List<SalesorderReportGraphList> Data { get; set; }
    }

    public class SalesorderReportList
    {
        public string year { get; set; }
        public string month { get; set; }
        public string salescount { get; set; }
        public double total_amount { get; set; }
    }

    public class SalesorderReportChildList
    {
        public string salesorder_refnumber { get; set; }
        public string invoice_date { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public double net_amount { get; set; }
        public string invoice_status { get; set; }
    }

    public class SalesorderReportGraphList
    {
        public string month { get; set; }
        public double amount { get; set; }
    }


}