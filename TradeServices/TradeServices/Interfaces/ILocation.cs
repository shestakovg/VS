using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TradeServices.DataEntitys.IncomingData;

namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocation" in both code and config file together.
    [ServiceContract]
    public interface ILocation
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "savecheckin",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveCheckIn(OutletCheckIn[] checkInArray);
    }
}
