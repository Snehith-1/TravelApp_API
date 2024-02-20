using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class DepartmentDBAccess
    {
        
        MySqlCommand cmd;        
        public Department GetAll()
        {
            Department department = new Department();
            try
            {
                cmd = new MySqlCommand("sp_sel_department");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Departmentlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Departmentlist
                        {

                            department_gid = int.Parse(rd["department_gid"].ToString()),
                            department_code = rd["department_code"].ToString(),
                            department_name = rd["department_name"].ToString(),
                        

                        });
                    }
                    department.departmentlist = summary;
                    department.status = true;
                    
                }
                else
                {
                    department.status = false;  
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                department.status = false;
                department.message = "Internal Error Occured";
                string error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return department;
        }
    }
}