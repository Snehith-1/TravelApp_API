using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class BankManager
    {
        public  Bank banksummary()
        {
            return new BankDBAccess().banksummary();
        }
        public Bankmodel bankadd(journaldetails val, string userGid)
        {
            return new BankDBAccess().bankadd(val,  userGid);
        }
        public Bankmodel journaladd(journaldetails val, string userGid,string comapnycode)
        {
            return new BankDBAccess().journaladd(val, userGid, comapnycode);
        }
           public Bankmodel chartofaccountadd(chartofaccountdetails val, string userGid)
        {
            return new BankDBAccess().chartofaccountadd(val, userGid);
        }
        public Bank getaccountgroupname()
        {
            return new BankDBAccess().getaccountgroupname();
        }
        public Bank chartofaccountsummary()
        {
            return new BankDBAccess().chartofaccountsummary();
        }
        public Bankmodel bankinsert()
        {
            return new BankDBAccess().bankinsert();
        }
        public Bank journaladdsummary()
        {
            return new BankDBAccess().journaladdsummary();
        }
        public Bank journaladddtlsummary(int values)
        {
            return new BankDBAccess().journaladddtlsummary(values);
        }
        public Bank chartofaccountchildsummary(string val)
        {
            return new BankDBAccess().chartofaccountchildsummary(val);
        }
        public chartofaccountdetails chartofaccountdetails(chartofaccountdetails val)
        {
            return new BankDBAccess().chartofaccountdetails(val);
        }

        public Bankmodel chartofaccountdelete(string val)
        {
            return new BankDBAccess().chartofaccountdelete(val);
        }
        public Bankmodel journaldelete(int values)
        {
            return new BankDBAccess().journaldelete(values);
        }
        public Bankmodel chartsofaccountdelete(string values)
        {
            return new BankDBAccess().chartsofaccountdelete(values);
        }
        public journaldetails journalentryedit(int val)
        {
            return new BankDBAccess().journalentryedit(val);
        }
        public Bankmodel journalentryeditdel(string val)
        {
            return new BankDBAccess().journalentryeditdel(val);
        }
        public Bankmodel journalentryeditadd(journaldetails val)
        {
            return new BankDBAccess().journalentryeditadd(val);
        }
    }
}