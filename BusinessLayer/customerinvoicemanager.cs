using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class customerinvoicemanager
    {
        public customerinvoicedetail customerinvoiceaddselect(int values)
        {
            return new customerinvoiceDBAccess().customerinvoiceaddselect(values);
        }
        public customerinvoicemodel Add(customerinvoicedetail val, string userGid)
        {
            return new customerinvoiceDBAccess().Add(val, userGid);
        }
        public customerinvoice Getall(string val, string user_gid)
        {
            return new customerinvoiceDBAccess().Getall(val, user_gid); 
        }
        public customerinvoice salesinvoicesummarysearch(customerinvoicedetail val, string user_gid)
        {
            return new customerinvoiceDBAccess().salesinvoicesummarysearch(val, user_gid);
        }
        public customerinvoice salesinvoicesummarysearchfunction(customerinvoicedetail val, string user_gid)
        {
            return new customerinvoiceDBAccess().salesinvoicesummarysearchfunction(val, user_gid);
        }
        public customerinvoice showallinvoicesummary(string val, string user_gid)
        {
            return new customerinvoiceDBAccess().showallinvoicesummary(val, user_gid);
        }
        public customerinvoice customerstatement(string val)
        {
            return new customerinvoiceDBAccess().customerstatement(val);
        }
        public customerinvoice receiptcustomerinvoicesummary(string val)
        {
            return new customerinvoiceDBAccess().receiptcustomerinvoicesummary(val); 
        }
        public outstandingCustomerInvoiceList outstandingcustomerinvoicesummary(outstandingCustomerInvoiceList val)
        {
            return new customerinvoiceDBAccess().outstandingcustomerinvoicesummary(val);
        }
        public customerinvoicemodel Delete(string values)
        {
            return new customerinvoiceDBAccess().Delete(values);
        }
        public SalesOrderForm CustomerReport()
        {
            return new customerinvoiceDBAccess().CustomerReport();
        }
     
        public customerinvoicedetail CustomerLedgerReport(customerinvoicedetail val)
        {
            return new customerinvoiceDBAccess().CustomerLedgerReport(val);
        }
        public customerinvoicedetail Customerstatementreport(customerinvoicedetail val)
        {
            return new customerinvoiceDBAccess().Customerstatementreport(val);
        }

       
        public customerinvoicedetail customerinvoiceprint(int values)
        {
            return new customerinvoiceDBAccess().customerinvoiceprint(values);
        }
        public customerinvoicedetail ivnvoicereferenceno(string usergid)
        {
            return new customerinvoiceDBAccess().ivnvoicereferenceno(usergid);
        }
        public Billingdetail airfileinvoiceedit(customerinvoicedetail val)
        {
            return new customerinvoiceDBAccess().airfileinvoiceedit(val);
        }
        public billingmodel airfileinvoiceupdate(Billingdetail val, string userGid)
        {
            return new customerinvoiceDBAccess().airfileinvoiceupdate(val, userGid);
        }
        public customerinvoicemodel ivnvoicereferencenocancel(string val)
        {
            return new customerinvoiceDBAccess().ivnvoicereferencenocancel(val);
        }
        public customertransactiondetails customertransctions(customertransactiondetails val)
        {
            return new customerinvoiceDBAccess().customertransctions(val);
        }
        public customerinvoice vendorpaymentpendinginvoicesummary()
        {
            return new customerinvoiceDBAccess().daVendorPaymentPendingInvoiceSummary();
        }
        public SalesOrderForm customerLedgerSummary()
        {
            return new customerinvoiceDBAccess().daCustomerLedgerSummary();
        }
        public customerinvoicedetail newCustomerLedgerReport(customerinvoicedetail val)
        {
            return new customerinvoiceDBAccess().daCustomerLedgerReport(val);
        }
    }
}