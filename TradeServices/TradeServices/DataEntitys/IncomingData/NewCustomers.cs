using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys.IncomingData
{
    [DataContract]
    public class NewCustomers
    {
        [DataMember(Name = "routeId")]
        public string routeId { get; set; }
        [DataMember(Name = "registrationDate")]
        public string registrationDate { get; set; }
        [DataMember (Name ="territory")]
        public string territory { get; set; }
        [DataMember(Name = "CustomerName")]
        public string CustomerName { get; set; }
        [DataMember(Name = "DeliveryAddress")]
        public string DeliveryAddress { get; set; }
        [DataMember(Name = "OutletCategoty")]
        public int OutletCategoty { get; set; }
        [DataMember(Name = "PriceType")]
        public string PriceType { get; set; }
        [DataMember(Name = "VisitDay")]
        public int VisitDay { get; set; }
        [DataMember(Name = "DeliveryDay")]
        public int DeliveryDay { get; set; }
        [DataMember(Name = "Manager1Name")]
        public string Manager1Name { get; set; }
        [DataMember(Name = "Manager2Name")]
        public string Manager2Name { get; set; }
        [DataMember(Name = "Manager1Phone")]
        public string Manager1Phone { get; set; }
        [DataMember(Name = "Manager2Phone")]
        public string Manager2Phone { get; set; }
        [DataMember(Name = "AdditionalInfo")]
        public string AdditionalInfo { get; set; }
    }
}