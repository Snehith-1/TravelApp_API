using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities.Models.DVC;
using DataAccess.Utilities;
using System.Data.SqlClient;
using System.Data;


namespace DataAccess.DBAccess.DVC
{
    public class DVCBalanceHistoryDataAccess
    {
        string lsselectparameter = string.Empty;
        string lsselectparametervalue = string.Empty;
      
        Cmnfunctions objcmnfunction = new Cmnfunctions();
        public bool BalanceHistory_Access(SqlConnection objcon,string user_gid, Balancehistory GetBalancehistory)
        {
            bool result = false;


            try
            {
                DataTable dt = new DataTable();
                string User_GID = string.Empty;

                SqlDataReader sqldr = null;


                lsselectparameter = "@corporate_user_gid";

                lsselectparametervalue = user_gid;


                sqldr = objcmnfunction.GetDataReader(lsselectparameter, lsselectparametervalue, "dvc_api_sel_getavilablebalence", 1, objcon);

                if (sqldr.HasRows == true)
                {
                    sqldr.Read();

                    GetBalancehistory.available_balance = sqldr["balance_amount"].ToString();
                }

                sqldr.Close();


                lsselectparameter = "@corporate_user_gid";

                lsselectparametervalue = user_gid;

                dt = objcmnfunction.Getdatatable(lsselectparameter, lsselectparametervalue, "dvc_api_sel_balancehistorysummary", 1, objcon);

                var lst_values = new List<Balancehistory_values>();
                if (dt.Rows.Count != 0)
                {

                    lst_values = dt.AsEnumerable().Select(row =>
                  new Balancehistory_values
                  {

                      row_no = row["row"].ToString(),
                      catagory_type = row["catagory_type"].ToString(),
                      reference_code = row["reference_code"].ToString(),
                      transaction_date = row["transaction_date"].ToString(),
                      transaction_status = row["status"].ToString(),
                      credit = row["CR"].ToString(),
                      debit = row["DR"].ToString(),
                      balance_amount = row["balance_amount"].ToString()
                  }).ToList();


                    GetBalancehistory.balancehistory_list = lst_values;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                objcmnfunction.Auditlog("BalanceHistory_Access", "Failure", ex.ToString(), "DVC", objcon);
            }
            finally
            {
            }
            return result;

        }

        public Dictionary<string, object> Balancehistory_Excel_Access(string user_gid, SqlConnection objcon)
        {
            var ls_response = new Dictionary<string, object>();
            List<DataRow> list = null;
            try
            {
                DataTable dt = new DataTable();

                lsselectparameter = "@corporate_user_gid";

                lsselectparametervalue = user_gid;


                dt = objcmnfunction.Getdatatable(lsselectparameter, lsselectparametervalue, "twi_mdvc_sel_balancehistoryrpt", 1, objcon);

                list = new List<DataRow>(dt.Select());

                if (dt.Rows.Count != 0)
                {
                    ls_response.Add("status", true);
                    ls_response.Add("datalist", list);
                }

            }
            catch (Exception ex)
            {
                objcmnfunction.Auditlog("Balancehistory_Excel_Access", "Failure", ex.ToString(), "DVC", objcon);
            }
            finally
            {

            }
            return ls_response;

        }
    }
}