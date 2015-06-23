using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeServices.Classes;
using TradeServices.DataEntitys;

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
            OrderWareHouse wh = new OrderWareHouse(30, SaveData.getConnection(), OrdersWH.MainWareHouse);
            if (wh.Initialized)
            {
                if (wh.prepare1CStructure())
                    wh.createOrder();
            }
        }
        [TestMethod]
        public void TestOrderThread()
        {
            
            ProcessIncomingData.ProcessData();
        }
    }
}
