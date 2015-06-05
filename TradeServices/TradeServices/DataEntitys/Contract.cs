using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{   [DataContract]
    public class Contract
    {
        /// <summary>
        /// 
        /// </summary>
        
        [DataMember(Name = "CustomerId")]
        public Guid CustomerId { get; set; }

        [DataMember(Name = "PriceId")]
        public Guid PriceId { get; set; }///

        [DataMember(Name = "PriceName")]
        public string PriceName { get; set; }


        [DataMember(Name = "LimitSum")]
        public double LimitSum { get; set; }

        [DataMember(Name = "Reprieve")]
        public string Reprieve { get; set; }

        [DataMember(Name = "PartnerId")]
        public Guid PartnerId { get; set; }


}
}