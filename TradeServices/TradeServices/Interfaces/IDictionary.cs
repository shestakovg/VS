using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TradeServices.Classes.IncomingData;
using TradeServices.DataEntitys;
using TradeServices.DataEntitys.IncomingData;

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

        [OperationContract]
        [WebGet(
            UriTemplate = "/getroute/{branchid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Route[] GetRoute(string branchid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getrouteset/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        RouteSet[] GetRouteSet(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getcontract/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Contract[] GetContracts(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getskugroup",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        SkuGroup[] GetSkuGroup();

        [OperationContract]
        [WebGet(
            UriTemplate = "/getsku",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Sku[] GetSku();

        [OperationContract]
        [WebGet(
            UriTemplate = "/getprice/{priceId}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Price[] GetPrice(string priceId);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getbalancesku/{branchId}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        BalanceSku[] GetBalanceSku(string branchId);

        #region Получение данных
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "saveheader",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveHeader(OrderHeader orderHeader);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "savedetail",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveOrderDetail(OrderDetail orderDetail);

        [OperationContract]
        [WebGet(
            UriTemplate = "/marcorder/{orderuuid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        MarcOrder MarcOrder(string orderuuid);
        #endregion
    }
}
