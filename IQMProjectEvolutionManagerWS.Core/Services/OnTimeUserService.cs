// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeUserService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime User repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Data;
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    /// <summary>
    /// The service to interact with the OnTime User repository. 
    /// </summary>
    public class OnTimeUserService : IOnTimeUserService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IOnTimeRepository<User> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeUserService"/> class.
        /// </summary>
        public OnTimeUserService()
        {
            this.repository = new OnTimeRepository<User>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="onlyActive">
        /// The only active.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<User> GetAll(bool onlyActive)
        {
            // Return all User if they should not be filtered to only include the active ones. 
            if (!onlyActive)
            {
                return this.repository.GetAll();
            }

            // Otherwise get only the active Users
            Expression<Func<User, bool>> expression = user => user.IsActive;
            return this.repository.GetAll(expression);
        }
    }
}