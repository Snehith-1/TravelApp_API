
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
    [RoutePrefix("api/partner")]
    public class PartnerController: ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("partnersummary")]
        public IHttpActionResult partnersummary()
        {
            return Ok(new PartnerManager().partnersummary());
        }
        [Authorize]
        [HttpPost]
        [Route("partneradd")]
        public IHttpActionResult partneradd(Partnerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new PartnerManager().partneradd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [Route("deletepartner")]
        public IHttpActionResult deletepartner(Partnerdetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new PartnerManager().Delete(values.partner_gid));
        }

        [Authorize]
        [HttpPost]
        [Route("partneredit")]
        public IHttpActionResult editpartner(Partnerdetails values)
        {
            IEnumerable<string> headervalues = Request.Headers.GetValues("Authorization");
            var id = headervalues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new PartnerManager().Get(values.partner_gid));

        }
        [Authorize]
        [HttpPost]
        [Route("partnerupdate")]
        public IHttpActionResult updatepartner(Partnerdetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new PartnerManager().Update(values, usergid));
           
        }


    }
}