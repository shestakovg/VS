using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    public class Contract
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }
        [DataMember(Name = "CustomerId")]
        public string CustomerId { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "PriceId")]
        public Guid PriceId { get; set; }

        [DataMember(Name = "Price")]
        public string Price { get; set; }

        [DataMember(Name = "PriceDescription")]
        public string PriceDescription { get; set; }

        [DataMember(Name = "LimitSum")]
        public double LimitSum { get; set; }

        [DataMember(Name = "Reprieve")]
        public int Reprieve { get; set; }
    }
}