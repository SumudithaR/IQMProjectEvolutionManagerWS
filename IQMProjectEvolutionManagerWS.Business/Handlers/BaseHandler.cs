using IQMProjectEvolutionManager.Core;
using IQMProjectEvolutionManagerWS.Business.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;
using IQMProjectEvolutionManagerWS.Data.DependencyResolution.Modules;
using IQMProjectEvolutionManagerWS.Dependecy.Modules;
using IQMProjectEvolutionManagerWS.Notify.DependencyResolution.Modules;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    public class BaseHandler : IBaseHandler
    {
        private readonly IDependencyResolver dependencyResolver;

        public BaseHandler()
        {
            var modules = new List<INinjectModule>(){
                new WSCoreServiceModule(),
                new CoreServiceModule(),
                new WSDataServiceModule(),
                new WSNotifyServiceModule()
            };

            dependencyResolver = new DependencyResolver(modules);
        }

        protected void RunDataManagementProcess()
        {
            var dataManagementHandler = new DataManagementHandler(dependencyResolver, new NotifyHandler(dependencyResolver));
            dataManagementHandler.InsertReleaseTypes();
            dataManagementHandler.InsertReleaseStatusTypes();
            dataManagementHandler.InsertStaffMembers();
            dataManagementHandler.InsertReleasesByPreference();
        }

        public void Start()
        {
            RunDataManagementProcess();
        }
    }
}