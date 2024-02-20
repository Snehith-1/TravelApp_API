using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace DataAccess
{
    public class ActivityDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;                
        MySqlDataReader rd;
        public Activity GetAll()
        {            
            Activity activity = new Activity();
            try
            { 
                cmd = new MySqlCommand("sp_sel_activity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<ActivityList>();
                if (rd.HasRows==true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new ActivityList
                        {

                            activity_gid = int.Parse(rd["activity_gid"].ToString()),
                            service_name = rd["service_name"].ToString(),
                            activity_name = rd["activity_name"].ToString(),
                            default_display = rd["default_display"].ToString(),
                            billable = rd["billable"].ToString()
                            //amount = Double.Parse(rd["total_amount"].ToString()
                           
                           
                        });
                    }                    
                    activity.activityList = summary;
                    activity.status = true;
                    rd.Close();

                }
               
                else
                {
                    activity.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception)
            {
                activity.status = false;
                activity.message = "Internal error occured";
                //Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return activity;
        }
        public Activity activityservice(string value)
        {
            Activity activity = new Activity();
            try
            {
                cmd = new MySqlCommand("sp_sel_serviceactivity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_service_name", value);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<ActivityList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new ActivityList
                        {
                            activity_gid = int.Parse(rd["activity_gid"].ToString()),
                            activity_name = rd["activity_name"].ToString() 
                        });
                    }
                    activity.activityList = summary;
                    activity.status = true;
                    
                }
                else
                {
                    activity.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception e)
            {
                activity.status = false;
                activity.message = "Internal Error Occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return activity;
        }

        public Activitydetail Get(int val)
        {
            Activitydetail activitydetail = new Activitydetail();

            try
            {
                cmd = new MySqlCommand("sp_sel_activityedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_gid", val);                
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    activitydetail.activity_gid = int.Parse(rd["activity_gid"].ToString());
                    activitydetail.service_name = rd["service_name"].ToString();
                    activitydetail.activity_name = rd["activity_name"].ToString();
                    activitydetail.default_display = rd["default_display"].ToString();
                    activitydetail.billable = rd["billable"].ToString();
                    //activitydetail.amount = Double.Parse(rd["total_amount"].ToString());
                    activitydetail.service_gid = rd["service_gid"].ToString();          
                    activitydetail.status = true;
                    
                }                
                else
                {
                    activitydetail.status = false;
                    activitydetail.message = "No Records found!";
                    
                }
                rd.Close();
            }
            
            catch
            {
                activitydetail.status = false;
                activitydetail.message = "Internal Error Occured!";
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return activitydetail;
        }
        public Activitymodel Add(Activitydetail val, string userGid)
        {
            try {
                cmd = new MySqlCommand("sp_ins_activity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_name", val.activity_name);
                cmd.Parameters.AddWithValue("p_service_name", val.service_name);
                cmd.Parameters.AddWithValue("p_service_gid", val.service_gid);
                cmd.Parameters.AddWithValue("p_default_display", val.default_display);
                cmd.Parameters.AddWithValue("p_billable", val.billable);
                //cmd.Parameters.AddWithValue("p_amount", val.amount);
                cmd.Parameters.AddWithValue("p_created_by", userGid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    val.status = true;
                    val.message = "Activity added sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal error occured";
                }
            }
            catch (Exception e)
            {
                val.status = false;
                val.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }                        
            return val;
        }
        public Activitymodel Update(ActivityList val, string usergid)
        {
            Activitymodel act = new Activitymodel();
            try
            {
               cmd = new MySqlCommand("sp_upt_activity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_billable", val.billable);
                cmd.Parameters.AddWithValue("p_activity_name", val.activity_name);
                cmd.Parameters.AddWithValue("p_default_display", val.default_display);
                //cmd.Parameters.AddWithValue("p_amount", val.amount);
                cmd.Parameters.AddWithValue("p_activity_gid", val.activity_gid);
                cmd.Parameters.AddWithValue("p_updated_by", usergid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    act.status = true;
                    act.message = "Activity updated succesfully";
                }
                else
                {
                    act.status = false;
                    act.message = "Internal error occured";
                }
            }
            catch(Exception e)
            {
                act.status = false;
                act.message = "Internal error occured";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return act;
        }
        public Activitymodel Delete(int values)
        {
            Activitymodel activitydelete = new Activitymodel();
            try
            {
               cmd = new MySqlCommand("sp_del_activity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_activity_gid", values);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    activitydelete.status = true;
                    activitydelete.message = "Activity deleted successfully";

                }
                else
                {
                    activitydelete.status = false;
                    activitydelete.message = " Internal error occured!";
                }
            }
            catch(Exception e)
            {
                activitydelete.status = false;
                activitydelete.message = " Internal error occured!";
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }                                           
            return activitydelete;
        }
    }
}