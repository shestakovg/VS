using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
     [DataContract]
    public class ClientCardSku
    {
         [DataMember(Name = "SkuId")]
         public Guid SkuId { get; set; }
         [DataMember(Name = "OutletId")]
         public Guid OutletId { get; set; }

         [DataMember(Name = "LastDate")]
         public string LastDate { get; set; }

         [DataMember(Name = "Qty")]
         public int Qty { get; set; }
    }
}