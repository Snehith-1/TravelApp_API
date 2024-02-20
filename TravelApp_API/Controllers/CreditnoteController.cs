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
    [RoutePrefix("api/creditnote")]
    public class CreditnoteController : ApiController
    { 
        [Authorize]
        [HttpPost]
        [ActionName("creditnotesubmit")]
        public IHttpActionResult creditnotesubmit(creditnotereceiptlist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CreditnoteManager().creditnotesubmit(val, userGid));
        }

        [HttpPost]
        [Authorize]
        [ActionName("creditnoteinvoiceselectlist")]
        public IHttpActionResult creditnoteinvoiceselectlist(creditnotereceiptlist val)
        {

            return Ok(new CreditnoteManager().creditnoteinvoiceselectlist(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("creditnotedetails")]
        public IHttpActionResult creditnotedetails([FromBody] creditnotereceiptlist values)
        {
            return Ok(new CreditnoteManager().creditnotedetails(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("paymentvendoroverallsubmit")]
        public IHttpActionResult paymentvendoroverallsubmit([FromBody] VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CreditnoteManager().paymentvendoroverallsubmit(val, userGid));
        }
        

        [Authorize]
        [HttpPost]
        [ActionName("creditnotesummary")]
        public IHttpActionResult creditnotesummary()
        {
            return Ok(new CreditnoteManager().creditnotesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("debinotesummary")]
        public IHttpActionResult debinotesummary()
        {
            return Ok(new CreditnoteManager().debitnotesummary());
        }

    }
}