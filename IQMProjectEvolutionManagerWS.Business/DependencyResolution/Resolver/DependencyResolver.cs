using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Dependecy.Configurations;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Business.DependencyResolution.Resolver
{
    public class DependencyResolver : IDependencyResolver 
    {
        private readonly List<INinjectModule> modules;

        public DependencyResolver(List<INinjectModule> modules)
        {
            this.modules = modules;
        }

        public IKernel GetKernel()
        {
            return DependencyConfiguration.GetKernel(modules);
        }
    }
}