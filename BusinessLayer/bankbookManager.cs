using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class bankbookManager
    {
        public Bank bankboodadddetails(string  val)
        {
            return new bankbookDBAccess().bankboodadddetails(val);
        }      
        public Bank opgetaccountname(string val)
        {
            return new bankbookDBAccess().opgetaccountname(val);
        }
        public Bank bankbooksummary(Bankdetails val)
        {
            return new bankbookDBAccess().bankbooksummary(val);
        }
        public Bankmodel bankbookdelete(journaldetails values)
        {
            return new bankbookDBAccess().bankbookdelete(values);
        }
    }
}