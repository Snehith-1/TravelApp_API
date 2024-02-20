using BusinessEntities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LoginManager
    {
        public LoginValidateUser Add(Login login)
        {
            return new LoginDBAccess().Add(login);
        }

       
    }
}
