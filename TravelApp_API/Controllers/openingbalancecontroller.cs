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
    [RoutePrefix("api/openingbalance")]
    public class openingbalancecontroller:ApiController 
    {
        [Authorize]
        [HttpPost]
        [Route("openingbalance")]
        public IHttpActionResult openingbalance()
        {
            return Ok(new openingbalanceManager().openingbalance());
        }
        [Authorize]
        [HttpPost]
        [Route("opgetaccountname")]
        public IHttpActionResult opgetaccountname(openingbalancedetail values)
        {
            return Ok(new openingbalanceManager().opgetaccountname(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("openingbalanceparentsummary")]
        public IHttpActionResult openingbalanceassetsummary()
        {
            return Ok(new openingbalanceManager().openingbalanceassetsummary());
        }
        [Authorize]
        [HttpPost]
        [Route("openingbalancechildsummary")]
        public IHttpActionResult openingbalancechildsummary(openingbalancedetail values)
        {
            return Ok(new openingbalanceManager().openingbalancechildsummary(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("openingbalancechild1summary")]
        public IHttpActionResult openingbalancechild1summary(openingbalancedetail values)
        {
            return Ok(new openingbalanceManager().openingbalancechild1summary(values.account_gid));
        }
        [Authorize]
        [HttpPost]
        [Route("openingbalancechild2summary")]
        public IHttpActionResult openingbalancechild2summary(openingbalancedetail values)
        {
            return Ok(new openingbalanceManager().openingbalancechild2summary(values.account_gid));
        }
    }
}