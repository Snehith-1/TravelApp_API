using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class VendorReportManager
    {
        public VendorReportSummary GetAll(VendorReport val)
        {
            return new VendorReportDBAccess().GetAll(val);
        }
        public VendorReportGraph GetAllgraph(VendorReport val)
        {
            return new VendorReportDBAccess().GetAllgraph(val);
        }

        public VendorReportSummaryChild GetAllChild(VendorReport val)
        {
            return new VendorReportDBAccess().GetAllChild(val);
        }
        public VendorReportSummary vendoroutstandingreport(VendorReport val)
        {
            return new VendorReportDBAccess().vendoroutstandingreport(val);
        }
    }
}