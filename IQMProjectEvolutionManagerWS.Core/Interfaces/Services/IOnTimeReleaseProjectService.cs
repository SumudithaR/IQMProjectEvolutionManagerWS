using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseProject repository. 
    /// </summary>
    public interface IOnTimeReleaseProjectService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<ReleaseProject> GetAll();

        /// <summary>
        /// Gets the associated projects. 
        /// The associated projects are the projects 
        /// linked to the release via the 
        /// ReleaseProject table. 
        /// </summary>
        /// <param name="release">The release.</param>
        /// <returns></returns>
        IList<Project> GetAssociatedProjects(Release release);
    }
}