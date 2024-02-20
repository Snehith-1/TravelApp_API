using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CustomerReportManager
    {
        public CustomerReportSummary GetAll(CustomerReport val)
        {
            return new CustomerReportDBAccess().GetAll(val);
        }
        public CustomerReportGraph GetAllgraph(CustomerReport val,string companycode)
        {
            return new CustomerReportDBAccess().GetAllgraph(val, companycode);
        }

        public CustomerReportSummaryChild GetAllChild(CustomerReport val)
        {
            return new CustomerReportDBAccess().GetAllChild(val);
        }
    }
}