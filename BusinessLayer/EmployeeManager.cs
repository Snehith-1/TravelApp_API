using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class EmployeeManager
    {
        public Employee employeesummary()
        {
            return new EmployeeDBAccess().employeesummary();
        }
        public Employeedetail employeecode(string usergid)
        {
            return new EmployeeDBAccess().employeecode(usergid);
        }
        public Employeedetail employeeedit(string values)
        {
            return new EmployeeDBAccess().employeeedit(values);
        }

        public Employeedetail employeeprofile(string user_gid)
        {
            return new EmployeeDBAccess().employeeprofile(user_gid);
        }
        public Employeemodel Getdocument(string companycode, HttpRequest httpRequest, string usergid)
        {
            return new EmployeeDBAccess().Getdocument(companycode, httpRequest, usergid);
        }
        public Employeemodel employeeupdate(string companycode, HttpRequest httpRequest, string usergid)
        {
            return new EmployeeDBAccess().employeeupdate(companycode, httpRequest, usergid);
        }
        public Employeemodel employeestatus(Employeelist val, string usergid)
        {
            return new EmployeeDBAccess().employeestatus(val, usergid);
        }
        public Employeemodel employeepswreset(Employeedetail val, string usergid)
        {
            return new EmployeeDBAccess().employeepswreset(val, usergid);
        }
        public Employeemodel EmployeeDocumentpath(Employeedetail values)
        {
            return new EmployeeDBAccess().EmployeeDocumentpath(values);
        }
    }
}