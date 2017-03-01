using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeServices.Models;
using UnicomWeb.LocationService;
//using UnicomWeb.Models;
using TradeUtils;
using UnicomWeb.ModelForJS;
using UnicomWeb.Enums;

namespace UnicomWeb.Controllers
{
    public class RouteTripController : Controller
    {
        // GET: RouteTrip
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult DailyRouteTrip(string routedate)
        {
            RouteTripEx[] tripArray = null;
            DateTime locDt = Convert.ToDateTime(routedate);
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                tripArray = client.GetRouteTrip(locDt.ToString("ddMMyyyy"));
            }
            if (tripArray == null) return HttpNotFound();
            ViewBag.RouteDate = locDt.ToString("ddMMyyyy");
            ViewBag.SelectedDate = locDt.ToString("dd.MM.yyyy"); ;
            return PartialView(tripArray.Where(c => !String.IsNullOrEmpty(c.RouteName)));

        }

        [Authorize]
        public ActionResult CheckInMap(string routedate, string routeid, string routename)
        {
            ViewBag.RouteDate = routedate.ToDateTime().ToString("dd.MM.yyyy");
            ViewBag.RouteId = routeid;
            ViewBag.RouteName = routename;
            ViewBag.DisplayType = EnumDisplayRouteType.CheckIn.ToString();
            ModelOutletCheckInEx[] checkInArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                checkInArray = client.GetCheckIn(routedate, routeid);
            }
            if (checkInArray == null) return HttpNotFound();
            return View(checkInArray);

        }
        [Authorize]
        public ActionResult LocationMap(string routedate, string routeid, string routename)
        {
            ViewBag.RouteDate = routedate.ToDateTime().ToString("dd.MM.yyyy");
            ViewBag.RouteId = routeid;
            ViewBag.RouteName = routename;
            ViewBag.DisplayType = EnumDisplayRouteType.Trip.ToString();
            ModelOutletCheckInEx[] checkInArray = new ModelOutletCheckInEx[] { };
            //using (LocationService.LocationClient client = new LocationService.LocationClient())
            //{
            //    client.Open();
            //    checkInArray = client.GetCheckIn(routedate, routeid);
            //}
            //if (checkInArray == null) return HttpNotFound();
            return View("CheckInMap", checkInArray);

        }
        [Authorize]
        public ActionResult CheckInMapWithTrip(string routedate, string routeid, string routename)
        {
            ViewBag.RouteDate = routedate.ToDateTime().ToString("dd.MM.yyyy");
            ViewBag.RouteId = routeid;
            ViewBag.RouteName = routename;
            ViewBag.DisplayType = EnumDisplayRouteType.Both.ToString();
            ModelOutletCheckInEx[] checkInArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                checkInArray = client.GetCheckIn(routedate, routeid);
            }
            if (checkInArray == null) return HttpNotFound();
            return View("CheckInMap", checkInArray);

        }
        [Authorize]
        public JsonResult GetJSONCheckIn(string routedate, string idroute)
        {
            ModelOutletCheckInEx[] checkInArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                checkInArray = client.GetCheckIn(routedate.Replace(".",""), idroute);
            }
            if (checkInArray == null) throw new Exception("Не удалось получить массив отметок");
            int numb = 1;
            return Json(
                          from c in  checkInArray
                          orderby c.CheckInTime
                          select new JSOutletCheckIn
                          {
                              Outlet = c.Outlet,
                              OutletId = c.OutletId,
                              CheckInTime = c.CheckInTime,
                              CheckInTimeStr = c.CheckInTime.ToString("HH:mm"),
                              Id = c.Id,
                              Latitude = c.Latitude,
                              Longtitude = c.Longtitude,
                              CheckInNumber = numb++
                          }
                            , JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetJSONTrip(string routedate, string idroute)
        {
            ModelRouteTripEx[] checkInArray = null;
            using (LocationService.LocationClient client = new LocationService.LocationClient())
            {
                client.Open();
                checkInArray = client.GetLocationTraking(routedate.Replace(".", ""), idroute);
            }
            if (checkInArray == null) throw new Exception("Не удалось получить массив отметок");
            int numb = 1;
            return new JsonResult(){
                Data = from c in checkInArray
                orderby c.CheckInTime
                select new ModelRouteTripEx
                {
                    CheckInTime = c.CheckInTime,
                    CheckInTimeString = c.CheckInTime.ToString("HH:mm:ss"),
                    Id = c.Id,
                    Latitude = c.Latitude,
                    Longtitude = c.Longtitude,
                    RowNum = numb++
                }
                            ,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue};
        }
    }
}