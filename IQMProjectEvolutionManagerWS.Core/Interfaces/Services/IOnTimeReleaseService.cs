// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeReleaseService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime Release repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime Release repository. 
    /// </summary>
    public interface IOnTimeReleaseService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Release> GetAll(bool onlyActive);

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
        IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive);

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
        Release GetParentRelease(Release childRelease, bool onlyActive);

        /// <summary>
        /// The get release of project.
        /// </summary>
        /// <param name="originRelease">
        /// The origin release.
        /// </param>
        /// <returns>
        /// The <see cref="Release"/>.
        /// </returns>
        Release GetReleaseOfProject(Release originRelease);
    }
}