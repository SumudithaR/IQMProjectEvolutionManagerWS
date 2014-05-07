// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyResolver.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the DependencyResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.DependencyResolution.Resolver
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Dependecy.Configurations;

    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// The dependency resolver.
    /// </summary>
    public class DependencyResolver : IDependencyResolver 
    {
        /// <summary>
        /// The modules.
        /// </summary>
        private readonly List<INinjectModule> modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        /// <param name="modules">
        /// The modules.
        /// </param>
        public DependencyResolver(List<INinjectModule> modules)
        {
            this.modules = modules;
        }

        /// <summary>
        /// The get kernel.
        /// </summary>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        public IKernel GetKernel()
        {
            return DependencyConfiguration.GetKernel(this.modules);
        }
    }
}