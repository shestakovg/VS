using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class BalanceSku
    {
        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }

        [DataMember(Name = "StockG")]
        public double StockG { get; set; }

        [DataMember(Name = "StockR")]
        public double StockR { get; set; }
    }
}