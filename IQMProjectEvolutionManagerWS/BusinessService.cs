using IQMProjectEvolutionManagerWS.Business.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManager
{
    public partial class BusinessService : ServiceBase
    {
        private CancellationTokenSource cancellationTokenSource;
        private BaseHandler baseProcess;

        public BusinessService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                baseProcess = new BaseHandler();

                while (true)
                {
                    baseProcess.Start();
                    Thread.Sleep(TimeSpan.FromMilliseconds(
                        double.Parse(ConfigurationManager.AppSettings["RefreshPeriod"])));

                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("IQM Project Evolution Manager Business Service has been stopped.");
                        break;
                    }
                }

                baseProcess = null;

            }, token);
        }

        protected override void OnStop()
        {
            cancellationTokenSource.Cancel();
        }

        public void OnDebug()
        {
            OnStart(null);
        }
    }
}