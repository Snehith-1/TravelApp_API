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
    [RoutePrefix("api/RefundInvoice")]
    public class RefundInvoiceController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("Refundsummary")]
        public IHttpActionResult Refundsummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new RefundInvoiceManager().GetAll(companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundaddselect")]
        public IHttpActionResult refundaddselect(customerinvoicedetail values)
        {
            return Ok(new RefundInvoiceManager().refundaddselect(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundsummarydetails")]
        public IHttpActionResult refundsummarydetails(customerinvoicedetail values)
        {
            return Ok(new RefundInvoiceManager().refundsummarydetails(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundoverallsubmit")]
        public IHttpActionResult refundoverallsubmit([FromBody] customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundInvoiceManager().refundoverallsubmit(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundServiceType")]
        public IHttpActionResult refundServiceType([FromBody] MdlRefundServiceType val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundInvoiceManager().refundServiceType(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundservicetypeupdate")]
        public IHttpActionResult refundservicetypeupdate([FromBody] MdlRefundServiceType val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundInvoiceManager().refundservicetypeupdate(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("refundCustomerInvoiceSummary")]
        public IHttpActionResult refundcustomerinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new RefundInvoiceManager().getRefundCustomerInvoiceSummary(companycode));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundview")]
        public IHttpActionResult refundview([FromBody] MdlRefundServiceType val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundInvoiceManager().refundview(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("editrefund")]
        public IHttpActionResult editrefund([FromBody] MdlRefundServiceType val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundInvoiceManager().editrefund(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerRefundLedgerReport")]
        public IHttpActionResult customerRefundLedgerReport(refundledgerdetails val)
        {
            return Ok(new RefundInvoiceManager().customerRefundLedgerReport(val));
        }
    }
}