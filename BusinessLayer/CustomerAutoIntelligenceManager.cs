using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class CustomerAutoIntelligenceManager
    {
        public CustomerAutoIntelligence customerautointelligencesummary(CustomerAutoIntelligenceDetails val )
        {
            return new CustomerAutoIntelligenceDBAccess().customerautointelligencesummary(val); //
        }
        public CustomerAutoIntelligence paxautointelligencesummary(CustomerAutoIntelligenceDetails val)
        {
            return new CustomerAutoIntelligenceDBAccess().paxautointelligencesummary(val); //
        }


        public journalaccountAutoIntelligence journalaccountlist(journalaccountlistdetails val)
        {
            return new CustomerAutoIntelligenceDBAccess().journalaccountlist(val); 
        }
             public journalaccountgrouplistAutoIntelligence journalaccountgrouplist(journalaccountgrouplistdetails val)
        {
            return new CustomerAutoIntelligenceDBAccess().journalaccountgrouplist(val);
        }
    }
}