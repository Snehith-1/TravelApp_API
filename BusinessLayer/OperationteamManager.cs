using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using DataAccess;

namespace BusinessLayer
{
    public class OperationteamManager
    {
        public Operationteam GetAll()
        {
            return new OperationteamDBAccess().GetAll();
        }
        public Operationteammodel Add(Operationteamdetail val, string userGid)
        {
            return new OperationteamDBAccess().Add(val, userGid);
        }
        public Operationteammodel Delete(int values)
        {
            return new OperationteamDBAccess().Delete(values);
        }
        public Operationteamdetail Get(int values)
        {
            return new OperationteamDBAccess().Get(values);
        }
        public Operationteammodel Update(Operationteamdetail val, string userGid)
        {
            return new OperationteamDBAccess().Update(val, userGid);
        }
        public Operationteam optteamemployee(int values)
        {
            return new OperationteamDBAccess().optteamemployee(values);
        }
        public Operationteam optteammanager(int values)
        {
            return new OperationteamDBAccess().optteammanager(values);
        }
        public Operationteammodel asignemployeesubmit(Operationteamdetail val, string userGid)
        {
            return new OperationteamDBAccess().asignemployeesubmit(val, userGid);
        }
        public Operationteammodel asignmanagersubmit(Operationteamdetail val, string userGid)
        {
            return new OperationteamDBAccess().asignmanagersubmit(val, userGid);
        }
    }
}