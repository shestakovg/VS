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
           BodyStyle = WebMessageBodyStyle.Wrapped,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        string GetTestString();

        [OperationContract]
        [WebGet(
          UriTemplate = "/getallactiveroutes", BodyStyle = WebMessageBodyStyle.Wrapped ,
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
         ]
        TradeServices.Models.Route[] GetAllActiveRoutes();

        [OperationContract]
        [WebGet(
          UriTemplate = "/getroutetrip/{routeTripDate}",  BodyStyle = WebMessageBodyStyle.Wrapped ,
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
         ]
        RouteTripEx[] GetRouteTrip(string routeTripDate);

        [OperationContract]
        [WebGet(
         UriTemplate = "/getcheckin/{routeTripDate}&{routeId}",  BodyStyle = WebMessageBodyStyle.Wrapped
         ,ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
        ]
        ModelOutletCheckInEx[] GetCheckIn(string routeTripDate, string routeId);

        [OperationContract]
        [WebGet(
         UriTemplate = "/getlocationtraking/{routeTripDate}&{routeId}", BodyStyle = WebMessageBodyStyle.Wrapped
         , ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
        ]
        ModelRouteTripEx[] GetLocationTraking(string routeTripDate, string routeId);

        [OperationContract]
        [WebGet(
        UriTemplate = "/getrouteset/{routeId}", BodyStyle = WebMessageBodyStyle.Wrapped
        , ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
       ]
        RouteSet[] GetRouteSet(string routeId);

        [OperationContract]
        [WebGet(
        UriTemplate = "/getknownlocation/{outletid}", BodyStyle = WebMessageBodyStyle.Wrapped
        , ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
       ]
        KnownOutletLocation[] GetKnownLocation(String outletid);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "saveapprovedlocation",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool SaveApprovedLocation(KnownOutletLocation location);
    }
}
