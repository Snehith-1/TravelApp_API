using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities;
using System.Web;
using System.IO;


namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("employeesummary")]
        public IHttpActionResult employeesummary()
        {
            return Ok(new EmployeeManager().employeesummary());
        }
        [HttpPost]
        [Authorize]
        [ActionName("employeecode")]
        public IHttpActionResult employeecode()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new EmployeeManager().employeecode(usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("employeeprofile")]
        public IHttpActionResult employeeprofile()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new EmployeeManager().employeeprofile(user_gid));

        }
  [Authorize]
        [HttpPost]
        [ActionName("employeeedit")]
        public IHttpActionResult employeeedit(Employeedetail values)
        {
            return Ok(new EmployeeManager().employeeedit(values.user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("employeeadd")]
        public HttpResponseMessage employeeadd()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Employeemodel getdocument = new Employeemodel();
            EmployeeManager GetdocumentFunctions = new EmployeeManager();
            Employeedetail Getdocument = new Employeedetail();
            httpRequest = HttpContext.Current.Request;
            getdocument = GetdocumentFunctions.Getdocument(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocument);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Documentpath")]
        public IHttpActionResult EmployeeDocumentpath(Employeedetail values)
        {
            return Ok(new EmployeeManager().EmployeeDocumentpath(values));
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("PreviewDownload")]
        public HttpResponseMessage PreviewDownload(string document_path)
        {
            HttpResponseMessage HttpResponse = new HttpResponseMessage();
            System.IO.FileInfo FileName = new System.IO.FileInfo(document_path);
            FileStream myFile = new FileStream(document_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader _BinaryReader = new BinaryReader(myFile);
            long startBytes = 0;
            string lastUpdateTiemStamp = File.GetLastWriteTimeUtc(document_path).ToString("r");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = false;
            HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
            HttpContext.Current.Response.AppendHeader("Last-Modified", lastUpdateTiemStamp);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + "document" + FileName.Extension);
            HttpContext.Current.Response.AddHeader("Content-Length", (FileName.Length - startBytes).ToString());
            HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
            _BinaryReader.BaseStream.Seek(startBytes, SeekOrigin.Begin);
            int maxCount = (int)Math.Ceiling((FileName.Length - startBytes + 0.0) / 1024);
            int i;
            for (i = 0; i < maxCount && HttpContext.Current.Response.IsClientConnected; i++)
            {
                HttpContext.Current.Response.BinaryWrite(_BinaryReader.ReadBytes(1024));
                HttpContext.Current.Response.Flush();
            }
            return HttpResponse;
        }

        [Authorize]
        [HttpPost]
        [ActionName("employeeupdate")]
        public HttpResponseMessage employeeupdate()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Employeemodel getdocument = new Employeemodel();
            EmployeeManager GetdocumentFunctions = new EmployeeManager();
            Employeedetail Getdocument = new Employeedetail();
            httpRequest = HttpContext.Current.Request;
            getdocument = GetdocumentFunctions.employeeupdate(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocument);
        }
                
        [Authorize]
        [HttpPost]
        [ActionName("employeestatus")]
        public IHttpActionResult employeestatus([FromBody] Employeelist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new EmployeeManager().employeestatus(val, usergid));
        }


        [Authorize]
        [HttpPost]
        [ActionName("employeepswreset")]
        public IHttpActionResult employeepswreset([FromBody] Employeedetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new EmployeeManager().employeepswreset(val, usergid));    
        }
    }
}