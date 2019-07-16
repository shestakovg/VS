using EdinLib;
using EdinLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradeServices.Classes;
using TradeServices.Edi.Dto;

namespace TradeServices.Edi.Helpers
{
    public class EdiServiceHelper
    {
        public static IEnumerable<EdiDtoOrder> GetOrders()
        {
            var edin = EdinFactory.CreateEdin();
            var files = edin.GetOrderList();
            List<String> list = new List<string>(files);
            list.Reverse();
            List<EdiDtoOrder> orderList = new List<EdiDtoOrder>();
            
            
            foreach (var  docName in list)
            {
                var rawDoc = edin.GetDoc(docName);
                try
                {
                    EdinDocOrder order = EdinFactory.GetEdinDoc(docName, rawDoc) as EdinDocOrder;
                    if (order != null && order.DocIdentified)
                    {
                        EdiDtoOrder dto = new EdiDtoOrder()
                        {
                            Number = order.OrderModel.Order.Number,
                            Date = order.OrderModel.Order.Date,
                            FileName = docName
                        };
                        orderList.Add(dto);
                    }
                }
                catch(Exception e)
                {
                    EdiDtoOrder dto = new EdiDtoOrder()
                    {
                        Number = docName,
                        Date = "2010-01-01",
                        FileName = rawDoc + e.Message + e.InnerException?.Message ?? "\n" 
                    };
                    orderList.Add(dto);
                }
            }

            return orderList;

        }

        public static List<EdiDtoProcessMessage> Create1COrderFromEdinOrder(string fileName)
        {
            var edin = EdinFactory.CreateEdin();
            var rawDoc = edin.GetDoc(fileName);
            EdinDocOrder order = EdinFactory.GetEdinDoc(fileName, rawDoc) as EdinDocOrder;
            OrderEdi orderEdi = new OrderEdi(order.OrderModel.Order);
            var checking = orderEdi.GetAllCheckingAndCreate();
            return checking;
            // List<EdiDtoProcessMessage> 
        }
    }
}