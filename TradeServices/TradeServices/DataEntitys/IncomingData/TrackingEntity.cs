using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys.IncomingData
{
    [DataContract]
    public class TrackingEntity
    {
        [DataMember(Name = "routeId")]
        public string routeId { get; set; }
        [DataMember(Name = "longtitude")]
        public double longtitude;
        [DataMember(Name = "latitude")]
        public double latitude;
        [DataMember(Name = "sateliteTime")]
        public string sateliteTime;
    }
}