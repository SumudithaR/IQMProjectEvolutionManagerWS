using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using IQMProjectEvolutionManagerWS.Business.Handlers;

namespace IQMProjectEvolutionManagerWS
{
    public partial class BusinessService : ServiceBase
    {
        /// <summary>
        /// The _cancellation token source
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// The _base process
        /// </summary>
        private BaseHandler _baseProcess;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService"/> class.
        /// </summary>
        public BusinessService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                _baseProcess = new BaseHandler();

                while (true)
                {
                    _baseProcess.Start();
                    Thread.Sleep(TimeSpan.FromMilliseconds(
                        double.Parse(ConfigurationManager.AppSettings["RefreshPeriod"])));

                    if (!token.IsCancellationRequested) continue;
                    Console.WriteLine("IQM Project Evolution Manager Business Service has been stopped.");
                    break;
                }

                _baseProcess = null;

            }, token);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Called when [debug].
        /// </summary>
        public void OnDebug()
        {
            OnStart(null);
        }
    }
}