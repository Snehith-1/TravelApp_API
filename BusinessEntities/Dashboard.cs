using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Dashboardmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class Dashboarddetails : Dashboardmodel
    {        
        public List<TransactionList> TransactionList { get; set; }
        public List<invoicecountlist> invoicecountlist { get; set; }

        public List<serviceList> serviceList { get; set; }        
    }

    public class Dashboardservicesales
    {
        public string file_path { get; set; }
        public string file_path1 { get; set; }
        public string path { get; set; }
            public string path3 { get; set; }
        public List<servicesalesList> servicesalesList { get; set; }
        public List<servicesalesbarList> servicesalesbarList { get; set; }
        public List<servicesalesList1> servicesalesList1 { get; set; }
    }
    public class TransactionList
    {
        public string salesorder_refnumber { get; set; }
        public string salesorder_date { get; set; }
        public string salesteam { get; set; }
        public double total_amount { get; set; }
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public double invoice_amount { get; set; }
        public string invoice_count { get; set; }
        public string user_code { get; set; }
        public string created_by { get; set; }

    }
    public class invoicecountlist
    {
     
        public string user_code { get; set; }
        public string created_by { get; set; }
        public string invoice_count { get; set; }
        public string total_amount { get; set; }


    }
    public class serviceList
    {
        public string service_type { get; set; }
        public string service_count { get; set; }
    }

    public class servicesalesList
    {
        public double value { get; set; }
        public string color { get; set; }
        public string label { get; set; }
        public string highlight { get; set; }
    }
    public class servicesalesbarList
    {
       // public string label { get; set; }
        //public string highlightFill { get; set; }
        //public string fillColor { get; set; }
        //public string highlightStroke { get; set; }
        //public string strokeColor { get; set; }
        public string data { get; set; }
    }
    public class servicesalesList1
    {
        public string label { get; set; }
    }

        public class Dashboardservicedetails 
    {
        public double passport { get; set; }
        public double visa { get; set; }
        public double flight { get; set; }
        public double car { get; set; }
        public double hotel { get; set; }
        public double forex { get; set; }
        public double insurance { get; set; }

        public string passportl { get; set; }
        public string visal { get; set; }
        public string flightl { get; set; }
        public string carl { get; set; }
        public string hotell { get; set; }
        public string forexl { get; set; }
        public string insurancel { get; set; }

        public string passportc { get; set; }
        public string visac { get; set; }
        public string flightc { get; set; }
        public string carc { get; set; }
        public string hotelc { get; set; }
        public string forexc { get; set; }
        public string insurancec { get; set; }
    }
}