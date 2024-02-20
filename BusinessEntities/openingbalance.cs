using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessEntities
{
    public class openingbalancemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class openingbalance : openingbalancemodel
    {
        public List<parentliabilitylist> parentliabilitylist { get; set; }
        public List<parentassetlist> parentassetlist { get; set; }
        public List<accountnanmeassetlist> accountnanmeassetlist { get; set; }
        public List<accountnanmeliabilitylist> accountnanmeliabilitylist { get; set; }
    }
    public class openingbalancedetail: openingbalancemodel
    {
        public string account_gid { get; set; }
        public List<accountnanmeassetlist> accountnanmeassetlist { get; set; }
        public List<accountnanmeliabilitylist> accountnanmeliabilitylist { get; set; }
    }
    public  class parentliabilitylist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
        public string transaction_date { get; set; }
        public string remarks { get; set; }
        public string transaction_type { get; set; }
        public double transaction_amount { get; set; }
        public string journal_refnumber { get; set; }
    }
    public class parentassetlist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string accountgroup_gid { get; set; }
        public string transaction_date { get; set; }
        public string remarks { get; set; }
        public string transaction_type { get; set; }
        public double transaction_amount { get; set; }
        public string journal_refnumber { get; set; }
    }
    public class accountnanmeassetlist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
    }
    public class accountnanmeliabilitylist
    {
        public string account_name { get; set; }
        public string account_gid { get; set; }
    }
}