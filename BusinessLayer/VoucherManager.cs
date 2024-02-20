using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;


namespace BusinessLayer
{
    public class VoucherManager
    {
        public Voucher vouchersummary(string val)
        {
            return new VoucherDBAccess().vouchersummary(val);
        }

        //public Voucherdetails Get(int values)
        //{
        //    return new VoucherDBAccess().Get(values);
        //}
        public Vouchermodel voucheradd(Voucherdetails val, string usergid)
        {
            return new VoucherDBAccess().voucheradd(val, usergid);
        }
        public Vouchermodel Delete(int values)
        {
            return new VoucherDBAccess().Delete(values);
        }

        //////public Partnermodel Update(Partnerdetails values, string usergid)
        //////{
        //////    return new PartnerDBAccess().Update(values, usergid);
        //////}



    }
}

