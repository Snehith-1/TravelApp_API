using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class QuotationManager
    {
        public Quotation GetAll(string val)
        {
            return new QuotationDBAccess().GetAll(val);
        }
        public Quotationmodel Add(Quotationdetail val, string userGid)
        {
            return new QuotationDBAccess().Add(val, userGid);
        }
        public Quotationdetail Get(int values)
        {
            return new QuotationDBAccess().Get(values);
        }
        public Quotationdetail Edit(int values)
        {
            return new QuotationDBAccess().Edit(values);
        }
        public Quotationmodel Update(Quotationdetail val, string usergid)
        {
            return new QuotationDBAccess().Update(val, usergid);
        }
        public Quotationmodel Cancel(QuotationList val, string usergid)
        {
            return new QuotationDBAccess().Cancel(val, usergid);
        }
        public Quotationmodel directquotationadd(Quotationdetail val, string userGid)
        {
            return new QuotationDBAccess().directquotationadd(val, userGid);
        }
        public Quotationdetail directquotationedit(int values)
        {
            return new QuotationDBAccess().directquotationedit(values);
        }
        public Quotationmodel directquotationupdate(Quotationdetail val, string usergid)
        {
            return new QuotationDBAccess().directquotationupdate(val, usergid);
        }
        public Quotationmodel quotationtosalesorder(int val, string usergid)
        {
            return new QuotationDBAccess().quotationtosalesorder(val, usergid);
        }
        public Quotationdetail quotationreferenceno(string usergid)
        {
            return new QuotationDBAccess().quotationreferenceno(usergid);
        }
    }
}