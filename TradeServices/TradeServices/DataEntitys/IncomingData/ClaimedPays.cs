using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys.IncomingData
{
    [DataContract]
    public class ClaimedPays
    {
        [DataMember(Name = "routeId")]
        public string routeId { get; set; }
        [DataMember(Name = "payDate")]
        public string payDate { get; set; }
        [DataMember(Name = "transactionId")]
        public string transactionId { get; set; }
        [DataMember(Name = "paySum")]
        public double paySum { get; set; }
    }
}