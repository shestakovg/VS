using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class Sku
    {

        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }

        [DataMember(Name = "SkuName")]
        public string SkuName { get; set; }

        [DataMember(Name = "SkuParentId")]
        public Guid SkuParentId { get; set; }

        [DataMember(Name = "QtyPack")]
        public double QtyPack { get; set; }

        [DataMember(Name = "Article")]
        public string Article { get; set; }

        [DataMember(Name = "OnlyFact")]
        public int OnlyFact { get; set; }
        [DataMember(Name = "CheckCountInBox")]
        public int CheckCountInBox { get; set; }
        [DataMember(Name = "onlyMWH")]
        public int OnlyMWH { get; set; }
    }
}
