using System;
using System.Runtime.Serialization;


namespace TradeServices.DataEntitys
{
    [DataContract]
    public class PriceChanges
    {
        [DataMember(Name = "Date")]
        public string Date;
        [DataMember(Name = "SkuId")]
        public Guid SkuId { get; set; }
        [DataMember(Name = "PriceId")]
        public Guid PriceId { get; set; }
        [DataMember(Name = "NewPrice")]
        public double NewPrice { get; set; }
        [DataMember(Name = "OldPrice")]
        public double OldPrice { get; set; }
    }
}