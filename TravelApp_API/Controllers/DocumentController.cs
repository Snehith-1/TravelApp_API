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
    [RoutePrefix("api/Document")]
    public class DocumentController : ApiController
    {
        [Authorize]
        [HttpPost]
        [ActionName("Documentsummary")]
        public IHttpActionResult Documentsummary()
        {
            return Ok(new DocumentManager().GetAll());
        }

        [Authorize]
        [HttpPost]
        [ActionName("UploadDocument")]
        public HttpResponseMessage UploadDocument()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);                        
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Documentmodel getdocumentimportexcel = new Documentmodel();
            DocumentManager GetdocumentFunctions = new DocumentManager();
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
            httpRequest = HttpContext.Current.Request;
            getdocumentimportexcel = GetdocumentFunctions.GetdocumentUploadExcel(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocumentimportexcel);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Reminder")]
        public IHttpActionResult Reminder(DocumentDetail values)
        {
            return Ok(new DocumentManager().Reminder(values.document_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("Reminderadd")]
        public IHttpActionResult Reminderadd([FromBody] DocumentDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var userGid = new TokenManager().GetuserID(id);
            return Ok(new DocumentManager().Add(val, userGid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("Documentpath")]
        public IHttpActionResult Documentpath(DocumentDetail values)
        {
            return Ok(new DocumentManager().Documentpath(values));
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
        [ActionName("documentedit")]
        public IHttpActionResult documentedit(DocumentDetail values)
        {
            return Ok(new DocumentManager().edit(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("documentupdate")]
        public HttpResponseMessage documentupdate()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Documentmodel getdocumentimportexcel = new Documentmodel();
            DocumentManager GetdocumentFunctions = new DocumentManager();
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
            httpRequest = HttpContext.Current.Request;
            getdocumentimportexcel = GetdocumentFunctions.documentupdate(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocumentimportexcel);
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesUploadDocument")]
        public HttpResponseMessage salesUploadDocument()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            var companycode = new TokenManager().GetcompanyCode(id);
            HttpRequest httpRequest;
            Documentmodel getdocumentimportexcel = new Documentmodel();
            DocumentManager GetdocumentFunctions = new DocumentManager();
            DocumentDetail GetdocumentImportExcel = new DocumentDetail();
            httpRequest = HttpContext.Current.Request;
            getdocumentimportexcel = GetdocumentFunctions.salesUploadDocument(companycode, httpRequest, usergid);
            return Request.CreateResponse(HttpStatusCode.OK, getdocumentimportexcel);
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesdocumentsummary")]
        public IHttpActionResult salesdocumentsummary(DocumentDetail val)
        {
            return Ok(new DocumentManager().salesdocumentsummary(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesdocumentdelete")]
        public IHttpActionResult salesdocumentdelete(DocumentDetail values)
        {
            return Ok(new DocumentManager().salesdocumentdelete(values.document_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesDocumentpath")]
        public IHttpActionResult salesDocumentpath(DocumentDetail values)
        {
            return Ok(new DocumentManager().salesDocumentpath(values));
        }
    }
}