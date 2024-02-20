using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class SalesteamManager
    {
        public Salesteam GetAll()
        {
            return new SalesteamDBAccess().GetAll();
        }
        public Salesteammodel Add(Salesteamdetail val, string userGid)
        {
            return new SalesteamDBAccess().Add(val, userGid);
        }
        public Salesteammodel Delete(int values)
        {
            return new SalesteamDBAccess().Delete(values);
        }
        public Salesteamdetail Get(int values)
        {
            return new SalesteamDBAccess().Get(values);
        }
        public Salesteammodel Update(Salesteamdetail val, string userGid)
        {
            return new SalesteamDBAccess().Update(val, userGid);
        }
        public Salesteam salesteamemployee(int values)
        {
            return new SalesteamDBAccess().salesteamemployee(values);
        }
        public Salesteam salesteammanager(int values)
        {
            return new SalesteamDBAccess().salesteammanager(values);
        }
        public Salesteammodel asignemployeesubmit(Salesteamdetail val, string userGid)
        {
            return new SalesteamDBAccess().asignemployeesubmit(val, userGid);
        }
        public Salesteammodel asignmanagersubmit(Salesteamdetail val, string userGid)
        {
            return new SalesteamDBAccess().asignmanagersubmit(val, userGid);
        }
    }
}