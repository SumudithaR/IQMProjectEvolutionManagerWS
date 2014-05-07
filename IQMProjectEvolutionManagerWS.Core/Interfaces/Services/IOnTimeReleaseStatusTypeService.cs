// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeReleaseStatusTypeService.cs" company="IQM Software">
//   Sumuditha Ranawka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime ReleaseStatusType repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseStatusType repository. 
    /// </summary>
    public interface IOnTimeReleaseStatusTypeService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<ReleaseStatusType> GetAll();

        /// <summary>
        /// The get by name and release type.
        /// </summary>
        /// <param name="releaseStatusTypeName">
        /// The release status type name.
        /// </param>
        /// <param name="releaseTypeId">
        /// The release type id.
        /// </param>
        /// <returns>
        /// The <see cref="ReleaseStatusType"/>.
        /// </returns>
        ReleaseStatusType GetByNameAndReleaseType(string releaseStatusTypeName, int releaseTypeId);
    }
}