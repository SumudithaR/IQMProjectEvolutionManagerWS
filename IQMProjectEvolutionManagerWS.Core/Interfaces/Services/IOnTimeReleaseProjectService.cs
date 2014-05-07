// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeReleaseProjectService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime ReleaseProject repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseProject repository. 
    /// </summary>
    public interface IOnTimeReleaseProjectService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<ReleaseProject> GetAll();

        /// <summary>
        /// The get associated projects.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Project> GetAssociatedProjects(Release release);
    }
}