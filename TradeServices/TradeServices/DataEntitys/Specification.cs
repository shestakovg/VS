using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class Specification
    {
        [DataMember(Name = "OutletId")]
        public Guid OutletId { get; set; }

        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }
    }
}