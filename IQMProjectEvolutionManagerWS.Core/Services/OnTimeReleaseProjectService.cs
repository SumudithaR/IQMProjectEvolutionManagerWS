using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime ReleaseProject repository. 
    /// </summary>
    public class OnTimeReleaseProjectService : IOnTimeReleaseProjectService
    {
        private readonly IOnTimeRepository<ReleaseProject> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseProjectService"/> class.
        /// </summary>
        public OnTimeReleaseProjectService()
        {
            _repository = new OnTimeRepository<ReleaseProject>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<ReleaseProject> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets the associated projects. 
        /// The associated projects are the projects 
        /// linked to the release via the 
        /// ReleaseProject table.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <returns></returns>
        public IList<Project> GetAssociatedProjects(Release release)
        {
            if (release == null) return null;

            /* 
             * Filter the records in the ReleaseProject table by the releaseId and select only the linked Projects, 
             * then return these Projects as a List collection. 
             */
            Expression<Func<ReleaseProject, bool>> clause = rProj => rProj.ReleaseId == release.ReleaseId;
            return _repository.GetAll(clause).Select(rP => rP.Project).ToList();
        }
    }
}