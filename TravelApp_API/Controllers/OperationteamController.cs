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
    [RoutePrefix("api/operationteam")]
    public class OperationteamController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("operationteamsummary")]
        public IHttpActionResult operationteamsummary()
        {
            return Ok(new OperationteamManager().GetAll());
        }
        [Authorize]
        [HttpPost]
        [ActionName("operationteamadd")]
        public IHttpActionResult operationteamadd([FromBody] Operationteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new OperationteamManager().Add(val, userGid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("operationteamdelete")]
        public IHttpActionResult operationteamdelete(Operationteamlist values)
        {
            return Ok(new OperationteamManager().Delete(values.operationteam_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("operationteamedit")]
        public IHttpActionResult operationteamedit(Operationteamdetail values)
        {
            return Ok(new OperationteamManager().Get(values.operationteam_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("operationteamupdate")]
        public IHttpActionResult operationteamupdate([FromBody] Operationteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OperationteamManager().Update(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("optteamemployee")]
        public IHttpActionResult optteamemployee(Operationteamlist values)
        {
            return Ok(new OperationteamManager().optteamemployee(values.operationteam_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("optteammanager")]
        public IHttpActionResult optteammanager(Operationteamlist values)
        {
            return Ok(new OperationteamManager().optteammanager(values.operationteam_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("asignemployeesubmit")]
        public IHttpActionResult asignemployeesubmit([FromBody] Operationteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OperationteamManager().asignemployeesubmit(val, usergid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("asignmanagersubmit")]
        public IHttpActionResult asignmanagersubmit([FromBody] Operationteamdetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OperationteamManager().asignmanagersubmit(val, usergid));
        }
    }
}