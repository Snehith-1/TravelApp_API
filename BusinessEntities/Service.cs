using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Servicemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
        //public string from_date { get; set; }
        //public string to_date { get; set; }
        //public string service_name { get; set; }
        //public string ticketno { get; set; }

    }
    public class Service : Servicemodel  
    {
       
        public List<Servicelist> servicelist { get; set; }
        public List<Servicereportlist> Servicereportlist { get; set; }
    }
    public class Servicelist
    {
        public int service_gid { get; set; }
        public string service_code { get; set; }
        public string service_name { get; set; }
    }

    public class Servicereportlist
    {
        public string customerinvoice_gid { get; set; }
        public string invoice_refnumber { get; set; }
        public string invoice_date { get; set; }
        public string service_name { get; set; }
        public string service_type { get; set; }
        public string customer_name { get; set; }
        public double service_amount { get; set; }
        public string created_date { get; set; }
        public string passenger_firstname { get; set; }
        public string created_by { get; set; }


    }
    public class Servicedetails : Servicemodel
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string service_name { get; set; }
        public string service_type { get; set; }

        public string ticket_no { get; set; }
        public List<Servicelist> servicelist { get; set; }
        public List<Servicereportlist> Servicereportlist { get; set; }
    }
}