using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("departmentsummary")]
        public IHttpActionResult departmentsummary()
        {
            return Ok(new DepartmentManager().GetAll());
        }
       

    }
}