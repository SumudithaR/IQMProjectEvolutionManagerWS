// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeReleaseService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime Release repository.
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
    /// The service to interact with the OnTime Release repository. 
    /// </summary>
    public class OnTimeReleaseService : IOnTimeReleaseService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IOnTimeRepository<Release> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseService"/> class.
        /// </summary>
        public OnTimeReleaseService()
        {
            this.repository = new OnTimeRepository<Release>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Release> GetAll(bool onlyActive)
        {
            // Return all Releases if they should not be filtered to only include the active ones. 
            if (!onlyActive)
            {
                return this.repository.GetAll();
            }

            // Otherwise get only the active Releases
            Expression<Func<Release, bool>> expression = rele => rele.IsActive;
            return this.repository.GetAll(expression);
        }

        /// <summary>
        /// The get releases by criteria.
        /// </summary>
        /// <param name="releaseType">
        /// The release type.
        /// </param>
        /// <param name="releaseStatusType">
        /// The release status type.
        /// </param>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive)
        {
            if (releaseStatusType != null && releaseType != null)
            {
                /*
                 * Get all the Releases where its ReleaseType and ReleaseStatusType match with the objects passed in as parameters.
                 * Also get Releases that are only active if onlyActive is true, otherwise get all Releases.
                */
                return this.GetAll(onlyActive).Where(rele => rele.ReleaseType.ReleaseTypeId == releaseType.ReleaseTypeId
                    && rele.ReleaseStatusType.ReleaseStatusTypeId == releaseStatusType.ReleaseStatusTypeId).ToList();
            }

            return null;
        }

        /// <summary>
        /// The get parent release.
        /// </summary>
        /// <param name="childRelease">
        /// The child release.
        /// </param>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="Release"/>.
        /// </returns>
        public Release GetParentRelease(Release childRelease, bool onlyActive)
        {
            // If childRelease is not null, get the release that has its Id in the ParentReleaseId column of the childRelease record. 
            return childRelease != null ? this.GetAll(onlyActive).SingleOrDefault(rele => rele.ReleaseId == childRelease.ParentReleaseId) : null;
        }

        /// <summary>
        /// The get release of project.
        /// </summary>
        /// <param name="originRelease">
        /// The origin release.
        /// </param>
        /// <returns>
        /// The <see cref="Release"/>.
        /// </returns>
        public Release GetReleaseOfProject(Release originRelease)
        {
            if (originRelease == null)
            {
                return null;
            }

            var currentRelease = originRelease;

            // Traverse through each parent release starting from the originRelease until the initial Release has been found.
            while (this.GetParentRelease(currentRelease, false) != null)
            {
                currentRelease = this.GetParentRelease(currentRelease, false);
            }

            return currentRelease;
        }
    }
}