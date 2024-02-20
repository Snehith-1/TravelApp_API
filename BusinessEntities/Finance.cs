using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Financemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Finance : Financemodel
    {
        public string reference_type { get; set; }
        public string account_code { get; set; }
        public string account_name { get; set; }
        public string reference_gid { get; set; }        
    }

    public class Financedetail : Finance
    {
        public string gid { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string has_child { get; set; }
        public string ledger_type { get; set; }
        public string display_type { get; set; }
        public string gl_code { get; set; }
        public string journal_date { get; set; }
        public string journal_year { get; set; }
        public string journal_month { get; set; }
        public string journal_day { get; set; }
        public string journal_refnumber { get; set; }
        public string fin_remarks { get; set; }
        public string transaction_gid { get; set; }
        public double transaction_amount { get; set; }
        public string screen_name { get; set; }
        public string module_name { get; set; }
        public string field_name { get; set; }
        public double invoice_amount { get; set; }
        public double cogs_amount { get; set; }
        public double addon_amount { get; set; }
        public double discount_amount { get; set; }
        public string payment_mode { get; set; }
        public string payment_gid { get; set; }
        public double basic_amount { get; set; }
        public string journal_gid { get; set; }
    }

    public class Financerefunddetail : Finance
    {
        public string gid { get; set; }
        public string account_gid { get; set; }
        public string accountgroup_gid { get; set; }
        public string accountgroup_name { get; set; }
        public string has_child { get; set; }
        public string ledger_type { get; set; }
        public string display_type { get; set; }
        public string gl_code { get; set; }
        public string journal_date { get; set; }
        public string journal_year { get; set; }
        public string journal_month { get; set; }
        public string journal_day { get; set; }
        public string journal_refnumber { get; set; }
        public string fin_remarks { get; set; }
        public string transaction_gid { get; set; }
        public double transaction_amount { get; set; }
        public string screen_name { get; set; }
        public string module_name { get; set; }
        public string field_name { get; set; }
        public double invoice_amount { get; set; }
        public double cogs_amount { get; set; }
        public double addon_amount { get; set; }
        public double discount_amount { get; set; }
        public string payment_mode { get; set; }
        public string payment_gid { get; set; }
        public double basic_amount { get; set; }
        public string journal_gid { get; set; }
        public List<customerinvoicelist> refundServiceTypeList { get; set; }
    }

}