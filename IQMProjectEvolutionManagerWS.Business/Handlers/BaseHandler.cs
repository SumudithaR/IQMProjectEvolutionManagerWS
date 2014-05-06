using IQMProjectEvolutionManager.Core;
using IQMProjectEvolutionManagerWS.Business.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;
using IQMProjectEvolutionManagerWS.Core.DependencyResolution.Modules;
using IQMProjectEvolutionManagerWS.Data.DependencyResolution.Modules;
using IQMProjectEvolutionManagerWS.Notify.DependencyResolution.Modules;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    using System.Configuration;

    using NHibernate.Cfg;

    public class BaseHandler : IBaseHandler
    {
        private readonly IDependencyResolver dependencyResolver;

        public BaseHandler()
        {
            var modules = new List<INinjectModule>(){
                new WsCoreServiceModule(),
                new CoreServiceModule(),
                new WSDataServiceModule(),
                new WSNotifyServiceModule()
            };

            dependencyResolver = new DependencyResolver(modules);
        }

        protected void RunDataProcesses()
        {
            DataUpdateHandler.Run(dependencyResolver, new NotifyHandler(dependencyResolver));
            DataCleanupHandler.Run(dependencyResolver, int.Parse(ConfigurationManager.AppSettings["DataCleanupFilterDays"]));
        }

        public void Start()
        {
            RunDataProcesses();
        }
    }
}