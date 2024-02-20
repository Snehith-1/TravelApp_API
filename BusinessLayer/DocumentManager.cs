using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;


namespace BusinessLayer
{
    public class DocumentManager
    {
        public Document GetAll()
        {
            return new DocumentDBAccess().GetAll();
        }
        public Documentmodel GetdocumentUploadExcel( string companycode, HttpRequest httpRequest, string usergid)
        {
            return new DocumentDBAccess().GetdocumentUploadExcel(companycode, httpRequest, usergid);
        }
        public DocumentDetail Reminder(string values)
        {
            return new DocumentDBAccess().Reminder(values);
        }
        public Documentmodel Add(DocumentDetail val, string usergid)
        {
            return new DocumentDBAccess().Add(val, usergid);
        }
        public DocumentDetail edit(DocumentDetail values)
        {
            return new DocumentDBAccess().edit(values);
        }
        public DocumentDetail Documentpath(DocumentDetail values)
        {
            return new DocumentDBAccess().Documentpath(values);
        }
        public Documentmodel documentupdate(string companycode, HttpRequest httpRequest, string usergid)
        {
            return new DocumentDBAccess().documentupdate(companycode, httpRequest, usergid);
        }
        public Documentmodel salesUploadDocument(string companycode, HttpRequest httpRequest, string usergid)
        {
            return new DocumentDBAccess().salesUploadDocument(companycode, httpRequest, usergid);
        }
        public Document salesdocumentsummary(DocumentDetail val)
        {
            return new DocumentDBAccess().salesdocumentsummary(val);
        }
        public Documentmodel salesdocumentdelete(string  values)
        {
            return new DocumentDBAccess().salesdocumentdelete(values);
        }
        public DocumentDetail salesDocumentpath(DocumentDetail values)
        {
            return new DocumentDBAccess().salesDocumentpath(values);
        }
    }    
}