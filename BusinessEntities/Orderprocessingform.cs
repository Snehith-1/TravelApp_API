using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class orderprocessingformmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Orderprocessingform : orderprocessingformmodel
    {
        public List<orderprocessinglist> orderprocessinglist { get; set; }
        public List<opfpassengerlist> opfpassengerlist { get; set; }
        public List<opfactivityList> opfactivityList { get; set; }
        //public List<billinglist> billinglist { get; set; }
        public List<vendoractivitylist> vendoractivitylist { get; set; }
        public List<ActivityList> activityList { get; set; }
        public List<Orderprocessinglistall> Orderprocessinglistall { get; set; }
    }
    public class orderprocessingdetail
    {
        
        public int salesorder_gid { get; set; }
        public int customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public double estimatedprice { get; set; }
        public  double customerprice { get; set; }
        public string salesorder_refno { get; set; }
        public string salesordervalue { get; set; }
        public string amountspend { get; set; }
        public string profit { get; set; }
        public string customerpaidamount { get; set; }
    }
    public class orderprocessinglist
    {
        public int salesorder_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
        public string contact_number{ get; set; }
        public double total_amount { get; set; }//estimatedprice changes as total
        public double vendor_amount { get; set; }
        public double net_amount{ get; set; }
        public string salesorder_refnumber { get; set; }
        public double discount_amount { get; set; }
        public string orderprocessing_refnumber { get; set; }
        public string salesorder_status { get; set; }
        // public DateTime created_date { get; set; }
         public string created_date { get; set; }
    }
    public class opfpassengerlist
    {
        public string passengerservice_gid { get; set; }
        public string passenger_name { get; set; }
        public string salesorder_gid { get; set; }
        public string passenger_lastname { get; set; }
        public string gender { get; set; }
        public string dob{ get; set; }
        public string passport_number { get; set; }
        public string passport_expirydate { get; set; }
        public string passport_issueddate { get; set; }
    }
    public class opfactivityList
    {
        public string salesorder_gid { get; set; }
        public string service_type { get; set; }
        public string reference { get; set; }
        public string remarks { get; set; }
        public string salesactivity_status { get; set; }
        public string total_amount { get; set; }
        public int activity_gid { get; set; }
        public string salesactivity_gid { get; set; }
    }
    public class Orderprocessingformdetails:orderprocessingformmodel
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string email_address { get; set; }
        public string national_id { get; set; }
        public string billing_address { get; set; }
        public string customer_status { get; set; }
        public string currency { get; set; }
        public string tmpsalestoactivity_gid { get; set; }
        public string currency_gid { get; set; }
    }
    public class Orderprocessinglistall
    {
        public int orderprocessing_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string customer_gid { get; set; }
        public string contact_number { get; set; }
        public double total_amount { get; set; }
        public double discount_amount{ get; set; }
        public double net_amount{ get; set; }
        public double vendor_amount { get; set; }
        public string created_by { get; set; }
        public string orderprocessing_refnumber { get; set; }
        public DateTime created_date { get; set; }
        public string  customer_name { get; set; }
    }

    public class vendoractivitylist
    {
        public int paymentnotemaindtl_gid { get;set;}
        public string paymentnotemain_gid { get; set; }
        public string activty_gid { get; set; }
        public string process { get; set; }
        public double vendor_amount { get; set; }
        public int salesorder_gid { get; set; }
        public string vendor_gid { get; set; }
        public string vendor_name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime salesorder_date { get; set; }
        public string salesorder_refnumber { get; set; }
    }

    public class vendoractivitydetail
    {
        public int paymentnotemain_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string vendor_gid { get; set; }
        public string vendor_amount { get; set; }
        public string created_by { get; set; }
        public List<ActivityList> activityList { get; set; }
        public List<vendoractivitylist> vendoractivitylist { get; set; }

    }
    public class totalvaluedetails : orderprocessingformmodel
    {
        public double net_amount{ get; set; }
        public int salesorder_gid { get; set; }
        public double customer_amount{ get; set; }
        public double vendor_amount { get; set; }
        public double profit_amount{ get; set; }
        public double total_amount { get; set; }
       

    }
}