using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    public class OnTimeProjectService : IOnTimeProjectService
    {
        private readonly IOnTimeRepository<Project> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeProjectService"/> class.
        /// </summary>
        public OnTimeProjectService()
        {
            repository = new OnTimeRepository<Project>();
        }

        public IList<Project> GetAll(bool onlyActive)
        {
            if (onlyActive)
            {
                Expression<Func<Project, bool>> expression = proj => proj.IsActive;
                return repository.GetAll(expression);
            }
            else
            {
                return repository.GetAll();
            }
        }
    }
}