using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{ [DataContract]
    public class SkuGroup
    {
   
        [DataMember(Name = "GroupId")]
        public Guid GroupId { get; set; }

        [DataMember(Name = "GroupName")]
        public string GroupName { get; set; }
   
         [DataMember(Name = "GroupParentId")]
        public Guid GroupParentId { get; set; }

        [DataMember(Name = "GroupParentName")]
        public string GroupParentName { get; set; }

        [DataMember(Name = "Amount")]
        public double Amount { get; set; }

        [DataMember(Name = "OutletCount")]
        public int OutletCount { get; set; }
    }
}