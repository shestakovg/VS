using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TradeServices.Classes.IncomingData
{
    [DataContract]
    public class OrderHeader
    {
        [DataMember(Name = "orderUUID")]
        public string orderUUID { get; set; }
        [DataMember(Name = "outletId")]
        public string outletId { get; set; }
        [DataMember(Name = "orderDate")]
        public string orderDate { get; set; }
        [DataMember(Name = "deliveryDate")]
        public string deliveryDate { get; set; }
        [DataMember(Name = "orderNumber")]
        public int orderNumber { get; set; }
        [DataMember(Name = "notes")]
        public string notes { get; set; }
        [DataMember(Name = "payType")]
        public int payType { get; set; }
        [DataMember(Name = "autoLoad")]
        public int autoLoad { get; set; }
        [DataMember(Name = "routeId")]
        public string routeId { get; set; }
        
    }
}