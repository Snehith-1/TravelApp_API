using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CreditnoteManager
    {
       
        public creditnotereceiptlist creditnotesubmit(creditnotereceiptlist val, string usergid)
        {

            return new CreditnoteDBAccess().creditnotesubmit(val, usergid);
        }
        public creditnotereceiptlist creditnoteinvoiceselectlist(creditnotereceiptlist val)
        {

            return new CreditnoteDBAccess().creditnoteinvoiceselectlist(val);
        }
        public creditnotereceiptlist creditnotedetails(creditnotereceiptlist val)
        {

            return new CreditnoteDBAccess().creditnotedetails(val);
        }
        public VendorPaymentmodel paymentvendoroverallsubmit(VendorPaymentdetails val, string userGid)
        {
            return new CreditnoteDBAccess().paymentvendoroverallsubmit(val, userGid);
        }
      
        public creditnotereceiptlist creditnotesummary()
        {
            return new CreditnoteDBAccess().creditnotesummary();
        }
        public creditnotereceiptlist debitnotesummary()
        {
            return new CreditnoteDBAccess().debitnotesummary();
        }
    }
}