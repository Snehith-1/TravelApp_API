using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class AirlineManager
    {
        public airlinedetails airlinesummary()
        {
            return new AirlineDBAccess().airlinesummary();
        }
        public Airlinemodel airlineradd(airlinedetails val, string usergid)
        {
            return new AirlineDBAccess().airlineradd(val, usergid);
        }
        public Airlinemodel airlinedelete(string val)
        {
            return new AirlineDBAccess().airlinedelete(val);
        }
        public Airlinemodel Excel(string company_code, HttpRequest httpreq, airlinedetails val, string userGid)
        {
            return new AirlineDBAccess().Excel(company_code, httpreq, val, userGid);
        }
        public Airlinemodel othersericesadd(otherservicedetails val, string usergid)
        {
            return new AirlineDBAccess().othersericesadd(val, usergid);
        }
        public otherservicedetails otherservicesummary()
        {
            return new AirlineDBAccess().otherservicesummary();
        }
        public otherservicedetails otherservicetype()
        {
            return new AirlineDBAccess().otherservicetype();
        }
        public Airlinemodel otherserviceedit(otherservicelist val, string usergid)
        {
            return new AirlineDBAccess().otherserviceedit(val, usergid);
        }
        public Airlinemodel otherserviceupdate(otherservicelist val, string usergid)
        {
            return new AirlineDBAccess().otherserviceupdate(val, usergid);
        }
        public Airlinemodel choosesubservicetype(otherservicelist val)
        {
            return new AirlineDBAccess().choosesubservicetype(val);
        }
    }
}