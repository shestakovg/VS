using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TradeServices;
using TradeServices.Classes;

namespace TradeServiceHost
{
    public partial class TradeWinService : ServiceBase
    {
        WebServiceHost host;
        public TradeWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (host!=null)
            {
                host.Close();
                host = null;
                GC.Collect();
            }
            host = new WebServiceHost(typeof(Dictionary));
            ServiceDebugBehavior stp = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;

            host.Open();

            ProcessIncomingData.Start();
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
                
            }

            ProcessIncomingData.Stop();
            GC.Collect();
        }
    }
}
