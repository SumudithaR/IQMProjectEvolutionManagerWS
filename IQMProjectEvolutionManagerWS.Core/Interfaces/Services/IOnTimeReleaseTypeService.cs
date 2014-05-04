using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseType repository. 
    /// </summary>
    public interface IOnTimeReleaseTypeService
    {
        /// <summary>s
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<ReleaseType> GetAll();

        /// <summary>
        /// Gets the single ReleaseType by its name.
        /// </summary>
        /// <param name="releaseTypeName">Name of the release type.</param>
        /// <returns></returns>
        ReleaseType GetByName(string releaseTypeName);
    }
}
