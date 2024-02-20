using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;
using MySql.Data.MySqlClient;

namespace BusinessLayer
{
    public class MailManagementManager
    {
        DBAccess objdbcon = new DBAccess();
        EmployeeDBAccess objemployeedbaccess = new EmployeeDBAccess();
        MySqlConnection objcon;

        public MailManagement GetAll()
        {
            return new MailManagementDBAccess().GetAll();
        }
        //public MailManagement Mailservice(List<customerlist> customer_gid, int mailmanagement_gid)
        //{
        //    return new MailManagementDBAccess().Mailservice(customer_gid, mailmanagement_gid);
        //}
       
        public MailManagementmodel Mailservice(List<customerlist> customer_gid, int mailmanagement_gid)
        {
            return new MailManagementDBAccess().Mailservice(customer_gid, mailmanagement_gid);
        }
        public MailManagementdetail Get(int values)
        {
            return new MailManagementDBAccess().Get(values);
        }

        public object Mailservice(string customer_gid, int mailmanagement_gid)
        {
            throw new NotImplementedException();
        }

        public MailManagementmodel Add(string companycode, HttpRequest httpRequest, string userGid)
        {
            return new MailManagementDBAccess().Add(companycode, httpRequest, userGid);
        }

        public MailManagementmodel Update(MailManagementdetail val, string usergid)
        {
            return new MailManagementDBAccess().Update(val, usergid);
        }
        public MailManagementmodel Delete(int values)
        {
            return new MailManagementDBAccess().Delete(values);
        }


    }
}