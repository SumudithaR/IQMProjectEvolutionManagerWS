using System.ServiceProcess;

namespace IQMProjectEvolutionManagerWS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
//#if DEBUG
//            var businessService = new BusinessService();
//            businessService.OnDebug();
//            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
//#else
            var servicesToRun = new ServiceBase[]
            {
                new BusinessService()
            };
            ServiceBase.Run(servicesToRun);
//#endif
        }
    }
}