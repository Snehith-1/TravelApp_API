using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class customerreceiptManager
    {
        public customerinvoicedetail customerreceiptaddselect(customerinvoicedetail values)
        {
            return new customerreceiptDBAccess().customerreceiptaddselect(values);
        }
        public customerinvoicedetail receiptoverallsubmit(customerinvoicedetail val, string usergid)
        {
            return new customerreceiptDBAccess().receiptoverallsubmit(val, usergid);
        }
        public customerinvoicedetail Getall(string val)
        {
            return new customerreceiptDBAccess().Getall(val);
        }
        public customerinvoicedetail customerreceiptprint(customerinvoicedetail val, string usergid)
        {
            return new customerreceiptDBAccess().customerreceiptprint(val, usergid);
        }
        public customerinvoicedetail receiptreferenceno(string usergid)
        {
            return new customerreceiptDBAccess().receiptreferenceno(usergid);
        }
        public customerinvoicedetail customerinvoiceselectlist(customerinvoicedetail val)
        {
            return new customerreceiptDBAccess().customerinvoiceselectlist(val);
        }
        public customerinvoicedetail customerreceiptdelete(customerinvoicedetail val)
        {
            return new customerreceiptDBAccess().Delete(val);
        }
    }
}