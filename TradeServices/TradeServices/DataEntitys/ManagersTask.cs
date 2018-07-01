using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class ManagersTask
    {
        [DataMember(Name = "reference")]
        public Guid Reference { get; set; }
        [DataMember(Name = "number")]
        public string Number { get; set; }
        [DataMember(Name = "routeId")]
        public Guid RouteId { get; set; }
        [DataMember(Name = "outletId")]
        public Guid OutletId { get; set; }
        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}