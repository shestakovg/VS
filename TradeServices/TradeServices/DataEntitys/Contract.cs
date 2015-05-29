using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    public class Contract
    {

        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "PriceId")]
        public Guid Id { get; set; }

        [DataMember(Name = "Price")]
        public string Description { get; set; }
        
        [DataMember(Name = "Price")]
        public string Description { get; set; }

        [DataMember(Name = "LimitSum")]
        public double LimitSum { get; set; }

        [DataMember(Name = "Reprieve")]
        public int Reprieve { get; set; }
    }
}