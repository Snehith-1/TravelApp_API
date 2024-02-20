using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class banknamemodel
    {
        public bool status { get; set; }
        public string message { get; set; }
     
    }
    public class bankname:banknamemodel
    {
        public List<banknamelist> banknamelist { get; set; }
    }
    public class banknamelist
    {
       public string bank_gid { get; set; }  
       public string bankcode { get; set; }
       public string bankname { get; set; }
    }
    public class banknamedetails:banknamemodel
    {
        public string bank_gid { get; set; }
        public string bankcode { get; set; }
        public string bankname { get; set; }
    }
}