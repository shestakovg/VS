using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeServices.Classes.IncomingData;
using TradeServices.Classes;
using TradeServices;
using TradeServices.DataEntitys.IncomingData;

namespace TradeServices.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetRouteSetTest()
        {
            Location loc = new Location();
            var res =loc.GetRouteSet("446609B1-9814-11E5-88AE-3640B58DD6A2");
            Assert.IsNotNull(res);
        }

        [TestMethod()]
        public void GetKnownLocationTest()
        {
            Location loc = new Location();
            var res = loc.GetKnownLocation("75767EC1-78B2-11E6-9E2B-3640B58DD6A2");
            Assert.IsNotNull(res);
        }

        [TestMethod()]
        public void SaveApprovedLocationTest()
        {
            Location loc = new Location();
            var res = loc.SaveApprovedLocation(new Models.KnownOutletLocation()
            {
                OutletId = new  Guid("2A037CCD-0E78-48B9-B8C7-6DF098F47EE6"),
                Latitude = 12.789,
                Longtitude = 67.890645
            }
                );
            Assert.IsNotNull(res);
        }
        [TestMethod()]
        public void SaveNoResultTest()
        {
            NoResultData[] nores = {
                    new NoResultData()
                    { Date = "03/21/2017",
                      OutletId = "CA9BB2D3-8BAF-4E35-928A-2A0564070E1B",
                      ReasonId =1
                    },
                     new NoResultData()
                    { Date = "03/21/2017",
                      OutletId = "CA9BB2D3-8BAF-4E35-928A-2A0564070E1B",
                      ReasonId =2
                    },
                      new NoResultData()
                    { Date = "03/21/2017",
                      OutletId = "CA9BB2D3-8BAF-4E35-928A-2A0564070E1B",
                      ReasonId =3
                    } };

                SaveData.SaveNoResult(nores);
            
        }
    }
}

namespace UnitTestTradeService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSaveOrderHeader()
        {
            OrderHeader header = new OrderHeader
            {
                orderUUID = "D54DE44D-BF7F-4820-AD83-557807EBA073",
                outletId = Guid.NewGuid().ToString(),
                orderDate = "11.06.2015",
                orderNumber = 1,
                notes = "НОВАЯ СТРОКА",
                payType = 0,
                autoLoad = 1,
                routeId = Guid.NewGuid().ToString()
            };
            SaveData.SaveHeader(header);
        }

        [TestMethod]
        public void TestSaveOrderDetail()
        {
            OrderDetail header = new OrderDetail
            {
                orderUUID = "D54DE44D-BF7F-4820-AD83-557807EBA073",
                skuId = Guid.NewGuid().ToString(),
                qty1 = 100,
                qty2 = 200
            };
            SaveData.SaveDetailOrder(header);
        
        }
          [TestMethod]
        public void TestSkuFact()
        {
            IDictionary dic = new Dictionary();
            dic.GetSkuFact();
        }

          [TestMethod]
          public void TestDayOfWeek()
          {
             DateTime dt = new DateTime(2015,12,13);
             var d = (int)dt.DayOfWeek;
          }


    }
}
