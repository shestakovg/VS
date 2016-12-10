using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TradeServices.Models
{
    [NotMapped]

    public class RouteTripEx: RouteTrip
    {

        public String RouteName { get; set; }

        public String LastOutletName { get; set; }

        public String LastOutletAdrress { get; set; }

        public String FirstOutletName { get; set; }

        public String FirstOutletAdrress { get; set; }

        //private String routeDateString;
        public String RouteDateString {
            get {
                return RouteDate.ToString("dd.MM.yyyy");
            }
            
        }
    }
}