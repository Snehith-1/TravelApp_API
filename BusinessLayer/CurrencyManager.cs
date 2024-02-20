using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CurrencyManager
    {
        public Currency GetAll()
        {
            return new CurrencyDBAccess().GetAll();
        }
        public Currency currencymastersummary()
        {

            return new CurrencyDBAccess().currencymastersummary();
        }
        public Currencymodel Update(CurrencyList val, string usergid)
        {
            return new CurrencyDBAccess().Update(val, usergid);
        }
        public Currencymodel currencyedit(CurrencyList val, string usergid)
        {
            return new CurrencyDBAccess().currencyedit(val, usergid);
        }
        public Currencymodel currencyupdate(CurrencyList val, string usergid)
        {
            return new CurrencyDBAccess().currencyupdate(val, usergid);
        }
        public Currencymodel Add(Currencydetail val, string userGid)
        {
            return new CurrencyDBAccess().Add(val, userGid);
        }
        public Currencydetail getcurrency(int val)
        {
            return new CurrencyDBAccess().getcurrency(val);
        }

        public Currency countrysummary()
        {
            return new CurrencyDBAccess().countrysummary();
        }
    }
}