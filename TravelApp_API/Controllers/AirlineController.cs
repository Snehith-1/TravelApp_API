using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities;
using DataAccess;
using System.Web;
using System.IO;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/airline")]
    public class AirlineController:ApiController 
    {
        [Authorize]
        [HttpPost]
        [ActionName("airlinesummary")]
        public IHttpActionResult airlinesummary()
        {
            return Ok(new AirlineManager().airlinesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("airlineradd")]
        public IHttpActionResult airlineradd(airlinedetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new AirlineManager().airlineradd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName ("airlinedelete")]
        public IHttpActionResult airlinedelete(airlinedetails val)
        {
        return Ok(new AirlineManager().airlinedelete(val.airline_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("airlineimportexcel")]
        public IHttpActionResult airlineimportexcel()
        {

            HttpRequest httpreq;
            httpreq = HttpContext.Current.Request;
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var company_code = new TokenManager().GetcompanyCode(id);
            airlinedetails val = new airlinedetails();
            return Ok(new AirlineManager().Excel(company_code, httpreq, val, userGid));
           
        }
        [Authorize]
        [HttpPost]
        [ActionName("othersericesadd")]
        public IHttpActionResult othersericesadd(otherservicedetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new AirlineManager().othersericesadd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("otherservicesummary")]
        public IHttpActionResult otherservicesummary()
        {
            return Ok(new AirlineManager().otherservicesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("otherservicetype")]
        public IHttpActionResult otherservicetype()
        {
            return Ok(new AirlineManager().otherservicetype());
        }
        [Authorize]
        [HttpPost]
        [ActionName("otherserviceedit")]
        public IHttpActionResult otherserviceedit([FromBody] otherservicelist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new AirlineManager().otherserviceedit(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("otherserviceupdate")]
        public IHttpActionResult otherserviceupdate([FromBody] otherservicelist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new AirlineManager().otherserviceupdate(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("choosesubservicetype")]
        public IHttpActionResult choosesubservicetype(otherservicelist val)
        {
            return Ok(new AirlineManager().choosesubservicetype(val));
        }
    }
}