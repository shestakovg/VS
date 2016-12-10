using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Routing;
using TradeServices.DataEntitys.IncomingData;
using TradeServices.Models;

namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocation" in both code and config file together.
    [ServiceContract]
    public interface ILocation
    {
        [OperationContract]
        [WebGet(
           UriTemplate = "/getteststring",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        string GetTestString();

        [OperationContract]
        [WebGet(
          UriTemplate = "/getallactiveroutes",
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
         ]
        TradeServices.Models.Route[] GetAllActiveRoutes();

        [OperationContract]
        [WebGet(
          UriTemplate = "/getroutetrip/{routeTripDate}",
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
         ]
        RouteTripEx[] GetRouteTrip(string routeTripDate);

        [OperationContract]
        [WebGet(
         UriTemplate = "/getcheckin/{routeTripDate}&{routeId}",
         ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
        ]
        ModelOutletCheckInEx[] GetCheckIn(string routeTripDate, string routeId);
    }
}
