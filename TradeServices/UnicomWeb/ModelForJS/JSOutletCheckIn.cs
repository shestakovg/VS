using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradeServices.Models;
using UnicomWeb.LocationService;

namespace UnicomWeb.ModelForJS
{
    public class JSOutletCheckIn: ModelOutletCheckInEx
    {
        public String CheckInTimeStr;
        public int CheckInNumber;
    }
}