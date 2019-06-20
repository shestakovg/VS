using EdinLib;
using EdinLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    EdinOrder order = EdinFactory.GetEdinDoc(docName, rawDoc) as EdinOrder;
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

                }
            }

            return orderList;

        }
    }
}