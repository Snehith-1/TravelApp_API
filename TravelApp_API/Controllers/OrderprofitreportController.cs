using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities; 

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/orderprofitreport")]
    public class OrderprofitreportController :ApiController 
    {
        [Authorize]
        [HttpPost]
        [ActionName("summary")]
        public IHttpActionResult summary(orderprofitdetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new OrderprofitreportManager().summary(values));
           // return Ok(new OrderprofitreportManager().summary(values));
        }
    }
}