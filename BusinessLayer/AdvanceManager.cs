using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class AdvanceManager
    {
        public Advance advancesummary(string val)
        {
            return new AdvanceDBAccess().advancesummary(val);
        }
        public Advancemodel advanceadd(Advancedetail val, string userGid)
        {
            return new AdvanceDBAccess().advanceadd(val, userGid);
        }
    }
}