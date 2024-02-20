using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CashbookManager
    {
        public Bank cashboodadddetails(string companycode)
        {
            return new CashbookDBAccess().cashboodadddetails(companycode);
        }
        public Bank opgetaccountname(string val)
        {
            return new CashbookDBAccess().opgetaccountname(val);
        }
        public Bank cashbooksummary(Bankdetails val,string companycode)
        {
            return new CashbookDBAccess().cashbooksummary(val, companycode);
        }
        public Bankmodel cashbookdelete(journaldetails values)
        {
            return new CashbookDBAccess().cashbookdelete(values);
        }
        public Bankmodel cashbookentry(journaldetails val, string userGid, string comapnycode)
        {
            return new CashbookDBAccess().cashbookentry(val, userGid, comapnycode);
        }
    }
}