// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeProjectService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime Project repository.
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
    /// The service to interact with the OnTime Project repository. 
    /// </summary>
    public class OnTimeProjectService : IOnTimeProjectService
    {
        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IOnTimeRepository<Project> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeProjectService" /> class.
        /// </summary>
        public OnTimeProjectService()
        {
            this.repository = new OnTimeRepository<Project>();
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
        public IList<Project> GetAll(bool onlyActive)
        {
            // Return all Projects if they should not be filtered to only include the active ones. 
            if (!onlyActive)
            {
                return this.repository.GetAll();
            }

            // Otherwise get only the active Projects
            Expression<Func<Project, bool>> expression = proj => proj.IsActive;
            return this.repository.GetAll(expression);
        }
    }
}