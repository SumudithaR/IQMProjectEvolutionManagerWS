using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime Release repository. 
    /// </summary>
    public interface IOnTimeReleaseService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        IList<Release> GetAll(bool onlyActive);

        /// <summary>
        /// Gets the releases by criteria.
        /// </summary>
        /// <param name="releaseType">Type of the release.</param>
        /// <param name="releaseStatusType">Type of the release status.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive);

        /// <summary>
        /// Gets the parent release.
        /// </summary>
        /// <param name="childRelease">The child release.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        Release GetParentRelease(Release childRelease, bool onlyActive);

        /// <summary>
        /// Gets the initial release indicating the project.
        /// </summary>
        /// <param name="originRelease">The origin release.</param>
        /// <returns></returns>
        Release GetReleaseOfProject(Release originRelease);
    }
}