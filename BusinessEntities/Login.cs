using System;


namespace BusinessEntities
{

    public class Login
    {
        public string company_code { get; set; }
        public string user_code { get; set; }
        public string password { get; set; }
        public string country_code { get; set; }
    }


    public class LoginValidateUser
    {
        public bool status { get; set; }
        public string redirect { get; set; }
        public string token_value { get; set; }       
        public string message { get; set; }
        public string user_code { get; set; }
        public string user_gid { get; set; }
        public string department_name { get; set; }
        public string country_gid { get; set; }
        public string currency_gid { get; set; }
    }
}
