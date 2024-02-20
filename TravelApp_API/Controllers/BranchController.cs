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
    [RoutePrefix("api/branch")]
    public class BranchController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("branchsummary")]
        public IHttpActionResult branchsummary()
        {
            return Ok(new BranchManager().GetAll());
        }
        [HttpPost]
        [Authorize]
        [ActionName("branchcode")]
        public IHttpActionResult branchcode(Branchdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BranchManager().branchcode(val,usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("branchadd")]
        public IHttpActionResult branchadd([FromBody] Branchdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new  BranchManager().branchadd(val,userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("branchedit")]
        public IHttpActionResult branchedit([FromBody] Branchdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BranchManager().branchedit(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("branchupdate")]
        public IHttpActionResult branchupdate([FromBody] Branchdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new BranchManager().branchupdate(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [Route("deletebranch")]
        public IHttpActionResult deletebranch(Branchdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new BranchManager().Delete(val.branch_gid));
        }

    }
}