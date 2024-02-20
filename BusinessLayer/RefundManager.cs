using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class RefundManager
    {
        public Refund GetAll()
        {
            return new RefundDBAccess().GetAll();
        }
        public Refund refundreceiptsummary(Refund val)
        {
            return new RefundDBAccess().refundreceiptsummary(val);
        }
        public Refund refundadvancesummary()
        {
            return new RefundDBAccess().refundadvancesummary();
        }
        public Refundlist Get(int values)
        {
            return new RefundDBAccess().Get(values);
        }
        public Refundmodel Add(Refunddetails val, string userGid)
        {
            return new RefundDBAccess().Add(val, userGid);
        }
        public Refundmodel Update(Refundlist val, string usergid)
        {
            return new RefundDBAccess().Update(val, usergid);
        }
        public Refundmodel Delete(int values)
        {
            return new RefundDBAccess().Delete(values);
        }
        public Refunddetails salesdetailrefund(int values)
        {
            return new RefundDBAccess().salesdetailrefund(values);
        }
        public Refunddetails refundreferenceno(string usergid)
        {
            return new RefundDBAccess().refundreferenceno(usergid);
        }
        public Refunddetails refundreceiptdetails(Refunddetails values, string usergid)
        {
            return new RefundDBAccess().refundreceiptdetails(values, usergid);
        }
        public Refunddetails refundadvancedetails(string values)
        {
            return new RefundDBAccess().refundadvancedetails(values);
        }
        public Refundmodel refundreceipadvanceadd(Refunddetails val, string userGid)
        {
            return new RefundDBAccess().refundreceipadvanceadd(val, userGid);
        }
        public Refunddetails refundvendordetails(Refunddetails val)
        {
            return new RefundDBAccess().refundvendordetails(val);
        }
    }
}