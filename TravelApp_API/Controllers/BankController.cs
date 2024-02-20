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
    [RoutePrefix("api/bank")]
    public class BankController:ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("banksummary")]
        public IHttpActionResult banksummary()
        {
            return Ok(new BankManager().banksummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("bankadd")]
        public IHttpActionResult bankadd(journaldetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new BankManager().bankadd(val,userGid));
        }
        [Authorize]
        [HttpPost]
        [Route("journaladd")]
        public IHttpActionResult journaladd(journaldetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyID(id);
            return Ok(new BankManager().journaladd(val, userGid,companycode));
        }
        [Authorize]
        [HttpPost]
        [Route("chartofaccountadd")]
        public IHttpActionResult chartofaccountadd(chartofaccountdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new BankManager().chartofaccountadd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [Route("getaccountgroupname")]
        public IHttpActionResult getaccountgroupname()
        {
            return Ok(new BankManager().getaccountgroupname());
        }
        [Authorize]
        [HttpPost]
        [Route("chartofaccountsummary")]
        public IHttpActionResult chartofaccountsummary()
        {
            return Ok(new BankManager().chartofaccountsummary());
        }
        [Authorize]
        [HttpPost]
        [Route("bankinsert")]
        public IHttpActionResult bankinsert()
        {
            return Ok(new BankManager().bankinsert());
        }
        [Authorize]
        [HttpPost]
        [Route("journaladdsummary")]
        public IHttpActionResult journaladdsummary()
        {
            return Ok(new BankManager().journaladdsummary());
        }
        [Authorize]
        [HttpPost]
        [Route("journaladddtlsummary")]
        public IHttpActionResult journaladddtlsummary(journaldetails values)
        {
            return Ok(new BankManager().journaladddtlsummary(values.journal_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("chartofaccountchildsummary")]
        public IHttpActionResult chartofaccountchildsummary(journaldetails val)
        {
            return Ok(new BankManager().chartofaccountchildsummary(val.account_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("chartofaccountdetails")]
        public IHttpActionResult chartofaccountdetails(chartofaccountdetails val)
        {
            return Ok(new BankManager().chartofaccountdetails(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("chartofaccountdelete")]
        public IHttpActionResult chartofaccountdelete(accountlist val)
        {
            return Ok(new BankManager().chartofaccountdelete(val.account_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("journaldelete")]
        public IHttpActionResult journaldelete(journaldetails values)
        {
            return Ok(new BankManager().journaldelete(values.journal_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("chartsofaccountdelete")]
        public IHttpActionResult chartsofaccountdelete(journaldetails values)
        {
            return Ok(new BankManager().chartsofaccountdelete(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("journalentryedit")]
        public IHttpActionResult journalentryedit(journaldetails values)
        {
            return Ok(new BankManager().journalentryedit(values.journal_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("journalentryeditdel")]
        public IHttpActionResult journalentryeditdel(journaldetails values)
        {
            return Ok(new BankManager().journalentryeditdel(values.journaldtl_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("journalentryeditadd")]
        public IHttpActionResult journalentryeditadd(journaldetails values)
        {
            return Ok(new BankManager().journalentryeditadd(values));
        }
    }
}