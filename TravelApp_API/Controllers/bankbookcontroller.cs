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
    [RoutePrefix("api/bankbook")]
    public class bankbookcontroller:ApiController 
    {
        [Authorize]
        [HttpPost]
        [Route("bankboodadddetails")]
        public IHttpActionResult bankboodadddetails(Bankdetails values)
        {
            return Ok(new bankbookManager().bankboodadddetails(values.bank_gid));
        }        
        [Authorize]
        [HttpPost]
        [Route("opgetaccountname")]
        public IHttpActionResult opgetaccountname(openingbalancedetail values)
        {
            return Ok(new bankbookManager().opgetaccountname(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("bankbooksummary")]
        public IHttpActionResult bankbooksummary(Bankdetails values)
        {
            return Ok(new bankbookManager().bankbooksummary(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("bankbookdelete")]
        public IHttpActionResult bankbookdelete(journaldetails values)
        {
            return Ok(new bankbookManager().bankbookdelete(values));
        }
    }
}