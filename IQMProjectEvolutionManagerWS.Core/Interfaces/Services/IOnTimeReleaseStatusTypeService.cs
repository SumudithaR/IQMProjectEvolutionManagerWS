using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseStatusType repository. 
    /// </summary>
    public interface IOnTimeReleaseStatusTypeService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<ReleaseStatusType> GetAll();

        /// <summary>
        /// Gets the single ReleaseStatusType by its name and the Id of its 
        /// associated ReleaseType.
        /// </summary>
        /// <param name="releaseStatusTypeName">Name of the release status type.</param>
        /// <param name="releaseTypeId">The release type identifier.</param>
        /// <returns></returns>
        ReleaseStatusType GetByNameAndReleaseType(string releaseStatusTypeName, int releaseTypeId);
    }
}