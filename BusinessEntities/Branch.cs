using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Branchmodel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Branch : Branchmodel
    {
        public List<Branchlist> branchlist { get; set; }
    }
    public class Branchlist
    {
        public int branch_gid { get; set; } //branchid changed as branch_gid
        public string branch_code { get; set; }
        public string branch_name { get; set; }

    }
    public class Branchdetails : Branchmodel
    {
        public int branch_gid { get; set; }
        public string branch_code{ get; set; }
        public string branch_name{ get; set; }
        public string created_by { get; set; }
        public string created_date{ get; set;}
        public string updated_by { get; set; }
        public string updated_date { get; set; }


    }

}