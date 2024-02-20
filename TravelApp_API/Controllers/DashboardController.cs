using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;
using System.Web.Http.Cors;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/dashboard")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("dashboardcounts")]
        public IHttpActionResult dashboardcounts()
        {
            return Ok(new DashboardManager().dashboardcounts());
        }

        [Authorize]
        [HttpPost]
        [ActionName("dashboardsttransaction")]
        public IHttpActionResult dashboardsttransaction()
        {
            return Ok(new DashboardManager().dashboardsttransaction());
        }

        [Authorize]
        [HttpPost]
        [ActionName("dashlistinvoicecount")]
        public IHttpActionResult dashlistinvoicecount()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new DashboardManager().dashlistinvoicecount(userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("dashboardservicesales")]
        public IHttpActionResult dashboardservicesales()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new DashboardManager().dashboardservicesales(companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("dashboardservicesalesbar")]
        public IHttpActionResult dashboardservicesalesbar()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new DashboardManager().dashboardservicesalesbar(companycode));
        }
    }
}