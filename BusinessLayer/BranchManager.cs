using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using BusinessEntities;
using BusinessLayer;

namespace BusinessLayer
{
    public class BranchManager
    {
        public Branch GetAll()
        {
            return new BranchDBAccess().GetAll();
        }
        public Branchmodel branchcode(Branchdetails val, string usergid)
        {
            return new BranchDBAccess().branchcode(val,usergid);
        }
        public Branchmodel branchadd(Branchdetails val, string userGid)
        {
            return new BranchDBAccess().branchadd(val, userGid);
        }
        public Branchmodel branchedit(Branchdetails val, string usergid)
        {
            return new BranchDBAccess().branchedit(val, usergid);
        }
        public Branchmodel branchupdate(Branchdetails val, string usergid)
        {
            return new BranchDBAccess().branchupdate(val, usergid);
        }
        public Branchmodel Delete(int val)
        {
            return new BranchDBAccess().Delete(val);
        }

    }
}