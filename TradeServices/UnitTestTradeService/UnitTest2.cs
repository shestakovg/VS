﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeServices.Classes;
using TradeServices.DataEntitys;
using TradeServices;
using TradeServices.DataEntitys.IncomingData;
using EdinLib.Helpers;
using EdinLib;
using TradeServices.Edi.Helpers;

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
            using (ServiceReference1.LocationClient cl = new ServiceReference1.LocationClient())
            {
                cl.Open();
                var b = cl.GetTestString();
                var s= cl.GetRouteTrip("09122016");
                var c = cl.GetCheckIn("09122016", "A0683EC4-DA9D-11E4-826D-240A64C9314E");
            }
        }

        [TestMethod]
        public void TestSimpleEdin()
        {
            var list = EdinFactory.CreateEdin().GetOrderList();
            Assert.AreNotEqual(list.GetEnumerator().MoveNext(), false);
        }

        [TestMethod]
        public void TestEdinOrdersDto()
        {
             var orders = EdiServiceHelper.GetOrders();
        }

        [TestMethod]
        public void TestEdinOrdersCreate1COrder()
        {
            string fileName = "order_20190625165014_497371924.xml"; 
            EdiServiceHelper.Create1COrderFromEdinOrder(fileName);
        }

        [TestMethod]
        public void GetDbStucture()
        {
            _1C.V8.Data.V8DbConnection con = _1CConnection.CreateAndOpenConnection();
            var res =  _1C.V8.Data.V8.Call(con, con.Connection, "ПолучитьСтруктуруХраненияБазыДанных");
        }
    }
}
