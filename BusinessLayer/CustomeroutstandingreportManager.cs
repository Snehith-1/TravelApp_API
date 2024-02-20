using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;
namespace BusinessLayer
{
    public class CustomeroutstandingreportManager
    {
        public customeroutstaindingdetails customeroutstandingreceipt(customeroutstaindingdetails val)
        {
            return new CustomeroutstandingreportDBAccess().customeroutstandingreceipt(val);
        }

    }
}