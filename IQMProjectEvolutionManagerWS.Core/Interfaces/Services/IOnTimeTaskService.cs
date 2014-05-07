// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeTaskService.cs" company="IQM Software">
//   Sumuditha Ranwaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime Task repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime Task repository. 
    /// </summary>
    public interface IOnTimeTaskService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Task> GetAll();

        /// <summary>
        /// The get by release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Task> GetByRelease(Release release);
    }
}