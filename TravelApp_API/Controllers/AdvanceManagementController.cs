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
    [RoutePrefix("api/advancemanagement")]
    public class AdvanceManagementController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("advancemanagementsummary")]
        public IHttpActionResult advancemanagementsummary()
        {
            return Ok(new AdvanceManagementManager().advancemanagementsummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("advancemanagementinvoicesummary")]
        public IHttpActionResult advancemanagementinvoicesummary(Advancecustomerdetails dtl)
        {
            return Ok(new AdvanceManagementManager().advancemanagementinvoicesummary(dtl));
        }

        [HttpPost]
        [Authorize]
        [ActionName("customerinvoiceselectlist")]
        public IHttpActionResult customerinvoiceselectlist(customerinvoicedetail val)
        {

            return Ok(new AdvanceManagementManager().customerinvoiceselectlist(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customerreceiptaddselect")]
        public IHttpActionResult customerreceiptaddselect(customerinvoicedetail values)
        {
            return Ok(new AdvanceManagementManager().customerreceiptaddselect(values));
        }
        
    }
}