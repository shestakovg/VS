using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys.IncomingData
{
    [DataContract]
    public class OrderDetail
    {
        [DataMember(Name = "orderUUID")]
        public string orderUUID { get; set; }
        [DataMember(Name = "skuId")]
        public string skuId { get; set; }
        [DataMember(Name = "qty1")]
        public int qty1 { get; set; }
        [DataMember(Name = "qty2")]
        public int qty2 { get; set; }
        [DataMember(Name = "priceType")]
        public string priceType { get; set; }
        [DataMember(Name = "finalDate")]
        public string finalDate { get; set; }
    }

}