using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using BusinessEntities;
using BusinessLayer;

namespace BusinessLayer
{
    public class BillingManager
    {
        public billingmodel billingadd(Billingdetail values, string usergid)
        {
            return new BillingDBAccess().billingadd(values, usergid);
        }
        public billingmodel paymentadd(Billingdetail values, string usergid)
        {
            return new BillingDBAccess().paymentadd(values, usergid);
        }
        public billingmodel billingGetall(int val)
        {
            return new BillingDBAccess().billingGetall(val);
        }
        public billingmodel billingGet(int val)
        {
            return new BillingDBAccess().billingGet(val);
        }
        public billingmodel billingdelete(int val)
        {
            return new BillingDBAccess().billingdelete(val);
        }
        public billingmodel activitylist(int val)
        {
            return new BillingDBAccess().activitylist(val);
        }
        public Billingdetail billingselect(Billingdetail  val)
        {
            return new BillingDBAccess().billingselect(val);
        }
        public billingmodel billingoverallsubmit(Billingdetail val,string usergid)
        {
            return new BillingDBAccess().billingoverallsubmit(val, usergid );
        }
        public billingmodel billingoverallsubmitairfile(Billingdetail val, string usergid)
        {
            return new BillingDBAccess().billingoverallsubmitairfile(val, usergid);
        }
        public billingmodel billingoverallsubmitairfileinvoice(Billingdetail val, string usergid)
        {
            return new BillingDBAccess().billingoverallsubmitairfileinvoice(val, usergid);
        }
        
        public billingmodel opfbillingdelete(billinglist val, string usergid)
        {
            return new BillingDBAccess().opfbillingdelete(val, usergid);
        }


        public billingmodel opfpaymentdelete(VendorPaymentlist val, string usergid)
        {
            return new BillingDBAccess().opfpaymentdelete(val, usergid);
        }
        public SalesOrderFormModel PassengerAdd(SOPassengerDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().PassengerAdd(val, user_gid);
        }
    }
}