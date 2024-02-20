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
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        [HttpPost]
        [Authorize]
        [ActionName("companyedit")]
        public IHttpActionResult companyedit( )
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var company_code = new TokenManager().GetcompanyCode(id);
            return Ok(new CompanyManager().get(Company_gid,company_code));
            
        }
        [Authorize]
        [HttpPost]
        [ActionName("companyupdate")]
        public IHttpActionResult companyupdate([FromBody] companydetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CompanyManager().Update(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Uploadcompanylogo")]
        public HttpResponseMessage Uploadcompanylogo()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            companymodel getcompanylogo = new companymodel();
            CompanyManager GetlogoFunctions = new CompanyManager();
            companydetails GetlogoImportExcel = new companydetails();
            httpRequest = HttpContext.Current.Request;
            getcompanylogo = GetlogoFunctions.getcompanylogoupload(Company_gid, httpRequest, usergid, companycode);
            return Request.CreateResponse(HttpStatusCode.OK, getcompanylogo);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Uploadwelcomelogo")]
        public HttpResponseMessage Uploadwelcomelogo()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            companymodel getcompanylogo = new companymodel();
            CompanyManager GetlogoFunctions = new CompanyManager();
            companydetails GetlogoImportExcel = new companydetails();
            httpRequest = HttpContext.Current.Request;
            getcompanylogo = GetlogoFunctions.getwelcomelogoupload(Company_gid, httpRequest, usergid, companycode);
            return Request.CreateResponse(HttpStatusCode.OK, getcompanylogo);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Uploadletterheadlogo")]
        public HttpResponseMessage Uploadletterheadlogo()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            companymodel getcompanylogo = new companymodel();
            CompanyManager GetlogoFunctions = new CompanyManager();
            companydetails GetlogoImportExcel = new companydetails();
            httpRequest = HttpContext.Current.Request;
            getcompanylogo = GetlogoFunctions.getletterheadupload(Company_gid, httpRequest, usergid, companycode);
            return Request.CreateResponse(HttpStatusCode.OK, getcompanylogo);
        }

    }
}