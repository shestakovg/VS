using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TradeServices.DataEntitys;

namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDictionary" in both code and config file together.
    [ServiceContract]

    public interface IDictionary
    {

        [OperationContract]
        [WebGet(
            UriTemplate = "/getbranch",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Branch[] GetBranch();
    }
}
