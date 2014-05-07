// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the BusinessService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS
{
    using System;
    using System.Configuration;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    using IQMProjectEvolutionManagerWS.Business.Handlers;

    /// <summary>
    /// The business service.
    /// </summary>
    public partial class BusinessService : ServiceBase
    {
        /// <summary>
        /// The _cancellation token source
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// The _base process
        /// </summary>
        private BaseHandler baseProcess;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService"/> class.
        /// </summary>
        public BusinessService()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Called when [debug].
        /// </summary>
        public void OnDebug()
        {
            this.OnStart(null);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            var token = this.cancellationTokenSource.Token;

            Task.Factory.StartNew(
                () =>
                    {
                        this.baseProcess = new BaseHandler();

                while (true)
                {
                    this.baseProcess.Start();
                    Thread.Sleep(TimeSpan.FromMilliseconds(
                        double.Parse(ConfigurationManager.AppSettings["RefreshPeriod"])));

                    if (!token.IsCancellationRequested)
                    {
                        continue;
                    }

                    Console.WriteLine("IQM Project Evolution Manager Business Service has been stopped.");
                    break;
                }

                this.baseProcess = null;
            },
                token);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.cancellationTokenSource.Cancel();
        }
    }
}