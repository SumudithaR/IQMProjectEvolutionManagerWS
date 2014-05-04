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
    /// The service to interact with the OnTime Release repository. 
    /// </summary>
    public class OnTimeReleaseService : IOnTimeReleaseService
    {
        private readonly IOnTimeRepository<Release> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseService"/> class.
        /// </summary>
        public OnTimeReleaseService()
        {
            _repository = new OnTimeRepository<Release>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        public IList<Release> GetAll(bool onlyActive)
        {
            // Return all Releases if they should not be filtered to only include the active ones. 
            if (!onlyActive) return _repository.GetAll();

            // Otherwise get only the active Releases
            Expression<Func<Release, bool>> expression = rele => rele.IsActive;
            return _repository.GetAll(expression);
        }

        /// <summary>
        /// Gets the releases by criteria.
        /// </summary>
        /// <param name="releaseType">Type of the release.</param>
        /// <param name="releaseStatusType">Type of the release status.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        public IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive)
        {
            if (releaseStatusType != null && releaseType != null)
            {
                /*
                 * Get all the Releases where its ReleaseType and ReleaseStatusType match with the objects passed in as parameters.
                 * Also get Releases that are only active if onlyActive is true, otherwise get all Releases.
                */
                return GetAll(onlyActive).Where(rele => rele.ReleaseType.ReleaseTypeId == releaseType.ReleaseTypeId
                    && rele.ReleaseStatusType.ReleaseStatusTypeId == releaseStatusType.ReleaseStatusTypeId).ToList();
            }
            return null;
        }

        /// <summary>
        /// Gets the parent release.
        /// </summary>
        /// <param name="childRelease">The child release.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        public Release GetParentRelease(Release childRelease, bool onlyActive)
        {
            // If childRelease is not null, get the release that has its Id in the ParentReleaseId column of the childRelease record. 
            return childRelease != null ? GetAll(onlyActive).SingleOrDefault(rele => rele.ReleaseId == childRelease.ParentReleaseId) : null;
        }

        /// <summary>
        /// Gets the initial release indicating the project.
        /// </summary>
        /// <param name="originRelease">The origin release.</param>
        /// <returns></returns>
        public Release GetReleaseOfProject(Release originRelease)
        {
            if (originRelease == null) return null;

            var currentRelease = originRelease;

            // Traverse through each parent release starting from the originRelease until the initial Release has been found.
            while (GetParentRelease(currentRelease, false) != null)
            {
                currentRelease = GetParentRelease(currentRelease, false);
            }

            return currentRelease;
        }
    }
}