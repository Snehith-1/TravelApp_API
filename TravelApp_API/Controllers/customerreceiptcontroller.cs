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
    [RoutePrefix("api/customerreceipt")]
    public class customerreceiptcontroller:ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("customerreceiptaddselect")]
        public IHttpActionResult customerreceiptaddselect(customerinvoicedetail values)
        {
            return Ok(new customerreceiptManager().customerreceiptaddselect(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("receiptoverallsubmit")]
        public IHttpActionResult receiptoverallsubmit([FromBody] customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerreceiptManager().receiptoverallsubmit(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("customerreceiptsummary")]
        public IHttpActionResult customerreceiptsummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new customerreceiptManager().Getall(companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerreceiptprint")]
        public IHttpActionResult customerreceiptprint([FromBody]customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerreceiptManager().customerreceiptprint(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("receiptreferenceno")]
        public IHttpActionResult receiptreferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new customerreceiptManager().receiptreferenceno(usergid));
        }

        [HttpPost]
        [Authorize]
        [ActionName("customerinvoiceselectlist")]
        public IHttpActionResult customerinvoiceselectlist(customerinvoicedetail val)
        {
            
            return Ok(new customerreceiptManager().customerinvoiceselectlist(val));
        }
        [Authorize]
        [HttpPost]
        [Route("customerreceiptdelete")]
        public IHttpActionResult customerreceiptdelete(customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerreceiptManager().customerreceiptdelete(val));
        }
    }
}