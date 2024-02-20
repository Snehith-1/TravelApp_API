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
    [RoutePrefix("api/customerinvoice")]
    public class customerinvoicecontroller : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("customerinvoiceaddselect")]
        public IHttpActionResult customerinvoiceaddselect(customerinvoicedetail values)
        {
            return Ok(new customerinvoicemanager().customerinvoiceaddselect(values.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerinvoiceadd")]
        public IHttpActionResult customerinvoiceadd([FromBody] customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().Add(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("customerinvoicesummary")]
        public IHttpActionResult customerinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().Getall(companycode,userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicesummarysearch")]
        public IHttpActionResult salesinvoicesummarysearch(customerinvoicedetail values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().salesinvoicesummarysearch(values, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicesummarysearchfunction")]
        public IHttpActionResult salesinvoicesummarysearchfunction(customerinvoicedetail values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().salesinvoicesummarysearchfunction(values, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("showallinvoicesummary")]
        public IHttpActionResult showallinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().showallinvoicesummary(companycode, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("customerstatement")]
        public IHttpActionResult customerstatement()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new customerinvoicemanager().customerstatement(companycode));
        }
        [HttpPost]
        [Authorize]
        [ActionName("receiptcustomerinvoicesummary")]
        public IHttpActionResult receiptcustomerinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new customerinvoicemanager().receiptcustomerinvoicesummary(companycode));
        }
        [HttpPost]
        [Authorize]
        [ActionName("outstandingcustomerinvoicesummary")]
        public IHttpActionResult outstandingcustomerinvoicesummary(outstandingCustomerInvoiceList values)
        {
            return Ok(new customerinvoicemanager().outstandingcustomerinvoicesummary(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerinvoicedelete")]
        public IHttpActionResult customerinvoicedelete(customerinvoicelist values)
        {
            return Ok(new customerinvoicemanager().Delete(values.customerinvoice_gid));
        }
        //[Authorize]
        //[HttpPost]
        //[ActionName("customerinvoiceedit")]
        //public IHttpActionResult customerinvoiceedit(customerinvoicedetail values)
        //{
        //    return Ok(new customerinvoicemanager().edit(values.invoicegid));
        //}
        [HttpPost]
        [Authorize]
        [ActionName("CustomerReport")]
        public IHttpActionResult CustomerReport()
        {
            return Ok(new customerinvoicemanager().CustomerReport());
        }
       
        [HttpPost]
        [Authorize]
        [ActionName("CustomerLedgerReport")]
        public IHttpActionResult CustomerLedgerReport(customerinvoicedetail val)
        {
            return Ok(new customerinvoicemanager().CustomerLedgerReport(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("Customerstatementreport")]
        public IHttpActionResult Customerstatementreport(customerinvoicedetail val)
        {
            return Ok(new customerinvoicemanager().Customerstatementreport(val));
        }
      
        [HttpPost]
        [Authorize]
        [ActionName("customerinvoiceprint")]
        public IHttpActionResult customerinvoiceprint(customerinvoicedetail val)
        {
            return Ok(new customerinvoicemanager().customerinvoiceprint(val.invoice_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("ivnvoicereferenceno")]
        public IHttpActionResult ivnvoicereferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().ivnvoicereferenceno(usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("airfileinvoiceedit")]
        public IHttpActionResult airfileinvoiceedit(customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            //var usergid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().airfileinvoiceedit(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("airfileinvoiceupdate")]
        public IHttpActionResult airfileinvoiceupdate([FromBody] Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new customerinvoicemanager().airfileinvoiceupdate(val, userGid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("ivnvoicereferencenocancel")]
        public IHttpActionResult ivnvoicereferencenocancel(customerinvoicedetail val)
        {            
            return Ok(new customerinvoicemanager().ivnvoicereferencenocancel(val.reference_code));
        }
        [HttpPost]
        [Authorize]
        [ActionName("customertransctions")]
        public IHttpActionResult customertransctions(customertransactiondetails val )
        {
         return Ok(new customerinvoicemanager().customertransctions(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorpaymentpendinginvoicesummary")]
        public IHttpActionResult vendorpaymentpendinginvoicesummary()
        {
            return Ok(new customerinvoicemanager().vendorpaymentpendinginvoicesummary());
        }
        [HttpPost]
        [Authorize]
        [ActionName("customerLedgerSummary")]
        public IHttpActionResult customerLedgerSummary()
        {
            return Ok(new customerinvoicemanager().customerLedgerSummary());
        }

        [HttpPost]
        [Authorize]
        [ActionName("newCustomerLedgerReport")]
        public IHttpActionResult newCustomerLedgerReport(customerinvoicedetail val)
        {
            return Ok(new customerinvoicemanager().newCustomerLedgerReport(val));
        }
    }
}