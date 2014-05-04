using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime Task repository. 
    /// </summary>
    public interface IOnTimeTaskService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<Task> GetAll();

        /// <summary>
        /// Gets the list of Tasks by release.
        /// </summary>
        /// <param name="release">The release.</param>
        /// <returns></returns>
        IList<Task> GetByRelease(Release release);
    }
}