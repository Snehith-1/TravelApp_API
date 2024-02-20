﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class OperationteamDBAccess
    {
        int mnresult = 0;
        MySqlCommand cmd = null;
        string error;     
        public Operationteam GetAll()
        {
            Operationteam operationteam = new Operationteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_operationteam");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Operationteamlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Operationteamlist
                        {

                            operationteam_gid = int.Parse(rd["operationteam_gid"].ToString()),
                            operationteam_code = rd["operationteam_code"].ToString(),
                            operationteam_name = rd["operationteam_name"].ToString(),
                            operationteam_manager_name = rd["operationteam_manager_name"].ToString(),
                            operationteam_employee_name = rd["operationteam_employee_name"].ToString(),
                            operationteam_numberof_employee = rd["operationteam_numberof_employee"].ToString()
                        });
                    }
                    operationteam.operationteamlist = summary;
                    operationteam.status = true;
                    
                }
                else
                {
                    operationteam.status = false;
                    
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                operationteam.status = false;
                operationteam.message = "Internal error occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return operationteam;
        }
        public Operationteammodel Add(Operationteamdetail val, string userGid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_operationteamcodevalidation");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_code", val.operationteam_code);
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.status = false;
                    val.message = "Operationteam code already exist!";
                    
                }
                else
                {
                    cmd = new MySqlCommand("sp_ins_operationteamadd");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_operationteam_name", val.operationteam_name);
                    //cmd.Parameters.AddWithValue("p_optteamemployeename", val.optteammanagername);
                    //cmd.Parameters.AddWithValue("p_optteamemployeegid", val.optteamemployeegid);
                    cmd.Parameters.AddWithValue("p_operationteam_code", val.operationteam_code);
                    cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                    cmd.Parameters.AddWithValue("p_created_by", userGid);
                    //cmd.Connection = con;
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                    if (mnresult == 1)
                    {
                        val.status = true;
                        val.message = "Records added sucessfully";
                    }
                    else
                    {
                        val.status = false;
                        val.message = "Error Occured While Inserting Operation Team";
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Error Occured While Inserting Operation Team";
                error = ex.ToString();
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
        public Operationteammodel Delete(int values)
        {
            Operationteammodel operationteammodel = new Operationteammodel();
            try
            {
                cmd = new MySqlCommand("sp_sel_operationteamdelete");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", values);            
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);              
                    if (rd.HasRows == false)
                    {
                        cmd = new MySqlCommand("sp_sel_operationteammanagerdelete");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_operationteam_gid", values);                        
                        MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                        if (rd1.HasRows == false)
                        {
                            cmd = new MySqlCommand("sp_del_operationteam");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_operationteam_gid", values);
                            //cmd.Connection = con;
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            if (mnresult == 1)
                            {
                                operationteammodel.status = true;
                                operationteammodel.message = "Deleted Successfully";
                                
                                
                            }
                            else
                            {
                                operationteammodel.status = false;
                                operationteammodel.message = "Error Occured While Deleting Operation Team";
                            }
                        }
                    rd1.Close();
                }                                          
                else
                {
                    operationteammodel.status = false;
                    operationteammodel.message = "Cannot Delete Team Assigned with the Employee";
                    
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                operationteammodel.status = false;
                operationteammodel.message = "Cannot Delete Team Assigned with the Employee";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return operationteammodel;
        }
        public Operationteamdetail Get(int val)
        {
            Operationteamdetail Operationteamdetail = new Operationteamdetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_oprationteamedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", val);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    Operationteamdetail.operationteam_gid = int.Parse(rd["operationteam_gid"].ToString());
                    Operationteamdetail.operationteam_name = rd["operationteam_name"].ToString();
                    Operationteamdetail.operationteam_code = rd["operationteam_code"].ToString();
                    Operationteamdetail.status = true;
                    
                }
                else
                {
                    Operationteamdetail.status = false;
                    Operationteamdetail.message = "No Records found!";
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                Operationteamdetail.status = false;
                Operationteamdetail.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Operationteamdetail;
        }
        public Operationteammodel Update(Operationteamdetail val, string usergid)
        {
            Operationteammodel operation = new Operationteammodel();
            try
            {
                cmd = new MySqlCommand("sp_upt_operationteamedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                cmd.Parameters.AddWithValue("p_operationteam_name", val.operationteam_name);
                cmd.Parameters.AddWithValue("p_operationteam_code", val.operationteam_code);
                cmd.Parameters.AddWithValue("p_updated_by", usergid);
                //cmd.Connection = con;
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                if (mnresult == 1)
                {
                    operation.status = true;
                    operation.message = "Operation team updated succesfully";
                }
                else
                {
                    operation.status = false;
                    operation.message = "Error Occured While Updating Operation team!";
                }
            }
            catch (Exception ex)
            {
                operation.status = false;
                operation.message = "Error Occured While Updating Operation team!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return operation;
        }
        public Operationteam optteamemployee(int values)
        {
            Operationteam sof = new Operationteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_optteamunassignemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", values);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<employeelists>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new employeelists
                        {
                            employee_name = rd["employee_name"].ToString(),
                            employee_gid = rd["user_gid"].ToString()
                        });
                    }
                    sof.employeelists = summary;
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_optteamassignemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", values);
                //cmd.Connection = con;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                var sum = new List<Operationteamlist>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        sum.Add(new Operationteamlist
                        {
                            employee_name = rd1["operationteam_employee_name"].ToString(),
                            employee_gid = rd1["operationteam_employee_gid"].ToString()
                        });
                    }

                }
                sof.operationteamlist = sum;
                sof.status = true;
                rd1.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "operationteam Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sof;
        }
        public Operationteam optteammanager(int values)
        {
            Operationteam sof = new Operationteam();
            try
            {
                cmd = new MySqlCommand("sp_sel_optteamunassingmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", values);
                //cmd.Connection = con;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<employeelists>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new employeelists
                        {
                            employee_name = rd["employee_name"].ToString(),
                            employee_gid = rd["user_gid"].ToString()
                        });
                    }
                    sof.employeelists = summary;
                    
                }
                rd.Close();
                cmd = new MySqlCommand("sp_sel_optteamassingmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", values);
                //cmd.Connection = con;
                MySqlDataReader rd1 = DBAccess.ExecuteReader(cmd);
                var sum = new List<Operationteamlist>();
                if (rd1.HasRows == true)
                {
                    while (rd1.Read())
                    {
                        sum.Add(new Operationteamlist
                        {
                            employee_name = rd1["operationteam_employee_name"].ToString(),
                            employee_gid = rd1["operationteam_employee_gid"].ToString()
                        });
                    }

                }
                sof.operationteamlist = sum;
                sof.status = true;
                rd1.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "operationteam Details Not Loaded";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sof;
        }
        public Operationteammodel asignemployeesubmit(Operationteamdetail val, string usergid)
        {

            Operationteammodel employee = new Operationteammodel();
            try
            {
                cmd = new MySqlCommand("sp_del_unassignoptemployee");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                //cmd.Parameters.AddWithValue("p_optteamemployee_gid", emp.employeeid);
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                foreach (var data in val.operationteamlist)
                {
                    cmd = new MySqlCommand("sp_ins_assignoptemployee");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                    cmd.Parameters.AddWithValue("p_optteamemployee_gid", data.employee_gid);
                    cmd.Parameters.AddWithValue("p_operationteam_employee_name", data.employee_name);
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                }             
                //foreach (var emp in val.employeelists)
                //{
                   
                //}
                if (mnresult== 1)
                {
                    employee.status = true;
                    employee.message = "Employee Assigned and un assigned successfully";
                }                
            }
            catch (Exception ex)
            {
                employee.status = true;
                employee.message = "Error occured while assigned  Employee";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return employee;
        }
        public Operationteammodel asignmanagersubmit(Operationteamdetail val, string usergid)
        {
            Operationteammodel manager = new Operationteammodel();
            try
            {
                cmd = new MySqlCommand("sp_del_unassignoptmanager");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                
                mnresult = DBAccess.ExecuteNonQuery(cmd);
                foreach (var data in val.operationteamlist)
                {
                    cmd = new MySqlCommand("sp_ins_assignoptmanager");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_operationteam_gid", val.operationteam_gid);
                    cmd.Parameters.AddWithValue("p_operationteam_manager_gid", data.employee_gid);
                    cmd.Parameters.AddWithValue("p_operationteam_manager_name", data.employee_name);
                    cmd.Parameters.AddWithValue("p_created_by", usergid);
                    ////cmd.Connection = con;
                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                }               
                //foreach (var emp in val.employeelists)
                //{
                   
                //}
                if (mnresult == 1)
                {
                    manager.status = true;
                    manager.message = "Manager Assigned and un assigned successfully";
                    
                }            
            }
            catch (Exception ex)
            {
                manager.status = true;
                manager.message = "Error occured while assigned and un assigned Manager";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return manager;
        }
    }
}