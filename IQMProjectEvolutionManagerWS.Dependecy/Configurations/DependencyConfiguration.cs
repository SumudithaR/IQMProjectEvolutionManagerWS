// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyConfiguration.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The dependency configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Dependecy.Configurations
{
    using System.Collections.Generic;

    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// The dependency configuration.
    /// </summary>
    public static class DependencyConfiguration
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private static IKernel kernel;

        /// <summary>
        /// The get kernel.
        /// </summary>
        /// <param name="modules">
        /// The modules.
        /// </param>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        public static IKernel GetKernel(List<INinjectModule> modules)
        {
            if (kernel == null)
            {
                SetupDependencyInjection(modules);
            }

            return kernel;
        }

        /// <summary>
        /// Setups the dependency injection.
        /// </summary>
        /// <param name="modules">
        /// The modules.
        /// </param>
        /// <returns>
        /// The currently configured kernel
        /// </returns>
        private static IKernel SetupDependencyInjection(List<INinjectModule> modules)
        {
            // Create the Ninject
            kernel = new StandardKernel(
                new NinjectSettings
                    {
                        UseReflectionBasedInjection = true
            }, 
            modules.ToArray());

            return kernel;
        }
    }
}