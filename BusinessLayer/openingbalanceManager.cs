using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class openingbalanceManager
    {
        public openingbalance openingbalance()
        {
            return new openingbalanceDBAccess().openingbalance();
        }
        public openingbalancedetail opgetaccountname(string val)
        {
            return new openingbalanceDBAccess().opgetaccountname(val);
        }
        public openingbalance openingbalanceassetsummary()
        {
            return new openingbalanceDBAccess().openingbalanceassetsummary();
        }
        public openingbalance openingbalancechildsummary(string val)
        {
            return new openingbalanceDBAccess().openingbalancechildsummary(val);
        }
        public openingbalance openingbalancechild1summary(string val)
        {
            return new openingbalanceDBAccess().openingbalancechild1summary(val);
        }
        public openingbalance openingbalancechild2summary(string val)
        {
            return new openingbalanceDBAccess().openingbalancechild2summary(val);
        }
    } 
}