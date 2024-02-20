using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Activitymodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Activity : Activitymodel
    {
        public List<ActivityList> activityList { get; set; }
    }
    public class ActivityList
    {
        public string service_name { get; set; }
        public string reference_gid { get; set; }
        public string salesorder_gid { get; set; }
        public int activity_gid { get; set; }
        public string service_gid { get; set; }
        public string servicetype { get; set; }
        public string activity_name { get; set; }
        public string default_display { get; set; }
        public string billable { get; set; }
        public double total_amount { get; set; }
        public string activityremark { get; set; }
        public string reference { get; set; }
    }
    public class Activitydetail: Activitymodel
    {
        public string service_name { get; set; }
        public string activityremark { get; set; }
        public string salesorderid { get; set; }
        public string service_type { get; set; }
        public string reference { get; set; }
        public int activity_gid { get; set; }
        public string referencegid { get; set; }
        public string service_gid { get; set; }
        public string activity_name { get; set; }
        public string default_display { get; set; }
        public string billable { get; set; }
        public double total_amount { get; set; }
        public List<ActivityList> activityList { get; set; }
    }
    public class Activitydelete: Activitymodel
    {
     public int activity_gid { get; set; }

    }
    
}