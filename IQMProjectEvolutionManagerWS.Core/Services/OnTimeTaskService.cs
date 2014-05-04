using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime Task repository. 
    /// </summary>
    public class OnTimeTaskService : IOnTimeTaskService
    {
        private readonly IOnTimeRepository<Task> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeTaskService"/> class.
        /// </summary>
        public OnTimeTaskService()
        {
            _repository = new OnTimeRepository<Task>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Task> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets the list of Tasks by release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <returns></returns>
        public IList<Task> GetByRelease(Release release)
        {
            // If release is not null, then get the collection of Task objects that are linked to the release. 
            return release != null ? GetAll().Where(tsk => tsk.Release.ReleaseId == release.ReleaseId).ToList() : null;
        }
    }
}