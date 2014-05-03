using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Interfaces.OnTimeModels;
using IQMProjectEvolutionManagerWS.Data.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IQMProjectEvolutionManagerWS.Data;

namespace IQMProjectEvolutionManagerWS.Data.DependencyResolution.Modules
{
    /// <summary>
    /// The data service module
    /// </summary>
    public class WSDataServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Bind<IProject>().To<Project>();
            //Bind<IRelease>().To<Release>();
            //Bind<IReleaseProject>().To<ReleaseProject>();
            //Bind<IReleaseType>().To<ReleaseType>();
            //Bind<IReleaseStatusType>().To<ReleaseStatusType>();
            //Bind<ITask>().To<Task>();
            //Bind<ITimeUnitType>().To<TimeUnitType>();
            //Bind<IUser>().To<User>();
            //Bind<IWorkLog>().To<WorkLog>();

            Bind(typeof(IOnTimeRepository<>)).To(typeof(OnTimeRepository<>));
        }
    }
}