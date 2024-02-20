using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Currencymodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Currency : Currencymodel
    {
        public List<CurrencyList> CurrencyList { get; set; }
        public List<Countrylist> Countrylist { get; set; }
    }
    public class CurrencyList
    {
        public int currency_gid { get; set; }
        public string currency_code { get; set; }
        public string country_code { get; set; }
        public string currency_name { get; set; }
        public string country_name { get; set; }
        public string currency_status { get; set; }
        public double currency_amount { get; set; }

    }
    public class Currencydetail : Currencymodel
    {
        public int currency_gid { get; set; }
        public string currency_code { get; set; }
        public string country_code { get; set; }
        public string currency_name { get; set; }
        public string country_name { get; set; }
        public string currency_status { get; set; }
        public double currency_amount { get; set; }
    }

    public class Countrylist
    {
        
        public string country_code { get; set; }
        public string currency_name { get; set; }
        public string country_name { get; set; }
        
    }
}