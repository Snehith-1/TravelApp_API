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
    [RoutePrefix("api/orderprocessingform")]
    public class Orderprocessingformcontroller:ApiController 
    {
        [HttpPost ]
        [Authorize ]
        [ActionName ("orderprocessingformsummary")]
        public IHttpActionResult orderprocessingformsummary()
        {
            return Ok(new OrderprocessingformManger().Getall());
        }
        [HttpPost]
        [Authorize]
        [ActionName("orderprocessingsummary")]
        public IHttpActionResult orderprocessingsummary()
        {
            return Ok(new OrderprocessingformManger().Orderprocessing());
        }
        [Authorize]
        [HttpPost]
        [ActionName("pageload")]
        public IHttpActionResult pageload(orderprocessinglist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OrderprocessingformManger().pageload(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("opfpassengersummary")]
        public IHttpActionResult opfpassengersummary(opfpassengerlist val)
        {
            return Ok(new OrderprocessingformManger().opfpassengersummary(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("opfactivity")]
        public IHttpActionResult opfactivity(opfactivityList val)
        {
            return Ok(new OrderprocessingformManger().activity(val));
        }
        [Authorize]
        [HttpPost]
        [ActionName("customer")]
        public IHttpActionResult customer(orderprocessingdetail values)
        {
            return Ok(new OrderprocessingformManger().Getcustomer(values.salesorder_gid));
        }
        [Authorize]
        [HttpPost]
        [ActionName("overallsubmit")]
        public IHttpActionResult overallsubmit(orderprocessinglist val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OrderprocessingformManger().oversubmit(val, usergid));
        }

        [Authorize]
        [HttpPost]
        [ActionName("submit")]
        public IHttpActionResult submit(billing val)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            var id = headerValues.FirstOrDefault();
            var usergid = new TokenManager().GetuserID(id);
            return Ok(new OrderprocessingformManger().submit(val, usergid));
        }
        [HttpPost]
        [Authorize]
        [ActionName("vendorpaymentsummary")]
        public IHttpActionResult vendorpaymentsummary(vendoractivitylist val)
        {
            return Ok(new OrderprocessingformManger().vendorpaymentsummary(val));
        }
        [HttpPost]
        [Authorize]
        [ActionName("orderprocessingmainsummary")]
        public IHttpActionResult orderprocessingmainsummary()
        {
            return Ok(new OrderprocessingformManger().orderprocessingmainsummary());
        }

        [HttpPost]
        [Authorize]
        [ActionName("ordersalesselectsummary")]
        public IHttpActionResult ordersalesselectsummary()
        {
            return Ok(new OrderprocessingformManger().ordersalesselectsummary());
        }
        [Authorize]
        [HttpPost]
        [ActionName("opfstatus")]
        public IHttpActionResult opfstatus(Orderprocessingformdetails values)
        {
            return Ok(new OrderprocessingformManger().opfstatus(values));
        }
        [HttpPost]
        [Authorize]
        [ActionName("soserviceget")]
        public IHttpActionResult soserviceget(SalesOrderFormModel val)
        {
            return Ok(new OrderprocessingformManger().soserviceget(val.salesorder_gid));
        }


        [Authorize]
        [HttpPost]
        [ActionName("totalvalue")]
        public IHttpActionResult totalvalue(totalvaluedetails values)
        {
            return Ok(new OrderprocessingformManger().totalvalue(values));
        }


    }
}