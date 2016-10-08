using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class SalesFact
    {
        [DataMember(Name = "GroupId")]
        public Guid GroupId { get; set; }
        [DataMember(Name = "FactAmount")]
        public double FactAmount { get; set; }
        [DataMember(Name = "FactOutletCount")]
        public int FactOutletCount { get; set; }
    }
}