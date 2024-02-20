using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessEntities;
using BusinessLayer;

namespace TravelApp_API
{
   [RoutePrefix("api/Customeroutstandingreport")]
    public class CustomeroutstandingreportController:ApiController
    {
        [HttpPost]
        [Authorize]
        [ActionName("customeroutstandingreceipt")]
        public IHttpActionResult customeroutstandingreceipt (customeroutstaindingdetails val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            return Ok(new CustomeroutstandingreportManager().customeroutstandingreceipt(val));
        }

    }
}