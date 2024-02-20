using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class DepartmentManager
    {
        public Department GetAll()
        {
            return new DepartmentDBAccess().GetAll();
        }
    }
}