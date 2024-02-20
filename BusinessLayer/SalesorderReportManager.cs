using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class SalesorderReportManager
    {
        public SalesorderReportSummary GetAll(SalesorderReport val)
        {
            return new SalesorderReportDBAccess().GetAll(val);
        }
        public SalesorderReportGraph GetAllgraph(SalesorderReport val)
        {
            return new SalesorderReportDBAccess().GetAllgraph(val);
        }

        public SalesorderReportSummaryChild GetAllChild(SalesorderReport val)
        {
            return new SalesorderReportDBAccess().GetAllChild(val);
        }
    }
}