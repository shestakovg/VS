using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class RouteDebtParams
    {
        [DataMember(Name = "debtControl")]
        public int debtControl;
        [DataMember(Name = "allowOverdueSum")]
        public double allowOverdueSum;
    }
}