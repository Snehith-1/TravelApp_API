using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessEntities
{
    public class Customeroutstandingreport
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class customeroutstandinglist
    {
        public string invoice_date { get; set; }
        public string invoice_refnumber { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public double invoice_amount { get; set; }
        public double received_amount { get; set; }
        public double outstanding_amount { get; set; }
       
    }
    public class customeroutstaindingdetails: Customeroutstandingreport
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public List<customeroutstandinglist> customeroutstandinglist { get; set; }
    }
}