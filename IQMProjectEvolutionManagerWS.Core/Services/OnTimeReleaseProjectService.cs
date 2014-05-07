// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeReleaseProjectService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime ReleaseProject repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Data;
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    /// <summary>
    /// The service to interact with the OnTime ReleaseProject repository. 
    /// </summary>
    public class OnTimeReleaseProjectService : IOnTimeReleaseProjectService
    {
        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IOnTimeRepository<ReleaseProject> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseProjectService"/> class.
        /// </summary>
        public OnTimeReleaseProjectService()
        {
            this.repository = new OnTimeRepository<ReleaseProject>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<ReleaseProject> GetAll()
        {
            return this.repository.GetAll();
        }

        /// <summary>
        /// The get associated projects.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Project> GetAssociatedProjects(Release release)
        {
            if (release == null)
            {
                return null;
            }

            /* 
             * Filter the records in the ReleaseProject table by the releaseId and select only the linked Projects, 
             * then return these Projects as a List collection. 
             */
            Expression<Func<ReleaseProject, bool>> clause = rProj => rProj.ReleaseId == release.ReleaseId;
            return this.repository.GetAll(clause).Select(rP => rP.Project).ToList();
        }
    }
}