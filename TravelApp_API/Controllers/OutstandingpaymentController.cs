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
    [RoutePrefix("api/OutstandingPayment")]
    public class OutstandingpaymentController : ApiController
    {
      
        [Authorize]
        [HttpPost]
        [ActionName("outstandingpaymentaddselect")]
        public IHttpActionResult outstandingpaymentaddselect(customerinvoicedetail values)
        {
            return Ok(new OutstandingpaymentManager().outstandingpaymentaddselect(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("airinvoiceaddselect")]
        public IHttpActionResult airinvoiceaddselect(customerinvoicedetail values)
        {
            return Ok(new OutstandingpaymentManager().airinvoiceaddselect(values));
        }


        //[Authorize]
        //[HttpPost]
        //[ActionName("salesinvoiceupdate")]
        //public IHttpActionResult salesinvoiceupdate(customerinvoicedetail values)
        //{
        //    return Ok(new OutstandingpaymentManager().salesinvoiceupdate(values));
        //}
        [Authorize]
        [HttpPost]
        [ActionName("outstandingpaymentoverallsubmit")]
        public IHttpActionResult outstandingpaymentoverallsubmit([FromBody] customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new OutstandingpaymentManager().outstandingpaymentoverallsubmit(val, userGid));
        }

    }
}