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

            /*WebServiceHost host = new WebServiceHost(typeof(Dictionary));
            ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;

            host.Open();*/
            //MailLocation.GetInstance("uniclocationdata@gmail.com", "Uniclocationdata8");
            //if (MailLocation.IsInstance()) MailLocation.GetInstance().Start();
            //Console.WriteLine("Service is up and running");
            //Console.WriteLine("Press enter to quit ");
            //Console.ReadLine();
            //if (MailLocation.IsInstance()) MailLocation.GetInstance().Dispose();
            ////host.Close();

            ////ProcessIncomingData.Start();

            //Uri baseAddress = new Uri("http://localhost:8100/GettingStarted/");
            WebServiceHost host = new WebServiceHost(typeof(Dictionary));
            ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;

            host.Open();
            foreach (var address in host.BaseAddresses )
                    Console.WriteLine(address + "  "+ host.State );

            WebServiceHost host2 = new WebServiceHost(typeof(Location));
            ServiceDebugBehavior stp2 = host2.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp2.HttpHelpPageEnabled = false;

            host2.Open();
            foreach (var address in host2.BaseAddresses)
                Console.WriteLine(address + "  " + host2.State);
            Console.ReadLine();

            host.Close();
            host2.Close();
        }
    }
}
