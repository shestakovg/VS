using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class OutletInfo
        
    {
        //OutletId	Category	Manager1	Manager2	Phone1	Phone2	DeliveryDay	ManagerTime	ReciveTime	ContactPersonOutletId	Category	Manager1	Manager2	Phone1	Phone2	DeliveryDay	ManagerTime	ReciveTime	ContactPerson
        [DataMember(Name = "OutletId")]
        public Guid OutletId { get; set; }

        [DataMember(Name = "Category")]
        public string Category { get; set; }
        
        [DataMember(Name ="Manager1")]
        public string Manager1 { get; set; }
        [DataMember(Name = "Manager2")]
        public string Manager2 { get; set; }
        [DataMember(Name = "Phone1")]
        public string Phone1 { get; set; }
        [DataMember(Name = "Phone2")]
        public string Phone2 { get; set; }
        [DataMember(Name = "DeliveryDay")]
        public string DeliveryDay { get; set; }
        [DataMember(Name = "ManagerTime")]
        public string ManagerTime { get; set; }
        [DataMember(Name = "ReciveTime")]
        public string ReciveTime { get; set; }
        [DataMember(Name = "ContactPerson")]
        public string ContactPerson { get; set; }
    }
}