using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class SalesOrderFormManager
    {
        public SalesOrderForm PageLoad(string user_gid)
        {
            return new SalesOrderFormDBAccess().PageLoad(user_gid);
        }
        public sotempactivity softempactivitydelete(sotempactivity val,string user_gid)
        {
            return new SalesOrderFormDBAccess().softempactivitydelete(val,user_gid);

        }
        public SalesOrderForm Activity(SOActivityList val)
        {
            return new SalesOrderFormDBAccess().Activity(val);
        }

        public SalesOrderForm Customer()
        {
            return new SalesOrderFormDBAccess().Customer();
        }
        public SalesOrderForm Currency()
        {
            return new SalesOrderFormDBAccess().Currency();
        }
        public SalesOrderForm Passenger(SOPassengerList val)
        {
            return new SalesOrderFormDBAccess().Passenger(val);
        }
        public SalesOrderForm EditPassenger(SOPassengerList val)
        {
            return new SalesOrderFormDBAccess().EditPassenger(val);
        }
        public SalesOrderForm EditPassport(SOPassportList val)
        {
            return new SalesOrderFormDBAccess().EditPassport(val);
        }
        public SalesOrderForm vendorpassport(SOPassportList val)
        {
            return new SalesOrderFormDBAccess().vendorpassport(val);
        }
        public SalesOrderForm Editvisa(SOVisaList val)
        {
            return new SalesOrderFormDBAccess().Editvisa(val);
        }
        public SalesOrderForm Editflight(SOFlightList val)
        {
            return new SalesOrderFormDBAccess().Editflight(val);

        }
        public SalesOrderForm editairinvoice(SOFlightList val)
        {
            return new SalesOrderFormDBAccess().editairinvoice(val);
        }
        public SalesOrderForm Edithotel(SOHotelList val)
        {
            return new SalesOrderFormDBAccess().Edithotel(val);
        }
        public SalesOrderForm Editcar(SOCarList val)
        {
            return new SalesOrderFormDBAccess().Editcar(val);
        }
        public SalesOrderForm Editpackages(SOPackageDetailList val)
        {
            return new SalesOrderFormDBAccess().Editpackages(val);
        }
        public SalesOrderForm Editotherservice(SOOtherServiceDetailList val)
        {
            return new SalesOrderFormDBAccess().Editotherservice(val);
        }
        public SalesOrderForm Editinsurance(SOInsurenceList val)
        {
            return new SalesOrderFormDBAccess().Editinsurance(val);
        }
        public Customerdetails sofcustomerget(Customerdetails val)
        {
            return new SalesOrderFormDBAccess().sofcustomerget(val);
        }
        public Customerdetails sofpaxget(Customerdetails val)
        {
            return new SalesOrderFormDBAccess().sofpaxget(val);
        }

        public SalesOrderForm Getall()
        {
            return new SalesOrderFormDBAccess().Getall();
        }
        public SalesOrderForm salesorderforminvoicesummary()
        {
            return new SalesOrderFormDBAccess().salesorderforminvoicesummary();
        }
        public SalesOrderFormModel PassengerAdd(SOPassengerDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().PassengerAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoicePassengerAdd(SOPassengerDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoicePassengerAdd(val, user_gid);
        }
        public SalesOrderFormModel VisaAdd(SOVisaDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().VisaAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceVisaAdd(SOVisaDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceVisaAdd(val, user_gid);
        }
        public SalesOrderFormModel FlightAdd(SOFlightDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().FlightAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceFlightAdd(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceFlightAdd(val, user_gid);
        }

        public SalesOrderFormModel SalesAirInvoice(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesAirInvoice(val, user_gid);
        }
        public SalesOrderFormModel HotelAdd(SOHotelDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().HotelAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceHotelAdd(SOHotelDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceHotelAdd(val, user_gid);
        }
        public SalesOrderFormModel CarAdd(SOCarDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().CarAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceCarAdd(SOCarDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceCarAdd(val, user_gid);
        }
        public SalesOrderFormModel ForexAdd(SOForexDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().ForexAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceForexAdd(SOForexDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceForexAdd(val, user_gid);
        }
        public SalesOrderFormModel InsuranceAdd(SOInsurenceDetail val,string user_gid)
        {
            return new SalesOrderFormDBAccess().InsuranceAdd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoiceInsuranceAdd(SOInsurenceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoiceInsuranceAdd(val, user_gid);
        }
        public SalesOrderFormModel overallsubmit(SalesOrderFormList val, string user_gid)
        {
            return new SalesOrderFormDBAccess().overallsubmit(val, user_gid);
        }
        public SalesOrderFormModel soeditsubmit(SalesOrderFormList val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soeditsubmit(val, user_gid);
        }

        //public SalesOrderFormModel PackageAdd(string companycode, HttpRequest httpRequest, string user_gid)
        //{
        //    return new SalesOrderFormDBAccess().PackageAdd(companycode, httpRequest, user_gid);
        //}
        public SalesOrderFormModel sopackageadd(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopackageadd(val, user_gid);
        }
        public SalesOrderFormModel SalesInvoicepackageadd(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().SalesInvoicepackageadd(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceotherservices(SOOtherServiceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceotherservices(val, user_gid);
        }
        //public SalesOrderFormModel GetpassdocumentUploadExcel(string companycode, HttpRequest httpRequest, string user_gid)
        //{
        //    return new SalesOrderFormDBAccess().GetpassdocumentUploadExcel(companycode, httpRequest, user_gid);
        //}

        public SalesOrderFormModel PassportUploadDocument(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().PassportUploadDocument(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepassport(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepassport(val, user_gid);
        }
     
        public SalesOrderForm getpassportno(int values)
        {
            return new SalesOrderFormDBAccess().getpassportno( values);
        }

        public SalesOrderFormModel Alltmpdelete(int salesorderid)
        {
            return new SalesOrderFormDBAccess().Alltmpdelete(salesorderid);
        }

        public SOPassengerDetail getpassenger(string values)
        {
            return new SalesOrderFormDBAccess().getpassenger(values);
        }
        public SalesOrderFormModel salesorderformpasdel(SOPassengerDetail val)
        {
            return new SalesOrderFormDBAccess().salesorderformpasdel(val);
        }
        public SalesOrderFormModel salesorderformcustomerinvoicedel(SOPassengerDetail val)
        {
            return new SalesOrderFormDBAccess().salesorderformcustomerinvoicedel(val);
        }
        public SalesOrderFormModel salesorderformcancel(int val)
        {
            return new SalesOrderFormDBAccess().salesorderformcancel(val);
        }

        public SalesOrderForm soservicetypeget(int salesorder_gid,int service_gid)
        {
            return new SalesOrderFormDBAccess().soservicetypeget(salesorder_gid, service_gid);
        }
      

        public soservicedelete sofpassengerdelete(string values)
        {
            return new SalesOrderFormDBAccess().sofpassengerdelete(values);
        }

        public soservicedelete sofpassportdelete(string values)
        {
            return new SalesOrderFormDBAccess().sofpassportdelete(values);
        }

        public soservicedelete sofvisadelete(string values)
        {
            return new SalesOrderFormDBAccess().sofvisadelete(values);
        }

        public soservicedelete sofflightdelete(string values)
        {
            return new SalesOrderFormDBAccess().sofflightdelete(values);
        }
        public soservicedelete sofairinvoicedelete(string values)
        {
            return new SalesOrderFormDBAccess().sofairinvoicedelete(values);
        }
        public soservicedelete sofhoteldelete(string values)
        {
            return new SalesOrderFormDBAccess().sofhoteldelete(values);
        }

        public soservicedelete sofcardelete(string values)
        {
            return new SalesOrderFormDBAccess().sofcardelete(values);
        }

        public soservicedelete sofforexdelete(string values)
        {
            return new SalesOrderFormDBAccess().sofforexdelete(values);
        }

        public soservicedelete sofpackagedelete(string values)
        {
            return new SalesOrderFormDBAccess().sofpackagedelete(values);
        }

        public soservicedelete sofotherservicedelete(string values)
        {
            return new SalesOrderFormDBAccess().sofotherservicedelete(values);
        }
        public soservicedelete sofinsdelete(string values)
        {
            return new SalesOrderFormDBAccess().sofinsdelete(values);
        }


        public SOPassengerDetail sopassengeredit(string val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopassengeredit(val, user_gid);
        }
        public SOPassengerDetail salesinvoicepassengeredit(string val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepassengeredit(val, user_gid);
        }
        public SalesOrderFormModel sovisaedit(SOVisaDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sovisaedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicevisaedit(SOVisaDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicevisaedit(val, user_gid);
        }
        public SalesOrderFormModel soflightedit(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soflightedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceflightedit(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceflightedit(val, user_gid);
        }

        public SalesOrderFormModel salesairinvoiceedit(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesairinvoiceedit(val, user_gid);
        }
        public SalesOrderFormModel sohoteledit(SOHotelDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sohoteledit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicehoteledit(SOHotelDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicehoteledit(val, user_gid);
        }
        public SalesOrderFormModel socaredit(SOCarDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().socaredit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicecaredit(SOCarDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicecaredit(val, user_gid);
        }
        public SalesOrderFormModel soforexedit(SOForexDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soforexedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceforexedit(SOForexDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceforexedit(val, user_gid);
        }
        public SalesOrderFormModel soinsuranceedit(SOInsurenceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soinsuranceedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceinsurencedit(SOInsurenceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceinsurencedit(val, user_gid);
        }
        public SalesOrderFormModel sopassportedit(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopassportedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepassportedit(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepassportedit(val, user_gid);
        }
        public SalesOrderFormModel sopackageedit(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopackageedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepackageedit(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepackageedit(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceotherservicesedit(SOOtherServiceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceotherservicesedit(val, user_gid);
        }
        public SalesOrderFormModel sopassengerupdate(SOPassengerDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopassengerupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepassengerupdate(SOPassengerDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepassengerupdate(val, user_gid);
        }
        public SalesOrderFormModel sovisaupdate(SOVisaDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sovisaupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicevisaupdate(SOVisaDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicevisaupdate(val, user_gid);
        }
        public SalesOrderFormModel soflightupdate(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soflightupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceflightupdate(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceflightupdate(val, user_gid);
        }
        public SalesOrderFormModel salesairinvoiceupdate(SOFlightDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesairinvoiceupdate(val, user_gid);
        }
        public SalesOrderFormModel sohotelupdate(SOHotelDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sohotelupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicehotelupdate(SOHotelDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicehotelupdate(val, user_gid);
        }
        public SalesOrderFormModel socarupdate(SOCarDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().socarupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicecarupdate(SOCarDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicecarupdate(val, user_gid);
        }
        public SalesOrderFormModel soforexupdate(SOForexDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soforexupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceforexupdate(SOForexDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceforexupdate(val, user_gid);
        }
        public SalesOrderFormModel soinsuranceupdate(SOInsurenceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().soinsuranceupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceinsuranceupdate(SOInsurenceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceinsuranceupdate(val, user_gid);
        }
        public SalesOrderFormModel sopassportupdate(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopassportupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepassportupdate(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepassportupdate(val, user_gid);
        }
        public SalesOrderFormModel salesvendorpassportupdate(SOPassportDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesvendorpassportupdate(val, user_gid);
        }
        public SalesOrderFormModel sopackageupdate(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().sopackageupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoicepackageupdate(SOPackageDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoicepackageupdate(val, user_gid);
        }
        public SalesOrderFormModel salesinvoiceotherservicesupdate(SOOtherServiceDetail val, string user_gid)
        {
            return new SalesOrderFormDBAccess().salesinvoiceotherservicesupdate(val, user_gid);
        }
        public customer sofcustomer2(Customerdetails val)
        {
            return new SalesOrderFormDBAccess().sofcustomer2(val);

        }
        public SalesOrderForm sofcustomer1(Customerdetails val)
        {
            return new SalesOrderFormDBAccess().sofcustomer1(val);

        }
    }
    
}