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
    [RoutePrefix("api/AIR")]
    public class AIRController:ApiController 
    {     
        [HttpPost]
        [ActionName("receiveairdata")]
        public IHttpActionResult receiveairdata(AIRDetails val)
        {
           //IEnumerable<string> headerValues = Request.Headers.GetValues("val");
            //var id = headerValues.FirstOrDefault();
            //var userGid = new TokenManager().GetuserID(id);
            return Ok(new AIRManager().receiveairdata(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("receiveairdatasummary")]
        public IHttpActionResult receiveairdatasummary()
        {
            return Ok(new AIRManager().receiveairdatasummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("raiseairfilesummary")]
        public IHttpActionResult raiseairfilesummary()
        {
            return Ok(new AIRManager().raiseairfilesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("invoicedetails")]
        public IHttpActionResult invoicedetails(Billingdetail val)
        {
            return Ok(new AIRManager().invoicedetails(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("airnotification")]
        public IHttpActionResult airnotification()
        {
            return Ok(new AIRManager().airnotification());
        }
    }
}