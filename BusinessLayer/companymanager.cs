using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CompanyManager
    {
        public companydetails get(string Company_gid,string company_code)
        {
            return new CompanyDBAccess().get(Company_gid, company_code);
        }

        public companymodel Update(companydetails val, string usergid)
        {
            return new CompanyDBAccess().Update(val, usergid);
        }

        public companymodel getcompanylogoupload(string Company_gid, HttpRequest httpRequest, string usergid,string companycode)
        {
            return new CompanyDBAccess().getcompanylogoupload(Company_gid, httpRequest, usergid, companycode);
        }

        public companymodel getwelcomelogoupload(string Company_gid, HttpRequest httpRequest, string usergid,string companycode)
        {
            return new CompanyDBAccess().getwelcomelogoupload(Company_gid, httpRequest, usergid, companycode);
        }
        public companymodel getletterheadupload(string Company_gid, HttpRequest httpRequest, string usergid, string company_code)
        {
            return new CompanyDBAccess().getletterheadupload(Company_gid, httpRequest, usergid, company_code);
        }
    }
}