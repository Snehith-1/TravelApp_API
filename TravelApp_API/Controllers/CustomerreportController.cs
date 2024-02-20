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
    [RoutePrefix("api/CustomerReport")]
    public class CustomerReportController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("Summary")]
        public IHttpActionResult Summary(CustomerReport values)
        {
            return Ok(new CustomerReportManager().GetAll(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Graph")]
        public IHttpActionResult Graph(CustomerReport values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new CustomerReportManager().GetAllgraph(values, companycode));
        }

        [Authorize]
        [HttpPost]
        [ActionName("SummaryChild")]
        public IHttpActionResult SummaryChild(CustomerReport values)
        {
            return Ok(new CustomerReportManager().GetAllChild(values));
        }
    }
}
