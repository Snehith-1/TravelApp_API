using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class EnquiryManager
    {
        public Enquiry GetAll()
        {
            return new EnquiryDBAccess().GetAll();
        }
        public Enquiry show()
        {
            return new EnquiryDBAccess().show();
        }
        public Enquirymodel Add(Enquirydetails val, string userGid)
        {
            return new EnquiryDBAccess().Add(val, userGid);
        }
        public Enquirydetails Edit(int values)
        {
            return new EnquiryDBAccess().Edit(values);
        }
        public Enquirymodel Update(Enquirydetails val, string usergid)
        {
            return new EnquiryDBAccess().Update(val, usergid);
        }
        public Enquirymodel Delete(int values)
        {
            return new EnquiryDBAccess().Delete(values);
        }
        public Enquirymodel Log(Enquirydetails val, string userGid)
        {
            return new EnquiryDBAccess().Log(val, userGid);
        }
        public quotationdetail quatationadd(quotationdetail quotationdetail)
        {
            return new EnquiryDBAccess().quatationadd(quotationdetail);
        }

        //public Enquirymodel Logedit(Enquirydetails val, string userGid)
        //{
        //    return new EnquiryDBAccess().Log(val, userGid);
        //}
        public Enquiry LogAll(Enquirydetails val)
        {
            return new EnquiryDBAccess().LogAll(val);
        }
        public Enquirymodel quatationaddall(quotationdetail val, string userGid)
        {
            return new EnquiryDBAccess().quatationaddall(val, userGid);
        }
        public Enquirydetails quotationaddbind(int val)
        {
            return new EnquiryDBAccess().quotationaddbind(val);
        }
        public Enquirymodel enquirylogdelete(Enquirydetails val)
        {
            return new EnquiryDBAccess().enquirylogdelete(val);
        }

        public Enquiry unit()
        {
            return new EnquiryDBAccess().unit();
        }
        public Enquirydetails enquiryreferenceno(string usergid)
        {
            return new EnquiryDBAccess().enquiryreferenceno(usergid);
        }
    }
}