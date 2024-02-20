using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
   
    public class menuModel
    {
        public bool status { get; set;}
        public string message { get; set; }
    }

    public class menu: menuModel
    {
        public List<menumainlist> menumainlist { get; set; }
        
    } 
    public class menumainlevel:menuModel
    {
        public int department_gid{ get; set; }
        public int user_gid { get; set; }
        public int module_gid { get; set; }
        public string assignedmenus { get; set; }
    }
     
    public class menumainlist 
    {
        public int department_gid { get; set; }
        public int user_gid { get; set; }
        public int module_gid { get; set; }
        public string text { get; set; }
        public string heading { get; set; }
        public string translate { get; set; }
        public string sref { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public string menulevel { get; set; }
        public string menurefgid { get; set; }
        public List<menusublevel> submenu { get; set; }
    }
    public class menusublevel
    {
        public int department_gid { get; set; }
        public int user_gid { get; set; }
        public int module_gid { get; set; }
        public string text { get; set; }
        public string heading { get; set; }
        public string translate { get; set; }
        public string sref { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public string menulevel { get; set; }
        public string menurefgid { get; set; } 
    }  
}