using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Routing;
using TradeServices.Classes;
using TradeServices.DataEntitys.IncomingData;
using TradeServices.Models;

namespace TradeServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Location" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Location.svc or Location.svc.cs at the Solution Explorer and start debugging.
    public class Location : ILocation
    {
        public TradeServices.Models.Route[] GetAllActiveRoutes()
        {
            TradeServices.Models.Route[] result = null;
            using (TradeContext db = new TradeContext())
            {
                IQueryable<TradeServices.Models.Route>  res = from r in db.Routes
                             where !r.isDeleted && !r.isFolder
                             select r;
                result = res.ToArray();

            }
            return result;
        }

        public ModelOutletCheckInEx[] GetCheckIn(string routeTripDate, string routeId)
        {
            DateTime locRouteTripDateStart = routeTripDate.ToDateTime();
            DateTime locRouteTripDatEnd = locRouteTripDateStart.AddDays(1).AddMilliseconds(-1);

            ModelOutletCheckInEx[] result = null;
            using (TradeContext db = new TradeContext())
            {
                var selection = from c in db.CheckIns
                                join otl in db.Outlets on c.OutletId equals otl.Id into cotl
                                from cotlRes in cotl.DefaultIfEmpty()
                                where c.CheckInTime >= locRouteTripDateStart && c.CheckInTime <= locRouteTripDatEnd && c.RouteId == new Guid(routeId)
                                orderby c.CheckInTime
                                select new ModelOutletCheckInEx
                                {
                                    Id = c.Id,
                                    CheckInTime = c.CheckInTime,
                                    RouteId = c.RouteId,
                                    OutletId = c.OutletId,
                                    Outlet = cotlRes.Description + " - "+ cotlRes.Address,
                                    Latitude = c.Latitude,
                                    Longtitude = c.Longtitude
                                };
                result = selection.ToArray();
            }
            return result;
        }

        public RouteTripEx[] GetRouteTrip(string routeTripDate)
        {
            DateTime locRouteTripDate = routeTripDate.ToDateTime();

            RouteTripEx[] result = null;
            using (TradeContext db = new TradeContext())
            {
                var selection = from trip in db.RouteTrips
                                join of in db.Outlets on trip.FirstOutlet equals of.Id into trof
                                join ol in db.Outlets on trip.LastOutlet equals ol.Id into trol
                                join r in db.Routes on trip.RouteId equals r.Id into trr
                                from tripOf in trof.DefaultIfEmpty()
                                from tripOl in trol.DefaultIfEmpty()
                                from tripR in trr.DefaultIfEmpty()
                                where trip.RouteDate == locRouteTripDate
                                orderby tripR.Description
                                select new RouteTripEx
                                {
                                    RouteId = trip.RouteId,
                                    RouteName = tripR.Description,
                                    RouteDate = trip.RouteDate,
                                    Distance = trip.Distance,
                                    DistanceBetweenCheckIn = trip.DistanceBetweenCheckIn,
                                    MinCheckInTime = trip.MinCheckInTime,
                                    MaxCheckInTime = trip.MaxCheckInTime,
                                    FirstOutlet = trip.FirstOutlet,
                                    FirstOutletName = tripOf.Description,
                                    FirstOutletAdrress = tripOf.Address,
                                    LastOutlet = trip.LastOutlet,
                                    LastOutletName = tripOl.Description,
                                    LastOutletAdrress = tripOl.Address

                                };

                result = selection.ToArray<RouteTripEx>();
            }
            return result;
        }

        public string GetTestString()
        {
            return "Recived from service";
        }
    }
}

