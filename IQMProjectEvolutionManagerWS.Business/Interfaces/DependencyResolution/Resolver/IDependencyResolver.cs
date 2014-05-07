// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyResolver.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the IDependencyResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver
{
    using Ninject;

    /// <summary>
    /// The DependencyResolver interface.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// The get kernel.
        /// </summary>
        /// <returns>
        /// The <see cref="IKernel"/>.
        /// </returns>
        IKernel GetKernel();
    }
}
