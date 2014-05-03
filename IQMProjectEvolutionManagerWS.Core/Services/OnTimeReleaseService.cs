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
    public class OnTimeReleaseService : IOnTimeReleaseService
    {
        /// <summary>
        /// The _repository
        /// </summary>
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
            if (!onlyActive) return _repository.GetAll();
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
            return childRelease != null ? GetAll(onlyActive).SingleOrDefault(rele => rele.ReleaseId == childRelease.ParentReleaseId) : null;
        }

        /// <summary>
        /// Gets the release of project.
        /// </summary>
        /// <param name="originRelease">The origin release.</param>
        /// <returns></returns>
        public Release GetReleaseOfProject(Release originRelease)
        {
            if (originRelease == null) return null;

            var currentRelease = originRelease;

            while (GetParentRelease(currentRelease, false) != null)
            {
                currentRelease = GetParentRelease(currentRelease, false);
            }

            return currentRelease;
        }
    }
}