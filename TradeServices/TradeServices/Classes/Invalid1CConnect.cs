using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeServices.Classes
{
    public class Invalid1CConnect : System.Exception
    {
        public Invalid1CConnect(string message) : base(message) { }
    }
}