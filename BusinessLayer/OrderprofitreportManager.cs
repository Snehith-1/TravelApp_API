using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class OrderprofitreportManager
    {
        public orderprofitdetails summary(orderprofitdetails val)
        {
            //return new OrderprofitreportDBAccess().summary(val);
            return new OrderprofitreportDBAccess().summary(val);
        }
    }
}