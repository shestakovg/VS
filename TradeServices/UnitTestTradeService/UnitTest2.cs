using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeServices.Classes;
using TradeServices.DataEntitys;
using TradeServices;
using TradeServices.DataEntitys.IncomingData;

namespace UnitTestTradeService
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void Test1CConnection()
        {
            _1C.V8.Data.V8DbConnection con =  _1CConnection.CreateAndOpenConnection();
        }

        [TestMethod]
        public void TestOrder()
        {
            //OrderWareHouse wh = new OrderWareHouse(30, SaveData.getConnection(), OrdersWH.MainWareHouse);
            //if (wh.Initialized)
            //{
            //    if (wh.prepare1CStructure())
            //        wh.createOrder();
            //}
        }
        [TestMethod]
        public void TestOrderThread()
        {
            
            ProcessIncomingData.ProcessData();
        }

        [TestMethod]
        public void TestCheckIn()
        {
            //Location loc = new Location();
            //OutletCheckIn[] chinAr = new OutletCheckIn[2]
            //    {
            //         new OutletCheckIn()
            //        {
            //            routeId = Guid.NewGuid().ToString(),
            //            outletId = Guid.NewGuid().ToString(),
            //            sateliteTime = "18.09.2016 12:12:12 PM",
            //            latitude = 45.789923,
            //            longtitude = 56.898324
            //        },
            //          new OutletCheckIn()
            //        {
            //            routeId = Guid.NewGuid().ToString(),
            //            outletId = Guid.NewGuid().ToString(),
            //            sateliteTime = "18.09.2016 12:10:10 PM",
            //            latitude = 45.789923,
            //            longtitude = 56.898324
            //        }
            //    };
            //loc.SaveCheckIn(chinAr);
        }
    }
}
