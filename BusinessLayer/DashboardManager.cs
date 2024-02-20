using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class DashboardManager 
    {
        public Dashboarddetails dashboardcounts()
        {
            return new DashboardDBAccess().dashboardcounts();
        }

        public Dashboarddetails dashboardsttransaction()
        {
            return new DashboardDBAccess().dashboardsttransaction();
        }
        public Dashboarddetails dashlistinvoicecount(string usergid)
        {
            return new DashboardDBAccess().dashlistinvoicecount(usergid);
        }
        public Dashboardservicesales dashboardservicesales(string companycode)
        {
            return new DashboardDBAccess().dashboardservicesales(companycode);
        }
        public Dashboardservicesales dashboardservicesalesbar(string companycode)
        {
            return new DashboardDBAccess().dashboardservicesalesbar(companycode);
        }
    }
}