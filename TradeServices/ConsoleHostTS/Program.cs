using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using TradeServices;
using TradeServices.Classes;

namespace ConsoleHostTS
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebServiceHost host = new WebServiceHost(typeof(Dictionary), new Uri("http://localhost:8100"));
            //WebServiceHost host = new WebServiceHost(typeof(Dictionary));
            //ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IDictionary), new WebHttpBinding(), "");
            ////WebServiceHost host = new WebServiceHost(typeof(Dictionary));
            //ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            //stp.HttpHelpPageEnabled = false;

            //host.Open();
            WebServiceHost host = new WebServiceHost(typeof(Dictionary));
            ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;

            host.Open();
            Console.WriteLine("Service is up and running");
            Console.WriteLine("Press enter to quit ");
            Console.ReadLine();
            host.Close();
            
            //ProcessIncomingData.Start();

            Uri baseAddress = new Uri("http://localhost:8100/GettingStarted/");
        }
    }
}
