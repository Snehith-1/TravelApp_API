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
    [RoutePrefix("api/currency")]
    public class CurrencyController : ApiController
    {
        // GET api/<controller>
        [Authorize]
        [HttpPost]
        [ActionName("currencysummary")]
        public IHttpActionResult currencysummary()
        {
            return Ok(new CurrencyManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("currencymastersummary")]
        public IHttpActionResult currencymastersummary()
        {
            return Ok(new CurrencyManager().currencymastersummary());
        }

        [Authorize]
        [HttpPost]
        [ActionName("currencyadd")]
        public IHttpActionResult currencyadd([FromBody] Currencydetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new CurrencyManager().Add(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("currencyedit")]       
         public IHttpActionResult currencyedit([FromBody] CurrencyList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CurrencyManager().currencyedit(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("currencystatus")]
        public IHttpActionResult currencystatus([FromBody] CurrencyList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CurrencyManager().Update(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("currencyupdate")]
        public IHttpActionResult currencyupdate([FromBody] CurrencyList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new CurrencyManager().currencyupdate(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("getcurrency")]
        public IHttpActionResult getcurrency(Currencydetail val)
        {
            return Ok(new CurrencyManager().getcurrency(val.currency_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("countrysummary")]
        public IHttpActionResult countrysummary()
        {
            return Ok(new CurrencyManager().countrysummary());
        }



    }
}