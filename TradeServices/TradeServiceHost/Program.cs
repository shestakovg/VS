using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TradeServiceHost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (DateTime.Today < new DateTime(2015, 8, 1))
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new TradeWinService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
               // throw new Exception("No many no hunny");
            }
        }
    }
}
