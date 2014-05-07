// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS
{
    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
#if DEBUG
            var businessService = new BusinessService();
            businessService.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            var servicesToRun = new ServiceBase[]
            {
                new BusinessService()
            };
            ServiceBase.Run(servicesToRun);
#endif
        }
    }
}