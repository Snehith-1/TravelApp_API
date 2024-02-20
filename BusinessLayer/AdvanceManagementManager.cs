using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class AdvanceManagementManager
    {
        public advancemanagement advancemanagementsummary()
        {
            return new AdvanceManagementDBAccess().advancemanagementsummary();
        }
        public Advancecustomerdetails advancemanagementinvoicesummary(Advancecustomerdetails dtl)
        {
            return new AdvanceManagementDBAccess().advancemanagementinvoicesummary(dtl);//
        }
        public customerinvoicedetail customerinvoiceselectlist(customerinvoicedetail val)
        {
            return new AdvanceManagementDBAccess().customerinvoiceselectlist(val);

        }
        public customerinvoicedetail customerreceiptaddselect(customerinvoicedetail values)
        {
            return new AdvanceManagementDBAccess().customerreceiptaddselect(values);
        }


    }
}