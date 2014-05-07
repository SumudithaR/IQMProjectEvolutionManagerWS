// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeReleaseTypeService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime ReleaseType repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime ReleaseType repository. 
    /// </summary>
    public interface IOnTimeReleaseTypeService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<ReleaseType> GetAll();

        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="releaseTypeName">
        /// The release type name.
        /// </param>
        /// <returns>
        /// The <see cref="ReleaseType"/>.
        /// </returns>
        ReleaseType GetByName(string releaseTypeName);
    }
}
