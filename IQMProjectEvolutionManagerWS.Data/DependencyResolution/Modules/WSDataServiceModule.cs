// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WSDataServiceModule.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The data service module
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Data.DependencyResolution.Modules
{
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    using Ninject.Modules;

    /// <summary>
    /// The data service module
    /// </summary>
    public class WsDataServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind(typeof(IOnTimeRepository<>)).To(typeof(OnTimeRepository<>));
        }
    }
}