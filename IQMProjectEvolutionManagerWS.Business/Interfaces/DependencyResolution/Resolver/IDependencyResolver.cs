using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver
{
    public interface IDependencyResolver
    {
        IKernel GetKernel();
    }
}
