using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class Incomeandexpensemanager
    {
        public Incomeandexpense incomeandexpensesummary(IEdetails val )
        {
            return new IncomeandexpenseDBAccess().incomeandexpensesummary(val);
        }

        public Incomeandexpense balancesheetsummary(IEdetails val, string value)
        {
            return new IncomeandexpenseDBAccess().balancesheetsummary(val, value);
        }
    }
}