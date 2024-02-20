using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;


namespace BusinessLayer
{
    public class PartnerManager
    {
        public Partner partnersummary() 
        {
            return new PartnerDBAccess().partnersummary();
        }

        public Partnerdetails Get(int values)
        {
            return new PartnerDBAccess().Get(values);
        }
        public Partnermodel partneradd(Partnerdetails val,string usergid)
        {
            return new PartnerDBAccess().partneradd(val,usergid);
        }
        public Partnermodel Delete(int values)
        {
            return new PartnerDBAccess().Delete(values);
        }

        public Partnermodel Update(Partnerdetails  values, string usergid)
        {
            return new PartnerDBAccess().Update(values, usergid);
        }

       

    }
}

