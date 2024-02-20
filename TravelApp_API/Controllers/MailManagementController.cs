using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;
using System.Web;

namespace TravelApp_API.Controllers
{

    [RoutePrefix("api/Mailmanagement")]
    public class MailManagementController : ApiController
    {
        // GET api/<controller> [HttpPost]
        [Authorize]
        [HttpPost]
        [ActionName("Mailsummary")]
        public IHttpActionResult Mailsummary()
        {
            return Ok(new MailManagementManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("Mailservice")]
        public IHttpActionResult Mailservice(MailPushdetail values)
        {

            /* return Ok(new MailManagementManager().Mailservice(values.customerList, values.mailmanagement_gid));

             return Ok(new MailManagementManager().Mailservice(values.customer_gid,values.mailmanagement_gid));*/

            return Ok(new MailManagementManager().Mailservice(values.customerList, values.mailmanagement_gid));

        }
      
        [Authorize]
        [HttpPost]
        [ActionName("Mailedit")]
        public IHttpActionResult Mailedit(MailManagementdetail values)
        {
            return Ok(new MailManagementManager().Get(values.mailmanagement_gid));
        }

       
        [Authorize]
        [HttpPost]
        [ActionName("Mailadd")]
        public HttpResponseMessage UploadDocument()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            MailManagementmodel getdocumentimportexcel = new MailManagementmodel();
            MailManagementManager GetdocumentFunctions = new MailManagementManager();
            MailManagementdetail GetdocumentImportExcel = new MailManagementdetail();
            httpRequest = HttpContext.Current.Request;
            // return Ok(new MailManagementManager().Add(companycode, httpRequest, usergid));
            getdocumentimportexcel = GetdocumentFunctions.Add(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocumentimportexcel);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Mailupdate")]
        public IHttpActionResult Mailupdate([FromBody] MailManagementdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new MailManagementManager().Update(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Maildelete")]
        public IHttpActionResult Maildelete(MailManagementdelete values)
        {
            return Ok(new MailManagementManager().Delete(values.mailmanagement_gid));
        }
    }
}