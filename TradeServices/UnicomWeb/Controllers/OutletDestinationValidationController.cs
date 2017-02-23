using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using UnicomWeb.ModelForJS;
using TradeServices.Models;
using UnicomWeb.LocationService;

namespace UnicomWeb.Controllers
{
    public class OutletDestinationValidationController : Controller
    {
        // GET: OutletDestinationValidation
        public ActionResult Index()
        {
            return View();
        }

      
        public string GetAllRoutes()
        {
           Route[] routeArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                routeArray = client.GetAllActiveRoutes();
            }
            if (routeArray == null) throw new Exception("Не удалось получить маршруты");
            return JsonConvert.SerializeObject(GetRouteTreeList(routeArray, Guid.Empty));
            //return new JsonResult() {Data = GetRouteTreeList(routeArray, Guid.Empty)  , JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

#if DEBUG
        public Route[] GetRoutes()
        {
            Route[] routeArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                routeArray = client.GetAllActiveRoutes();
            }
            return routeArray;
        }
#endif
        private  List<JsTreeNode> GetRouteTreeList(Route[] source, Guid parentId)
        {
            List<JsTreeNode> nodeList = new List<JsTreeNode>();
            var nodes = from node in source
                where node.ParentId == parentId
                select node;

            foreach (var node in nodes)
            {
                JsTreeNode newNode = new JsTreeNode()
                {
                    Text = node.Description,
                    Tags =new string[] { node.ParentId.ToString()},
                    Id = node.Id.ToString(),
                    Silent = true
                };

                if (source.Count(x => x.ParentId == node.Id) > 0)
                {
                    newNode.Nodes = GetRouteTreeList(source, node.Id);
                }
                nodeList.Add(newNode);
            }
            return nodeList;
        }

        public JsonResult GetRouteSet(string routeId, bool showOnlyUnknown)
        {
            if (routeId == null) routeId = Guid.Empty.ToString();
            RouteSet[] routeArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                routeArray = client.GetRouteSet(routeId);
            }
            if (routeArray == null) throw new Exception("Не удалось получить маршруты");
            return
               //JsonConvert.SerializeObject(from m in routeArray select new  {outletname = m.OutletName});
               // JsonConvert.SerializeObject(routeArray);
               new JsonResult() { Data = 
                (from r in routeArray
                 where (r.Longtitude == null && showOnlyUnknown) || !showOnlyUnknown
                 select r), JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult GetKnownLocation(string outletid)
        {
            KnownOutletLocation[] ol = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                ol = client.GetKnownLocation(outletid);
            }
            return new JsonResult() {Data = ol, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

       // [HttpPost]
        public bool SaveApprovedLocation(KnownOutletLocation location)
        {
            bool result = true;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                result = client.SaveApprovedLocation(location);
            }
            return result;
        }
    }
    
}