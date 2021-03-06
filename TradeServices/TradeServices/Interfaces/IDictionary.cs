﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TradeServices.Classes.IncomingData;
using TradeServices.DataEntitys;
using TradeServices.DataEntitys.IncomingData;
using TradeServices.Edi.Dto;

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
           UriTemplate = "/getclientcardsku/{routeid}",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        ClientCardSku[] GetClientCardSku(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getoutletinfo/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        OutletInfo[] GetOutletInfo(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getcontract/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Contract[] GetContracts(string routeid);

        [WebGet(
            UriTemplate = "/getdebt/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        DebtData[] GetDebt(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getskugroup/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        SkuGroup[] GetSkuGroup(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getsku",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Sku[] GetSku();

        [OperationContract]
        [WebGet(
           UriTemplate = "/getskuext/{routeid}",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        SkuExt[] GetSkuExt(string routeid);

        [OperationContract]
        [WebGet(
          UriTemplate = "/getsalesfact/{routeid}",
          //BodyStyle = WebMessageBodyStyle.WrappedRequest,
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
         ]
        SalesFact[] GetSalesFact(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getskufact",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        SkuFact[] GetSkuFact();

        [OperationContract]
        [WebGet(
            UriTemplate = "/getprice/{priceId}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        Price[] GetPrice(string priceId);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getpricechanges",
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        PriceChanges[] GetPriceChanges();

        [OperationContract]
        [WebGet(
            UriTemplate = "/getbalancesku/{branchId}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        BalanceSku[] GetBalanceSku(string branchId);

        [WebGet(
           UriTemplate = "/getdebtparams/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        
        RouteDebtParams[] GetDebtParams(string routeid);


        [WebGet(
           UriTemplate = "/getspecification/{routeid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        Specification[] GetSpecification(string routeId);

        [WebGet(
           UriTemplate = "/getoutletcategory",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        Enum1c[] GetOutletCategory();

        [WebGet(
           UriTemplate = "/getroutedays",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        Enum1c[] GetRouteDays();

        [WebGet(
           UriTemplate = "/getdeliveryarea",
           //BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        DeliveryArea[] GetDeliveryArea();

        [WebGet(
           UriTemplate = "/gettasks/{routeid}",
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
          ]
        ManagersTask[] GetTasks(string routeid);

        [OperationContract]
        [WebGet(
            UriTemplate = "/getapk"
            )
           ]
        System.IO.Stream getapk();

        [OperationContract]
        [WebGet(
            UriTemplate = "/getversion",
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        VersionApk getversion();
        //Edi services
        #region EdiServices
        [WebGet(
          UriTemplate = "/getediorders",
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
        ]
        EdiDtoOrder[] GetEdiOrders();
        [WebGet(
          UriTemplate = "/newediorder/{edifile}",
          ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
        ]
        EdiDtoProcessMessage[] NewEdiOrder(string edifile);
        //[WebGet(
        //  UriTemplate = "/getediordersxml",
        //  ResponseFormat = WebMessageFormat.Xml, RequestFormat = WebMessageFormat.Json)
        //]
        //EdiDtoOrder[] GetEdiOrdersXml();
        #endregion
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

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "savedetail2",
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //void SaveOrderDetail2(OrderDetail2 orderDetail2);

        [OperationContract]
        [WebGet(
            UriTemplate = "/marcorder/{orderuuid}",
            //BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)
           ]
        MarcOrder MarcOrder(string orderuuid);


        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "savepay",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SavePay(ClaimedPays clamedPays);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "savenewcustomer",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveNewCustomer(NewCustomers  newCustomer);
        #endregion

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "checkin",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveCheckIn(OutletCheckIn[] checkInArray);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "tracking",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveTracking(TrackingEntity[] checkInArray);

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "savenoresult",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveNoResult(NoResultData[] noresult);

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "savetask",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SaveTask(ManagersTask[] tasks);

    }
}
