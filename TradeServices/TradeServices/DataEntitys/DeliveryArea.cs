using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class DeliveryArea
    {
        [DataMember(Name = "idRef")]
        public Guid idRef { get; set; }
        [DataMember(Name = "Description")]
        public String Description { get; set; }
    }
}