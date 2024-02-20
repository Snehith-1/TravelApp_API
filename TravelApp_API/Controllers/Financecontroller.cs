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
    [RoutePrefix("api/finance")]
    public class Financecontroller : ApiController
    {
            [Authorize]
            [HttpPost]
            [ActionName("financemasteradd")]
            public IHttpActionResult financemasteradd(Financedetail val)
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
                var id = headerValues.FirstOrDefault();
                var userGid = new TokenManager().GetuserID(id);                
                return Ok(new FinanceManager().financemasteradd(val,userGid));
            }

            [Authorize]
            [HttpPost]
            [ActionName("financemasterupdate")]
            public IHttpActionResult financemasterupdate(Financedetail val)
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
                var id = headerValues.FirstOrDefault();
                var userGid = new TokenManager().GetuserID(id);
                return Ok(new FinanceManager().financemasterupdate(val, userGid));
            }

            [Authorize]
            [HttpPost]
            [ActionName("financeinvoice")]
            public IHttpActionResult financeinvoice(Financedetail val)
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
                var id = headerValues.FirstOrDefault();
                var userGid = new TokenManager().GetuserID(id);
                return Ok(new FinanceManager().financeinvoice(val, userGid));
            }
            [Authorize]
            [HttpPost]
            [ActionName("financepayment")]
            public IHttpActionResult financepayment(Financedetail val)
            {
                IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
                var id = headerValues.FirstOrDefault();
                var userGid = new TokenManager().GetuserID(id);
                return Ok(new FinanceManager().financepayment(val, userGid));
            }
     

        [Authorize]
        [HttpPost]
        [ActionName("finaceadvance")]
        public IHttpActionResult financeadvance(Financedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new FinanceManager().financeadvance(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("financerefund")]
        public IHttpActionResult financerefund(Financedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new FinanceManager().financerefund(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("invoicedelete")]
        public IHttpActionResult invoicedelete(Financedetail val)
        {
            return Ok(new FinanceManager().invoicedelete(val.transaction_gid));

        }

        [Authorize]
        [HttpPost]
        [ActionName("financenewrefund")]
        public IHttpActionResult financenewrefund(Financerefunddetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new FinanceManager().financenewrefund(val, userGid));
        }
    }
}