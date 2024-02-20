using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class VendorManager
    {
      public Vendor GetAll()
        {
            return new VendorDBAccess().GetAll();
        }
        public Vendor activevendorsummary()
        {
            return new VendorDBAccess().activevendorsummary();
        }
        public Vendormodel Add(Vendordetail val, string usergid)
        {
            return new VendorDBAccess().Add(val, usergid);
        }
        public Vendormodel vendorcode(string usergid)
        {
            return new VendorDBAccess().vendorcode(usergid);
        }
        public Vendormodel Delete(int values)
        {
            return new VendorDBAccess().Delete(values);
        }
        public Vendordetail Get(string  values)
        {
            return new VendorDBAccess().Get(values);
        }
        public Vendormodel Update(Vendordetail val, string userGid)
        {
            return new VendorDBAccess().Update(val, userGid);
        }
        public Vendormodel Status(Vendordetail val, string userGid)
        {
            return new VendorDBAccess().Status(val, userGid);
        }
        public Vendor paymentvendorsummary()
        {
            return new VendorDBAccess().paymentvendorsummary();
        }
        public Vendor  ticketvendor()
        {
            return new VendorDBAccess().ticketvendor();
        }
        public Vendormodel vendoradvanceadd(Vendordetail val, string userGid)
        {
            return new VendorDBAccess().vendoradvanceadd(val, userGid);
        }
        public Vendormodel submitvendorbudget(Vendordetail val, string userGid)
        {
            return new VendorDBAccess().submitvendorbudget(val, userGid);
        }
        public vendorBudget vendorbudgetsummary(vendorBudget val)
        {
            return new VendorDBAccess().vendorbudgetsummary(val);
        }
        public vendorBudget monthlypaymentsummary(vendorBudget val)
        {
            return new VendorDBAccess().monthlypaymentsummary(val);
        }
        public vendorBudget vendorbudgetsummarysearch(vendorBudget val)
        {
            return new VendorDBAccess().vendorbudgetsummarysearch(val);
        }
        public Vendor vendoradvancesummary(string val)
        {
            return new VendorDBAccess().vendoradvancesummary(val);
        }
        public Vendor vendorledgersummary()
        {
            return new VendorDBAccess().vendorledgersummary();
        }
      
        public Vendor vendorledgerchildreport(Vendordetail val)
        {
            return new VendorDBAccess().vendorledgerchildreport(val);
        }

        //public vendorMonthlyBudget monthlyvendorbudgetsummary(vendorMonthlyBudget val)
        //{
        //    return new VendorDBAccess().monthlyvendorbudgetsummary(val);
        //}
    }
}