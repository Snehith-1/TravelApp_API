using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class MenuManager
    {
        public menuModel modulesummary()
        {
            return new MenuDBAccess().modulesummary();
        }
        public menuModel userprivilige(menumainlevel val)
        {
            return new MenuDBAccess().userprivilige(val);
        }
        public menuModel assignprivilege(menumainlevel val)
        {
            return new MenuDBAccess().assignprivilege(val);
        }
    }
}