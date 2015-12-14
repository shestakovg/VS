using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeServices.Classes.IncomingData;
using TradeServices.Classes;
using TradeServices;
using TradeServices.DataEntitys.IncomingData;


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
