using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using BusinessEntities;
using System.Configuration;

namespace DataAccess
{
    public class AIRDBAccess
    {
        int mnresult = 0;

        MySqlCommand cmd = null;
        MySqlDataReader rd, rd1, rd2;
        string error;
        public AIRmodel receiveairdata(AIRDetails val)
        {
            AIRDetails add = new AIRDetails();
            try
            {
                int air_gid = 0;
                foreach (var pnr in val.PNR)
                {
                    cmd = new MySqlCommand("sp_sel_checkpnr_number");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_pnr_number", pnr.pnr_number);
                    // cmd.Parameters.AddWithValue("p_ar_gid", pnr.airgid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        cmd = new MySqlCommand("sp_upt_airfilespnr");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_pnr_number", pnr.pnr_number);
                        cmd.Parameters.AddWithValue("p_air_gid", "0");
                        cmd.Parameters.AddWithValue("p_total_ticket_price", pnr.total_ticket_price);
                        cmd.Parameters.AddWithValue("p_flag", pnr.flag);
                        cmd.Parameters.AddWithValue("p_agent_gid", pnr.agent_gid);//changes made here 
                        cmd.Parameters.AddWithValue("o_air_gid", "");
                        cmd.Parameters["o_air_gid"].Direction = System.Data.ParameterDirection.Output;
                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                        air_gid = Convert.ToInt32(cmd.Parameters["o_air_gid"].Value.ToString());
                        if (mnresult == 1)
                        {
                            //cmd = new MySqlCommand("sp_sel_air_gid");
                            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("p_pnr_number", pnr.pnr_number);
                            //rd1 = DBAccess.ExecuteReader(cmd);
                            //rd1.Read();
                            foreach (var data1 in val.sectordetails)
                            {
                                cmd = new MySqlCommand("sp_ins_sectordetailsupdate");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_air_gid", air_gid);
                                cmd.Parameters.AddWithValue("p_from_place", data1.from_place);
                                cmd.Parameters.AddWithValue("p_to_place", data1.to_place);
                                cmd.Parameters.AddWithValue("p_flight_number", data1.flight_number);
                                cmd.Parameters.AddWithValue("p_takeoff", data1.take_off);
                                cmd.Parameters.AddWithValue("p_landoff", data1.land_off);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                                if (mnresult == 1)
                                {
                                    add.status = true;
                                    add.message = "Sector details Inserted Sucessfully!";
                                }
                                else
                                {
                                    add.status = false;
                                    add.message = "Error Occured While Insert Sector Details";
                                }
                            }
                            foreach (var data2 in val.Passengerdetails)
                            {
                                cmd = new MySqlCommand("sp_ins_passengerdetailsupdate");
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_air_gid", air_gid);
                                cmd.Parameters.AddWithValue("p_e_ticketnumber", data2.ticket_number);
                                cmd.Parameters.AddWithValue("p_passenger_name", data2.passenger_name);
                                mnresult = DBAccess.ExecuteNonQuery(cmd);
                                if (mnresult == 1)
                                {
                                    add.status = true;
                                    add.message = "Passenger details Inserted Sucessfully!";

                                }
                                else
                                {
                                    add.status = false;
                                    add.message = "Error Occured While Insert Passenger Details";
                                }
                            }
                        }
                        //rd1.Close();
                    }
                    else
                    {

                        foreach (var data in val.PNR)
                        {
                            cmd = new MySqlCommand("sp_ins_airticketdetails");
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_pnr_number", data.pnr_number);
                            cmd.Parameters.AddWithValue("p_total_ticket_price", data.total_ticket_price);
                            cmd.Parameters.AddWithValue("p_flag", data.flag);
                            cmd.Parameters.AddWithValue("p_agent_gid", data.agent_gid);
                            cmd.Parameters.AddWithValue("p_air_gid", "");
                            cmd.Parameters["p_air_gid"].Direction = System.Data.ParameterDirection.Output;
                            //rd2 = DBAccess.ExecuteReader(cmd);
                            mnresult = DBAccess.ExecuteNonQuery(cmd);
                            air_gid = Convert.ToInt32(cmd.Parameters["p_air_gid"].Value.ToString());
                            if (mnresult == 1)
                            {
                                mnresult = 1;
                                add.status = true;
                                add.message = "Air ticket details Inserted Sucessfully!";
                                // After inserting the air ticket the SP returns air_gid
                                //air_gid = int.Parse(rd["air_gid"].ToString());
                            }
                            else
                            {
                                mnresult = 0;
                                add.status = false;
                                add.message = "Error Occured While Insert Air Details";
                            }
                            //rd2.Close();
                            if ((mnresult == 1) && (air_gid > 0)) // Only on successful air_gid retrieval
                            {
                                foreach (var data1 in val.sectordetails)
                                {
                                    cmd = new MySqlCommand("sp_ins_sectordetails");
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("p_air_gid", air_gid);
                                    cmd.Parameters.AddWithValue("p_from_place", data1.from_place);
                                    cmd.Parameters.AddWithValue("p_to_place", data1.to_place);
                                    cmd.Parameters.AddWithValue("p_flight_number", data1.flight_number);
                                    cmd.Parameters.AddWithValue("p_take_off", data1.take_off);
                                    cmd.Parameters.AddWithValue("p_land_off", data1.land_off);
                                    mnresult = DBAccess.ExecuteNonQuery(cmd);
                                    if (mnresult == 1)
                                    {
                                        add.status = true;
                                        add.message = "Sector details Inserted Sucessfully!";
                                    }
                                    else
                                    {

                                        add.status = false;
                                        add.message = "Error Occured While Insert Sector Details";
                                    }
                                }
                                if (mnresult == 1)
                                {
                                    foreach (var data2 in val.Passengerdetails)
                                    {
                                        cmd = new MySqlCommand("sp_ins_passengerdetails");
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("p_air_gid", air_gid);
                                        cmd.Parameters.AddWithValue("p_ticket_number", data2.ticket_number);
                                        cmd.Parameters.AddWithValue("p_passenger_name", data2.passenger_name);
                                        mnresult = DBAccess.ExecuteNonQuery(cmd);
                                        if (mnresult == 1)
                                        {
                                            add.status = true;
                                            add.message = "Passenger details Inserted Sucessfully!";
                                        }
                                        else
                                        {
                                            add.status = false;
                                            add.message = "Error Occured While Insert Passenger Details";
                                        }
                                    }
                                }

                            }

                        }
                    }
                    //rd1.Close();
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                add.status = false;
                add.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return add;
        }

        public AIR receiveairdatasummary()
        {
            AIR val = new AIR();
            try
            {
                cmd = new MySqlCommand("sp_sel_receiveairdatasummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<AIRlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new AIRlist
                        {
                            air_gid = int.Parse(rd["air_gid"].ToString()),
                            pnr_number = rd["pnr_number"].ToString(),
                            // Passenger_details = long.Parse(rd["passenger_name"].ToString()),
                            //sector=byte.Parse(rd["sector"].ToString())
                            travel_date = rd["travel_date"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            sector_no = rd["sector_no"].ToString(),
                            flight_number = rd["flight_number"].ToString(),
                            journey = rd["journey"].ToString(),
                            submit_flag=rd["submit_flag"].ToString(),
                            //reference = rd["reference"].ToString(),
                            flag=rd["flag"].ToString(),
                            agent_gid=rd["agent_gid"].ToString(),
                            total_ticket_price = rd["total_ticket_price"].ToString(),
                            air_Line = rd["air_Line"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),





                        });
                    }
                    val.AIRlist = summary;
                    val.status = true;

                }
                val.status = false;
                val.message = "Records Added";
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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
        public AIR raiseairfilesummary()
        {
            AIR val = new AIR();
            try
            {
                cmd = new MySqlCommand("sp_sel_raiseairfilesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<AIRlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new AIRlist
                        {
                            air_gid = int.Parse(rd["air_gid"].ToString()),
                            pnr_number = rd["pnr_number"].ToString(),
                            // Passenger_details = long.Parse(rd["passenger_name"].ToString()),
                            //sector=byte.Parse(rd["sector"].ToString())
                            travel_date = rd["travel_date"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            sector_no = rd["sector_no"].ToString(),
                            flight_number = rd["flight_number"].ToString(),
                            journey = rd["journey"].ToString(),
                            submit_flag =rd["submit_flag"].ToString(),
                            //reference = rd["reference"].ToString(),
                            flag = rd["flag"].ToString(),
                            agent_gid = rd["agent_gid"].ToString(),
                            total_ticket_price = rd["total_ticket_price"].ToString(),
                            air_Line = rd["air_Line"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),
                            invoice_refnumber = rd["invoice_refnumber"].ToString(),


                        });
                    }
                    val.AIRlist = summary;
                    val.status = true;

                }
                val.status = false;
                val.message = "Records Added";
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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
        public Billingdetail invoicedetails(Billingdetail val)
        {
            //AIRDetails val = new AIRDetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_ticketprice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                rd1 = DBAccess.ExecuteReader(cmd);
                rd1.Read();
                val.total_ticket_price = Double.Parse(rd1["total_ticket_price"].ToString());
                val.pnr_number =rd1["pnr_number"].ToString();

                rd1.Close();
                cmd = new MySqlCommand("sp_sel_airfileinvoicedata");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<billinglist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new billinglist
                        {
                            //pnr_number = rd["description"].ToString(),
                            description = rd["description"].ToString(),
                            service_name = rd["service_name"].ToString()
                        });
                    }
                    val.billinglist = summary;
                    val.status = true;

                }
                else
                {
                    val.status = false;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
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

        public AIRDetails airnotification()
        {
            AIRDetails val = new AIRDetails();
            try
            {
                cmd = new MySqlCommand("sp_sel_airfilescount");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    val.airfiles_count = rd["airfilescount"].ToString();
                    val.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
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
    }
}


