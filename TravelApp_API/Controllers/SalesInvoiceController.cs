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
    [RoutePrefix("api/salesinvoice")]
    public class salesinvoicecontroller : ApiController
    {
        [HttpPost]
        [Authorize]
        [ActionName("salesinvoicesummary")]
        public IHttpActionResult salesinvoicesummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new SalesInvoiceManager().Getall(companycode));
        }

        [HttpPost]
        [Authorize]
        [ActionName("salesinvoiceservicetypelist")]
        public IHttpActionResult salesinvoiceservicetypelist(SalesInvoice values)
        {
            
            return Ok(new SalesInvoiceManager().salesinvoiceservicetypelist(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("SalesInvoiceoverallsubmitairfile")]
        public IHttpActionResult SalesInvoiceoverallsubmitairfile([FromBody] Billingdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new SalesInvoiceManager().SalesInvoiceoverallsubmitairfile(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceedit")]
        public IHttpActionResult salesinvoiceedit(customerinvoicedetail values)
        {
            return Ok(new SalesInvoiceManager().salesinvoiceedit(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("servicetypescount")]
        public IHttpActionResult servicetypescount(customerinvoicedetail values)
        {
            return Ok(new SalesInvoiceManager().servicetypescount(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicedetails")]
        public IHttpActionResult salesinvoicedetails(customerinvoicedetail values)
        {
            return Ok(new SalesInvoiceManager().salesinvoicedetails(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceupdate")]
        public IHttpActionResult salesinvoiceupdate([FromBody] customerinvoicedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new SalesInvoiceManager().Update(val, usergid));
        }
       
    }
}




