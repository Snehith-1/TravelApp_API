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
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("vendorsummary")]
        public IHttpActionResult vendorsummary()
        {
            return Ok(new VendorManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("activevendorsummary")]
        public IHttpActionResult activevendorsummary()
        {
            return Ok(new VendorManager().activevendorsummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendoradd")]
        public IHttpActionResult vendoradd([FromBody] Vendordetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().Add(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorcode")]
        public IHttpActionResult vendorcode()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().vendorcode(usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("vendordelete")]
        public IHttpActionResult vendordelete(Vendordelete values)
        {
            return Ok(new VendorManager().Delete(values.vendor_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendoredit")]
        public IHttpActionResult vendoredit(Vendordetail values)
        {
            return Ok(new VendorManager().Get(values.vendor_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorupdate")]
        public IHttpActionResult vendorupdate([FromBody] Vendordetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().Update(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorstatus")]
        public IHttpActionResult vendorstatus([FromBody] Vendordetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().Status(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("paymentvendorsummary")]
        public IHttpActionResult paymentvendorsummary()
        {
            return Ok(new VendorManager().paymentvendorsummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("ticketvendor")]
        public IHttpActionResult ticketvendor()
        {
            return Ok(new VendorManager().ticketvendor());
        }

        [Authorize]
        [HttpPost]
        [ActionName("vendoradvanceadd")]
        public IHttpActionResult vendoradvanceadd([FromBody] Vendordetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().vendoradvanceadd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("submitvendorbudget")]
        public IHttpActionResult submitvendorbudget([FromBody] Vendordetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().submitvendorbudget(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorbudgetsummary")]
        public IHttpActionResult vendorbudgetsummary([FromBody] vendorBudget val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().vendorbudgetsummary(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("monthlypaymentsummary")]
        public IHttpActionResult monthlypaymentsummary([FromBody] vendorBudget val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().monthlypaymentsummary(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorbudgetsummarysearch")]
        public IHttpActionResult vendorbudgetsummarysearch([FromBody] vendorBudget val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorManager().vendorbudgetsummarysearch(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendoradvancesummary")]
        public IHttpActionResult vendoradvancesummary(Vendordetail val)
        {
            return Ok(new VendorManager().vendoradvancesummary(val.vendor_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("vendorledgersummary")]
        public IHttpActionResult vendorledgersummary()
        {
            return Ok(new VendorManager().vendorledgersummary());
        }
        
        [Authorize]
        [HttpPost]
        [ActionName("vendorledgerchildreport")]
        public IHttpActionResult vendorledgerchildreport(Vendordetail val)
        {
            return Ok(new VendorManager().vendorledgerchildreport(val));
        }
        //[Authorize]
        //[HttpPost]
        //[ActionName("monthlyvendorbudgetsummary")]
        //public IHttpActionResult monthlyvendorbudgetsummary([FromBody] vendorMonthlyBudget val)
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var userGid = new TokenManager().GetuserID(id);
        //    return Ok(new VendorManager().monthlyvendorbudgetsummary(val));
        //}
    }
}