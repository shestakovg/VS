﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TradeServices.Classes;
using TradeServices.Classes.IncomingData;
using TradeServices.DataEntitys;
using TradeServices.DataEntitys.IncomingData;

//dddf
namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Dictionary" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Dictionary.svc or Dictionary.svc.cs at the Solution Explorer and start debugging.
    public class Dictionary : IDictionary
    {
        public  Branch[] GetBranch()
        {
            _1CBranches branches = new _1CBranches();
            var res = branches.ConvertToArray() as Branch[];
            (branches as _1CUtManager).Dispose();
            return res;
        }


        public Route[] GetRoute(string branchid)
        {
            _1CRoutes branches = new _1CRoutes(branchid);
            var res = branches.ConvertToArray() as Route[];
            (branches as _1CUtManager).Dispose();
            return res;
        }


        public RouteSet[] GetRouteSet(string routeid)
        {
            _1CRouteSet routeSet = new _1CRouteSet(routeid);
            var res = routeSet.ConvertToArray() as RouteSet[];
            (routeSet as _1CUtManager).Dispose();
            return res;

        }


        public Contract[] GetContracts(string routeid)
        {
            _1CContract routeSet = new _1CContract(routeid);
            var res = routeSet.ConvertToArray() as Contract[]; 
            (routeSet as _1CUtManager).Dispose();
            return res;
        }


        public SkuGroup[] GetSkuGroup(string routeid)
        {
            _1CSkuGroup getskugroup = new _1CSkuGroup(routeid);
             var res =  getskugroup.ConvertToArray() as SkuGroup[];
            (getskugroup as _1CUtManager).Dispose();
            return res;
        }


        public Sku[] GetSku()
        {
            _1CSku getsku = new _1CSku();
             Sku[] res = getsku.ConvertToArray() as Sku[];
            (getsku as _1CUtManager).Dispose();
            return res;
        }


        public Price[] GetPrice(string priceId)
        {
            _1CPrice getprice = new _1CPrice(priceId);
            Price[] res = getprice.ConvertToArray() as Price[];
            (getprice as _1CUtManager).Dispose();
            return res;
        }


        public BalanceSku[] GetBalanceSku(string branchId)
        {
            _1CBalanceSku getprice = new _1CBalanceSku(branchId);
            BalanceSku[] res = getprice.ConvertToArray() as BalanceSku[];
            (getprice as _1CUtManager).Dispose();
            return res;
        }


        public void SaveHeader(OrderHeader header)
        {
            SaveData.SaveHeader(header);
        }


        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            SaveData.SaveDetailOrder(orderDetail);
        }

        


        public MarcOrder MarcOrder(string orderuuid)
        {
            SaveData.MarcOrder(new Guid(orderuuid)); ;
            return new MarcOrder{result = orderuuid};
        }


        public Stream getapk()
        {
            Stream result = null ;
            try
            {
                string filePath = ConfigurationManager.AppSettings["versionDir"] + "\\app-release.apk";
                //@"c:\Users\g.shestakov\Documents\GitHub\VS\VS\TradeServices\TradeServiceHost\bin\Debug\app-release.apk";
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                //if (!fileInfo.Exists)
                //    throw new System.IO.FileNotFoundException("File not found",
                //                                              request.FileName);

                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath,
                          System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result 
                //result.FileName = request.FileName;
                //result.Length = fileInfo.Length;
                result  = stream;
            }
            catch (Exception ex)
            {

            }
            return result; 
        }


        public VersionApk getversion()
        {
            SqlConnection con = SaveData.getConnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select top 1 ver, convert(varchar(10),verdate,104) verDate from dbo.[version]";
            SqlDataReader reader = cmd.ExecuteReader();
            VersionApk vers = new VersionApk() 
            { 
                ver = "1.1", verdate="now"
            };
            if (reader.HasRows)
            {
                reader.Read();
                vers.ver = reader.GetString(0);
                vers.verdate = reader.GetString(1);
            }
            reader.Close();
            return vers;
        }


        public DebtData[] GetDebt(string routeid)
        {
            return (new _1CDebt(routeid)).ConvertToArray() as DebtData[];
        }


        public RouteDebtParams[] GetDebtParams(string routeid)
        {
            return (new _1CRouteDebtParams(routeid)).ConvertToArray() as RouteDebtParams[];
        }


        public void SavePay(ClaimedPays clamedPays)
        {
            SaveData.SavePay(clamedPays); 
        }
    }
}
