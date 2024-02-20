using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;

namespace TravelApp_API.Controllers
{
  [RoutePrefix ("api/billingnotes")]
    public class billingnotescontroller : ApiController 
    {
        [Authorize]
        [HttpPost]
        [ActionName("billingadd")]
        public IHttpActionResult billingadd(Billingdetail values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().billingadd(values, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("paymentadd")]
        public IHttpActionResult paymentadd(Billingdetail values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().paymentadd(values, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("billinggets")]
        public IHttpActionResult billingget(Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().billingGet(val.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("billingsummary")]
        public IHttpActionResult billingsummary(Billingdetail val)
        {            
            return Ok(new BillingManager().billingGetall(val.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("billingdelete")]
        public IHttpActionResult billingdelete(int val)
        {            
            return Ok(new BillingManager().billingdelete(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentactivitylist")]
        public IHttpActionResult vendorpaymentactivitylist(billinglist val)
        {
            return Ok(new BillingManager().activitylist(val.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("billingselect")]
        public IHttpActionResult billingselect(Billingdetail val)
        {
            return Ok(new BillingManager().billingselect(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("billingoverallsubmit")]
        public IHttpActionResult billingoverallsubmit([FromBody] Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().billingoverallsubmit(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("billingoverallsubmitairfile")]
        public IHttpActionResult billingoverallsubmitairfile([FromBody] Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
           
            return Ok(new BillingManager().billingoverallsubmitairfile(val, userGid));
        }

       


        [Authorize]
        [HttpPost]
        [ActionName("billingoverallsubmitairfileinvoice")]
        public IHttpActionResult billingoverallsubmitairfileinvoice([FromBody] Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().billingoverallsubmitairfileinvoice(val, userGid));
        }
      
      



        [Authorize]
        [HttpPost]
        [ActionName("opfbillingdelete")]
        public IHttpActionResult opfbillingdelete(billinglist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().opfbillingdelete(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("opfpaymentdelete")]
        public IHttpActionResult opfpaymentdelete(VendorPaymentlist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BillingManager().opfpaymentdelete(val, usergid));
        }
    }
}