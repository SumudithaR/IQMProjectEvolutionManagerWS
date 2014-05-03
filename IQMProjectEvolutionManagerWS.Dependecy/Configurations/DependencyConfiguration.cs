using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Dependecy.Configurations
{
    public static class DependencyConfiguration
    {
        private static IKernel kernel;

        /// <summary>
        /// Setups the dependency injection.
        /// </summary>
        /// <returns>The currently configured kernel</returns>
        private static IKernel SetupDependencyInjection(List<INinjectModule> modules)
        {
            // Create the Ninject
            kernel = new StandardKernel(new NinjectSettings
            {
                UseReflectionBasedInjection = true
            }, modules.ToArray());

            return kernel;
        }

        public static IKernel GetKernel(List<INinjectModule> modules)
        {
            if (kernel == null)
            {
                SetupDependencyInjection(modules);
            }

            return kernel;
        }
    }
}