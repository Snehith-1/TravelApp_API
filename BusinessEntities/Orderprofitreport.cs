using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Orderprofitreportmodel
    {
        public bool status { get; set; }
        public string message { get; set; } 
    }
    public class orderprofitdetails: Orderprofitreportmodel
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string branch_gid { get; set; }
        public string service_name { get; set; }
        public List<orderprofitlist> orderprofitlist { get; set; }
    }
    public class orderprofitlist
    {
        public DateTime created_date { get; set; }
        public string order_refnumber { get; set; }
        public string customer_name { get; set; }
        public double salesorder_amount { get; set; }
        public double income { get; set; }
        public double expense { get; set; }
        public double profit_amount{ get; set; }
        public string branch_name { get; set; }
        public string company_code { get; set; }
    }
}