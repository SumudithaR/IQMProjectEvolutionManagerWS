// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WsCoreServiceModule.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The data service module
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.DependencyResolution.Modules
{
    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Core.Services;

    using Ninject.Modules;

    /// <summary>
    /// The data service module
    /// </summary>
    public class WsCoreServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // Bind each of the concrete services to their implementing interfaces. 
            Bind<IOnTimeProjectService>().To<OnTimeProjectService>();
            Bind<IOnTimeReleaseProjectService>().To<OnTimeReleaseProjectService>();
            Bind<IOnTimeReleaseService>().To<OnTimeReleaseService>();
            Bind<IOnTimeReleaseStatusTypeService>().To<OnTimeReleaseStatusTypeService>();
            Bind<IOnTimeReleaseTypeService>().To<OnTimeReleaseTypeService>();
            Bind<IOnTimeTaskService>().To<OnTimeTaskService>();
            Bind<IOnTimeUserService>().To<OnTimeUserService>();
            Bind<IOnTimeWorkLogService>().To<OnTimeWorkLogService>();
        }
    }
}