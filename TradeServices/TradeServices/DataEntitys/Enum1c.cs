using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class Enum1c
    {
        [DataMember(Name = "Name")]
        public String Name { get; set; }
        [DataMember(Name = "Order")]
        public int Order { get; set; }
    }
}