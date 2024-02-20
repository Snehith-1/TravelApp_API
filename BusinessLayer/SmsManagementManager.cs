using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;
using MySql.Data.MySqlClient;

namespace BusinessLayer
{
    public class SmsManagementManager
    {
        DBAccess objdbcon = new DBAccess();
        EmployeeDBAccess objemployeedbaccess = new EmployeeDBAccess();
        MySqlConnection objcon;

        public SmsManagement GetAll()
        {
            return new SmsManagementDBAccess().GetAll();
        }
        public SmsManagement smsservice(List<customerlist> customer_gid, int mailmanagement_gid)
        {
            return new SmsManagementDBAccess().smsservice(customer_gid, mailmanagement_gid);
        }
        public SmsManagementdetail Get(string values)
        {
            return new SmsManagementDBAccess().Get(values);
        }        
        public SmsManagementmodel Add(SmsManagementdetail val, string userGid)
        {
            return new SmsManagementDBAccess().Add(val, userGid);
        }
        public SmsManagementmodel Update(SmsManagementdetail val, string usergid)
        {
            return new SmsManagementDBAccess().Update(val, usergid);
        }
        public SmsManagementmodel Delete(int values)
        {
            return new SmsManagementDBAccess().Delete(values);
        }
    }
}