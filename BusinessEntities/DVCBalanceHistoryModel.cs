using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities.Models.DVC
{
    public class DVCBalanceHistoryModel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class Balancehistory : DVCBalanceHistoryModel
    {
        public string available_balance { get; set; }
        public List<Balancehistory_values> balancehistory_list { get; set; }
    }
    public class Balancehistory_values
    {
        public string row_no { get; set; }
        public string catagory_type { get; set; }
        public string reference_code { get; set; }
        public string transaction_date { get; set; }
        public string transaction_status { get; set; }
        public string credit { get; set; }
        public string debit { get; set; }
        public string balance_amount { get; set; }
      
    }

}