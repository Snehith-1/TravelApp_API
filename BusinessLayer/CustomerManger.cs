using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;


namespace BusinessLayer
{
    public class CustomerManger
    {
        public Customer GetAll()
        {
            return new CustomerDBAccess().GetAll();
        }
        public Customermodel customerstatementsummary(Customerdetails val)
        {
            return new CustomerDBAccess().customerstatementsummary(val);
        }
        public Customermodel customerstatementsummarysearch(Customerdetails val)
        {
            return new CustomerDBAccess().customerstatementsummarysearch(val);
        }
        public Customermodel Add(Customerdetails val, string userGid)
        {
            return new CustomerDBAccess().Add(val, userGid);
        }
        public Customermodel Status(Customerdetails val, string userGid)
        {
            return new CustomerDBAccess().Status(val, userGid);
        }
        public Customerdetails Get(string  values)
        {
            return new CustomerDBAccess().Get(values);
        }
        public Customermodel Update(Customerdetails val, string userGid)
        {
            return new CustomerDBAccess().Update(val, userGid);
        }
        public Customermodel Excel(string company_code,HttpRequest httpreq,  Customerdetails val, string userGid)
        {
            return new CustomerDBAccess().Excel(company_code,httpreq, val, userGid);
        }

    }
}