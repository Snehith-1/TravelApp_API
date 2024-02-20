using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Documentmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Document : Documentmodel
    {
        public List<DocumentList> DocumentList { get; set; }
        public string file_name { get; set; }
    }

    public class documentdownload :Documentmodel
    {
        public string document_gid { get; set; }
        public string uploaddocuments { get; set; }
    }
    public class DocumentDetail : Documentmodel
    {
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_type { get; set; }
        public string next_renewaldate { get; set; }
        public string reminder_date { get; set; }
        public string upload_documents { get; set; }
        public string reminder_type { get; set; }
        public string sms { get; set; }
        public string email_address{ get; set; }
        public string salesorder_gid { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        public string document_path { get; set; }
        

    }
    public class DocumentList
    {
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_type { get; set; }
        public string next_renewaldate { get; set; }
        public string reminder_date { get; set; }
        public string upload_documents { get; set; }
        public string uploaded_by { get; set; }
        public DateTime uploaded_date {get;set;}
        public string salesorder_gid { get; set; }
        public string remarks { get; set; }
    }
}