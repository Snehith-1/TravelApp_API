using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class OrderprocessingformManger
    {
        public Orderprocessingform Getall()
        {
            return new OrderproceesingformDBAccess().Getall();
        }
        public Orderprocessingform Orderprocessing()
        {
            return new OrderproceesingformDBAccess().Orderprocessing();
        }
        public orderprocessingformmodel pageload(orderprocessinglist val, string usergid)
        {
            return new OrderproceesingformDBAccess().pageload(val, usergid);
        }
        public Orderprocessingform opfpassengersummary(opfpassengerlist val)
        {
            return new OrderproceesingformDBAccess().opfpassengersummary(val);
        }
        public Orderprocessingform activity(opfactivityList val)
        {
            return new OrderproceesingformDBAccess().activity(val);
        }
        public Orderprocessingformdetails Getcustomer(int values)
        {
            return new OrderproceesingformDBAccess().Getcustomer(values);
        }
        public orderprocessingformmodel oversubmit(orderprocessinglist val, string usergid)
        {
            return new OrderproceesingformDBAccess().oversubmit(val, usergid);
        }
        public orderprocessingformmodel submit(billing val, string usergid)
        {
            return new OrderproceesingformDBAccess().submit(val, usergid);
        }
        public Orderprocessingform vendorpaymentsummary(vendoractivitylist val)
        {
            return new OrderproceesingformDBAccess().vendorpaymentsummary(val);
        }
        public Orderprocessingform orderprocessingmainsummary()
        {
            return new OrderproceesingformDBAccess().orderprocessingmainsummary();
        }
        public Orderprocessingform ordersalesselectsummary()
        {
            return new OrderproceesingformDBAccess().ordersalesselectsummary();
        }
        public orderprocessingformmodel opfstatus(Orderprocessingformdetails val)
        {
            return new OrderproceesingformDBAccess().opfstatus(val);
        }
        public SalesOrderForm soserviceget(int salesorder_gid)
        {
            return new OrderproceesingformDBAccess().soserviceget(salesorder_gid);
        }
        public orderprocessingformmodel totalvalue(totalvaluedetails values)
        {
            return new OrderproceesingformDBAccess().totalvalue(values);
        }
    }
}