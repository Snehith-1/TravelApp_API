using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using BusinessEntities;
using DataAccess;
using System.Web;
using System.IO;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/reconciliation")]
    public class ReconciliationController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("reconciliationsummary")]
        public IHttpActionResult reconciliationsummary()
        {
            return Ok(new ReconciliationManager().GetAll());
        }
        [Authorize]
        [HttpPost]
        [ActionName("reconciliation")]
        public HttpResponseMessage reconciliation()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Reconciliation getdocumentimportexcel = new Reconciliation();
            ReconciliationManager GetdocumentFunctions = new ReconciliationManager();
            reconciliationDetail GetdocumentImportExcel = new reconciliationDetail();
            httpRequest = HttpContext.Current.Request;
            getdocumentimportexcel = GetdocumentFunctions.GetdocumentUploadExcel(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocumentimportexcel);
        }
        [Authorize]
        [HttpPost]
        [ActionName("reconciliationcount")]
        public IHttpActionResult reconciliationcount(reconciliationcountlist values)
        {
            return Ok(new ReconciliationManager().reconciliationcount(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("reconciliationpath")]
        public IHttpActionResult reconciliationpath(reconciliationlist value)
        {
            return Ok(new ReconciliationManager().reconciliationpath(value));
        }
        [Authorize]
        [HttpPost]
        [ActionName("reconciliationmatchingcount")]
        public IHttpActionResult reconciliationmatchingcount(matchingcountlist val)
        {
            return Ok(new ReconciliationManager().reconciliationmatchingcount(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("reconciliationmatchingagentcount")]
        public IHttpActionResult reconciliationmatchingagentcount(matchingwithagentcount val)
        {
            return Ok(new ReconciliationManager().reconciliationmatchingagentcount(val));
        }
        [AllowAnonymous]
        [HttpGet]
        [ActionName("reconciliationdownload")]
        public HttpResponseMessage reconciliationdownload(string document_path)
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
    }
}