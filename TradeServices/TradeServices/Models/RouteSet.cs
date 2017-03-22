using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeServices.Models
{
    public class RouteSet
    {
        public Guid Outletid { get; set; }
        public String OutletName { get; set; }
        public String Adrress { get; set; }
        public Guid RouteId { get; set; }
        public String RouteName { get; set; }
        public Double? Latitude { get; set; }
        public Double? Longtitude { get; set; }
    }
}