using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities;


namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/advance")]
    public class AdvanceController:ApiController 
    {
        [Authorize]
        [HttpPost]
        [ActionName("advancesummary")]
        public IHttpActionResult advancesummary(Advancedetail val)
        {
            return Ok(new AdvanceManager().advancesummary(val.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("advanceadd")]
        public IHttpActionResult advanceadd(Advancedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new AdvanceManager().advanceadd(val, userGid));
        }
    }
}