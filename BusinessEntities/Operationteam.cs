using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Operationteammodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Operationteam : Salesteammodel
    {
        public List<Operationteamlist> operationteamlist { get; set; }
        public List<employeelists> employeelists { get; set; }
    }
    public class Operationteamlist
    {
        public int operationteam_gid { get; set; }
        public string operationteam_code { get; set; }
        public string operationteam_name { get; set; }
        public string operationteam_manager_name { get; set; }
        public string operationteam_employee_name { get; set; }
        public string operationteam_numberof_employee { get; set; }
        public string operationteam_employee_gid { get; set; }
        public int operationteam_manager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class employeelists
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class Operationteamdetail : Operationteammodel
    {
        public int operationteam_gid { get; set; }
        public int operationteam_employee_gid { get; set; }
        public int operationteam_manager_gid { get; set; }
        public string operationteam_name { get; set; }
        public string operationteam_manager_name { get; set; }
        public string operationteam_employee_name { get; set; }
        public string operationteam_numberof_employee { get; set; }
        public string operationteam_code { get; set; }
        public List<Operationteamlist> operationteamlist { get; set; }
        public List<employeelists> employeelists { get; set; }
    }
}