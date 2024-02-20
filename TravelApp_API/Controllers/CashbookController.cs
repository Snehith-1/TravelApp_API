using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;


namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/cashbook")]
    public class CashbookController:ApiController 
    {
        [Authorize]
        [HttpPost]
        [Route("cashbookadddetails")]
        public IHttpActionResult cashboodadddetails()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyID(id);
            return Ok(new CashbookManager().cashboodadddetails(companycode));
        }
        [Authorize]
        [HttpPost]
        [Route("opgetaccountname")]
        public IHttpActionResult opgetaccountname(openingbalancedetail values)
        {
            return Ok(new CashbookManager().opgetaccountname(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("cashbooksummary")]
        public IHttpActionResult cashbooksummary(Bankdetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyID(id);
            return Ok(new CashbookManager().cashbooksummary(values, companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("cashbookdelete")]
        public IHttpActionResult cashbookdelete(journaldetails values)
        {
            return Ok(new CashbookManager().cashbookdelete(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("cashbookentry")]
        public IHttpActionResult  cashbookadd(journaldetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyID(id);
            return Ok(new CashbookManager().cashbookentry(val, userGid, companycode));

        }
    }
}