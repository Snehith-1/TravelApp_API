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
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("servicesummary")]
        public IHttpActionResult servicesummary()
        {
            return Ok(new ServiceManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("servicereportsummary")]
        public IHttpActionResult servicereportsummary(Servicedetails val)
        {
            return Ok(new ServiceManager().servicereportsummary(val));
        }

    }
}