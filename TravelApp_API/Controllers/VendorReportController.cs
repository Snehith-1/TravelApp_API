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
    [RoutePrefix("api/VendorReport")]
    public class VendorReportController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("Summary")]
        public IHttpActionResult Summary(VendorReport values)
        {
            return Ok(new VendorReportManager().GetAll(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Graph")]
        public IHttpActionResult Graph(VendorReport values)
        {
            return Ok(new VendorReportManager().GetAllgraph(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("SummaryChild")]
        public IHttpActionResult SummaryChild(VendorReport values)
        {
            return Ok(new VendorReportManager().GetAllChild(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendoroutstandingreport")]
        public IHttpActionResult vendoroutstandingreport(VendorReport values)
        {
            return Ok(new VendorReportManager().vendoroutstandingreport(values));
        }
    }
}
