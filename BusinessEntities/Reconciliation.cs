using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Reconciliation
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class reconciliation : Reconciliation
    {
        public List<reconciliationlist> reconciliationlist { get; set; }
        public string file_name { get; set; }
    }
    public class reconciliationDetail : Reconciliation
    {
        public string reconciliation_gid { get; set; }
        public string agency_name { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        public string upload_airfile { get; set; }
        public string document_path { get; set; }

    }
    public class reconciliationlist : Reconciliation
    {
        public string reconciliation_gid { get; set; }
        public string vendor_gid { get; set; }
        public string agency_name { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        public string upload_airfile { get; set; }
        public string document_path { get; set; }
        public DateTime created_date { get; set; }
        public HttpResponse httpdocument { get; set; }
    }

    public class reconciliationcountlist : Reconciliation
    {
        public string reconciliation_gid { get; set; }
        public string vendor_gid { get; set; }
        public string matchingagent_records { get; set; }
        public string total_records_file { get; set; }
        public string matching_records_file { get; set; }

    }
    public class matchingcountlist : Reconciliation
    {
        public string reconciliation_gid { get; set; }
        public string vendor_gid { get; set; }
        public DateTime date { get; set; }
        public string sector { get; set; }
        public string document_number { get; set; }
        public string pax_name { get; set; }
        public string ticket_number { get; set; }
        public double selling_amount { get; set; }
        public List<matchingcountlist> matchingcount { get; set; }

    }
    public class matchingwithagentcount : Reconciliation
    {
        public string reconciliation_gid { get; set; }
        public string vendor_gid { get; set; }
        public DateTime date { get; set; }
        public string sector { get; set; }
        public string document_number { get; set; }
        public string pax_name { get; set; }
        public string ticket_number { get; set; }
        public double selling_amount { get; set; }
        public List<matchingwithagentcount> matchingwithagent { get; set; }

    }
}