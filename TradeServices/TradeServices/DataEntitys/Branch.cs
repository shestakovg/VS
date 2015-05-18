using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class Branch
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}