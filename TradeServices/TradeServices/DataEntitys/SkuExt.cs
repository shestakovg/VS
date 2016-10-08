﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class SkuExt: Sku
    {
        [DataMember(Name = "Color")]
        public String Color { get; set; }

        [DataMember(Name = "OutStockColor")]
        public String OutStockColor { get; set; }
    }
}