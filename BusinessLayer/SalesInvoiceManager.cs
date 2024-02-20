using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class SalesInvoiceManager
    {
        public SalesInvoice Getall(string val)
        {
            return new SalesInvoiceDBAccess().Getall(val);
        }
        public SalesInvoice salesinvoiceservicetypelist(SalesInvoice values)
        {
            return new SalesInvoiceDBAccess().salesinvoiceservicetypelist(values);
        }
        public billingmodel SalesInvoiceoverallsubmitairfile(Billingdetail val, string usergid)
        {
            return new SalesInvoiceDBAccess().SalesInvoiceoverallsubmitairfile(val, usergid);
        }
        public customerinvoicedetail salesinvoiceedit(customerinvoicedetail values)
        {
            return new SalesInvoiceDBAccess().salesinvoiceedit(values);
        }
        public customerinvoicedetail servicetypescount(customerinvoicedetail values)
        {
            return new SalesInvoiceDBAccess().servicetypescount(values);
        }
        public customerinvoicedetail salesinvoicedetails(customerinvoicedetail values)
        {
            return new SalesInvoiceDBAccess().salesinvoicedetails(values);
        }
        public customerinvoicedetail Update(customerinvoicedetail val, string userGid)
        {
            return new SalesInvoiceDBAccess().Update(val, userGid);
        }

    }
}