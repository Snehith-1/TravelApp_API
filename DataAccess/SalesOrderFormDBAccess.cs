using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Configuration;
using BusinessEntities;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Threading;
using System.IO;



namespace DataAccess
{
    public class SalesOrderFormDBAccess
    {
        int mnrestult, mnresult1 = 0;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader rd;
        string error;
        public SalesOrderForm Customer()
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_customer");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("p_customer_type",val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Customerlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new Customerlist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["cutomer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            email_address = rd["email_address"].ToString(),
                            national_id = rd["national_id"].ToString(),
                            billing_address = rd["billing_address"].ToString()
                        });
                    }
                    sof.customerList = summary;

                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Customer Details Not Loaded";
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

        public SalesOrderForm Activity(SOActivityList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesorderformtoactivitysummary");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SOActivityList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOActivityList
                        {
                            salesactivity_gid = rd["salesactivity_gid"].ToString(), // serviceid as salesactivity_gid 
                            salesorder_gid = rd["salesorder_gid"].ToString(),
                            service_type = rd["service_type"].ToString(),
                            reference = rd["reference"].ToString(),
                            remarks = rd["remarks"].ToString(),
                            salesactivity_status = rd["salesactivity_status"].ToString(),
                            total_amount = rd["total_amount"].ToString(),
                            vendor_amount = rd["vendor_amount"].ToString(),
                            vendor_company_name = rd["vendor_company_name"].ToString(),

                        });
                    }
                    sof.SOActivityList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Activity Details Not Loaded";
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

     
        public SalesOrderForm Passenger(SOPassengerList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_sopassenger");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SOPassengerList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPassengerList
                        {
                            passengerservice_gid = rd["passengerservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            gender = rd["gender"].ToString(),
                            dob = rd["dob"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            passport_number = rd["passport_number"].ToString(),
                            passport_expirydate = rd["passport_expirydate"].ToString(),
                            //passportissueddate = rd["passportissued_date"].ToString(),
                            status = true

                        });
                    }

                    sof.SOPassengerList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "passenger Details Not Loaded";
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


        public SalesOrderForm EditPassenger(SOPassengerList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditpassenger");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOPassengerList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPassengerList
                        {
                            passengerservice_gid = rd["passengerservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            gender = rd["gender"].ToString(),
                            salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                            passport_number = rd["passport_number"].ToString(),
                            //passportissueddate = rd["passportissued_date"].ToString(),
                            status = true

                        });
                    }

                    sof.SOPassengerList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "passenger Details Not Loaded";
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
        public SalesOrderForm EditPassport(SOPassportList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditpassport");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOPassportList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPassportList
                        {
                            passportservice_gid = rd["passportservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            id_proof = rd["id_proof"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),

                            status = true

                        });
                    }

                    sof.SOPassportList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Passport Details Not Loaded";
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
        public SalesOrderForm vendorpassport(SOPassportList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_vendorinvpassport");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOPassportList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPassportList
                        {
                            passportservice_gid = rd["passportservice_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                          
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            passvendor_name = rd["passvendor_name"].ToString(),
                            pass_vamount = Double.Parse(rd["pass_vamount"].ToString()),
                            status = true

                        });
                    }

                    sof.SOPassportList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Passport Details Not Loaded";
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
        public SalesOrderForm Editvisa(SOVisaList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditvisa");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var visasummary = new List<SOVisaList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        visasummary.Add(new SOVisaList
                        {
                            visaservice_gid = rd["visaservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            country = rd["country"].ToString(),
                            application_date = rd["application_date"].ToString(),
                            expiry_date = rd["expiry_date"].ToString(),
                            visa_period = rd["visa_period"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            status = true

                        });
                    }

                    sof.SOVisaList = visasummary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Visa Details Not Loaded";
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

        public SalesOrderForm Editflight(SOFlightList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditflight");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOFlightList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOFlightList
                        {
                            flightservice_gid = rd["flightservice_gid"].ToString(),
                            flight_number = rd["flight_number"].ToString(),
                            flight_name = rd["flight_name"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            departure_date = rd["departure_date"].ToString(),
                            flight_time = rd["flight_time"].ToString(),
                            flight_from = rd["flight_from"].ToString(),
                            flight_to = rd["flight_to"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            pnr_number = rd["pnr_number"].ToString(),
                            sector_number = rd["sector_number"].ToString(),
                            ticket_number = rd["ticket_number"].ToString(),
                            flight_class = rd["flight_class"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            flight_routing = rd["flight_routing"].ToString(),
                            flighttrip_type = rd["flighttrip_type"].ToString(),
                            segment = rd["segment"].ToString(),
                            status = true

                        });
                    }

                    sof.SOFlightList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Flight Details Not Loaded";
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
        public SalesOrderForm editairinvoice(SOFlightList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditairinvoice");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOFlightList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOFlightList
                        {
                            air_gid = rd["air_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            epax_name = rd["epax_name"].ToString(),
                            eticket_number = rd["eticket_number"].ToString(),
                            epnr_no = rd["epnr_no"].ToString(),
                            eflag = rd["eflag"].ToString(),
                            eagent_gid = rd["eagent_gid"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),

                            status = true

                        });
                    }

                    sof.SOFlightList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Flight Details Not Loaded";
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
        public SalesOrderForm Edithotel(SOHotelList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesedithotel");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOHotelList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOHotelList
                        {
                            hotelservice_gid = rd["hotelservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            hotel_name = rd["hotel_name"].ToString(),
                            category = rd["category"].ToString(),
                            city = rd["city"].ToString(),
                            check_in = rd["check_in"].ToString(),
                            check_out = rd["check_out"].ToString(),
                            total_numberofdays = int.Parse(rd["total_numberofdays"].ToString()),
                            total_numberofpassengers = int.Parse(rd["total_numberofpassengers"].ToString()),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            status = true

                        });
                    }

                    sof.SOHotelList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Hotel Details Not Loaded";
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
        public SalesOrderForm Editcar(SOCarList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditcar");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOCarList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOCarList
                        {
                            carservice_gid = rd["carservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            car_type = rd["car_type"].ToString(),
                            from_date = rd["from_date"].ToString(),
                            to_date = rd["to_date"].ToString(),
                            pickup_city = rd["pickup_city"].ToString(),
                            drop_city = rd["drop_city"].ToString(),
                            numberof_persons = int.Parse(rd["numberof_persons"].ToString()),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            submit_flag = rd["submit_flag"].ToString(),
                            status = true

                        });
                    }

                    sof.SOCarList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Hotel Details Not Loaded";
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
        public SalesOrderForm Editpackages(SOPackageDetailList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditpackages");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOPackageDetailList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPackageDetailList
                        {
                            packageservice_gid = rd["packageservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            package_name= rd["package_name"].ToString(),
                            package_category =rd["package_category"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            remarks = rd["remarks"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            status = true

                        });
                    }

                    sof.SOPackageDetailList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Hotel Details Not Loaded";
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
        public SalesOrderForm Editotherservice(SOOtherServiceDetailList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditotherservice");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOOtherServiceDetailList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOOtherServiceDetailList
                        {
                            otherservice_gid = rd["otherservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),
                            service_name = rd["service_name"].ToString(),
                            total_amount = Double.Parse(rd["total_amount"].ToString()),
                            remarks = rd["remarks"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            status = true

                        });
                    }

                    sof.SOOtherServiceDetailList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Other Service Details Not Loaded";
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
        public SalesOrderForm Editinsurance(SOInsurenceList val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_saleseditinsurance");
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader rd = DBAccess.ExecuteReader(cmd);

                var summary = new List<SOInsurenceList>();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOInsurenceList
                        {
                            insuranceservice_gid = rd["insuranceservice_gid"].ToString(),
                            paymentnote_gid = rd["paymentnote_gid"].ToString(),
                            submit_flag = rd["submit_flag"].ToString(),
                            name = rd["name"].ToString(),
                            dob = rd["dob"].ToString(),
                            arrival_port = rd["arrival_port"].ToString(),
                            start_date = rd["start_date"].ToString(),
                            end_date = rd["end_date"].ToString(),
                            total_amount = double.Parse(rd["total_amount"].ToString()),
                            status = true

                        });
                    }

                    sof.SOInsurenceList = summary;
                    sof.status = true;
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Hotel Details Not Loaded";
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
        public SalesOrderForm Currency()
        {
            SalesOrderForm currency = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_currency");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<CurrencyList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new CurrencyList
                        {
                            currency_gid = int.Parse(rd["currency_gid"].ToString()),
                            currency_code = rd["currency_code"].ToString(),
                            currency_name = rd["currency_name"].ToString(),
                            country_code = rd["country_code"].ToString(),
                            country_name = rd["country_name"].ToString(),
                            currency_status = rd["currency_status"].ToString(),
                            currency_amount = Double.Parse(rd["currency_amount"].ToString()
                           )
                        });
                    }

                    currency.CurrencyList = summary;
                    currency.status = true;
                }
                else
                {
                    currency.status = false;
                    cmd.Connection.Close();

                }
                rd.Close();

            }
            catch (Exception ex)
            {
                currency.status = false;
                currency.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return currency;
        }
        public Customerdetails sofcustomerget(Customerdetails val)
        {
            Customerdetails sof = new Customerdetails();
            string customer_gid = string.Empty;
            string[] arrCustomerData = val.contact_number.Split('|');
            int arrCustomerDataLength = arrCustomerData.Length;
            //string strNationalID = "";
            string strCustomerName = arrCustomerData[0].Trim();


            //if (arrCustomerDataLength > 1)
            //{
            //    strNationalID = arrCustomerData[1].Trim();
            //}


            int flagCustomerExists = 0;

            try
            {
                cmd = new MySqlCommand("sp_sel_customername_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_name", strCustomerName);
                //cmd.Parameters.AddWithValue("p_national_id", strNationalID);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    flagCustomerExists = 1;
                    customer_gid = rd["customer_gid"].ToString();
                }
                else
                {
                    val.status = false;
                    val.message = "ERR078";
                }

                if (flagCustomerExists == 1)
                {

                    cmd = new MySqlCommand("sp_sel_customerid");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_customer_gid", customer_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();
                        sof.customer_gid = rd["customer_gid"].ToString();
                        sof.customer_name = rd["cutomer_name"].ToString();
                        sof.contact_number = rd["contact_number"].ToString();
                        sof.passport_number = rd["passport_number"].ToString();
                        sof.billing_companyname = rd["billing_companyname"].ToString();
                        sof.email_address = rd["email_address"].ToString();
                        sof.national_id = rd["national_id"].ToString();
                        sof.billing_address = rd["billing_address"].ToString();
                        //sof.currency = rd["currency_gid"].ToString();
                    }

                    sof.status = true;
                    sof.message = "Customer Details Loaded Successfully";
                    rd.Close();

                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Customer Details Not Loaded";
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

        public Customerdetails sofpaxget(Customerdetails val)
        {
            Customerdetails sof = new Customerdetails();
            string[] arrCustomerData = val.epax_name.Split('|');
            int arrCustomerDataLength = arrCustomerData.Length;
            string strPNRno = "";
           
            string strpassengerName = arrCustomerData[0].Trim();


            if (arrCustomerDataLength > 1)
            {
                strPNRno = arrCustomerData[1].Trim();
            }


            int flagPassengerrExists = 0;

            try
            {
                cmd = new MySqlCommand("sp_sel_eair_gid");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_epax_name", val.epax_name);
                cmd.Parameters.AddWithValue("p_epnr_no", strPNRno);
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);

                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    flagPassengerrExists = 1;
                   val.air_gid = rd["air_gid"].ToString();
                    strPNRno = rd["epnr_no"].ToString();


                }

                else
                {
                    val.status = false;
                    val.message = "ERR078";
                }

                if (flagPassengerrExists == 1)
                {

                    cmd = new MySqlCommand("sp_sel_eairid");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_epnr_no", strPNRno);
                    cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);

                    rd = DBAccess.ExecuteReader(cmd);
                    if (rd.HasRows == true)
                    {
                        rd.Read();

                       sof.air_gid = rd["air_gid"].ToString();
                        sof.epax_name = rd["epax_name"].ToString();
                        sof.epnr_no = rd["epnr_no"].ToString();
                        sof.eticket_number = rd["eticket_number"].ToString();
                        sof.eflag = rd["eflag"].ToString();
                        sof.eagent_gid = rd["eagent_gid"].ToString();
                     
                        sof.efirstplace_from = rd["efirstplace_from"].ToString();
                        sof.efirstplace_to = rd["efirstplace_to"].ToString();
                        sof.efirststart_time = rd["efirststart_time"].ToString();
                        sof.efirstend_time = rd["efirstend_time"].ToString();
                        sof.esecondplace_from = rd["esecondplace_from"].ToString();
                        sof.esecondplace_to = rd["esecondplace_to"].ToString();
                        sof.esecondstart_time = rd["esecondstart_time"].ToString();
                        sof.esecondend_time = rd["esecondend_time"].ToString();
                        sof.ethirdplace_from = rd["ethirdplace_from"].ToString();
                        sof.ethirdplace_to = rd["ethirdplace_to"].ToString();
                        sof.ethirdstart_time = rd["ethirdstart_time"].ToString();
                        sof.ethirdend_time = rd["ethirdend_time"].ToString();
                        sof.efourthplace_from = rd["efourthplace_from"].ToString();
                        sof.efourthplace_to = rd["efourthplace_to"].ToString();
                        sof.efourthstart_time = rd["efourthstart_time"].ToString();
                        sof.efourthend_time = rd["efourthend_time"].ToString();
                        sof.eoneflight_number = rd["eoneflight_number"].ToString();
                        sof.esecondflight_number = rd["esecondflight_number"].ToString();
                        sof.ethirdflight_number = rd["ethirdflight_number"].ToString();
                        sof.efourthflight_number = rd["efourthflight_number"].ToString();
                        sof.eticket_camount = rd["eticket_camount"].ToString();
                        sof.air_Line = rd["air_Line"].ToString();


                        //sof.currency = rd["currency_gid"].ToString();
                    }

                    sof.status = true;
                    sof.message = "Passenger Details Loaded Successfully";
                    rd.Close();

                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Passenger Details Not Loaded";
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

















        public SalesOrderForm PageLoad(string user_gid)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_pl_salesorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    cmd = new MySqlCommand("sp_sel_salesorder_gid");
                    cmd.Parameters.AddWithValue("p_user_gid", user_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);

                    if (rd.Read())
                    {
                        sof.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                        sof.status = true;
                    }
                    else
                    {
                        sof.status = false;
                    }
                    rd.Close();
                }
            }

            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Internal Error Occured!";
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
        public SalesOrderForm Getall()
        {
            double total = 0;
            SalesOrderForm SOF = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesOrderFormList>();
                while (rd.Read())
                {
                    if (rd["total_amount"] == System.DBNull.Value)
                    {
                        total = 0;

                    }
                    else
                    {
                        total = double.Parse(rd["total_amount"].ToString());
                    }
                    summary.Add(new SalesOrderFormList
                    {
                        salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                        customer_name = rd["customer_name"].ToString(),
                        customer_gid = rd["customer_gid"].ToString(),
                        contact_number = rd["contact_number"].ToString(),
                        total_amount = total,
                        salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                        created_by = rd["created_by"].ToString(),
                        created_date = rd["created_date"].ToString(),
                        branch_name = rd["branch_name"].ToString(),
                        salesorder_status = rd["salesorder_status"].ToString(),

                    });
                }
                rd.Close();
                SOF.SalesOrderList = summary;
                SOF.status = true;
            }

            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }
        public SalesOrderForm salesorderforminvoicesummary()
        {
            SalesOrderForm SOF = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_salesorderinvoicesummary");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SalesOrderFormList>();
                while (rd.Read())
                {
                    summary.Add(new SalesOrderFormList
                    {
                        salesorder_gid = int.Parse(rd["salesorder_gid"].ToString()),
                        customer_name = rd["customer_name"].ToString(),
                        customer_gid = rd["customer_gid"].ToString(),
                        contact_number = rd["contact_number"].ToString(),
                        total_amount = Double.Parse(rd["total_amount"].ToString()),
                        salesorder_refnumber = rd["salesorder_refnumber"].ToString(),
                        created_by = rd["created_by"].ToString(),
                        created_date = rd["created_date"].ToString()
                        
                    });
                }
                rd.Close();
                SOF.SalesOrderList = summary;
                SOF.status = true;
            }

            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }
        public SalesOrderFormModel PassengerAdd(SOPassengerDetail val, string user_gid)
        {
            SalesOrderFormModel passenger = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_sopassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passenger_firstname", val.passenger_firstname);
                cmd.Parameters.AddWithValue("p_passenger_lastname", val.passenger_lastname);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_passport_issueddate", val.passport_issueddate);
                cmd.Parameters.AddWithValue("p_passport_expirydate", val.passport_expirydate);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    passenger.status = true;
                    passenger.message = "Added Successfully!";
                }
                else
                {
                    passenger.status = false;
                    passenger.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return passenger;
        }

        public SalesOrderFormModel SalesInvoicePassengerAdd(SOPassengerDetail val, string user_gid)
        {
            SalesOrderFormModel passenger = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invpassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passenger_firstname", val.passenger_firstname);
                cmd.Parameters.AddWithValue("p_passenger_lastname", val.passenger_lastname);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    passenger.status = true;
                    passenger.message = "Added Successfully!";
                }
                else
                {
                    passenger.status = false;
                    passenger.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return passenger;
        }

        public SalesOrderFormModel VisaAdd(SOVisaDetail val, string user_gid)
        {
            SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_sovisa");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_country", val.country);
                cmd.Parameters.AddWithValue("p_application_date", val.application_date);
                cmd.Parameters.AddWithValue("p_visa_period", val.visa_period);
                cmd.Parameters.AddWithValue("p_expiry_date", val.expiry_date);
                // cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_passport_number", "");
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    visa.status = true;
                }
                else
                {
                    visa.status = false;
                    visa.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return visa;
        }

        public SalesOrderFormModel SalesInvoiceVisaAdd(SOVisaDetail val, string user_gid)
        {
            SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invvisa");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_invoice_refnumber", val.invoice_refnumber);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_country", val.country);
                cmd.Parameters.AddWithValue("p_application_date", val.application_date);
                cmd.Parameters.AddWithValue("p_visa_period", val.visa_period);
                cmd.Parameters.AddWithValue("p_expiry_date", val.expiry_date);
                // cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_passport_number", "");
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_visavendor_name", val.visavendor_name);
                cmd.Parameters.AddWithValue("p_visa_vamount", val.visa_vamount);

                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    visa.status = true;
                }
                else
                {
                    visa.status = false;
                    visa.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Visa Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Visa Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return visa;
        }
        public SalesOrderFormModel FlightAdd(SOFlightDetail val, string user_gid)
        {
            SalesOrderFormModel flight = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_soflight");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_flight_number", val.flight_number);
                cmd.Parameters.AddWithValue("p_flight_name", val.flight_name);
                cmd.Parameters.AddWithValue("p_departure_date", val.departure_date);
                cmd.Parameters.AddWithValue("p_flight_time", val.flight_time);
                cmd.Parameters.AddWithValue("p_flight_from", val.flight_from);
                cmd.Parameters.AddWithValue("p_flight_to", val.flight_to);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                cmd.Parameters.AddWithValue("p_sector_number", val.sector_number);
                cmd.Parameters.AddWithValue("p_flight_class", val.flight_class);
                cmd.Parameters.AddWithValue("p_segment", val.segment);
                cmd.Parameters.AddWithValue("p_flight_airline", "");
                cmd.Parameters.AddWithValue("p_flight_routing", val.flight_routing);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    foreach (var data in val.SOPassengerList)
                    {
                        MySqlCommand filghtdtl = new MySqlCommand("sp_ins_soflightpassengerdtl");
                        filghtdtl.CommandType = System.Data.CommandType.StoredProcedure;
                        filghtdtl.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        filghtdtl.Parameters.AddWithValue("p_passengerservice_gid", data.passengerservice_gid);
                        filghtdtl.Parameters.AddWithValue("p_passenger_firstname", data.passenger_name);
                        // filghtdtl.Parameters.AddWithValue("p_passenger_lastname", data.passenger_lastname);
                        filghtdtl.Parameters.AddWithValue("p_passport_number", data.passport_number);
                        filghtdtl.Parameters.AddWithValue("p_created_by", user_gid);
                        filghtdtl.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                        mnresult1 = DBAccess.ExecuteNonQuery(filghtdtl);
                        if (mnresult1 == 1)
                        {
                            val.status = true;
                            val.message = "Records added sucessfully";
                        }
                    }
                    flight.status = true;
                    flight.message = "Records added sucessfully";
                }
                else
                {
                    flight.status = false;
                    flight.message = "Internal Error Occured!";
                }

            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return flight;
        }
        public SalesOrderFormModel SalesInvoiceFlightAdd(SOFlightDetail val, string user_gid)
        {
            string lsdate = Convert.ToDateTime(val.flight_time).ToString("HH:mm:ss");
            SalesOrderFormModel flight = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invflight");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_flight_number", val.flight_number);
                cmd.Parameters.AddWithValue("p_flight_name", val.flight_name);
                cmd.Parameters.AddWithValue("p_departure_date", val.departure_date);
                cmd.Parameters.AddWithValue("p_flight_time", lsdate);
                cmd.Parameters.AddWithValue("p_flight_from", val.flight_from);
                cmd.Parameters.AddWithValue("p_flight_to", val.flight_to);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                cmd.Parameters.AddWithValue("p_sector_number", val.sector_number);
                cmd.Parameters.AddWithValue("p_flight_class", val.flight_class);
                cmd.Parameters.AddWithValue("p_segment", val.segment);
                cmd.Parameters.AddWithValue("p_flight_airline", "");
                cmd.Parameters.AddWithValue("p_flight_routing", val.flight_routing);
                cmd.Parameters.AddWithValue("p_flighttrip_type", val.flighttrip_type);
                cmd.Parameters.AddWithValue("p_ticketvendor_name", val.ticketvendor_name);
                cmd.Parameters.AddWithValue("p_ticket_vamount", val.ticket_vamount);
                cmd.Parameters.AddWithValue("p_flight_fare", val.flight_fare);
                cmd.Parameters.AddWithValue("p_flight_comm", val.flight_comm);
                cmd.Parameters.AddWithValue("p_flight_sc", val.flight_sc);
                cmd.Parameters.AddWithValue("p_flight_xt", val.flight_xt);
                cmd.Parameters.AddWithValue("p_flight_totalcalcamount", val.flight_totalcalcamount);

                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    
                    {
                        MySqlCommand filghtdtl = new MySqlCommand("sp_ins_soflightpassengerdtl");
                        filghtdtl.CommandType = System.Data.CommandType.StoredProcedure;
                        filghtdtl.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                        filghtdtl.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                        filghtdtl.Parameters.AddWithValue("p_passenger_firstname", val.passenger_name);
                        // filghtdtl.Parameters.AddWithValue("p_passenger_lastname", data.passenger_lastname);
                        filghtdtl.Parameters.AddWithValue("p_passport_number", val.passport_number);
                        filghtdtl.Parameters.AddWithValue("p_created_by", user_gid);
                        filghtdtl.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                        mnresult1 = DBAccess.ExecuteNonQuery(filghtdtl);
                        if (mnresult1 == 1)
                        {
                            val.status = true;
                            val.message = "Records added sucessfully";
                        }
                    }
                    flight.status = true;
                    flight.message = "Records added sucessfully";
                }
                else
                {
                    flight.status = false;
                    flight.message = "Internal Error Occured!";
                }

            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Ticket Service:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Ticket service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return flight;
        }
        public SalesOrderFormModel SalesAirInvoice(SOFlightDetail val, string user_gid)
        {
            SalesOrderFormModel airinvoice = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invairinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_etrip_type", val.etrip_type);
                cmd.Parameters.AddWithValue("p_esegment", val.esegment);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_epax_name", val.epax_name);
                cmd.Parameters.AddWithValue("p_eticket_number", val.eticket_number);
                cmd.Parameters.AddWithValue("p_epnr_no", val.epnr_no);
                cmd.Parameters.AddWithValue("p_eflag", val.eflag);
                cmd.Parameters.AddWithValue("p_eagent_gid", val.eagent_gid);
                cmd.Parameters.AddWithValue("p_eairline", val.eairline);
                cmd.Parameters.AddWithValue("p_esystem_use", val.esystem_use);
                cmd.Parameters.AddWithValue("p_efirstplace_from", val.efirstplace_from);
                cmd.Parameters.AddWithValue("p_efirstplace_to", val.efirstplace_to);
                cmd.Parameters.AddWithValue("p_efirststart_time", val.efirststart_time);
                cmd.Parameters.AddWithValue("p_efirstend_time", val.efirstend_time);
                cmd.Parameters.AddWithValue("p_esecondplace_from", val.esecondplace_from);
                cmd.Parameters.AddWithValue("p_esecondplace_to", val.esecondplace_to);
                cmd.Parameters.AddWithValue("p_esecondstart_time", val.esecondstart_time);
                cmd.Parameters.AddWithValue("p_esecondend_time", val.esecondend_time);
                cmd.Parameters.AddWithValue("p_ethirdplace_from", val.ethirdplace_from);
                cmd.Parameters.AddWithValue("p_ethirdplace_to", val.ethirdplace_to);
                cmd.Parameters.AddWithValue("p_ethirdstart_time", val.ethirdstart_time);
                cmd.Parameters.AddWithValue("p_ethirdend_time", val.ethirdend_time);
                cmd.Parameters.AddWithValue("p_efourthplace_from", val.efourthplace_from);
                cmd.Parameters.AddWithValue("p_efourthplace_to", val.efourthplace_to);
                cmd.Parameters.AddWithValue("p_efourthstart_time", val.efourthstart_time);
                cmd.Parameters.AddWithValue("p_efourthend_time", val.efourthend_time);
                cmd.Parameters.AddWithValue("p_eticket_camount", val.eticket_camount);
                cmd.Parameters.AddWithValue("p_evendor_name", val.evendor_name);
                cmd.Parameters.AddWithValue("p_evendor_vamount", val.evendor_vamount);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_flight_fare", val.flight_fare);
                cmd.Parameters.AddWithValue("p_flight_comm", val.flight_comm);
                cmd.Parameters.AddWithValue("p_flight_sc", val.flight_sc);
                cmd.Parameters.AddWithValue("p_flight_xt", val.flight_xt); 
                cmd.Parameters.AddWithValue("p_flight_totalcalcamount", val.flight_totalcalcamount);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    airinvoice.status = true;
                }
                else
                {
                    airinvoice.status = false;
                    airinvoice.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("AIR File:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while AIR File Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return airinvoice;
        }
        public SalesOrderFormModel HotelAdd(SOHotelDetail val, string user_gid)
        {
            SalesOrderFormModel hotel = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_sohotel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_hotel_name", val.hotel_name);
                cmd.Parameters.AddWithValue("p_category", val.category);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_destination", val.destination);
                cmd.Parameters.AddWithValue("p_check_in", val.check_in);
                cmd.Parameters.AddWithValue("p_check_out", val.check_out);
                cmd.Parameters.AddWithValue("p_total_numberofdays", val.total_numberofdays);
                cmd.Parameters.AddWithValue("p_total_numberofpassengers", val.total_numberofpassengers);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    hotel.status = true;
                }
                else
                {
                    hotel.status = false;
                    hotel.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return hotel;
        }

        public SalesOrderFormModel SalesInvoiceHotelAdd(SOHotelDetail val, string user_gid)
        {
            SalesOrderFormModel hotel = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invhotel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_hotel_name", val.hotel_name);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_category", val.category);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_destination", val.destination);
                cmd.Parameters.AddWithValue("p_check_in", val.check_in);
                cmd.Parameters.AddWithValue("p_check_out", val.check_out);
                cmd.Parameters.AddWithValue("p_total_numberofdays", val.total_numberofdays);
                cmd.Parameters.AddWithValue("p_total_numberofpassengers", val.total_numberofpassengers);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_hotelvendor_name", val.hotelvendor_name);
                cmd.Parameters.AddWithValue("p_hotel_vamount", val.hotel_vamount);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    hotel.status = true;
                }
                else
                {
                    hotel.status = false;
                    hotel.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Hotel Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Hotel Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return hotel;
        }
        public SalesOrderFormModel CarAdd(SOCarDetail val, string user_gid)
        {
            SalesOrderFormModel car = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_socar");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_car_type", val.car_type);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_pickup_city", val.pickup_city);
                cmd.Parameters.AddWithValue("p_drop_city", val.drop_city);
                cmd.Parameters.AddWithValue("p_numberof_persons", val.numberof_persons);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    car.status = true;
                }
                else
                {
                    car.status = false;
                    car.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return car;
        }
        public SalesOrderFormModel SalesInvoiceCarAdd(SOCarDetail val, string user_gid)
        {
            SalesOrderFormModel car = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invcar");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_car_type", val.car_type);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_pickup_city", val.pickup_city);
                cmd.Parameters.AddWithValue("p_drop_city", val.drop_city);
                cmd.Parameters.AddWithValue("p_numberof_persons", val.numberof_persons);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_carvendor_name", val.carvendor_name);
                cmd.Parameters.AddWithValue("p_car_vamount", val.car_vamount);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    car.status = true;
                }
                else
                {
                    car.status = false;
                    car.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Car Rental Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Car Rental Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return car;
        }
        public SalesOrderFormModel ForexAdd(SOForexDetail val, string user_gid)
        {
            SalesOrderFormModel forex = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_soforex");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customerpaid_amount", val.customerpaid_amount);
                cmd.Parameters.AddWithValue("p_customerreceived_amount", val.customerreceived_amount);
                cmd.Parameters.AddWithValue("p_total_paidamount", val.total_paidamount);
                cmd.Parameters.AddWithValue("p_total_receivedamount", val.total_receivedamount);
                cmd.Parameters.AddWithValue("p_paidamount_exchangerate", val.paidamount_exchangerate);
                cmd.Parameters.AddWithValue("p_receivedamount_exchangerate", val.receivedamount_exchangerate);
                cmd.Parameters.AddWithValue("p_paidamount_currency", val.paidamount_currency);
                cmd.Parameters.AddWithValue("p_receivedamount_currency", val.receivedamount_currency);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    forex.status = true;
                }
                else
                {
                    forex.status = false;
                    forex.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return forex;
        }
        public SalesOrderFormModel SalesInvoiceForexAdd(SOForexDetail val, string user_gid)
        {
            SalesOrderFormModel forex = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invforex");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customerpaid_amount", val.customerpaid_amount);
                cmd.Parameters.AddWithValue("p_customerreceived_amount", val.customerreceived_amount);
                cmd.Parameters.AddWithValue("p_total_paidamount", val.total_paidamount);
                cmd.Parameters.AddWithValue("p_total_receivedamount", val.total_receivedamount);
                cmd.Parameters.AddWithValue("p_paidamount_exchangerate", val.paidamount_exchangerate);
                cmd.Parameters.AddWithValue("p_receivedamount_exchangerate", val.receivedamount_exchangerate);
                cmd.Parameters.AddWithValue("p_paidamount_currency", val.paidamount_currency);
                cmd.Parameters.AddWithValue("p_receivedamount_currency", val.receivedamount_currency);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    forex.status = true;
                }
                else
                {
                    forex.status = false;
                    forex.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return forex;
        }
        public SalesOrderFormModel InsuranceAdd(SOInsurenceDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_soinsurance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_name", val.name);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_arrival_port", val.arrival_port);
                cmd.Parameters.AddWithValue("p_start_date", val.start_date);
                cmd.Parameters.AddWithValue("p_end_date", val.end_date);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        public SalesOrderFormModel SalesInvoiceInsuranceAdd(SOInsurenceDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invinsurance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_name", val.name);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_arrival_port", val.arrival_port);
                cmd.Parameters.AddWithValue("p_start_date", val.start_date);
                cmd.Parameters.AddWithValue("p_end_date", val.end_date);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_insvendor_name", val.insvendor_name);
                cmd.Parameters.AddWithValue("p_insurance_vamount", val.insurance_vamount);
                cmd.Parameters.AddWithValue("p_insurance_type", val.insurance_type);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Insurance Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Insurance Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        public SalesOrderFormModel overallsubmit(SalesOrderFormList val, string user_gid)
        {

            SalesOrderFormModel sub = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_salesorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customer_name", "");
                cmd.Parameters.AddWithValue("p_contact_number", "");
                cmd.Parameters.AddWithValue("p_national_id", "");
                cmd.Parameters.AddWithValue("p_email_address", "");
                cmd.Parameters.AddWithValue("p_customer_gid", val.customer_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_net_amount", val.net_amount);
                cmd.Parameters.AddWithValue("p_discount_amount", val.discount_amount);
                cmd.Parameters.AddWithValue("p_advance_paid", val.advance_paid);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_branch_gid", "");
                cmd.Parameters.AddWithValue("p_branch_name", "");
                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    sub.status = true;
                    sub.message = "Sales Order Added Sucessfully";
                }
                else
                {
                    sub.status = false;
                    sub.message = "Error Occurred While Adding Sales Order";
                }
            }
            catch (Exception ex)
            {
                sub.status = false;
                sub.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sub;
        }
        public SalesOrderFormModel soeditsubmit(SalesOrderFormList val, string user_gid)
        {

            SalesOrderFormModel sub = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_salesorder");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_updated_by", user_gid);
                cmd.Parameters.AddWithValue("p_branch_gid", "");
                cmd.Parameters.AddWithValue("p_branch_name", "");
                mnrestult = DBAccess.ExecuteNonQuery(cmd);

                if (mnrestult == 1)
                {
                    sub.status = true;
                    sub.message = "Sales Order Added Sucessfully";
                }
                else
                {
                    sub.status = false;
                    sub.message = "Error Occurred While Adding Sales Order";
                }
            }
            catch (Exception ex)
            {
                sub.status = false;
                sub.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return sub;
        }
        public SalesOrderForm Alltmpdelete(int salesorder_gid)
        {
            SalesOrderForm Alltmpdelete = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_del_allsotemptables");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    Alltmpdelete.status = true;
                    Alltmpdelete.message = "All Temp Tables Deleted Successfully";

                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return Alltmpdelete;
        }

        public SalesOrderFormModel sopackageadd(SOPackageDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_sopackage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_upload_documents", "0");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }

        public SalesOrderFormModel SalesInvoicepackageadd(SOPackageDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invpackage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_upload_documents", "0");
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_package_name", val.package_name);
                cmd.Parameters.AddWithValue("p_package_category", val.package_category);
                cmd.Parameters.AddWithValue("p_totalnoPassenger", val.totalnoPassenger);
                cmd.Parameters.AddWithValue("p_packagevendor_name", val.packagevendor_name);
                cmd.Parameters.AddWithValue("p_package_vamount", val.package_vamount);
                cmd.Parameters.AddWithValue("p_period", val.period);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_country", val.country);


                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Package Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Package Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        public SalesOrderFormModel salesinvoiceotherservices(SOOtherServiceDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invotherservices");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                cmd.Parameters.AddWithValue("p_service_name", val.service_name);
                cmd.Parameters.AddWithValue("p_otherservicevendor_name", val.otherservicevendor_name);
                cmd.Parameters.AddWithValue("p_otherServices_vamount", val.otherServices_vamount);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Other Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Other Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        //public SalesOrderFormModel PackageAdd(string companycode, HttpRequest httpRequest, string user_gid)
        //{
        //    SOPackageDetail package = new SOPackageDetail();            
        //    HttpPostedFile httpPostedFile;
        //    HttpFileCollection httpFileCollection;
        //    try
        //    {
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            httpFileCollection = httpRequest.Files;

        //            for (int i = 0; i < httpFileCollection.Count; i++)
        //            {
        //                httpPostedFile = httpFileCollection[i];
        //                string FileExtension = httpPostedFile.FileName;
        //                FileExtension = Path.GetExtension(FileExtension).ToLower();
        //                package.packageuploaddocs = HttpContext.Current.Server.MapPath("../../" + companycode+ConfigurationManager.AppSettings["package_file_path"].ToString() + httpPostedFile.FileName);
        //                httpPostedFile.SaveAs(package.packageuploaddocs);
        //            }
        //            if (File.Exists(package.packageuploaddocs))
        //            {
        //                cmd = new MySqlCommand("sp_ins_sopackage");
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_salesorder_gid", httpRequest.Form["salesorder_gid"]);
        //                cmd.Parameters.AddWithValue("p_remarks", httpRequest.Form["packageremarks"]);
        //                cmd.Parameters.AddWithValue("p_upload_documents", package.packageuploaddocs);
        //                cmd.Parameters.AddWithValue("p_created_by",user_gid);
        //                cmd.Parameters.AddWithValue("p_amount", httpRequest.Form["packageamount"]);
        //                cmd.Parameters.AddWithValue("p_reference_gid", 0);
        //                mnrestult = DBAccess.ExecuteNonQuery(cmd);

        //                if (mnrestult == 1)
        //                {
        //                    package.status = true;
        //                    package.message = "File Uploaded Successfully!";
        //                }
        //                else
        //                {
        //                    package.status = false;
        //                    package.message = "Error Occured While Uploading file!";
        //                }                       
        //            }
        //            else
        //            {
        //                package.status = false;
        //                package.message = "Error Occured While uploading file!";
        //            }
        //        }
        //        else
        //        {
        //            package.status = false;
        //            package.message = "File Not Available!";
        //        }
        //    }

        //    catch (Exception ex)
        //    { }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return package;
        //}
        public SalesOrderFormModel PassportUploadDocument(SOPassportDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_sopassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_id_proof", val.id_proof);
                cmd.Parameters.AddWithValue("p_additional_proof", val.additional_proof);
                cmd.Parameters.AddWithValue("p_photo", val.photo);
                cmd.Parameters.AddWithValue("p_upload_documents", "0");
                cmd.Parameters.AddWithValue("p_anygovt_document", val.anygovt_document);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        public SalesOrderFormModel salesinvoicepassport(SOPassportDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_ins_invpassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_id_proof", val.id_proof);
                cmd.Parameters.AddWithValue("p_additional_proof", val.additional_proof);
                cmd.Parameters.AddWithValue("p_photo", val.photo);
                cmd.Parameters.AddWithValue("p_upload_documents", "0");
                cmd.Parameters.AddWithValue("p_anygovt_document", val.anygovt_document);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_passvendor_name", val.passvendor_name);
                cmd.Parameters.AddWithValue("p_pass_vamount", val.pass_vamount);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    insurance.status = true;
                }
                else
                {
                    insurance.status = false;
                    insurance.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Passport Service Type:" + val.salesorder_gid);
                    sw.WriteLine("Error: Error occured while Adding Passport Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);
                }
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return insurance;
        }
        //public SalesOrderFormModel GetpassdocumentUploadExcel(string companycode, HttpRequest httpRequest, string user_gid)
        //{
        //    SOPassportDetail GetpassdocumentImportExcel = new SOPassportDetail();
        //    HttpPostedFile httpPostedFile;
        //    HttpFileCollection httpFileCollection;
        //    try
        //    {
        //        //if (httpRequest.Files.Count > 0)
        //        //{
        //            httpFileCollection = httpRequest.Files;

        //            for (int i = 0; i < httpFileCollection.Count; i++)
        //            {
        //                httpPostedFile = httpFileCollection[i];
        //                string FileExtension = httpPostedFile.FileName;
        //                FileExtension = Path.GetExtension(FileExtension).ToLower();
        //                GetpassdocumentImportExcel.uploaddocument = HttpContext.Current.Server.MapPath("../../" + companycode+ConfigurationManager.AppSettings["Passport_file_path"].ToString() + httpPostedFile.FileName);
        //                httpPostedFile.SaveAs(GetpassdocumentImportExcel.uploaddocument);
        //            }
        //            if (File.Exists(GetpassdocumentImportExcel.uploaddocument))
        //            {
        //                cmd = new MySqlCommand("sp_ins_sopassport");
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("p_salesorder_gid", httpRequest.Form["salesorder_gid"]);
        //                cmd.Parameters.AddWithValue("p_passenger_name", httpRequest.Form["passportpassengername"]);
        //                cmd.Parameters.AddWithValue("p_id_proof", httpRequest.Form["passportidproof"]);
        //                cmd.Parameters.AddWithValue("p_additional_proof", httpRequest.Form["passportadditionalproof"]);
        //                cmd.Parameters.AddWithValue("p_photo", httpRequest.Form["photocount"]);
        //                cmd.Parameters.AddWithValue("p_upload_documents", GetpassdocumentImportExcel.uploaddocument);
        //                cmd.Parameters.AddWithValue("p_anygovt_document", httpRequest.Form["passportgovtdocument"]);
        //                cmd.Parameters.AddWithValue("p_amount", httpRequest.Form["passportamount"]);
        //                cmd.Parameters.AddWithValue("p_tmppassengerservice_gid", httpRequest.Form["passenger_gid"]);
        //                cmd.Parameters.AddWithValue("p_created_by", user_gid);
        //                cmd.Parameters.AddWithValue("p_reference_gid", 0);
        //                //cmd.Connection = con;
        //                mnrestult = DBAccess.ExecuteNonQuery(cmd);

        //                if (mnrestult == 1)
        //                {
        //                    GetpassdocumentImportExcel.status = true;
        //                    GetpassdocumentImportExcel.message = "File Uploaded Successfully!";
        //                }
        //                else
        //                {
        //                    GetpassdocumentImportExcel.status = false;
        //                    GetpassdocumentImportExcel.message = "Error Occured While Uploading file!";
        //                }                       
        //            }
        //            else
        //            {
        //                GetpassdocumentImportExcel.status = false;
        //                GetpassdocumentImportExcel.message = "File Not Detected!";
        //            }
        //        //}
        //        //else
        //        //{
        //        //    GetpassdocumentImportExcel.status = false;
        //        //    GetpassdocumentImportExcel.message = "Error Occured While uploading file!";
        //        //}
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == System.Data.ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return GetpassdocumentImportExcel;
        //}
        public SalesOrderForm getpassportno(int val)
        {
            SalesOrderForm SOF = new SalesOrderForm();
            try
            {
                cmd = new MySqlCommand("sp_sel_flightpassportno");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Connection = con;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<SOPassengerList>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        summary.Add(new SOPassengerList
                        {
                            name = rd["name"].ToString(),
                            passport_number = rd["passport_number"].ToString(),
                            tmppassengerservice_gid = rd["tmppassengerservice_gid"].ToString()
                        });
                    }
                    SOF.SOPassengerList = summary;
                    SOF.status = true;
                    SOF.message = "Records successfully selected!";
                }
                else
                {
                    SOF.status = false;
                    SOF.message = "No records found!";
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }



        public SOPassengerDetail getpassenger(string val)
        {
            SOPassengerDetail SOF = new SOPassengerDetail();
            try
            {
                cmd = new MySqlCommand("sp_sel_getpassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_tmppassengerservice_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    rd.Read();
                    SOF.passenger_firstname = rd["first_name"].ToString();
                    SOF.passenger_lastname = rd["last_name"].ToString();
                    SOF.gender = rd["gender"].ToString();
                    SOF.dob = rd["dob"].ToString();
                    SOF.passport_number = rd["passport_number"].ToString();
                    SOF.passport_issueddate = rd["passport_issueddate"].ToString();
                    SOF.passport_expirydate = rd["passport_expirydate"].ToString();
                    SOF.status = true;

                }
                else
                {
                    SOF.status = false;
                    SOF.message = "No records found!";
                }
                rd.Close();
            }

            catch (Exception ex)
            {
                SOF.status = false;
                SOF.message = "Internal Error Occured!";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return SOF;
        }
        public SalesOrderFormModel salesorderformpasdel(SOPassengerDetail values)
        {
            SalesOrderFormModel delete = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_del_salesorderformpasdel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_tmppassengerservice_gid", values.tmppassengerservice_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    delete.status = true;
                }
                else
                {
                    delete.status = false;
                }
            }
            catch (Exception ex)
            {
                delete.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return delete;
        }
        public SalesOrderFormModel salesorderformcustomerinvoicedel(SOPassengerDetail values)
        {
            SalesOrderFormModel delete = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_del_salesorderformcustomerinvoicedel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_tmpsalesactivity_gid", values.salesactivity_gid);
                cmd.Parameters.AddWithValue("p_service_type", "");
                cmd.Parameters.AddWithValue("p_reference_gid", "0");
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    delete.status = true;
                }
                else
                {
                    delete.status = false;
                }
            }
            catch (Exception ex)
            {
                delete.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return delete;
        }
        public SalesOrderFormModel salesorderformcancel(int salesorder_gid)
        {
            SalesOrderFormModel cancel = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_salesorderformcancel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    cancel.status = true;
                }
                else
                {
                    cancel.status = false;
                }
            }
            catch (Exception ex)
            {
                cancel.status = false;
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }

            return cancel;
        }

        public SalesOrderForm soservicetypeget(int salesorder_gid, int service_gid)
        {
            SalesOrderForm sof = new SalesOrderForm();

            try
            {
                if (service_gid == 1)
                {
                    cmd = new MySqlCommand("sp_sel_sopassport");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var passportsummary = new List<SOPassportList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            passportsummary.Add(new SOPassportList
                            {
                                passportservice_gid = rd["passportservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                passenger_name = rd["passenger_name"].ToString(),
                                id_proof = rd["id_proof"].ToString(),
                                total_amount = Double.Parse(rd["total_amount"].ToString())

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOPassportList = passportsummary;
                        sof.status = true;
                    }
                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }

                else if (service_gid == 2)
                {
                    cmd = new MySqlCommand("sp_sel_sovisa");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var visasummary = new List<SOVisaList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            visasummary.Add(new SOVisaList
                            {
                                visaservice_gid = rd["visaservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),

                                passenger_name = rd["passenger_name"].ToString(),
                                country = rd["country"].ToString(),
                                application_date = rd["application_date"].ToString(),
                                expiry_date = rd["expiry_date"].ToString(),
                                visa_period = rd["visa_period"].ToString(),
                                total_amount = Double.Parse(rd["total_amount"].ToString())

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOVisaList = visasummary;
                        sof.status = true;
                    }

                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }

                else if (service_gid == 3)
                {
                    cmd = new MySqlCommand("sp_sel_soflight");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var flightsummary = new List<SOFlightList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            flightsummary.Add(new SOFlightList
                            {
                                flightservice_gid = rd["flightservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                flight_number = rd["flight_number"].ToString(),
                                flight_name = rd["flight_name"].ToString(),
                                passenger_name = rd["passenger_name"].ToString(),       
                                departure_date = rd["departure_date"].ToString(),
                                flight_time = rd["flight_time"].ToString(),
                                flight_from = rd["flight_from"].ToString(),
                                flight_to = rd["flight_to"].ToString(),
                                total_amount = double.Parse(rd["total_amount"].ToString()),
                                pnr_number = rd["pnr_number"].ToString(),
                                sector_number = rd["sector_number"].ToString(),
                                ticket_number = rd["ticket_number"].ToString(),
                                flight_class = rd["flight_class"].ToString(),
                                flight_routing = rd["flight_routing"].ToString(),

                                segment = rd["segment"].ToString()
                            });
                        }


                        cmd.Connection.Close();
                        sof.SOFlightList = flightsummary;
                        sof.status = true;
                    }

                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }

                else if (service_gid == 4)
                {
                    cmd = new MySqlCommand("sp_sel_sohotel");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var hotelsummary = new List<SOHotelList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            hotelsummary.Add(new SOHotelList
                            {
                                hotelservice_gid = rd["hotelservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),

                                hotel_name = rd["hotel_name"].ToString(),
                                category = rd["category"].ToString(),
                                passenger_name = rd["passenger_name"].ToString(),
                                remarks = rd["remarks"].ToString(),

                                city = rd["city"].ToString(),
                                check_in = rd["check_in"].ToString(),
                                check_out = rd["check_out"].ToString(),
                                total_numberofdays = int.Parse(rd["total_numberofdays"].ToString()),
                                total_numberofpassengers = int.Parse(rd["total_numberofpassengers"].ToString()),
                                total_amount = Double.Parse(rd["total_amount"].ToString())

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOHotelList = hotelsummary;
                        sof.status = true;
                    }

                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }

                else if (service_gid == 5)
                {
                    cmd = new MySqlCommand("sp_sel_socar");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var carsummary = new List<SOCarList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            carsummary.Add(new SOCarList
                            {
                                carservice_gid = rd["carservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),

                                car_type = rd["car_type"].ToString(),
                                from_date = rd["from_date"].ToString(),
                                to_date = rd["to_date"].ToString(),
                                pickup_city = rd["pickup_city"].ToString(),
                                drop_city = rd["drop_city"].ToString(),
                                numberof_persons = int.Parse(rd["numberof_persons"].ToString()),
                                total_amount = Double.Parse(rd["total_amount"].ToString())

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOCarList = carsummary;
                        sof.status = true;
                    }

                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }

                else if (service_gid == 6)
                {
                    cmd = new MySqlCommand("sp_sel_soairinvoice");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var airinvoicesummary = new List<SOFlightList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            airinvoicesummary.Add(new SOFlightList
                            {
                                air_gid = rd["air_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                epax_name = rd["epax_name"].ToString(),
                                eticket_number = rd["eticket_number"].ToString(),
                                epnr_no = rd["epnr_no"].ToString(),
                                eflag = rd["eflag"].ToString(),
                                eagent_gid = rd["eagent_gid"].ToString(),
                                total_amount = double.Parse(rd["total_amount"].ToString())


                            });
                        }


                        cmd.Connection.Close();
                        sof.SOFlightList = airinvoicesummary;
                        sof.status = true;
                    }

                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }
                //else if (service_gid == 6)
                //{
                //    cmd = new MySqlCommand("sp_sel_soforex");
                //    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    rd = DBAccess.ExecuteReader(cmd);
                //    var forexsummary = new List<SOForexList>();
                //    if (rd.HasRows == true)
                //    {
                //        while (rd.Read())
                //        {
                //            forexsummary.Add(new SOForexList
                //            {
                //                forexservice_gid = rd["forexservice_gid"].ToString(),
                //                paidamount_currency = rd["paidamount_currency"].ToString(),
                //                customerpaid_amount = double.Parse(rd["customerpaid_amount"].ToString()),
                //                total_paidamount = double.Parse(rd["total_paidamount"].ToString()),
                //                receivedamount_currency = rd["receivedamount_currency"].ToString(),
                //                customerreceived_amount = double.Parse(rd["customerreceived_amount"].ToString()),
                //                total_receivedamount = int.Parse(rd["total_receivedamount"].ToString())

                //            });
                //        }

                //        cmd.Connection.Close();
                //        sof.SOForexList = forexsummary;
                //        sof.status = true;
                //    }

                //    else
                //    {
                //        cmd.Connection.Close();
                //    }
                //    rd.Close();
                //}

                else if (service_gid == 7)
                {
                    cmd = new MySqlCommand("sp_sel_sopackage");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var packagesummary = new List<SOPackageDetailList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            packagesummary.Add(new SOPackageDetailList
                            {
                                packageservice_gid = rd["packageservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                package_name=rd["package_name"].ToString(),
                                package_category = rd["package_category"].ToString(),
                                total_amount = Double.Parse(rd["total_amount"].ToString()),
                                remarks = rd["remarks"].ToString()

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOPackageDetailList = packagesummary;
                        sof.status = true;
                    }
                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }
                else if (service_gid == 10)
                {
                    cmd = new MySqlCommand("sp_sel_sootherservices");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var otherservicesummary = new List<SOOtherServiceDetailList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            otherservicesummary.Add(new SOOtherServiceDetailList
                            {
                                otherservice_gid = rd["otherservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                passenger_name = rd["passenger_name"].ToString(),
                                service_name = rd["service_name"].ToString(),
                                total_amount = Double.Parse(rd["total_amount"].ToString()),
                                remarks = rd["remarks"].ToString()

                            });
                        }

                        cmd.Connection.Close();
                        sof.SOOtherServiceDetailList = otherservicesummary;
                        sof.status = true;
                    }
                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }
                else if (service_gid == 8)
                {
                    cmd = new MySqlCommand("sp_sel_soinsurance");
                    cmd.Parameters.AddWithValue("p_salesorder_gid", salesorder_gid);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    rd = DBAccess.ExecuteReader(cmd);
                    var insurancesummary = new List<SOInsurenceList>();
                    if (rd.HasRows == true)
                    {
                        while (rd.Read())
                        {
                            insurancesummary.Add(new SOInsurenceList
                            {
                                insuranceservice_gid = rd["insuranceservice_gid"].ToString(),
                                paymentnote_gid = rd["paymentnote_gid"].ToString(),
                                name = rd["name"].ToString(),
                                dob = rd["dob"].ToString(),
                                arrival_port = rd["arrival_port"].ToString(),
                                start_date = rd["start_date"].ToString(),
                                end_date = rd["end_date"].ToString(),
                                total_amount = double.Parse(rd["total_amount"].ToString())
                            });
                        }

                        cmd.Connection.Close();
                        sof.SOInsurenceList = insurancesummary;
                        sof.status = true;
                    }
                    else
                    {
                        cmd.Connection.Close();
                    }
                    rd.Close();
                }
               

            }

            catch (Exception ex)
            {
                sof.status = false;
                sof.message = "Customer Details Not Loaded";
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

     
        public soservicedelete sofpassengerdelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofpassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passengerservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Passenger Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete passenger Information!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete passenger Information!";
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

        public soservicedelete sofpassportdelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofpassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Passport Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Passport Information!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Passport Information!";
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

        public soservicedelete sofvisadelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofvisa");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_visaservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Visa Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Visa Information!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Visa Information!";
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

        public soservicedelete sofflightdelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofflight");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flightservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Flight Tickets Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Flight Tickets !";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Flight Tickets !";
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
        public soservicedelete sofairinvoicedelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofairinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_air_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "AIR File Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete AIR File !";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete AIR File !";
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

        public soservicedelete sofhoteldelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofhotel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_hotelservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Hotel Booking Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Hotel Booking !";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Hotel Booking !";
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

        public soservicedelete sofcardelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofcar");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_carservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Car Booking Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Car Booking !";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Car Booking !";
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

        public soservicedelete sofforexdelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofforex");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_forexservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Forex Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Forex!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Forex!";
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

        public soservicedelete sofpackagedelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofpackage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_packageservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Package Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Package!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Package!";
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
        public soservicedelete sofotherservicedelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofotherservice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_otherservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Other Service Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete Other Service!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete Other Service!";
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

        public soservicedelete sofinsdelete(string values)
        {
            soservicedelete sof = new soservicedelete();

            try
            {
                cmd = new MySqlCommand("sp_del_sofinsurence");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_insuranceservice_gid", values);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    sof.status = true;
                    sof.message = "Insurance Deleted Successfully";
                }
                else
                {
                    sof.status = false;
                    sof.message = " Error Occured while delete insurance!";
                }
            }
            catch (Exception ex)
            {
                sof.status = false;
                sof.message = " Error Occured while delete insurance!";
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

        public SOPassengerDetail sopassengeredit(string val, string user_gid)
        {
            SOPassengerDetail pass = new SOPassengerDetail();
            try
            {

                cmd = new MySqlCommand("sp_sel_sopassengeredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {

                    pass.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    pass.passenger_firstname = rd["passenger_firstname"].ToString();
                    pass.passenger_lastname = rd["passenger_lastname"].ToString();
                    pass.gender = rd["gender"].ToString();
                    pass.dob = rd["dob"].ToString();
                    pass.passport_number = rd["passport_number"].ToString();
                    pass.passport_expirydate = rd["passport_expirydate"].ToString();
                    pass.passport_issueddate = rd["passport_issueddate"].ToString();
                    pass.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    //pass.salesactivitygid = int.Parse(rd[""].ToString());
                    pass.status = true;

                }
                else
                {
                    pass.status = false;
                    pass.message = "Internal Error Occured";
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                pass.status = false;
                pass.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return pass;
        }
        public SOPassengerDetail salesinvoicepassengeredit(string val, string user_gid)
        {
            SOPassengerDetail pass = new SOPassengerDetail();
            try
            {

                cmd = new MySqlCommand("sp_sel_invpassengeredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {

                    pass.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    pass.passenger_firstname = rd["passenger_firstname"].ToString();
                    pass.passenger_lastname = rd["passenger_lastname"].ToString();
                    pass.gender = rd["gender"].ToString();
                    pass.passport_number = rd["passport_number"].ToString();
                    pass.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    //pass.salesactivitygid = int.Parse(rd[""].ToString());
                    pass.status = true;

                }
                else
                {
                    pass.status = false;
                    pass.message = "Internal Error Occured";
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                pass.status = false;
                pass.message = "Internal Error Occured";
                error = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }

            }
            return pass;
        }
        public SalesOrderFormModel sovisaedit(SOVisaDetail val, string user_gid)
        {
            // SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {

                cmd = new MySqlCommand("sp_sel_sovisaedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_visaservice_gid", val.visaservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.visaservice_gid = rd["visaservice_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.country = rd["country"].ToString();
                    val.application_date = rd["application_date"].ToString();
                    val.expiry_date = rd["expiry_date"].ToString();
                    val.visa_period = int.Parse(rd["visa_period"].ToString());
                    val.currency_gid = rd["currency_gid"].ToString();
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicevisaedit
            
            (SOVisaDetail val, string user_gid)
        {
            // SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {

                cmd = new MySqlCommand("sp_sel_invvisaedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_visaservice_gid", val.visaservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.visaservice_gid = rd["visaservice_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.country = rd["country"].ToString();
                    val.application_date = rd["application_date"].ToString();
                    val.expiry_date = rd["expiry_date"].ToString();
                    val.visa_period = int.Parse(rd["visa_period"].ToString());
                    val.currency_gid = rd["currency_gid"].ToString();
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.visavendor_name = rd["visavendor_name"].ToString();
                    val.visa_vamount = double.Parse(rd["visa_vamount"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel soflightedit(SOFlightDetail val, string user_gid)
        {
            // SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_sel_soflightedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.flightservice_gid = rd["flightservice_gid"].ToString();
                    val.flight_name = rd["flight_name"].ToString();
                    val.flight_from = rd["flight_from"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.flight_to = rd["flight_to"].ToString();
                    val.departure_date = rd["departure_date"].ToString();
                    val.flight_time = rd["flight_time"].ToString();
                    val.flight_number = rd["flight_number"].ToString();
                    val.sector_number = rd["sector_number"].ToString();
                    val.pnr_number = rd["pnr_number"].ToString();
                    val.ticket_number = rd["ticket_number"].ToString();
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.flight_class = rd["flight_class"].ToString();
                    val.flight_routing = rd["flight_routing"].ToString();
                    //val.flightairline = rd["flightairline"].ToString();
                    val.segment = rd["segment"].ToString();
                    val.status = true;
                    //rd.Close();

                    cmd = new MySqlCommand("sp_sel_soflightpassengereditdtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<SOPassengerList>();
                    while (rd.Read())
                    {
                        summary.Add(new SOPassengerList
                        {
                            //salesorder_gid = rd["salesorder_gid"].ToString(),
                            passengerservice_gid = rd["passengerservice_gid"].ToString(),
                            passport_number = rd["passport_number"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),

                        });
                    }

                    val.SOPassengerList = summary;
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoiceflightedit(SOFlightDetail val, string user_gid)
        {
            // SalesOrderFormModel visa = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_sel_invflightedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.passport_number = rd["passport_number"].ToString();

                    val.flightservice_gid = rd["flightservice_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.flight_name = rd["flight_name"].ToString();
                    val.flight_from = rd["flight_from"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.flight_to = rd["flight_to"].ToString();
                    val.departure_date = rd["departure_date"].ToString();
                    val.flight_time = rd["flight_time"].ToString();
                    val.flight_number = rd["flight_number"].ToString();
                    val.sector_number = rd["sector_number"].ToString();
                    val.pnr_number = rd["pnr_number"].ToString();
                    val.ticket_number = rd["ticket_number"].ToString();
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.flight_class = rd["flight_class"].ToString();
                    val.flight_routing = rd["flight_routing"].ToString();
                    //val.flightairline = rd["flightairline"].ToString();
                    val.segment = rd["segment"].ToString();
                    val.ticket_vamount = double.Parse(rd["ticket_vamount"].ToString());
                    val.ticketvendor_name = rd["ticketvendor_name"].ToString();
                    val.flighttrip_type = rd["flighttrip_type"].ToString();
                    val.flight_fare = rd["flight_fare"].ToString();
                    val.flight_comm = rd["flight_comm"].ToString();
                    val.flight_sc = rd["flight_sc"].ToString();
                    val.flight_xt = rd["flight_xt"].ToString();
                    val.flight_totalcalcamount = double.Parse(rd["flight_totalcalcamount"].ToString());

                    val.status = true;
                    //rd.Close();

                    cmd = new MySqlCommand("sp_sel_soflightpassengereditdtl");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                    rd = DBAccess.ExecuteReader(cmd);
                    var summary = new List<SOPassengerList>();
                    while (rd.Read())
                    {
                        summary.Add(new SOPassengerList
                        {
                            //salesorder_gid = rd["salesorder_gid"].ToString(),
                            passengerservice_gid = rd["passengerservice_gid"].ToString(),
                            passport_number = rd["passport_number"].ToString(),
                            passenger_name = rd["passenger_name"].ToString(),

                        });
                    }

                    val.SOPassengerList = summary;
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesairinvoiceedit(SOFlightDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invairinvoiceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.air_gid = rd["air_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();

                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.eticket_number = rd["eticket_number"].ToString();
                    val.epax_name = rd["epax_name"].ToString();
                    val.epnr_no = rd["epnr_no"].ToString();
                    val.etrip_type = rd["etrip_type"].ToString();
                    val.esegment = rd["esegment"].ToString();
                    val.eflag = rd["eflag"].ToString();
                    val.eagent_gid = rd["eagent_gid"].ToString();
                    val.efirstplace_from = rd["efirstplace_from"].ToString();
                    val.efirstplace_to = rd["efirstplace_to"].ToString();
                    val.efirststart_time = rd["efirststart_time"].ToString();
                    val.efirstend_time = rd["efirstend_time"].ToString();
                    val.esecondplace_from = rd["esecondplace_from"].ToString();
                    val.esecondplace_to = rd["esecondplace_to"].ToString();
                    val.esecondstart_time = rd["esecondstart_time"].ToString();
                    val.esecondend_time = rd["esecondend_time"].ToString();
                    val.ethirdplace_from = rd["ethirdplace_from"].ToString();
                    val.ethirdplace_to = rd["ethirdplace_to"].ToString();
                    val.ethirdstart_time = rd["ethirdstart_time"].ToString();
                    val.ethirdend_time = rd["ethirdend_time"].ToString();
                    val.efourthplace_from = rd["efourthplace_from"].ToString();
                    val.efourthplace_to = rd["efourthplace_to"].ToString();
                    val.efourthstart_time = rd["efourthstart_time"].ToString();
                    val.efourthend_time = rd["efourthend_time"].ToString();
                    val.eticket_camount = rd["eticket_camount"].ToString();
                    val.evendor_name = rd["evendor_name"].ToString();
                    val.evendor_vamount = double.Parse(rd["evendor_vamount"].ToString());

                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.eairline = rd["eairline"].ToString();
                    val.esystem_use = rd["esystem_use"].ToString();
                    val.flight_fare = rd["flight_fare"].ToString();
                    val.flight_comm = rd["flight_comm"].ToString();
                    val.flight_sc = rd["flight_sc"].ToString();
                    val.flight_xt = rd["flight_xt"].ToString();
                    val.flight_totalcalcamount = double.Parse(rd["flight_totalcalcamount"].ToString());

                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel sohoteledit(SOHotelDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_sohoteledit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_hotelservice_gid", val.hotelservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.hotelservice_gid = rd["hotelservice_gid"].ToString();
                    val.hotel_name = rd["hotel_name"].ToString();
                    val.category = rd["category"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.city = rd["city"].ToString();
                    val.destination = rd["destination"].ToString();
                    val.check_in = rd["check_in"].ToString();
                    val.check_out = rd["check_out"].ToString();
                    val.total_numberofdays = int.Parse(rd["total_numberofdays"].ToString());
                    val.total_numberofpassengers = int.Parse(rd["total_numberofpassengers"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicehoteledit(SOHotelDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invhoteledit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_hotelservice_gid", val.hotelservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.hotelservice_gid = rd["hotelservice_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.remarks = rd["remarks"].ToString();
                    val.hotel_name = rd["hotel_name"].ToString();
                    val.category = rd["category"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.city = rd["city"].ToString();
                    val.destination = rd["destination"].ToString();
                    val.check_in = rd["check_in"].ToString();
                    val.check_out = rd["check_out"].ToString();
                    val.total_numberofdays = int.Parse(rd["total_numberofdays"].ToString());
                    val.total_numberofpassengers = int.Parse(rd["total_numberofpassengers"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());

                    val.hotelvendor_name = rd["hotelvendor_name"].ToString();
                    val.hotel_vamount = double.Parse(rd["hotel_vamount"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel socaredit(SOCarDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_socaredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_carservice_gid", val.carservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.carservice_gid = rd["carservice_gid"].ToString();
                    //val.cartype = rd["car_type"].ToString();
                    val.car_type = rd["car_type"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    //val.city = rd["city"].ToString();
                    val.pickup_city = rd["pickup_city"].ToString();
                    val.drop_city = rd["drop_city"].ToString();
                    val.from_date = rd["from_date"].ToString();
                    val.to_date = rd["to_date"].ToString();
                    val.numberof_persons = int.Parse(rd["numberof_persons"].ToString());
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicecaredit(SOCarDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invcaredit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_carservice_gid", val.carservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.carservice_gid = rd["carservice_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();

                    //val.cartype = rd["car_type"].ToString();
                    val.car_type = rd["car_type"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    //val.city = rd["city"].ToString();
                    val.pickup_city = rd["pickup_city"].ToString();
                    val.drop_city = rd["drop_city"].ToString();
                    val.from_date = rd["from_date"].ToString();
                    val.to_date = rd["to_date"].ToString();
                    val.numberof_persons = int.Parse(rd["numberof_persons"].ToString());
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.carvendor_name = rd["carvendor_name"].ToString();
                    val.car_vamount = double.Parse(rd["car_vamount"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel soforexedit(SOForexDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_soforexedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_forexservice_gid", val.forexservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.forexservice_gid = rd["forexservice_gid"].ToString();
                    val.customerpaid_amount = double.Parse(rd["customerpaid_amount"].ToString());
                    val.customerreceived_amount = double.Parse(rd["customerreceived_amount"].ToString());
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.paidamount_exchangerate = rd["paidamount_exchangerate"].ToString();
                    val.total_paidamount = double.Parse(rd["total_paidamount"].ToString());
                    val.total_receivedamount = double.Parse(rd["total_receivedamount"].ToString());//customerreceived_amount
                    val.receivedamount_exchangerate = rd["receivedamount_exchangerate"].ToString();
                    val.paidamount_currency = rd["paidamount_currency"].ToString();
                    val.receivedamount_currency = rd["receivedamount_currency"].ToString();
                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoiceforexedit(SOForexDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invforexedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_forexservice_gid", val.forexservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.forexservice_gid = rd["forexservice_gid"].ToString();
                    val.customerpaid_amount = double.Parse(rd["customerpaid_amount"].ToString());
                    val.customerreceived_amount = double.Parse(rd["customerreceived_amount"].ToString());
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.paidamount_exchangerate = rd["paidamount_exchangerate"].ToString();
                    val.total_paidamount = double.Parse(rd["total_paidamount"].ToString());
                    val.total_receivedamount = double.Parse(rd["total_receivedamount"].ToString());//customerreceived_amount
                    val.receivedamount_exchangerate = rd["receivedamount_exchangerate"].ToString();
                    val.paidamount_currency = rd["paidamount_currency"].ToString();
                    val.receivedamount_currency = rd["receivedamount_currency"].ToString();
                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel soinsuranceedit(SOInsurenceDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_soinsuranceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_insuranceservice_gid", val.insuranceservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.insuranceservice_gid = rd["insuranceservice_gid"].ToString();
                    val.name = rd["name"].ToString();
                    val.dob = rd["dob"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.arrival_port = rd["arrival_port"].ToString();
                    val.start_date = rd["start_date"].ToString();
                    val.end_date = rd["end_date"].ToString();//customerreceived_amount
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoiceinsurencedit(SOInsurenceDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invinsuranceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_insuranceservice_gid", val.insuranceservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.insuranceservice_gid = rd["insuranceservice_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();

                    val.name = rd["name"].ToString();
                    val.dob = rd["dob"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.arrival_port = rd["arrival_port"].ToString();
                    val.start_date = rd["start_date"].ToString();
                    val.end_date = rd["end_date"].ToString();//customerreceived_amount
                    val.currency_gid = int.Parse(rd["currency_gid"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());

                    val.insurance_type = rd["insurance_type"].ToString();
                    val.insvendor_name = rd["insvendor_name"].ToString();
                    val.insurance_vamount = double.Parse(rd["insurance_vamount"].ToString());


                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel sopassportedit(SOPassportDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_sopassportedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", val.passportservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.passportservice_gid = rd["passportservice_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.id_proof = rd["id_proof"].ToString();
                    val.additional_proof = rd["additional_proof"].ToString();
                    val.anygovt_document = rd["anygovt_document"].ToString();
                    //val.uploaddocument = rd["currency"].ToString();
                    val.photo = int.Parse(rd["photo"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());

                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicepassportedit(SOPassportDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invpassportedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", val.passportservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.passportservice_gid = rd["passportservice_gid"].ToString();
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.id_proof = rd["id_proof"].ToString();
                    val.additional_proof = rd["additional_proof"].ToString();
                    val.anygovt_document = rd["anygovt_document"].ToString();
                    //val.uploaddocument = rd["currency"].ToString();
                    val.photo = int.Parse(rd["photo"].ToString());
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.passvendor_name = rd["passvendor_name"].ToString();
                    val.pass_vamount = double.Parse(rd["pass_vamount"].ToString());



                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel sopackageedit(SOPackageDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_sopackageedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_packageservice_gid", val.packageservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    //val.passengerservice_gid = rd["insuranceservice_gid"].ToString();
                    val.packageservice_gid = rd["packageservice_gid"].ToString();
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.remarks = rd["remarks"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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

        public SalesOrderFormModel salesinvoicepackageedit(SOPackageDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invpackageedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_packageservice_gid", val.packageservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    //val.passengerservice_gid = rd["insuranceservice_gid"].ToString();
                    val.packageservice_gid = rd["packageservice_gid"].ToString();
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.remarks = rd["remarks"].ToString();
                    val.package_name = rd["package_name"].ToString();
                    val.package_category = rd["package_category"].ToString();
                    val.totalnoPassenger = rd["totalnoPassenger"].ToString();
                    val.packagevendor_name = rd["packagevendor_name"].ToString();
                    val.package_vamount = double.Parse(rd["package_vamount"].ToString());
                    val.period = rd["period"].ToString();
                    val.from_date = rd["from_date"].ToString();
                    val.to_date = rd["to_date"].ToString();
                    val.country = rd["country"].ToString();
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoiceotherservicesedit(SOOtherServiceDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_sel_invotherserviceedit");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_otherservice_gid", val.otherservice_gid);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.Read())
                {
                    val.otherservice_gid = rd["otherservice_gid"].ToString();
                    val.passenger_name = rd["passenger_name"].ToString();
                    val.passengerservice_gid = rd["passengerservice_gid"].ToString();
                    val.total_amount = double.Parse(rd["total_amount"].ToString());
                    val.salesorder_gid = int.Parse(rd["salesorder_gid"].ToString());
                    val.paymentnote_gid = rd["paymentnote_gid"].ToString();
                    val.remarks = rd["remarks"].ToString();
                    val.service_name = rd["service_name"].ToString();
                    val.otherservicevendor_name = rd["otherservicevendor_name"].ToString();
                    val.otherServices_vamount = double.Parse(rd["otherServices_vamount"].ToString());
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel sopassengerupdate(SOPassengerDetail val, string user_gid)
        {
            SalesOrderFormModel passengerupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_sopassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_passenger_firstname", val.passenger_firstname);
                cmd.Parameters.AddWithValue("p_passenger_lastname", val.passenger_lastname);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_passport_issueddate", val.passport_issueddate);
                cmd.Parameters.AddWithValue("p_passport_expirydate", val.passport_expirydate);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Added Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
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
        public SalesOrderFormModel salesinvoicepassengerupdate(SOPassengerDetail val, string user_gid)
        {
            SalesOrderFormModel passengerupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invpassenger");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_passenger_firstname", val.passenger_firstname);
                cmd.Parameters.AddWithValue("p_passenger_lastname", val.passenger_lastname);
                cmd.Parameters.AddWithValue("p_gender", val.gender);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Added Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel sovisaupdate(SOVisaDetail val, string user_gid)
        {
            SalesOrderFormModel visaupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_sovisa");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_visaservice_gid", val.visaservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_country", val.country);
                cmd.Parameters.AddWithValue("p_application_date", val.application_date);
                cmd.Parameters.AddWithValue("p_visa_period", val.visa_period);
                cmd.Parameters.AddWithValue("p_expiry_date", val.expiry_date);
                //cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_passport_no", "");
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
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
        public SalesOrderFormModel salesinvoicevisaupdate(SOVisaDetail val, string user_gid)
        {
            SalesOrderFormModel visaupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invvisa");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_visaservice_gid", val.visaservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);

                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_country", val.country);
                cmd.Parameters.AddWithValue("p_application_date", val.application_date);
                cmd.Parameters.AddWithValue("p_visa_period", val.visa_period);
                cmd.Parameters.AddWithValue("p_expiry_date", val.expiry_date);
                //cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);

                cmd.Parameters.AddWithValue("p_visavendor_name", val.visavendor_name);
                cmd.Parameters.AddWithValue("p_visa_vamount", val.visa_vamount);
                //cmd.Parameters.AddWithValue("p_passport_no", "");
               
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Visa-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Visa-visaservice_gid:" + val.visaservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Visa Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel soflightupdate(SOFlightDetail val, string user_gid)
        {
            SalesOrderFormModel flight = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_soflight");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_flight_number", val.flight_number);
                cmd.Parameters.AddWithValue("p_flight_name", val.flight_name);
                cmd.Parameters.AddWithValue("p_departure_date", val.departure_date);
                cmd.Parameters.AddWithValue("p_flight_time", val.flight_time);
                cmd.Parameters.AddWithValue("p_flight_from", val.flight_from);
                cmd.Parameters.AddWithValue("p_flight_to", val.flight_to);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                cmd.Parameters.AddWithValue("p_sector_number", val.sector_number);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_flight_class", val.flight_class);
                cmd.Parameters.AddWithValue("p_segment", val.segment);
                cmd.Parameters.AddWithValue("p_flight_airline", "");
                cmd.Parameters.AddWithValue("p_flight_routing", val.flight_routing);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Records updated sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }

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
        public SalesOrderFormModel salesinvoiceflightupdate(SOFlightDetail val, string user_gid)
        {
            SalesOrderFormModel flight = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invflight");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_flightservice_gid", val.flightservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_flight_number", val.flight_number);
                cmd.Parameters.AddWithValue("p_flight_name", val.flight_name);
                cmd.Parameters.AddWithValue("p_departure_date", val.departure_date);
                cmd.Parameters.AddWithValue("p_flight_time", val.flight_time);
                cmd.Parameters.AddWithValue("p_flight_from", val.flight_from);
                cmd.Parameters.AddWithValue("p_flight_to", val.flight_to);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_pnr_number", val.pnr_number);
                cmd.Parameters.AddWithValue("p_ticket_number", val.ticket_number);
                cmd.Parameters.AddWithValue("p_sector_number", val.sector_number);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_flight_class", val.flight_class);
                cmd.Parameters.AddWithValue("p_segment", val.segment);
                cmd.Parameters.AddWithValue("p_flight_airline", "");
                cmd.Parameters.AddWithValue("p_flight_routing", val.flight_routing);
                cmd.Parameters.AddWithValue("p_passport_number", val.passport_number);
                cmd.Parameters.AddWithValue("p_ticketvendor_name", val.ticketvendor_name);
                cmd.Parameters.AddWithValue("p_ticket_vamount", val.ticket_vamount);
                cmd.Parameters.AddWithValue("p_flighttrip_type", val.flighttrip_type);
                cmd.Parameters.AddWithValue("p_flight_fare", val.flight_fare);
                cmd.Parameters.AddWithValue("p_flight_comm", val.flight_comm);
                cmd.Parameters.AddWithValue("p_flight_sc", val.flight_sc);
                cmd.Parameters.AddWithValue("p_flight_xt", val.flight_xt);
                cmd.Parameters.AddWithValue("p_flight_totalcalcamount", val.flight_totalcalcamount);


                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Records updated sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }

            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Ticket-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Ticket-flightservice_gid:" + val.flightservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Ticket Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel salesairinvoiceupdate(SOFlightDetail val, string user_gid)
        {
            SalesOrderFormModel airinvoice = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invairinvoice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_air_gid", val.air_gid);
                cmd.Parameters.AddWithValue("p_etrip_type", val.etrip_type);
                cmd.Parameters.AddWithValue("p_esegment", val.esegment);
                cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_epax_name", val.epax_name);
                cmd.Parameters.AddWithValue("p_eticket_number", val.eticket_number);
                cmd.Parameters.AddWithValue("p_epnr_no", val.epnr_no);
                cmd.Parameters.AddWithValue("p_eflag", val.eflag);
                cmd.Parameters.AddWithValue("p_eagent_gid", val.eagent_gid);
                cmd.Parameters.AddWithValue("p_eairline", val.eairline);
                cmd.Parameters.AddWithValue("p_esystem_use", val.esystem_use);
                cmd.Parameters.AddWithValue("p_efirstplace_from", val.efirstplace_from);
                cmd.Parameters.AddWithValue("p_efirstplace_to", val.efirstplace_to);
                cmd.Parameters.AddWithValue("p_efirststart_time", val.efirststart_time);
                cmd.Parameters.AddWithValue("p_efirstend_time", val.efirstend_time);
                cmd.Parameters.AddWithValue("p_esecondplace_from", val.esecondplace_from);
                cmd.Parameters.AddWithValue("p_esecondplace_to", val.esecondplace_to);
                cmd.Parameters.AddWithValue("p_esecondstart_time", val.esecondstart_time);
                cmd.Parameters.AddWithValue("p_esecondend_time", val.esecondend_time);
                cmd.Parameters.AddWithValue("p_ethirdplace_from", val.ethirdplace_from);
                cmd.Parameters.AddWithValue("p_ethirdplace_to", val.ethirdplace_to);
                cmd.Parameters.AddWithValue("p_ethirdstart_time", val.ethirdstart_time);
                cmd.Parameters.AddWithValue("p_ethirdend_time", val.ethirdend_time);
                cmd.Parameters.AddWithValue("p_efourthplace_from", val.efourthplace_from);
                cmd.Parameters.AddWithValue("p_efourthplace_to", val.efourthplace_to);
                cmd.Parameters.AddWithValue("p_efourthstart_time", val.efourthstart_time);
                cmd.Parameters.AddWithValue("p_efourthend_time", val.efourthend_time);
                cmd.Parameters.AddWithValue("p_eticket_camount", val.eticket_camount);
                cmd.Parameters.AddWithValue("p_evendor_name", val.evendor_name);
                cmd.Parameters.AddWithValue("p_evendor_vamount", val.evendor_vamount);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_flight_fare", val.flight_fare);
                cmd.Parameters.AddWithValue("p_flight_comm", val.flight_comm);
                cmd.Parameters.AddWithValue("p_flight_sc", val.flight_sc);
                cmd.Parameters.AddWithValue("p_flight_xt", val.flight_xt);
                cmd.Parameters.AddWithValue("p_flight_totalcalcamount", val.flight_totalcalcamount);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Records updated sucessfully";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }

            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("AIR File-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("AIR File-air_gid:" + val.air_gid);
                    sw.WriteLine("Error: Error occured while Updating AIR File");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel sohotelupdate(SOHotelDetail val, string user_gid)
        {
            SalesOrderFormModel hotelupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_sohotel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_hotelservice_gid", val.hotelservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_hotel_name", val.hotel_name);
                cmd.Parameters.AddWithValue("p_category", val.category);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_destination", val.destination);
                cmd.Parameters.AddWithValue("p_check_in", val.check_in);
                cmd.Parameters.AddWithValue("p_check_out", val.check_out);
                cmd.Parameters.AddWithValue("p_total_numberofdays", val.total_numberofdays);
                cmd.Parameters.AddWithValue("p_total_numberofpassengers", val.total_numberofpassengers);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
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
        public SalesOrderFormModel salesinvoicehotelupdate(SOHotelDetail val, string user_gid)
        {
            SalesOrderFormModel hotelupt = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invhotel");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_hotelservice_gid", val.hotelservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_hotel_name", val.hotel_name);
                cmd.Parameters.AddWithValue("p_category", val.category);
                cmd.Parameters.AddWithValue("p_city", val.city);
                cmd.Parameters.AddWithValue("p_destination", val.destination);
                cmd.Parameters.AddWithValue("p_check_in", val.check_in);
                cmd.Parameters.AddWithValue("p_check_out", val.check_out);
                cmd.Parameters.AddWithValue("p_total_numberofdays", val.total_numberofdays);
                cmd.Parameters.AddWithValue("p_total_numberofpassengers", val.total_numberofpassengers);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_hotelvendor_name", val.hotelvendor_name);
                cmd.Parameters.AddWithValue("p_hotel_vamount", val.hotel_vamount);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Hotel-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Hotel-hotelservice_gid:" + val.hotelservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Hotel Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel socarupdate(SOCarDetail val, string user_gid)
        {
            SalesOrderFormModel car = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_socar");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_carservice_gid", val.carservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_car_type", val.car_type);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_pickup_city", val.pickup_city);
                cmd.Parameters.AddWithValue("p_drop_city", val.drop_city);
                cmd.Parameters.AddWithValue("p_numberof_persons", val.numberof_persons);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                //cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
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
        public SalesOrderFormModel salesinvoicecarupdate(SOCarDetail val, string user_gid)
        {
            SalesOrderFormModel car = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invcar");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_carservice_gid", val.carservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);

                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_car_type", val.car_type);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_pickup_city", val.pickup_city);
                cmd.Parameters.AddWithValue("p_drop_city", val.drop_city);
                cmd.Parameters.AddWithValue("p_numberof_persons", val.numberof_persons);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                //cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_carvendor_name", val.carvendor_name);
                cmd.Parameters.AddWithValue("p_car_vamount", val.car_vamount);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Car-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Car-carservice_gid:" + val.carservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Passport Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel soforexupdate(SOForexDetail val, string user_gid)
        {
            SalesOrderFormModel forex = new SalesOrderFormModel();
            try
            {

                cmd = new MySqlCommand("sp_upt_soforex");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_forexservice_gid", val.forexservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customerpaid_amount", val.customerpaid_amount);
                cmd.Parameters.AddWithValue("p_customerreceived_amount", val.customerreceived_amount);
                cmd.Parameters.AddWithValue("p_total_paidamount", val.total_paidamount);
                cmd.Parameters.AddWithValue("p_total_receivedamount", val.total_receivedamount);
                //cmd.Parameters.AddWithValue("p_paidamount_exchangerate", val.paidexchangerate);
                //cmd.Parameters.AddWithValue("p_receivedamount_exchangerate", val.receiveexchangerate);
                cmd.Parameters.AddWithValue("p_paidamount_currency", val.paidamount_currency);
                cmd.Parameters.AddWithValue("p_receivedamount_currency", val.receivedamount_currency);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                // cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
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
        public SalesOrderFormModel salesinvoiceforexupdate(SOForexDetail val, string user_gid)
        {
            SalesOrderFormModel forex = new SalesOrderFormModel();
            try
            {

                cmd = new MySqlCommand("sp_upt_invforex");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_forexservice_gid", val.forexservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_customerpaid_amount", val.customerpaid_amount);
                cmd.Parameters.AddWithValue("p_customerreceived_amount", val.customerreceived_amount);
                cmd.Parameters.AddWithValue("p_total_paidamount", val.total_paidamount);
                cmd.Parameters.AddWithValue("p_total_receivedamount", val.total_receivedamount);
                //cmd.Parameters.AddWithValue("p_paidamount_exchangerate", val.paidexchangerate);
                //cmd.Parameters.AddWithValue("p_receivedamount_exchangerate", val.receiveexchangerate);
                cmd.Parameters.AddWithValue("p_paidamount_currency", val.paidamount_currency);
                cmd.Parameters.AddWithValue("p_receivedamount_currency", val.receivedamount_currency);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                // cmd.Parameters.AddWithValue("p_reference_gid", 0);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel soinsuranceupdate(SOInsurenceDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_soinsurance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_insuranceservice_gid", val.insuranceservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_name", val.name);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_arrival_port", val.arrival_port);
                cmd.Parameters.AddWithValue("p_start_date", val.start_date);
                cmd.Parameters.AddWithValue("p_end_date", val.end_date);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
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
        public SalesOrderFormModel salesinvoiceinsuranceupdate(SOInsurenceDetail val, string user_gid)
        {
            SalesOrderFormModel insurance = new SalesOrderFormModel();
            try
            {
                cmd = new MySqlCommand("sp_upt_invinsurance");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_insuranceservice_gid", val.insuranceservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);

                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_name", val.name);
                cmd.Parameters.AddWithValue("p_dob", val.dob);
                cmd.Parameters.AddWithValue("p_arrival_port", val.arrival_port);
                cmd.Parameters.AddWithValue("p_start_date", val.start_date);
                cmd.Parameters.AddWithValue("p_end_date", val.end_date);
                cmd.Parameters.AddWithValue("p_currency_gid", val.currency_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                //cmd.Parameters.AddWithValue("p_reference_gid", 0);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_insvendor_name", val.insvendor_name);
                cmd.Parameters.AddWithValue("p_insurance_vamount", val.insurance_vamount);
                cmd.Parameters.AddWithValue("p_insurance_type", val.insurance_type);
                //cmd.Connection = con;
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured!";
                }
            }
            catch (Exception ex)
            {
                val.status = false;
                val.message = "Internal Error Occured!";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Insurance-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Insurance-insuranceservice_gid:" + val.insuranceservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Insurance Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel sopassportupdate(SOPassportDetail val, string user_gid)
        {
            try
            {


                cmd = new MySqlCommand("sp_upt_sopassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", val.passportservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);  // changes made
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_id_proof", val.id_proof);
                cmd.Parameters.AddWithValue("p_additional_proof", val.additional_proof);
                cmd.Parameters.AddWithValue("p_photo", val.photo);
                cmd.Parameters.AddWithValue("p_anygovt_document", val.anygovt_document);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                //cmd.Parameters.AddWithValue("p_upload_documents", val.insamount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicepassportupdate(SOPassportDetail val, string user_gid)
        {
            try
            {


                cmd = new MySqlCommand("sp_upt_invpassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", val.passportservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_id_proof", val.id_proof);
                cmd.Parameters.AddWithValue("p_additional_proof", val.additional_proof);
                cmd.Parameters.AddWithValue("p_photo", val.photo);
                cmd.Parameters.AddWithValue("p_anygovt_document", val.anygovt_document);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_passvendor_name", val.passvendor_name);
                cmd.Parameters.AddWithValue("p_pass_vamount", val.pass_vamount);
                //cmd.Parameters.AddWithValue("p_upload_documents", val.insamount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Passport-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Passport-passportservice_gid:" + val.passportservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Passport Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel salesvendorpassportupdate(SOPassportDetail val, string user_gid)
        {
            try
            {


                cmd = new MySqlCommand("sp_upt_vendorpassport");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_passportservice_gid", val.passportservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);  // changes made
                cmd.Parameters.AddWithValue("p_passenger_name", "");
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_passvendor_name", val.passvendor_name);
                cmd.Parameters.AddWithValue("p_pass_vamount", val.pass_vamount);
                cmd.Parameters.AddWithValue("p_pass_vpaidamount", val.pass_vpaidamount);

                //cmd.Parameters.AddWithValue("p_upload_documents", val.insamount);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }
            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel sopackageupdate(SOPackageDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_upt_sopackage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_packageservice_gid", val.packageservice_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }

            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
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
        public SalesOrderFormModel salesinvoicepackageupdate(SOPackageDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_upt_invpackage");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_packageservice_gid", val.packageservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_package_name", val.package_name);
                cmd.Parameters.AddWithValue("p_package_category", val.package_category);
                cmd.Parameters.AddWithValue("p_totalnoPassenger", val.totalnoPassenger);
                cmd.Parameters.AddWithValue("p_packagevendor_name", val.packagevendor_name);
                cmd.Parameters.AddWithValue("p_package_vamount", val.package_vamount);
                cmd.Parameters.AddWithValue("p_period", val.period);
                cmd.Parameters.AddWithValue("p_from_date", val.from_date);
                cmd.Parameters.AddWithValue("p_to_date", val.to_date);
                cmd.Parameters.AddWithValue("p_country", val.country);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }

            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Package-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Package-packageservice_gid:" + val.packageservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Package Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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
        public SalesOrderFormModel salesinvoiceotherservicesupdate(SOOtherServiceDetail val, string user_gid)
        {
            try
            {
                cmd = new MySqlCommand("sp_upt_invotherservice");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_otherservice_gid", val.otherservice_gid);
                cmd.Parameters.AddWithValue("p_paymentnote_gid", val.paymentnote_gid);
                cmd.Parameters.AddWithValue("p_salesorder_gid", val.salesorder_gid);
                cmd.Parameters.AddWithValue("p_passenger_name", val.passenger_name);
                cmd.Parameters.AddWithValue("p_passengerservice_gid", val.passengerservice_gid);
                cmd.Parameters.AddWithValue("p_total_amount", val.total_amount);
                cmd.Parameters.AddWithValue("p_remarks", val.remarks);
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_service_name", val.service_name);
                cmd.Parameters.AddWithValue("p_otherservicevendor_name", val.otherservicevendor_name);
                cmd.Parameters.AddWithValue("p_otherServices_vamount", val.otherServices_vamount);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Updated Successfully!";
                }
                else
                {
                    val.status = false;
                    val.message = "Internal Error Occured";
                }

            }
            catch (Exception ex)
            {
                val.status = true;
                val.message = "Internal Error Occured";
                error = ex.ToString();
                string strPath = HttpContext.Current.Server.MapPath("../Error_Log.txt");
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Other Service-salesorder_gid:" + val.salesorder_gid);
                    sw.WriteLine("Other Service-otherservice_gid:" + val.otherservice_gid);
                    sw.WriteLine("Error: Error occured while Updating Other Service Type");
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
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

        public customer sofcustomer2(Customerdetails val)
        {
            customer sof = new customer();
            try
            {
                cmd = new MySqlCommand("sp_sel_customersalesall");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                rd = DBAccess.ExecuteReader(cmd);
                if (rd.HasRows == true)
                {
                    sof.customer_name = "[";
                    while (rd.Read())
                    {
                        sof.customer_name = sof.customer_name + "," + "'" + rd["cutomer_name"].ToString() + "'";
                    }
                    sof.customer_name = sof.customer_name.Remove(1, 1);
                    sof.customer_name = sof.customer_name + "]";
                }
                rd.Close();
            }
            catch (Exception ex)
            {
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
        public SalesOrderForm sofcustomer1(Customerdetails val)
        {
            SalesOrderForm sof = new SalesOrderForm();
            try
            {
                cmd.Parameters.Clear();
                cmd = new MySqlCommand("sp_sel_customersales");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_customer_type", val.customer_type);
                rd = DBAccess.ExecuteReader(cmd);
                var summary = new List<Customerlist>();
                if (rd.HasRows == true)
                {
                    while (rd.Read())

                        summary.Add(new Customerlist
                        {
                            customer_gid = rd["customer_gid"].ToString(),
                            customer_name = rd["cutomer_name"].ToString(),
                            contact_number = rd["contact_number"].ToString(),
                            email_address = rd["email_address"].ToString(),
                            national_id = rd["national_id"].ToString(),
                            billing_address = rd["billing_address"].ToString()
                        });
                }
                sof.customerList = summary;
                rd.Close();
            }
            catch (Exception ex)
            {
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

        public sotempactivity softempactivitydelete(sotempactivity val, string user_gid)
        {
            sotempactivity sotempactivitydelte = new sotempactivity();
            try
            {
                cmd = new MySqlCommand("sp_del_tempactivity");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_created_by", user_gid);
                cmd.Parameters.AddWithValue("p_activity_source", val.activity_source);
                mnrestult = DBAccess.ExecuteNonQuery(cmd);
                if (mnrestult == 1)
                {
                    val.status = true;
                    val.message = "Temp Activity Deleted Successfully";

                }
                else
                {
                    val.status = false;
                    val.message = " Error Occured while delete Temp Activity !";
                }            

            }
            catch (Exception ex)
            {
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


