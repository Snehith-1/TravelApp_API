using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{ 
    public class AIRmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }  
    public class AIR : AIRmodel
    {
        public List<AIRlist> AIRlist { get; set; }
    }
    public class AIRDetails : AIRmodel
    {
        public int air_gid { get; set; }
        public string PNR_NO { get; set; }
        public string from_place { get; set; }
        public string to_place { get; set; }
        public string flight_number { get; set; }
        public string take_off{ get; set; }
        public string land_off { get; set; }
        public string ticket_number { get; set; }
        public string passenger_name { get; set; }
        public string sector { get; set; }
        public string company_code { get; set; }
        public List<PNR> PNR { get; set; }
        public List<sectordetails> sectordetails { get; set; }
        public List<Passengerdetails> Passengerdetails { get; set; }
        public List<AIRlist> AIRlist { get; set; }
        public List<airfileinvoicelist> airfileinvoicelist { get; set; }
        public string airfiles { get; set; }
        public double total_ticket_price { get; set; }
        public string airfiles_count { get; set; }
    }

    public class PNR
    {
        public string pnr_number { get; set; }
        public int air_gid { get; set; }
        public string total_price { get; set; }
        public double total_ticket_price { get; set; }
        public string flag { get; set; }
        public string agent_gid { get; set; }

    }

    public class sectordetails 
    {
        public string from_place { get; set; }
        public string to_place { get; set; }
        public string flight_number { get; set; }
        public string take_off { get; set; }
        public string land_off { get; set; }
    }

    public class Passengerdetails 
    {
        public string ticket_number { get; set; }
        public string passenger_name { get; set; }       
    }
    public class AIRlist
    {
        public int air_gid { get; set; }
        public string pnr_number { get; set; }
        public string invoice_refnumber { get; set; }
        public string travel_date { get; set; }
        public string passenger_name { get; set; }
        //public string traveldate { get; set; }
        public string sector_no { get; set; }
        public string reference { get; set; }
        public string flag { get; set; }
        public string agent_gid { get; set; }
        public string flight_number { get; set; }
        public string journey { get; set; }
        public string submit_flag { get; set; }
        public string ticket_number { get; set; }
        public string total_ticket_price { get; set; }
        public string air_Line { get; set; }


    }
    public class airfileinvoicelist
    {
        public string description { get; set; }
        public string service_name { get; set; }
    }
}