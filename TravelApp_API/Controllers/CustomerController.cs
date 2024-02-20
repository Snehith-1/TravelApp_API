using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;
using System.Web;
using DataAccess;



namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("customersummary")]

        public IHttpActionResult customersummary()
        {
            return Ok(new CustomerManger().GetAll());
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerstatementsummary")]
        public IHttpActionResult customerstatementsummary([FromBody] Customerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CustomerManger().customerstatementsummary(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerstatementsummarysearch")]
        public IHttpActionResult customerstatementsummarysearch([FromBody] Customerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CustomerManger().customerstatementsummarysearch(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customeradd")]
        public IHttpActionResult customeradd([FromBody] Customerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CustomerManger().Add(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerstatus")]
        public IHttpActionResult customerstatus([FromBody] Customerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CustomerManger().Status(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customeredit")]
        public IHttpActionResult customeredit(Customerdetails values)
        {
            return Ok(new CustomerManger().Get(values.customer_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerupdate")]
        public IHttpActionResult customerupdate([FromBody] Customerdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CustomerManger().Update(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("ExcelUploadIDFCFirstDoDebitDetails")]
        public IHttpActionResult ExcelUploadIDFCFirstDoDebitDetails()
        {

            HttpRequest httpreq;
            httpreq = HttpContext.Current.Request;
            //HttpRequest httpreq=new HttpRequest();
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var company_code = new TokenManager().GetcompanyCode(id);
            Customerdetails val = new Customerdetails();
            return Ok(new CustomerManger().Excel(company_code,httpreq, val, userGid));
            //return Request.CreateResponse(HttpStatusCode.OK, Excel);
        }

    }
}