using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace BusinessLayer
{
    public class TokenManager
    {
        public string GetuserID(string token)
        {

            return new TokenDBAccess().GetuserID(token);
        }

        public string GetcompanyID(string token)
        {

            return new TokenDBAccess().GetcompanyID(token);
        }

        public string GetcompanyCode(string token)
        {

            return new TokenDBAccess().GetcompanyCode(token);
        }

    }
}
