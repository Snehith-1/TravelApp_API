using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class ReconciliationManager
    {
        public reconciliation GetAll()
        {
            return new ReconciliationDBAccess().GetAll();
        }

        public Reconciliation GetdocumentUploadExcel(string companycode, HttpRequest httpRequest, string usergid)
        {
            return new ReconciliationDBAccess().GetdocumentUploadExcel(companycode, httpRequest, usergid);
        }
        public Reconciliation reconciliationcount(reconciliationcountlist values)
        {
            return new ReconciliationDBAccess().reconciliationcount(values);
        }
        public Reconciliation reconciliationpath(reconciliationlist value)
        {
            return new ReconciliationDBAccess().reconciliationpath(value);
        }
        public Reconciliation reconciliationmatchingcount(matchingcountlist val)
        {
            return new ReconciliationDBAccess().reconciliationmatchingcount(val);
        }
        public Reconciliation reconciliationmatchingagentcount(matchingwithagentcount val)
        {
            return new ReconciliationDBAccess().reconciliationmatchingagentcount(val);
        }
    }
}