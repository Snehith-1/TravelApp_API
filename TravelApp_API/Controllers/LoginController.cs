using BusinessEntities;
using BusinessLayer;
using System.Web.Http;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Collections.Generic;


namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]        
        [ActionName("loginvalidate")]
        public IHttpActionResult loginvalidate([FromBody]Login login)
        {            
            return Ok(new LoginManager().Add(login)); 
                       
        }
    }
}