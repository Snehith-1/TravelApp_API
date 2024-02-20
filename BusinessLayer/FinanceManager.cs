using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessEntities;
using DataAccess;
namespace BusinessLayer
{
    public class FinanceManager 
    {
        public Financemodel financemasteradd(Financedetail val, string userGid)
        {
            return new FinanceDBAcess().financemasteradd(val, userGid);
        }

        public Financemodel financemasterupdate(Financedetail val, string userGid)
        {
            return new FinanceDBAcess().financemasterupdate(val, userGid);
        }

        public Financemodel financeinvoice(Financedetail val, string userGid)
        {
            return new FinanceDBAcess().financeinvoice(val, userGid);
        }
        public Financemodel financepayment(Financedetail val, string userGid)
        {
            return new FinanceDBAcess().financepayment(val, userGid);
        }
        public Financemodel financeadvance(Financedetail val,string userGid)
        {
            return new FinanceDBAcess().financeadvance(val, userGid);
        }
        public Financemodel financerefund(Financedetail val,string userGid)
        {
            return new FinanceDBAcess().financerefund(val, userGid);
        }
        public Financemodel invoicedelete(string val)
        {
            return new FinanceDBAcess().invoicedelete(val);

        }
        public Financemodel financenewrefund(Financerefunddetail val, string userGid)
        {
            return new FinanceDBAcess().financenewrefund(val, userGid);
        }
    }
}