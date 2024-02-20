using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{

    public class Vouchermodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Voucher : Vouchermodel
    {
        public List<Voucherlist> Voucherlist { get; set; }
    }
    public class Voucherdetails : Vouchermodel
    {
        public int voucher_gid { get; set; }
        public string guest_name { get; set; }
        public string property { get; set; }
        public string check_in_date { get; set; }
        public string check_out_date { get; set; }
        public string check_in_time { get; set; }
        public string check_out_time { get; set; }
        public string total_numberofdays { get; set; }
        public string total_numberofpaxs { get; set; }
        public string meal_plan { get; set; }
        public string extras { get; set; }
        public string bookings_doneby { get; set; }
        public string company_code { get; set; }


        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }


    }
    public class Voucherlist
    {

        public int voucher_gid { get; set; }
        public string guest_name { get; set; }
        public string property { get; set; }
        public string check_in_date { get; set; }
        public string check_out_date { get; set; }
        public string check_in_time { get; set; }
        public string check_out_time { get; set; }
        public string total_numberofdays { get; set; }
        public string total_numberofpaxs { get; set; }
        public string meal_plan { get; set; }
        public string extras { get; set; }
        public string bookings_doneby { get; set; }
        public string company_name { get; set; }
        public string company_code { get; set; }


        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}