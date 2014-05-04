using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime User repository. 
    /// </summary>
    public class OnTimeUserService : IOnTimeUserService
    {
        private readonly IOnTimeRepository<User> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeUserService"/> class.
        /// </summary>
        public OnTimeUserService()
        {
            _repository = new OnTimeRepository<User>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        public IList<User> GetAll(bool onlyActive)
        {
            // Return all User if they should not be filtered to only include the active ones. 
            if (!onlyActive) return _repository.GetAll();

            // Otherwise get only the active Users
            Expression<Func<User, bool>> expression = user => user.IsActive;
            return _repository.GetAll(expression);
        }
    }
}