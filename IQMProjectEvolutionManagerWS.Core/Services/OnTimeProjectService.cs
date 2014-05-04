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
    /// The service to interact with the OnTime Project repository. 
    /// </summary>
    public class OnTimeProjectService : IOnTimeProjectService
    {
        private readonly IOnTimeRepository<Project> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeProjectService" /> class.
        /// </summary>
        public OnTimeProjectService()
        {
            _repository = new OnTimeRepository<Project>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <returns></returns>
        public IList<Project> GetAll(bool onlyActive)
        {
            // Return all Projects if they should not be filtered to only include the active ones. 
            if (!onlyActive) return _repository.GetAll();

            // Otherwise get only the active Projects
            Expression<Func<Project, bool>> expression = proj => proj.IsActive;
            return _repository.GetAll(expression);
        }
    }
}