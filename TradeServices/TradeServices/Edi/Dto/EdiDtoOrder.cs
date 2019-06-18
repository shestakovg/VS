using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.Edi.Dto
{
    [DataContract]
    public class EdiDtoOrder
    {
        [DataMember(Name ="number")]
        public string Number { get; set; }
        [DataMember(Name = "date")]
        public string Date { get; set; }
        [DataMember(Name = "file")]
        public string FileName { get; set; }
    }
}