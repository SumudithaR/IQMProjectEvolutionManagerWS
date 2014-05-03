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
    public class OnTimeUserService : IOnTimeUserService
    {
        private readonly IOnTimeRepository<User> repository;

        public OnTimeUserService()
        {
            repository = new OnTimeRepository<User>();
        }

        public IList<User> GetAll(bool onlyActive)
        {
            if (onlyActive)
            {
                Expression<Func<User, bool>> expression = user => user.IsActive;
                return repository.GetAll(expression);
            }
            else
            {
                return repository.GetAll();
            }
        }
    }
}