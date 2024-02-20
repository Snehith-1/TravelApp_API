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
    [RoutePrefix("api/salesteam")]
    public class SalesteamController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("salesteamsummary")]
        public IHttpActionResult salesteamsummary()
        {
            return Ok(new SalesteamManager().GetAll());
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesteamedit")]
        public IHttpActionResult salesteamedit(Salesteamdetail values)
        {
            return Ok(new SalesteamManager().Get(values.salesteam_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesteamadd")]
        public IHttpActionResult salesteamadd([FromBody] Salesteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new SalesteamManager().Add(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesteamdelete")]
        public IHttpActionResult salesteamdelete(Salesteamlist values)
        {
            return Ok(new SalesteamManager().Delete(values.salesteam_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesteamupdate")]
        public IHttpActionResult salesteamupdate([FromBody] Salesteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new SalesteamManager().Update(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("salesteamemployee")]
        public IHttpActionResult salesteamemployee(Salesteamlist values)
        {
            return Ok(new SalesteamManager().salesteamemployee(values.salesteam_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("salesteammanager")]
        public IHttpActionResult salesteammanager(Salesteamlist values)
        {
            return Ok(new SalesteamManager().salesteammanager(values.salesteam_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("asignemployeesubmit")]
        public IHttpActionResult asignemployeesubmit([FromBody] Salesteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new SalesteamManager().asignemployeesubmit(val, usergid)); 
        }
        [Authorize]
        [HttpPost]
        [ActionName("asignmanagersubmit")]
        public IHttpActionResult asignmanagersubmit([FromBody] Salesteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new SalesteamManager().asignmanagersubmit(val, usergid));
        }
    }
}