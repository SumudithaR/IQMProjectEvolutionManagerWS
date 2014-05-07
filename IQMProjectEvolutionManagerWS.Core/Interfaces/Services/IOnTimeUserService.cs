// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeUserService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime User repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime User repository. 
    /// </summary>
    public interface IOnTimeUserService
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
        IList<User> GetAll(bool onlyActive);
    }
}