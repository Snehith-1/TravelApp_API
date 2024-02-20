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

    [RoutePrefix("api/activity")]
    public class ActivityController : ApiController
    {
        // GET api/<controller> [HttpPost]
        [Authorize]
        [HttpPost]
        [ActionName("activitysummary")]
        public IHttpActionResult activitysummary()
        {
            return Ok(new ActivityManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("activityservice")]
        public IHttpActionResult activityservice(Activitydetail values)
        {
            return Ok(new ActivityManager().activityservice(values.service_type));
        }

        [Authorize]
        [HttpPost]
        [ActionName("activityedit")]
        public IHttpActionResult activityedit(Activitydetail values)
        {
            return Ok(new ActivityManager().Get(values.activity_gid ));
        }

        [Authorize]
        [HttpPost]
        [ActionName("activityadd")]
       public IHttpActionResult activityadd([FromBody] Activitydetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();           
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new ActivityManager().Add(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("activityupdate")]
        public IHttpActionResult activityupdate([FromBody] ActivityList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new ActivityManager().Update(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("activitydelete")]
        public IHttpActionResult activitydelete(Activitydelete values)
        {
            return Ok(new ActivityManager().Delete(values.activity_gid));
        }
    }
}