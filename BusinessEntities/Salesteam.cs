using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Salesteammodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Salesteam : Salesteammodel
    {
        public List<Salesteamlist> salesteamlist { get; set; }
        public List<employeelist> employeelist { get; set; }
    }
    public class Salesteamlist
    {
        public int salesteam_gid { get; set; }
        public string salesteam_name { get; set; }
        public string soteammanagername { get; set; }
        public string soteamemployeename { get; set; }
        public string sonoofemployee { get; set; }
        public string salesteam_code { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class employeelist
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class Salesteamdetail:Salesteammodel
    {
        public int salesteam_gid { get; set; }
        public int soteamemployee_gid { get; set; }
        public string salesteam_name { get; set; }
        public string soteammanager_name { get; set; }
        public string soteamemployeename { get; set; }
        public string sonoofemployee { get; set; }
        public string salesteam_code { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public List<Salesteamlist> salesteamlist { get; set; }
        public List<employeelist> employeelist { get; set; }
    }
}