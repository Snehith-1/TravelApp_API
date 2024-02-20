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
    [RoutePrefix("api/refund")]
    public class RefundController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("refundsummary")]
        public IHttpActionResult refundsummary()
        {
            return Ok(new RefundManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundreceiptsummary")]
        public IHttpActionResult refundreceiptsummary(Refund val)
        {

            return Ok(new RefundManager().refundreceiptsummary(val));
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundadvancesummary")]
        public IHttpActionResult refundadvancesummary()
        {

            return Ok(new RefundManager().refundadvancesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundedit")]
        public IHttpActionResult refundedit(Refundlist values)
        {
            return Ok(new RefundManager().Get(values.refund_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundadd")]
        public IHttpActionResult refundadd([FromBody] Refunddetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundManager().Add(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundupdate")]
        public IHttpActionResult refundupdate([FromBody] Refundlist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new RefundManager().Update(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refunddelete")]
        public IHttpActionResult refunddelete(Refundlist values)
        {
            return Ok(new RefundManager().Delete(values.refund_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesdetailrefund")]
        public IHttpActionResult salesdetailrefund(Refundlist values)
        {
            return Ok(new RefundManager().salesdetailrefund(values.salesorder_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("refundreferenceno")]
        public IHttpActionResult refundreferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new RefundManager().refundreferenceno(usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundreceiptdetails")]
        public IHttpActionResult refundreceiptdetails([FromBody] Refunddetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new RefundManager().refundreceiptdetails(values, usergid));
            //return Ok(new RefundManager().refundreceiptdetails(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundadvancedetails")]
        public IHttpActionResult refundadvancedetails([FromBody] refundadvancelist values)
        {
            return Ok(new RefundManager().refundadvancedetails(values.advance_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("refundreceipadvanceadd")]
        public IHttpActionResult refundreceipadvanceadd([FromBody] Refunddetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new RefundManager().refundreceipadvanceadd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("refundvendordetails")]
        public IHttpActionResult refundvendordetails(Refunddetails val)
        {
            return Ok(new RefundManager().refundvendordetails(val));
        }
    }
}