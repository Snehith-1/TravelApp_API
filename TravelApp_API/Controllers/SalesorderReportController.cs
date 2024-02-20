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
    [RoutePrefix("api/SalesorderReport")]
    public class SalesorderReportController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("Summary")]
        public IHttpActionResult Summary(SalesorderReport values)
        {
            return Ok(new SalesorderReportManager().GetAll(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Graph")]
        public IHttpActionResult Graph(SalesorderReport values)
        {
            return Ok(new SalesorderReportManager().GetAllgraph(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("SummaryChild")]
        public IHttpActionResult SummaryChild(SalesorderReport values)
        {
            return Ok(new SalesorderReportManager().GetAllChild(values));
        }
    }
}
