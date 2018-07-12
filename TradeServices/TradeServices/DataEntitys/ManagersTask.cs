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
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "resultDescription")]
        public string ResultDescription { get; set; } = String.Empty;
        [DataMember(Name = "status")]
        public int Status { get; set; } = 0;

        public override string ToString() => $"Number {this.Number} Status {this.Status}  ResultDescription {ResultDescription}";
        
    }
}