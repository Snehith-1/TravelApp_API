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
    [RoutePrefix("api/Quotation")]
    public class QuotationController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("quotationsummary")]
        public IHttpActionResult quotationsummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new QuotationManager().GetAll(companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationadd")]
        public IHttpActionResult quotationadd( Quotationdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().Add(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationdetail")]
        public IHttpActionResult quotationdetail(Quotationdetail values)
        {
            return Ok(new QuotationManager().Get(values.enquiry_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationedit")]
        public IHttpActionResult quotationedit(Quotationdetail values)
        {
            return Ok(new QuotationManager().Edit(values.quotation_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationupdate")]
        public IHttpActionResult quotationupdate([FromBody] Quotationdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().Update(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationcancel")]
        public IHttpActionResult quotationcancel([FromBody] QuotationList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().Cancel(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("directquotationadd")]
        public IHttpActionResult directquotationadd(Quotationdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().directquotationadd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("directquotationedit")]
        public IHttpActionResult directquotationedit(Quotationdetail values)
        {
            return Ok(new QuotationManager().directquotationedit(values.quotation_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("directquotationupdate")]
        public IHttpActionResult directquotationupdate([FromBody] Quotationdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().directquotationupdate(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationtosalesorder")]
        public IHttpActionResult quotationtosalesorder(Quotationdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().quotationtosalesorder(val.quotation_gid, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("quotationreferenceno")]
        public IHttpActionResult quotationreferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new QuotationManager().quotationreferenceno(usergid));
        }
    }
}