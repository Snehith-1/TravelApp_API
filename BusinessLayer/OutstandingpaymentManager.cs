using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class OutstandingpaymentManager
    {
       
        public customerinvoicedetail outstandingpaymentaddselect(customerinvoicedetail values)
        {
            return new OutstandingpaymentDBAccess().outstandingpaymentaddselect(values);
        }
 
        public customerinvoicedetail airinvoiceaddselect(customerinvoicedetail values)
        {
            return new OutstandingpaymentDBAccess().airinvoiceaddselect(values);
        }


        //public customerinvoicedetail salesinvoiceupdate(customerinvoicedetail values)
        //{
        //    return new OutstandingpaymentDBAccess().salesinvoiceupdate(values);
        //}
        public customerinvoicedetail outstandingpaymentoverallsubmit(customerinvoicedetail val, string usergid)
        {
            return new OutstandingpaymentDBAccess().outstandingpaymentoverallsubmit(val, usergid);
        }

    }
}