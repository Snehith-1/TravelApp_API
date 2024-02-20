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

    [RoutePrefix("api/smsmanagement")]
    public class SmsManagementController : ApiController
    {
        // GET api/<controller> [HttpPost]
        [Authorize]
        [HttpPost]
        [ActionName("smssummary")]
        public IHttpActionResult smssummary()
        {
            return Ok(new SmsManagementManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("Smsservice")]
        public IHttpActionResult smsservice(smsPushdetail values)
        {
            return Ok(new SmsManagementManager().smsservice(values.customerList, values.smsmanagement_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("smsedit")]
        public IHttpActionResult smsedit(SmsManagementdetail values)
        {
            return Ok(new SmsManagementManager().Get(values.smsmanagement_gid ));
        }

        [Authorize]
        [HttpPost]
        [ActionName("smsadd")]
       public IHttpActionResult smsadd([FromBody] SmsManagementdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();           
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new SmsManagementManager().Add(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("smsupdate")]
        public IHttpActionResult smsupdate([FromBody] SmsManagementdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new SmsManagementManager().Update(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("smsdelete")]
        public IHttpActionResult smsdelete(SmsManagementdelete values)
        {
            return Ok(new SmsManagementManager().Delete(values.smsmanagement_gid));
        }
    }
}