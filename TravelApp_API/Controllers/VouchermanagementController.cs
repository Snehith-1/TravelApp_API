
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
    [RoutePrefix("api/voucher")]
    public class VoucherManagementController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("vouchersummary")]
        public IHttpActionResult vouchersummary()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new VoucherManager().vouchersummary(companycode));
            //return Ok(new VoucherManager().vouchersummary());
        }
        [Authorize]
        [HttpPost]
        [Route("voucheradd")]
        public IHttpActionResult voucheradd(Voucherdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VoucherManager().voucheradd(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [Route("deletevoucher")]
        public IHttpActionResult deletevoucher(Voucherdetails values)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new VoucherManager().Delete(values.voucher_gid));
        }

        //[Authorize]
        //[HttpPost]
        //[Route("voucheredit")]
        //public IHttpActionResult editvoucher(Partnerdetails values)
        //{
        //    IEnumerable<string> headervalues = Request.Headers.GetValues("Authorization");
        //    var id = headervalues.FirstOrDefault();
        //    var userGid = new TokenManager().GetuserID(id);
        //    return Ok(new VoucherManager().Get(values.partner_gid));

        //}
        //[Authorize]
        //[HttpPost]
        //[Route("voucherupdate")]
        //public IHttpActionResult updatevoucher(Partnerdetails values)
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var usergid = new TokenManager().GetuserID(id);
        //    return Ok(new VoucherManager().Update(values, usergid));

        //}


    }
}