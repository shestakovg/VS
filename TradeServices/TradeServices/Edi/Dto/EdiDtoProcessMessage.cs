using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.Edi.Dto
{
    [DataContract]
    public class EdiDtoProcessMessage
    {
        [DataMember(Name = "error")]
        public int Error { get; set; } = 0;
        [DataMember(Name = "text")]
        public string Text { get; set; } = String.Empty;
        [DataMember(Name = "document")]
        public Guid DocumentId { get; set; } = Guid.Empty;
    }
}