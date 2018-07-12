using System;
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

        public ClientCardSku[] GetClientCardSku(string routeid)
        {
            _1CClientCardSku branches = new _1CClientCardSku(routeid);
            var res = branches.ConvertToArray() as ClientCardSku[];
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

        public SkuExt[] GetSkuExt(string routeid)
        {
            _1CSkuExt getsku = new _1CSkuExt(routeid);
            SkuExt[] res = getsku.ConvertToArray() as SkuExt[];
            (getsku as _1CUtManager).Dispose();
            return res;
        }

        public SalesFact[] GetSalesFact(string routeid)
        {
            _1CSalesFact getsku = new _1CSalesFact(routeid);
            SalesFact[] res = getsku.ConvertToArray() as SalesFact[];
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

        public Specification[] GetSpecification(string routeId)
        {
            _1CSpecification getSpec = new _1CSpecification(routeId);
            Specification[] res = getSpec.ConvertToArray() as Specification[];
            (getSpec as _1CUtManager).Dispose();
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


        public SkuFact[] GetSkuFact()
        {
            _1CSkuFact getsku = new _1CSkuFact();
            SkuFact[] res = getsku.ConvertToArray() as SkuFact[];
            (getsku as _1CUtManager).Dispose();
            return res;
        }


        public OutletInfo[] GetOutletInfo(string routeid)
        {
            _1COutletInfo routeSet = new _1COutletInfo(routeid);
            var res = routeSet.ConvertToArray() as OutletInfo[];
            (routeSet as _1CUtManager).Dispose();
            return res;
        }

        public void SaveCheckIn(OutletCheckIn[] checkInArray)
        {
           
            string cmdText = @"INSERT INTO [dbo].[OutletCheckIn]
                                   ([routeId]
                                   ,[outletId]
                                   ,[checkInTime]
                                   ,[latitude]
                                   ,[longtitude])
                             VALUES
                                   (@routeId
                                   ,@outletId
                                   ,@checkInTime
                                   ,@latitude
                                   ,@longtitude)";
            SqlConnection con = SaveData.getConnection();

            foreach (OutletCheckIn checkIn in checkInArray)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("routeId", new Guid(checkIn.routeId));
                cmd.Parameters.AddWithValue("outletId", new Guid(checkIn.outletId));
                cmd.Parameters.AddWithValue("checkInTime", checkIn.sateliteTime);
                cmd.Parameters.AddWithValue("latitude", Convert.ToDecimal(checkIn.latitude));
                cmd.Parameters.AddWithValue("longtitude", Convert.ToDecimal(checkIn.longtitude));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
               // cmd.Dispose();
            }
            con.Close();
           
        }

        public void SaveTracking(TrackingEntity[] checkInArray)
        {

            string cmdText = @"INSERT INTO [dbo].[LocationTraking]
                                   ([routeId]
                                   ,[checkInTime]
                                   ,[latitude]
                                   ,[longtitude])
                             VALUES
                                   (@routeId
                                   ,@checkInTime
                                   ,@latitude
                                   ,@longtitude)";
            SqlConnection con = SaveData.getConnection();

            foreach (TrackingEntity checkIn in checkInArray)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("routeId", new Guid(checkIn.routeId));
                cmd.Parameters.AddWithValue("checkInTime", checkIn.sateliteTime);
                cmd.Parameters.AddWithValue("latitude", Convert.ToDecimal(checkIn.latitude));
                cmd.Parameters.AddWithValue("longtitude", Convert.ToDecimal(checkIn.longtitude));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                // cmd.Dispose();
            }
            con.Close();

        }

        public Enum1c[] GetOutletCategory()
        {
            _1COutletCategory getcategory = new _1COutletCategory();
            Enum1c[] res = getcategory.ConvertToArray() as Enum1c[];
            (getcategory as _1CUtManager).Dispose();
            return res;
        }

        public Enum1c[] GetRouteDays()
        {
            _1CRouteDay getcategory = new _1CRouteDay();
            Enum1c[] res = getcategory.ConvertToArray() as Enum1c[];
            (getcategory as _1CUtManager).Dispose();
            return res;
        }

        public DeliveryArea[] GetDeliveryArea()
        {
            _1CDeliveryArea getcategory = new _1CDeliveryArea();
            DeliveryArea[] res = getcategory.ConvertToArray() as DeliveryArea[];
            (getcategory as _1CUtManager).Dispose();
            return res;
        }

        public void SaveNewCustomer(NewCustomers newCustomer)
        {
            SaveData.SaveNewCustomer(newCustomer);
        }

        public void SaveNoResult(NoResultData[] noresult)
        {
            SaveData.SaveNoResult(noresult);
        }

        public ManagersTask[] GetTasks(string routeid)
        {
            _1CTask gettask = new _1CTask("a0683ec4-da9d-11e4-826d-240a64c9314e");
            ManagersTask[] res = gettask.ConvertToArray() as ManagersTask[];
            (gettask as _1CUtManager).Dispose();
            return res;
        }

        public void SaveTask(ManagersTask[] tasks)
        {
            SaveData.SaveTask(tasks);
        }
    }
}
