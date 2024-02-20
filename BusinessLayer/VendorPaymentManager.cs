using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class VendorPaymentManager
    {
        public VendorPayment vendorpaymentoverallsubmit()
        {
            return new VendorPaymentDBAccess().vendorpaymentoverallsubmit();
        }
        //public VendorPaymentmodel vendorpaymentadd(VendorPaymentdetails val,string userGid)
        //{
        //    return new VendorPaymentDBAccess().vendorpaymentadd(val, userGid);
        //}
        //public VendorPaymentmodel vendorpaymentsubmit(VendorPaymentdetails val, string userGid)
        //{
        //    return new VendorPaymentDBAccess().vendorpaymentsubmit(val, userGid);
        //}
        public VendorPaymentmodel vendorpaymentselect(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().vendorpaymentselect(val, userGid);
        }
        public VendorPaymentmodel vendorpaymentselectsummary(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().vendorpaymentselectsummary(val, userGid);
        }
       
        public VendorPaymentmodel vendorpaymentsubmit(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().vendorpaymentsubmit(val, userGid);
        }
        public VendorPaymentmodel vendorpaymentmain(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().vendorpaymentmain(val, userGid);
        }
        public VendorPaymentdetails paymentselect(VendorPaymentdetails val)
        {
            return new VendorPaymentDBAccess().paymentselect(val);
        }
        public VendorPaymentdetails salesvendorpaymentselect(VendorPaymentdetails val)
        {
            return new VendorPaymentDBAccess().salesvendorpaymentselect(val);
        }
        public VendorPaymentmodel paymentoverallsubmit(VendorPaymentdetails val,string userGid)
        {
            return new VendorPaymentDBAccess().paymentoverallsubmit(val, userGid);
        }
        public VendorPaymentmodel salesvendoroverallpayment(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().salesvendoroverallpayment(val, userGid);
        }
        public VendorPaymentdetails vendorinvoicesummary(string val)
        {
            return new VendorPaymentDBAccess().vendorinvoicesummary(val);
        }
        public VendorPaymentdetails paymentvendorinvoicesummary()
        {
            return new VendorPaymentDBAccess().paymentvendorinvoicesummary();
        }
        public VendorPaymentmodel vendorinvoicedelete(string values)
        {
            return new VendorPaymentDBAccess().vendorinvoicedelete(values);
        }
        public VendorPaymentdetails vendorpaymentaddselect(VendorPaymentdetails values)
        {
            return new VendorPaymentDBAccess().vendorpaymentaddselect(values);
        }
        public VendorPaymentmodel paymentvendoroverallsubmit(VendorPaymentdetails val, string userGid)
        {
            return new VendorPaymentDBAccess().paymentvendoroverallsubmit(val, userGid);
        }
        public VendorPaymentdetails vendorpaymentsummary()
        {
            return new VendorPaymentDBAccess().vendorpaymentsummary();
        }
        public VendorPaymentdetails paymentlist(string val)
        {
            return new VendorPaymentDBAccess().paymentlist(val);
        }
        public VendorPaymentdetails vendorpaymentprint(string  val, string usergid)
        {
            return new VendorPaymentDBAccess().vendorpaymentprint(val, usergid);
        }
        public VendorPaymentdetails paymentreferenceno(string usergid)
        {
            return new VendorPaymentDBAccess().paymentreferenceno(usergid);
        }
        public VendorPaymentdetails vendorinvoicereferenceno(string usergid)
        {
            return new VendorPaymentDBAccess().vendorinvoicereferenceno(usergid);
        }
        public VendorPaymentdetails vendorinvoiceview(VendorPaymentdetails val)
        {
            return new VendorPaymentDBAccess().vendorinvoiceview(val);
        }
        public VendorPaymentdetails salesvendorinvoiceview(VendorPaymentdetails val)
        {
            return new VendorPaymentDBAccess().salesvendorinvoiceview(val);
        }
        public VendorPaymentdetails vendorstatementreport(VendorPaymentdetails val)
        {
            return new VendorPaymentDBAccess().vendorstatementreport(val);
        }
        public VendorPaymentdetails vendoroutstandingsummary(string val)
        {
            return new VendorPaymentDBAccess().daVendorOutstandingSummary(val);
        }
        public VendorPaymentdetails vendorledgeroutstandingsummary(string val)
        {
            return new VendorPaymentDBAccess().vendorledgeroutstandingsummary(val);
        }
    }
}