using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Departmentmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Department : Departmentmodel
    {
        public List<Departmentlist> departmentlist { get; set; }
    }
    public class Departmentlist
    {
        public int department_gid { get; set; }
        public string department_code { get; set; }
        public string department_name { get; set; }
  
    }

}