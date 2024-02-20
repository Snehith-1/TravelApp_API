using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities;


namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/incomeandexpense")]
      public class IncomeandexpenseController:ApiController
    {
       [Authorize]
       [HttpPost]
       [ActionName("summary")]
       public IHttpActionResult incomeandexpensesummary(IEdetails val)
        {
            return Ok(new Incomeandexpensemanager().incomeandexpensesummary(val));
        }

        [Authorize]
        [HttpPost]
        [ActionName("bssummary")]
        public IHttpActionResult balancesheetsummary(IEdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var Company_gid = new TokenManager().GetcompanyID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            return Ok(new Incomeandexpensemanager().balancesheetsummary(val, companycode));
        }
    }
}