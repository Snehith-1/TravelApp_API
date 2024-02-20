using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Incomeandexpense
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class IEdetails : Incomeandexpense
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
            public string branch_gid { get; set; }
        public double expense_closing { get; set; }
        public double income_closing { get; set; }
        //public string Companycode { get; set; }

        public List<incomelist> incomelist { get; set; }
        public List<expenselist> expenselist { get; set; }
    }
    public class incomelist
    {
        public string account_gid { get; set; }
        public string account_name { get; set; }
        public string accountgroup_name { get; set; }
        public double debit_amount { get; set; }
        public double credit_amount { get; set; }
        public string branch_name { get; set; }
        public string company_code { get; set; }
    }
    public class expenselist
    {
        public string account_gid { get; set; }
        public string account_name { get; set; }
        public string accountgroup_name { get; set; }
        public double debit_amount { get; set; }
        public double credit_amount { get; set; }
        public string branch_name { get; set; }
        public string company_code { get; set; }
    }
}