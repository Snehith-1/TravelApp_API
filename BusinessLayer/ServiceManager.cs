using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using BusinessEntities;


namespace BusinessLayer
{
    public class ServiceManager
    {
        public Service GetAll()
        {
            return new ServiceDBAccess().GetAll();
        }
        public Service servicereportsummary(Servicedetails val)
        {
            return new ServiceDBAccess().servicereportsummary(val);
        }
    }
}