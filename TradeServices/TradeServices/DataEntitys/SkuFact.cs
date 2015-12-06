using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class SkuFact
    {
        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }
        [DataMember(Name = "PriceId")]
        public Guid PriceId { get; set; }
    }
}