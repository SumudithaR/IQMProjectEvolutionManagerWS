using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Core.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Dependecy.Modules
{
    /// <summary>
    /// The data service module
    /// </summary>
    public class WSCoreServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
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