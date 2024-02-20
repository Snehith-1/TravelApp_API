using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Employeemodel
    {
        public bool status { get; set; }        
        public string message { get; set; }
    }
    public class Employee : Employeemodel
    {
        public List<Employeelist> employeelist { get; set; }
    }
    public class Employeelist
    {
        public int user_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string department_name { get; set; }
        public string active_status { get; set; }
        public string branch_name { get; set; }
        public string upload_documents { get; set; }

    }
    public class Employeedetail:Employeemodel
    {
        public string user_gid { get; set; }
        public string upload_docs { get; set; }
        public string employee_code { get; set; }
        public string user_code { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string department_gid { get; set; }
        public string active_status { get; set; }
        public string gender { get; set; }
        public string email_address{ get; set; }
        public string contact_number{ get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string currency_gid { get; set; }
        public string password { get; set; }
        public string address_gid { get; set; }
        public string passport_number{ get; set; }
        public string national_id{ get; set; }
        public string branch_gid{ get; set; }
        public string branch_name { get; set; }
        public string doj { get; set; }
        public string upload_documents { get; set; }
        public string empchangepassword { get; set; }
    }    
}