using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class RouteSet
    {
        [DataMember(Name = "OutletId")]
        public Guid OutletId { get; set; }
        [DataMember(Name = "OutletName")]
        public string OutletName { get; set; }
        [DataMember(Name = "VisitDay")]
        public string VisitDay { get; set; }
        [DataMember(Name = "VisitDayId")]
        public int VisitDayId { get; set; }
        [DataMember(Name = "VisitOrder")]
        public int VisitOrder { get; set; }
        [DataMember(Name = "CustomerId")]
        public Guid CustomerId { get; set; }
        [DataMember(Name = "CustomerName")]
        public string CustomerName { get; set; }
        [DataMember(Name = "PartnerId")] 
        public Guid PartnerId { get; set; }
        [DataMember(Name = "PartnerName")]
        public string PartnerName { get; set; }
        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "IsRoute")]
        public int IsRoute { get; set; }

        [DataMember(Name = "CustomerClass")]
        public string CustomerClass { get; set; }
    }
}
