// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeTaskService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime Task repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Data;
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    /// <summary>
    /// The service to interact with the OnTime Task repository. 
    /// </summary>
    public class OnTimeTaskService : IOnTimeTaskService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IOnTimeRepository<Task> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeTaskService"/> class.
        /// </summary>
        public OnTimeTaskService()
        {
            this.repository = new OnTimeRepository<Task>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Task> GetAll()
        {
            return this.repository.GetAll();
        }

        /// <summary>
        /// The get by release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Task> GetByRelease(Release release)
        {
            // If release is not null, then get the collection of Task objects that are linked to the release. 
            return release != null ? this.GetAll().Where(tsk => tsk.Release.ReleaseId == release.ReleaseId).ToList() : null;
        }
    }
}