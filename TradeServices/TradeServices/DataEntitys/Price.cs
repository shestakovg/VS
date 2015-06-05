using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
     [DataContract]
    public class Price
    {
        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }

        [DataMember(Name = "PriceId")]
        public Guid PriceId { get; set; }

       [DataMember(Name = "Pric")]
        public double Pric { get; set; }
     }
}