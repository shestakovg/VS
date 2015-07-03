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
        [DataMember]
        public string routeId;
        [DataMember]
        public string payDate;
        [DataMember]
        public string transactionId;
        [DataMember]
        public double paySum;
    }
}