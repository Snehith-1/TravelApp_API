using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    
        public class Airlinemodel
        {
            public bool status { get; set; }
            public string message { get; set; }
        }      
        public class Airlinelist
        {
            public string airline_gid { get; set; }
            public string airline_name { get; set; }
            public string city { get; set; }
            public string airline_country { get; set; }
            public string created_by { get; set; }
            public string iata_designator { get; set; }
            public string icao_designator { get; set; }
            public string faa_designator { get; set; }
        public DateTime created_date { get; set; }
        }
        public class  airlinedetails: Airlinemodel
        {
            public string airline_gid { get; set; }
            public string airline_name { get; set; }
            public string city { get; set; }
            public string airline_country { get; set; }
            public string iata_designator { get; set; }
            public string icao_designator { get; set; }
            public string faa_designator { get; set; }
            public List<Airlinelist> Airlinelist { get; set; }
        }
    public class otherservicedetails : Airlinemodel
    {
        public string servicetype_name { get; set; }
        public string customer_amount { get; set; }
        public string vendor_name { get; set; }
        public string vendor_amount { get; set; }
        public string otherservice_details { get; set; }
        public string otherservice_gid { get; set; }
        public List<otherservicelist> otherservicelist { get; set; }
        public List<otherservicetype> otherservicetype { get; set; }



    }
    public class otherservicelist
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string servicetype_name { get; set; }
        public string customer_amount { get; set; }
        public string vendor_name { get; set; }
        public string vendor_amount { get; set; }
        public string otherservice_details { get; set; }
        public string otherservice_gid { get; set; }
    }
    public class otherservicetype
    {
        public string servicetype_name { get; set; }
        public string otherservice_gid { get; set; }
    }

}