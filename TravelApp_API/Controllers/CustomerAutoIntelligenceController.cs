using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/customerautointelligence")]
    public class CustomerAutoIntelligenceController :ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("customerautointelligencesummary")]
        public IHttpActionResult customerautointelligencesummary(CustomerAutoIntelligenceDetails val )
        {
            return Ok(new CustomerAutoIntelligenceManager().customerautointelligencesummary(val));
        }

        [Authorize]
        [HttpPost]
        [ActionName("paxautointelligencesummary")]
        public IHttpActionResult paxautointelligencesummary(CustomerAutoIntelligenceDetails val)
        {
            return Ok(new CustomerAutoIntelligenceManager().paxautointelligencesummary(val));
        }








        [Authorize]
        [HttpPost]
        [ActionName("journalaccountlist")]
        public IHttpActionResult journalaccountlist(journalaccountlistdetails val)
        {
            return Ok(new CustomerAutoIntelligenceManager().journalaccountlist(val));
        }

        [Authorize]
        [HttpPost]
        [ActionName("journalaccountgrouplist")]
        public IHttpActionResult journalaccountgrouplist(journalaccountgrouplistdetails val)
        {
            return Ok(new CustomerAutoIntelligenceManager().journalaccountgrouplist(val));
        }
    }
}