using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class AIRManager
    {        
        public AIRmodel receiveairdata(AIRDetails val)
        {
            return new AIRDBAccess().receiveairdata(val);
        }
        public AIR receiveairdatasummary()
        {
            return new AIRDBAccess().receiveairdatasummary();
        }
        public AIR raiseairfilesummary()
        {
            return new AIRDBAccess().raiseairfilesummary();
        }
        public Billingdetail invoicedetails(Billingdetail val)
        {
            return new AIRDBAccess().invoicedetails(val);
        }
        public AIRDetails airnotification()
        {
            return new AIRDBAccess().airnotification();
        }
    }
    
}