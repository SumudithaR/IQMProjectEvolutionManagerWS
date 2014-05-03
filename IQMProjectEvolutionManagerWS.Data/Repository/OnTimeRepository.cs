using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Data.Repository
{
    public class OnTimeRepository<T> : IOnTimeRepository<T>
        where T : class
    {
        private readonly OnTime10Entities ontimeDBContext;

        public OnTimeRepository()
        {
            ontimeDBContext = new OnTime10Entities();
        }

        public IList<T> GetAll()
        {
            return ontimeDBContext.Set<T>().ToList();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return ontimeDBContext.Set<T>().Where(filter).ToList();
        }

        public T GetById(object id)
        {
            return ontimeDBContext.Set<T>().Find(id);
        }
    }
}