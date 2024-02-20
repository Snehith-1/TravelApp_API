using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using BusinessEntities;
using BusinessLayer;

namespace TravelApp_API.Controllers
{
    [RoutePrefix("api/salesorderform")]
    public class SalesOrderFormController : ApiController
    {
        [HttpPost]
        [Authorize]
        [ActionName("sofcustomer")]
        public IHttpActionResult sofcustomer()
        {
            return Ok(new SalesOrderFormManager().Customer());
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofcurrency")]
        public IHttpActionResult sofcurrency()
        {
            return Ok(new SalesOrderFormManager().Currency());
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofactivity")]
        public IHttpActionResult sofactivity(SOActivityList val)
        {
            return Ok(new SalesOrderFormManager().Activity(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofpassenger")]
        public IHttpActionResult sofpassenger(SOPassengerList val)
        {
            return Ok(new SalesOrderFormManager().Passenger(val));
        }

        [HttpPost]
        [Authorize]
        [ActionName("editpassenger")]
        public IHttpActionResult editpassenger(SOPassengerList val)
        {
            return Ok(new SalesOrderFormManager().EditPassenger(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editpassport")]
        public IHttpActionResult editpassport(SOPassportList val)
        {
            return Ok(new SalesOrderFormManager().EditPassport(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorpassport")]
        public IHttpActionResult vendorpassport(SOPassportList val)
        {
            return Ok(new SalesOrderFormManager().vendorpassport(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editvisa")]
        public IHttpActionResult editvisa(SOVisaList val)
        {
            return Ok(new SalesOrderFormManager().Editvisa(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editflight")]
        public IHttpActionResult editflight(SOFlightList val)
        {
            return Ok(new SalesOrderFormManager().Editflight(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editairinvoice")]
        public IHttpActionResult editairinvoice(SOFlightList val)
        {
            return Ok(new SalesOrderFormManager().editairinvoice(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("edithotel")]
        public IHttpActionResult edithotel(SOHotelList val)
        {
            return Ok(new SalesOrderFormManager().Edithotel(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editcar")]
        public IHttpActionResult editcar(SOCarList val)
        {
            return Ok(new SalesOrderFormManager().Editcar(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editpackages")]
        public IHttpActionResult editpackages(SOPackageDetailList val)
        {
            return Ok(new SalesOrderFormManager().Editpackages(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editotherservice")]
        public IHttpActionResult editotherservice(SOOtherServiceDetailList val)
        {
            return Ok(new SalesOrderFormManager().Editotherservice(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("editinsurance")]
        public IHttpActionResult editinsurance(SOInsurenceList val)
        {
            return Ok(new SalesOrderFormManager().Editinsurance(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofcustomerget")]
        public IHttpActionResult sofcustomerget(Customerdetails val)
        {
            return Ok(new SalesOrderFormManager().sofcustomerget(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofpaxget")]
        public IHttpActionResult sofpaxget(Customerdetails val)
        {
            return Ok(new SalesOrderFormManager().sofpaxget(val));
        }


        [HttpPost]
        [Authorize]
        [ActionName("salesorderformpageload")]
        public IHttpActionResult salesorderformpageload()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().PageLoad(user_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("salesorderformsummary")]
        public IHttpActionResult salesorderformsummary()
        {
            return Ok(new SalesOrderFormManager().Getall());
        }
        [HttpPost]
        [Authorize]
        [ActionName("salesorderforminvoicesummary")]
        public IHttpActionResult salesorderforminvoicesummary()
        {
            return Ok(new SalesOrderFormManager().salesorderforminvoicesummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopassengeradd")]
        public IHttpActionResult sopassengeradd(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().PassengerAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassenger")]
        public IHttpActionResult salesinvoicepassenger(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoicePassengerAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sovisaadd")]
        public IHttpActionResult sovisaadd(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().VisaAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicevisa")]
        public IHttpActionResult salesinvoicevisa(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceVisaAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soflightadd")]
        public IHttpActionResult soflightadd(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().FlightAdd(val, user_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceflight")]
        public IHttpActionResult salesinvoiceflight(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceFlightAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesairinvoice")]
        public IHttpActionResult salesairinvoice(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesAirInvoice(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sohoteladd")]
        public IHttpActionResult sohoteladd(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().HotelAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicehotel")]
        public IHttpActionResult salesinvoicehotel(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceHotelAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("socaradd")]
        public IHttpActionResult socaradd(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().CarAdd(val, user_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicecar")]
        public IHttpActionResult salesinvoicecar(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceCarAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soforexadd")]
        public IHttpActionResult soforexadd(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().ForexAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceforex")]
        public IHttpActionResult salesinvoiceforex(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceForexAdd(val, user_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("soinsuranceadd")]
        public IHttpActionResult soinsuranceadd(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().InsuranceAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceinsurance")]
        public IHttpActionResult salesinvoiceinsurance(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoiceInsuranceAdd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sooverallsubmit")]
        public IHttpActionResult sooverallsubmit(SalesOrderFormList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().overallsubmit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soeditsubmit")]
        public IHttpActionResult soeditsubmit(SalesOrderFormList val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soeditsubmit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("PassportUploadDocument")]
        public IHttpActionResult PassportUploadDocument(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().PassportUploadDocument(val, user_gid));
        }


        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassport")]
        public IHttpActionResult salesinvoicepassport(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepassport(val, user_gid));
        }
        //[Authorize]
        //[HttpPost]
        //[ActionName("PassportUploadDocument")]
        //public HttpResponseMessage PassportUploadDocument()
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var user_gid = new TokenManager().GetuserID(id);
        //    var companycode = new TokenManager().GetcompanyCode(id);
        //    HttpRequest httpRequest;
        //    SalesOrderFormModel getpassdocumentimportexcel = new SalesOrderFormModel();
        //    SalesOrderFormManager GetdocumentFunctions = new SalesOrderFormManager();
        //    httpRequest = HttpContext.Current.Request;
        //    getpassdocumentimportexcel = GetdocumentFunctions.GetpassdocumentUploadExcel(companycode, httpRequest, user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, getpassdocumentimportexcel);
        //}
        [Authorize]
        [HttpPost]
        [ActionName("sopackageadd")]
        public IHttpActionResult sopackageadd(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopackageadd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepackage")]
        public IHttpActionResult salesinvoicepackage(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().SalesInvoicepackageadd(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceotherservices")]
        public IHttpActionResult salesinvoiceotherservices(SOOtherServiceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceotherservices(val, user_gid));
        }
        //[Authorize]
        //[HttpPost]
        //[ActionName("sopackageadd")]
        //public HttpResponseMessage sopackageadd()
        //{
        //    IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
        //    var id = headerValues.FirstOrDefault();
        //    var user_gid = new TokenManager().GetuserID(id);
        //    var companycode = new TokenManager().GetcompanyCode(id);
        //    HttpRequest httpRequest;
        //    SalesOrderFormModel getsopackage = new SalesOrderFormModel();
        //    SalesOrderFormManager getpackage = new SalesOrderFormManager();
        //    httpRequest = HttpContext.Current.Request;
        //    getsopackage = getpackage.PackageAdd(companycode, httpRequest, user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, getsopackage);
        //}

        [HttpPost]
        [Authorize]
        [ActionName("sogetpassportno")]
        public IHttpActionResult sogetpassportno(SOPassengerDetail values)
        {
            return Ok(new SalesOrderFormManager().getpassportno(values.salesorder_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("soalltemptabldelete")]
        public IHttpActionResult soalltemptabldelete(SalesOrderForm sales)
        {
            return Ok(new SalesOrderFormManager().Alltmpdelete(sales.salesorder_gid));
        }

        [HttpPost]
        [Authorize]
        [ActionName("getpassenger")]
        public IHttpActionResult getpassenger(SOPassengerDetail values)
        {
            return Ok(new SalesOrderFormManager().getpassenger(values.tmppassengerservice_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesorderformpasdel")]
        public IHttpActionResult salesorderformpasdel(SOPassengerDetail values)
        {
            return Ok(new SalesOrderFormManager().salesorderformpasdel(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesorderformcustomerinvoicedel")]
        public IHttpActionResult salesorderformcustomerinvoicedel(SOPassengerDetail values)
        {
            return Ok(new SalesOrderFormManager().salesorderformcustomerinvoicedel(values));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesorderformcancel")]
        public IHttpActionResult salesorderformcancel(SalesOrderFormDetail values)
        {
            return Ok(new SalesOrderFormManager().salesorderformcancel(values.salesorder_gid));
        }

        [HttpPost]
        [Authorize]
        [ActionName("soservicetypeget")]
        public IHttpActionResult soservicetypeget(SalesOrderFormModel val)
        {
            return Ok(new SalesOrderFormManager().soservicetypeget(val.salesorder_gid, val.service_gid));
        }


        [Authorize]
        [HttpPost]
        [ActionName("sofpassengerdelete")]
        public IHttpActionResult sofpassengerdelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofpassengerdelete(values.passengerservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofpassportdelete")]
        public IHttpActionResult sofpassportdelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofpassportdelete(values.passportservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofvisadelete")]
        public IHttpActionResult sofvisadelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofvisadelete(values.visaservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofflightdelete")]
        public IHttpActionResult sofflightdelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofflightdelete(values.flightservice_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sofairinvoicedelete")]
        public IHttpActionResult sofairinvoicedelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofairinvoicedelete(values.air_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sofhoteldelete")]
        public IHttpActionResult sofhoteldelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofhoteldelete(values.hotelservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofcardelete")]
        public IHttpActionResult sofcardelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofcardelete(values.carservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofforexdelete")]
        public IHttpActionResult sofforexdelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofforexdelete(values.forexservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sofpackagedelete")]
        public IHttpActionResult sofpackagedelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofpackagedelete(values.packageservice_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sofotherservicedelete")]
        public IHttpActionResult sofotherservicedelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofotherservicedelete(values.packageservice_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sofinsdelete")]
        public IHttpActionResult sofinsdelete(soservicedelete values)
        {
            return Ok(new SalesOrderFormManager().sofinsdelete(values.insuranceservice_gid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("sopassengeredit")]
        public IHttpActionResult sopassengeredit(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopassengeredit(val.passengerservice_gid, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassengeredit")]
        public IHttpActionResult salesinvoicepassengeredit(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepassengeredit(val.passengerservice_gid, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sovisaedit")]
        public IHttpActionResult sovisaedit(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sovisaedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicevisaedit")]
        public IHttpActionResult salesinvoicevisaedit(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicevisaedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soflightedit")]
        public IHttpActionResult soflightedit(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soflightedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceflightedit")]
        public IHttpActionResult salesinvoiceflightedit(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceflightedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesairinvoiceedit")]
        public IHttpActionResult salesairinvoiceedit(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesairinvoiceedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sohoteledit")]
        public IHttpActionResult sohoteledit(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sohoteledit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicehoteledit")]
        public IHttpActionResult salesinvoicehoteledit(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicehoteledit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("socaredit")]
        public IHttpActionResult socaredit(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().socaredit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicecaredit")]
        public IHttpActionResult salesinvoicecaredit(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicecaredit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soforexedit")]
        public IHttpActionResult soforexedit(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soforexedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceforexedit")]
        public IHttpActionResult salesinvoiceforexedit(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceforexedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soinsuranceedit")]
        public IHttpActionResult soinsuranceedit(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soinsuranceedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceinsurencedit")]
        public IHttpActionResult salesinvoiceinsurencedit(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceinsurencedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopassportedit")]
        public IHttpActionResult sopassportedit(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopassportedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassportedit")]
        public IHttpActionResult salesinvoicepassportedit(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepassportedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopackageedit")]
        public IHttpActionResult sopackageedit(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopackageedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepackageedit")]
        public IHttpActionResult salesinvoicepackageedit(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepackageedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceotherservicesedit")]
        public IHttpActionResult salesinvoiceotherservicesedit(SOOtherServiceDetail   val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceotherservicesedit(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopassengerupdate")]
        public IHttpActionResult sopassengerupdate(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopassengerupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassengerupdate")]
        public IHttpActionResult salesinvoicepassengerupdate(SOPassengerDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepassengerupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sovisaupdate")]
        public IHttpActionResult sovisaupdate(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sovisaupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicevisaupdate")]
        public IHttpActionResult salesinvoicevisaupdate(SOVisaDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicevisaupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soflightupdate")]
        public IHttpActionResult soflightupdate(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soflightupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceflightupdate")]
        public IHttpActionResult salesinvoiceflightupdate(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceflightupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesairinvoiceupdate")]
        public IHttpActionResult salesairinvoiceupdate(SOFlightDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesairinvoiceupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sohotelupdate")]
        public IHttpActionResult sohotelupdate(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sohotelupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicehotelupdate")]
        public IHttpActionResult salesinvoicehotelupdate(SOHotelDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicehotelupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("socarupdate")]
        public IHttpActionResult socarupdate(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().socarupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicecarupdate")]
        public IHttpActionResult salesinvoicecarupdate(SOCarDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicecarupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soforexupdate")]
        public IHttpActionResult soforexupdate(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soforexupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceforexupdate")]
        public IHttpActionResult salesinvoiceforexupdate(SOForexDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceforexupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("soinsuranceupdate")]
        public IHttpActionResult soinsuranceupdate(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().soinsuranceupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceinsuranceupdate")]
        public IHttpActionResult salesinvoiceinsuranceupdate(SOInsurenceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceinsuranceupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopassportupdate")]
        public IHttpActionResult sopassportupdate(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopassportupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepassportupdate")]
        public IHttpActionResult salesinvoicepassportupdate(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepassportupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesvendorpassportupdate")]
        public IHttpActionResult salesvendorpassportupdate(SOPassportDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesvendorpassportupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("sopackageupdate")]
        public IHttpActionResult sopackageupdate(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().sopackageupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoicepackageupdate")]
        public IHttpActionResult salesinvoicepackageupdate(SOPackageDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoicepackageupdate(val, user_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("salesinvoiceotherservicesupdate")]
        public IHttpActionResult salesinvoiceotherservicesupdate(SOOtherServiceDetail val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().salesinvoiceotherservicesupdate(val, user_gid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofcustomer2")]
        public IHttpActionResult sofcustomer2(Customerdetails val)
        {
            return Ok(new SalesOrderFormManager().sofcustomer2(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("sofcustomer1")]
        public IHttpActionResult sofcustomer1(Customerdetails val)
        {
            return Ok(new SalesOrderFormManager().sofcustomer1(val));
        }
        
        [Authorize]
        [HttpPost]
        [ActionName("softempactivitydelete")]
        public IHttpActionResult softempactivitydelete(sotempactivity val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var user_gid = new TokenManager().GetuserID(id);
            return Ok(new SalesOrderFormManager().softempactivitydelete(val,user_gid));
        }
    }
}