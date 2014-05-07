// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The base handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    using System.Collections.Generic;
    using System.Configuration;

    using IQMProjectEvolutionManager.Core;

    using IQMProjectEvolutionManagerWS.Business.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;
    using IQMProjectEvolutionManagerWS.Core.DependencyResolution.Modules;
    using IQMProjectEvolutionManagerWS.Data.DependencyResolution.Modules;
    using IQMProjectEvolutionManagerWS.Notify.DependencyResolution.Modules;

    using Ninject.Modules;

    /// <summary>
    /// The base handler.
    /// </summary>
    public class BaseHandler : IBaseHandler
    {
        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHandler"/> class.
        /// </summary>
        public BaseHandler()
        {
            var modules = new List<INinjectModule>()
                              {
                                  new WsCoreServiceModule(),
                                  new CoreServiceModule(),
                                  new WsDataServiceModule(),
                                  new WsNotifyServiceModule()
                              };

            this.dependencyResolver = new DependencyResolver(modules);
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
            this.RunDataProcesses();
        }

        /// <summary>
        /// The run data processes.
        /// </summary>
        protected void RunDataProcesses()
        {
            DataUpdateHandler.Run(this.dependencyResolver, new NotifyHandler(this.dependencyResolver));
            DataCleanupHandler.Run(this.dependencyResolver, int.Parse(ConfigurationManager.AppSettings["DataCleanupFilterDays"]));
        }
    }
}