using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.DataEntitys
{
    [DataContract]
    public class VersionApk
    {
        [DataMember(Name="ver")] //
        public string ver;
        [DataMember(Name = "verdate")]
        public string verdate;
        ///
    }

    
}
