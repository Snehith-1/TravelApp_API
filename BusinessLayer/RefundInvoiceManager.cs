using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class RefundInvoiceManager
    {
        public Refund GetAll(string val)
        {
            return new RefundInvoiceDBAccess().GetAll(val);
        }
        public customerinvoicedetail refundsummarydetails(customerinvoicedetail values)
        {
            return new RefundInvoiceDBAccess().refundsummarydetails(values);
        }
        public customerinvoicedetail refundaddselect(customerinvoicedetail values)
        {
            return new RefundInvoiceDBAccess().refundaddselect(values);
        }
        
        public customerinvoicedetail refundoverallsubmit(customerinvoicedetail val, string usergid)
        {
            return new RefundInvoiceDBAccess().refundoverallsubmit(val, usergid);
        }
        public MdlRefundServiceType refundServiceType(MdlRefundServiceType val, string usergid)
        {
            return new RefundInvoiceDBAccess().refundServiceType(val, usergid);
        }
        public MdlRefundServiceType refundservicetypeupdate(MdlRefundServiceType val, string usergid)
        {
            return new RefundInvoiceDBAccess().refundservicetypeupdate(val, usergid);
        }
        public customerinvoice getRefundCustomerInvoiceSummary(string val)
        {
            return new RefundInvoiceDBAccess().daRefundCustomerInvoiceSummary(val);
        }
        public MdlRefundServiceType refundview(MdlRefundServiceType val, string usergid)
        {
            return new RefundInvoiceDBAccess().refundview(val, usergid);
        }
        public MdlRefundServiceType editrefund(MdlRefundServiceType val, string usergid)
        {
            return new RefundInvoiceDBAccess().editrefund(val, usergid);
        }
        public refundledgerdetails customerRefundLedgerReport(refundledgerdetails val)
        {
            return new RefundInvoiceDBAccess().daCustomerRefundLedgerReport(val);
        }
    }
}