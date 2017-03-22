using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys.IncomingData
{
    [DataContract]
    public class NoResultData
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }
        [DataMember(Name = "outletId")]
        public string OutletId { get; set; }
        [DataMember(Name = "reasonId")]
        public int ReasonId { get; set; }
    }
}