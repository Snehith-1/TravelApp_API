using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using BusinessEntities;

namespace DataAccess
{
    public class MenuDBAccess
    {
        MySqlCommand cmd, cmd1 = new MySqlCommand();
        MySqlDataReader rd;        
        MySqlDataAdapter sqlad = new MySqlDataAdapter();
        int i = 0;
        string[] ls_parametername = null;
        string error;
        public menu modulesummary()
        {
            menu mod = new menu();
            try
            {
                sqlad.SelectCommand = new MySqlCommand("sp_sel_mainmenu");
                sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DataTable dt = DBAccess.GetDataTable(sqlad);
                var mainmenu = new List<menumainlist>();
                foreach (DataRow dr in dt.Rows)
                {
                    mainmenu.Add(new menumainlist
                    {
                        module_gid = int.Parse(dr["module_gid"].ToString()),
                        text = dr["module_name"].ToString(),
                        heading = dr["heading"].ToString(),
                        translate = dr["translate"].ToString(),
                        sref = dr["sref"].ToString(),
                        label = dr["label"].ToString(),
                        icon = dr["icon"].ToString(),
                        menulevel = dr["menu_level"].ToString(),
                        menurefgid = dr["menu_ref_gid"].ToString()
                    });                    
                    sqlad.SelectCommand = new MySqlCommand("sp_sel_submenu");
                    sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlad.SelectCommand.Parameters.AddWithValue("p_menu_ref_gid", dr["module_gid"].ToString());
                    DataTable dtr = DBAccess.GetDataTable(sqlad);

                    var submenu = new List<menusublevel>();
                    foreach (DataRow drw in dtr.Rows)
                    {
                        submenu.Add(new menusublevel()
                        {
                            module_gid = int.Parse(drw["module_gid"].ToString()),
                            text = drw["module_name"].ToString(),
                            heading = drw["heading"].ToString(),
                            translate = drw["translate"].ToString(),
                            sref = drw["sref"].ToString(),
                            label = drw["label"].ToString(),
                            icon = drw["icon"].ToString(),
                            menulevel = drw["menu_level"].ToString(),
                            menurefgid = drw["menu_ref_gid"].ToString()
                        });
                    }
                    mainmenu[i].submenu = submenu;
                    i += 1;
                }
                mod.menumainlist = mainmenu;
                mod.status = true;
            }
            catch (Exception ex)
            {
                mod.status = false;
                mod.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (sqlad.SelectCommand.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlad.SelectCommand.Connection.Close();
                }
            }
            return mod;
        }
        public menu userprivilige(menumainlevel val)
        { 
            menu mod = new menu();
            try
            {
                cmd = new MySqlCommand("sp_sel_checkuserprivilege");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", val.user_gid);
                rd = DBAccess.ExecuteReader(cmd);
                cmd.Connection.Close();
                if (rd.HasRows == true)
                {
                    sqlad.SelectCommand = new MySqlCommand("sp_sel_userprivilege");
                    sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlad.SelectCommand.Parameters.AddWithValue("p_user_gid", val.user_gid);
                    DataTable dt = DBAccess.GetDataTable(sqlad);
                    var mainmenu = new List<menumainlist>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        mainmenu.Add(new menumainlist
                        {
                            module_gid = int.Parse(dr["module_gid"].ToString()),
                            text = dr["module_name"].ToString(),
                            heading = dr["heading"].ToString(),
                            translate = dr["translate"].ToString(),
                            sref = dr["sref"].ToString(),
                            label = dr["label"].ToString(),
                            icon = dr["icon"].ToString(),
                            menulevel = dr["menu_level"].ToString(),
                            menurefgid = dr["menu_ref_gid"].ToString()
                        });

                        //mod.menumainlist = mainmenu;
                        sqlad.SelectCommand = new MySqlCommand("sp_sel_submenu_userprevilege");
                        sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlad.SelectCommand.Parameters.AddWithValue("p_menu_ref_gid", dr["module_gid"].ToString());
                        sqlad.SelectCommand.Parameters.AddWithValue("p_user_gid", val.user_gid);
                        DataTable dtr = DBAccess.GetDataTable(sqlad);
                        var submenu = new List<menusublevel>();
                        foreach (DataRow drw in dtr.Rows)
                        {
                            submenu.Add(new menusublevel()
                            {
                                module_gid = int.Parse(drw["module_gid"].ToString()),
                                text = drw["module_name"].ToString(),
                                heading = drw["heading"].ToString(),
                                translate = drw["translate"].ToString(),
                                sref = drw["sref"].ToString(),
                                label = drw["label"].ToString(),
                                icon = drw["icon"].ToString(),
                                menulevel = drw["menu_level"].ToString(),
                                menurefgid = drw["menu_ref_gid"].ToString()
                            });
                        }
                        mainmenu[i].submenu = submenu;
                        i += 1;
                    }
                    mod.menumainlist = mainmenu;
                }
                else
                {
                    sqlad.SelectCommand = new MySqlCommand("sp_sel_departmentprivilege");
                    sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlad.SelectCommand.Parameters.AddWithValue("p_department_gid", val.department_gid);
                    DataTable dt = DBAccess.GetDataTable(sqlad);
                    var mainmenu = new List<menumainlist>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        mainmenu.Add(new menumainlist
                        {
                            module_gid = int.Parse(dr["module_gid"].ToString()),
                            text = dr["module_name"].ToString(),
                            heading = dr["heading"].ToString(),
                            translate = dr["translate"].ToString(),
                            sref = dr["sref"].ToString(),
                            label = dr["label"].ToString(),
                            icon = dr["icon"].ToString(),
                            menulevel = dr["menu_level"].ToString(),
                            menurefgid = dr["menu_ref_gid"].ToString()
                        });

                        //mod.menumainlist = mainmenu;
                        sqlad.SelectCommand = new MySqlCommand("sp_sel_submenu_departmentprevilege");
                        sqlad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlad.SelectCommand.Parameters.AddWithValue("p_menu_ref_gid", dr["module_gid"].ToString());
                        sqlad.SelectCommand.Parameters.AddWithValue("p_department_gid", val.department_gid );
                        DataTable dtr = DBAccess.GetDataTable(sqlad);

                        var submenu = new List<menusublevel>();
                        foreach (DataRow drw in dtr.Rows)
                        {
                            submenu.Add(new menusublevel()
                            {
                                module_gid = int.Parse(drw["module_gid"].ToString()),
                                text = drw["module_name"].ToString(),
                                heading = drw["heading"].ToString(),
                                translate = drw["translate"].ToString(),
                                sref = drw["sref"].ToString(),
                                label = drw["label"].ToString(),
                                icon = drw["icon"].ToString(),
                                menulevel = drw["menu_level"].ToString(),
                                menurefgid = drw["menu_ref_gid"].ToString()
                            });
                        }
                        mainmenu[i].submenu = submenu;
                        i += 1;
                    }
                    mod.menumainlist = mainmenu;
                }              

                mod.status = true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (sqlad.SelectCommand.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlad.SelectCommand.Connection.Close();
                }
            }
            return mod;
        }
        public menuModel assignprivilege(menumainlevel val)
        {
            menuModel getmenu = new menuModel();
            int mnresult; 
            cmd = new MySqlCommand("sp_del_userprivilege");
            cmd.Parameters.AddWithValue("p_user_gid", val.user_gid);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            mnresult = cmd.ExecuteNonQuery();
            if (mnresult == 0)
            { 
            }
            try
            {
                ls_parametername = val.assignedmenus.Split(new string[] { "||" }, StringSplitOptions.None);
                var parametercount = ls_parametername.Count();
                for (int i = 0; i <= parametercount - 1; i++)
                {
                    cmd = new MySqlCommand("sp_ins_userprivilege");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_module_gid", val.module_gid);
                    cmd.Parameters.AddWithValue("p_user_gid", val.user_gid);
                    mnresult = cmd.ExecuteNonQuery();
                }
                if (mnresult == 1)
                {
                    getmenu.status = true;
                    getmenu.message = "Assigned Successfully";
                }

            }
            catch (Exception ex)
            {
                getmenu.status = false;
                getmenu.message = "Assign failed";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return getmenu;
        }
    }
}