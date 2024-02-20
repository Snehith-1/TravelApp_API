
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
    [RoutePrefix("api/enquiry")]
    public class EnquiryController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("enquirysummary")]
        public IHttpActionResult Post()
        {
            return Ok(new EnquiryManager().GetAll());
        }
        

        [Authorize]
        [HttpPost]
        [ActionName("servicedetails")]
        public IHttpActionResult show()
        {
            return Ok(new EnquiryManager().show());
        }


        [Authorize]
        [HttpPost]
        [ActionName("enquiryadd")]
        public IHttpActionResult Add(Enquirydetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().Add(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("enquirydelete")]
        public IHttpActionResult Delete(Enquirydelete values)
        {
            return Ok(new EnquiryManager().Delete(values.enquiry_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquirylog")]
        public IHttpActionResult Log([FromBody] Enquirydetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().Log(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquiryedit")]
        public IHttpActionResult Edit(Enquirydetails values)
        {
            return Ok(new EnquiryManager().Edit(values.enquiry_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquiryupdate")]
        public IHttpActionResult Put([FromBody] Enquirydetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().Update(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquiryquotationadd")]
        public IHttpActionResult quatationadd(quotationdetail quotationdetail)
        {
            return Ok(new EnquiryManager().quatationadd(quotationdetail));
        }

        [Authorize]
        [HttpPost]
        [ActionName("enquirylogedit")]
        public IHttpActionResult Logedit([FromBody] Enquirydetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().Log(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquirylogsummary")]
        public IHttpActionResult enquirylogsummary(Enquirydetails values)
        {
            return Ok(new EnquiryManager().LogAll(values));
        }

        [Authorize]
        [HttpPost]
        [ActionName("quotationaddall")]
        public IHttpActionResult quatationaddall(quotationdetail val)

        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().quatationaddall(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("quotationaddbind")]
        public IHttpActionResult quotationaddbind(Enquirydetails val)
        {
            return Ok(new EnquiryManager().quotationaddbind(val.enquiry_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("enquirylogdelete")]
        public IHttpActionResult enquirylogdelete(Enquirydetails values)
        {
            return Ok(new EnquiryManager().enquirylogdelete(values));
        }


        [Authorize]
        [HttpPost]
        [ActionName("unitlist")]
        public IHttpActionResult unit()
        {
            return Ok(new EnquiryManager().unit());
        }

        [HttpPost]
        [Authorize]
        [ActionName("enquiryreferenceno")]
        public IHttpActionResult enquiryreferenceno()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new EnquiryManager().enquiryreferenceno(usergid));
        }
    }
}