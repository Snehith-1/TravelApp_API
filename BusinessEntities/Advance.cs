using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Advancemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Advance : Advancemodel
    {
        public List<Advancelist> Advancelist { get; set; }
    }
    public class Advancelist
    {
        public int advance_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string salesorder_refnumber { get; set; }       
        public string payment_mode { get; set; }
        public string payment_details { get; set; }
        public double advance_amount { get; set; }
        public string advance_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class Advancedetail : Advancemodel
    {
        public string payment_mode { get; set; }
        public string payment_details { get; set; }
        public double advance_amount { get; set; }
        public string advance_date { get; set; }
        public string salesorder_gid { get; set; }
        public string bank_name { get; set; }
        public string bank_gid { get; set; }
        public string transaction_number { get; set; }
    }
}