using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;
using MySql.Data.MySqlClient;

namespace BusinessLayer
{
    public class ActivityManager
    {
        DBAccess objdbcon = new DBAccess();
        EmployeeDBAccess objemployeedbaccess = new EmployeeDBAccess();
        MySqlConnection objcon;

        public Activity GetAll()
        {
            return new ActivityDBAccess().GetAll();
        }
        public Activity activityservice(string values)
        {
            return new ActivityDBAccess().activityservice(values);
        }
        public Activitydetail Get(int values)
        {
            return new ActivityDBAccess().Get(values);
        }        
        public Activitymodel Add(Activitydetail val, string userGid)
        {
            return new ActivityDBAccess().Add(val, userGid);
        }
        public Activitymodel Update(ActivityList val, string usergid)
        {
            return new ActivityDBAccess().Update(val, usergid);
        }
        public Activitymodel Delete(int values)
        {
            return new ActivityDBAccess().Delete(values);
        }
    }
}