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
            ProcessOrderLog l = new ProcessOrderLog(SaveData.getConnection());
            l.WriteLog(Guid.NewGuid(), "Service has been started");
            MailLocation.GetInstance("uniclocationdata@gmail.com", "Uniclocationdata8");
            if (MailLocation.IsInstance()) MailLocation.GetInstance().Start();
            l.WriteLog(Guid.NewGuid(), "MailLocation has been started");

            ProcessIncomingData.Start();
            l.WriteLog(Guid.NewGuid(), "ProcessIncomingData has been started");
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
                
            }

            if (MailLocation.IsInstance()) MailLocation.GetInstance().Dispose();

            ProcessIncomingData.Stop();
            GC.Collect();
        }
    }
}
