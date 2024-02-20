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
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("modulesummary")]
        public IHttpActionResult modulesummary()
        {
            return Ok(new MenuManager().modulesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("userprivilige")]
        public IHttpActionResult userprivilige(menumainlevel val)
        {
            return Ok(new MenuManager().userprivilige(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("assignprivilege")]
        public IHttpActionResult assignprivilege(menumainlevel val)
        {
            return Ok(new MenuManager().assignprivilege(val));
        }         
    }
}