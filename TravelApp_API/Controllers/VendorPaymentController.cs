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
    [RoutePrefix("api/vendorpayment")]
    public class VendorPaymentController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentoverallsubmit")]
        public IHttpActionResult vendorpaymentoverallsubmit()
        {
            return Ok(new VendorPaymentManager().vendorpaymentoverallsubmit());
        }
        //[Authorize]
        //[HttpPost]
        //[ActionName("vendorpaymentadd")]
        //public IHttpActionResult vendorpaymentadd(VendorPaymentdetails val)
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var userGid = new TokenManager().GetuserID(id);
        //    return Ok(new VendorPaymentManager().vendorpaymentadd(val,userGid));
        //}
        //public IHttpActionResult vendorpaymentsubmit(VendorPayment val)
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var userGid = new TokenManager().GetuserID(id);
        //    return Ok(new VendorPaymentManager().vendorpaymentsubmit(val, userGid));
        //}
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentselect")]
        public IHttpActionResult vendorpaymentselect(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorpaymentselect(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentselectsummary")]
        public IHttpActionResult vendorpaymentselectsummary(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorpaymentselectsummary(val, userGid));
        }
     
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentsubmit")]
        public IHttpActionResult vendorpaymentsubmit(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorpaymentsubmit(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentmain")]
        public IHttpActionResult vendorpaymentmain(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new VendorPaymentManager().vendorpaymentmain(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("paymentselect")]
        public IHttpActionResult paymentselect(VendorPaymentdetails val)
        {
            return Ok(new VendorPaymentManager().paymentselect(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesvendorpaymentselect")]
        public IHttpActionResult salesvendorpaymentselect(VendorPaymentdetails val)
        {
            return Ok(new VendorPaymentManager().salesvendorpaymentselect(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("paymentoverallsubmit")]
        public IHttpActionResult paymentoverallsubmit([FromBody] VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().paymentoverallsubmit(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesvendoroverallpayment")]
        public IHttpActionResult salesvendoroverallpayment([FromBody] VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().salesvendoroverallpayment(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorinvoicesummary")]
        public IHttpActionResult vendorinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new VendorPaymentManager().vendorinvoicesummary(companycode));
        }
        [HttpPost]
        [Authorize]
        [ActionName("paymentvendorinvoicesummary")]
        public IHttpActionResult paymentvendorinvoicesummary()
        {
            return Ok(new VendorPaymentManager().paymentvendorinvoicesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorinvoicedelete")]
        public IHttpActionResult vendorinvoicedelete(VendorPaymentdetails values)
        {
            return Ok(new VendorPaymentManager().vendorinvoicedelete(values.vendorinvoice_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentaddselect")]
        public IHttpActionResult vendorpaymentaddselect(VendorPaymentdetails values)
        {
            return Ok(new VendorPaymentManager().vendorpaymentaddselect(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("paymentvendoroverallsubmit")]
        public IHttpActionResult paymentvendoroverallsubmit(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().paymentvendoroverallsubmit(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorpaymentsummary")]
        public IHttpActionResult vendorpaymentsummary()
        {
            return Ok(new VendorPaymentManager().vendorpaymentsummary());
        }
        [HttpPost]
        [Authorize]
        [ActionName("paymentlist")]
        public IHttpActionResult paymentlist(VendorPaymentdetails val)
        {
            return Ok(new VendorPaymentManager().paymentlist(val.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorpaymentprint")]
        public IHttpActionResult vendorpaymentprint(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorpaymentprint(val.vendorpayment_gid, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("paymentreferenceno")]
        public IHttpActionResult paymentreferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().paymentreferenceno(usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorinvoicereferenceno")]
        public IHttpActionResult vendorinvoicereferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorinvoicereferenceno(usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorinvoiceview")]
        public IHttpActionResult vendorinvoiceview(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorinvoiceview(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesvendorinvoiceview")]
        public IHttpActionResult salesvendorinvoiceview(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().salesvendorinvoiceview(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("vendorstatementreport")]
        public IHttpActionResult vendorstatementreport(VendorPaymentdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VendorPaymentManager().vendorstatementreport(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendoroutstandingsummary")]
        public IHttpActionResult vendoroutstandingsummary(VendorPaymentdetails val)
        {
            return Ok(new VendorPaymentManager().vendoroutstandingsummary(val.salesorder_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorledgeroutstandingsummary")]
        public IHttpActionResult vendorledgeroutstandingsummary(VendorPaymentdetails val)
        {
            return Ok(new VendorPaymentManager().vendorledgeroutstandingsummary(val.vendor_gid));
        }
    }
}