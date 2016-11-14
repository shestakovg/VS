using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TradeServices.Classes;

namespace ProccessHost
{
    public partial class ProcessService : ServiceBase
    {
        public ProcessService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ProcessOrderLog l = new ProcessOrderLog(SaveData.getConnection());
            l.WriteLog(Guid.NewGuid(), "Process Service has been started");
            MailLocation.GetInstance("uniclocationdata@gmail.com", "Uniclocationdata8");
            if (MailLocation.IsInstance()) MailLocation.GetInstance().Start();
            l.WriteLog(Guid.NewGuid(), "MailLocation has been started");

            ProcessIncomingData.Start();
            l.WriteLog(Guid.NewGuid(), "Process ProcessIncomingData has been started");
        }

        protected override void OnStop()
        {
            if (MailLocation.IsInstance()) MailLocation.GetInstance().Dispose();

            ProcessIncomingData.Stop();
            GC.Collect();
        }
    }
}
