using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Data.Interfaces.Repository
{
    public interface IOnTimeRepository<T>
    {
        IList<T> GetAll();
        IList<T> GetAll(Expression<Func<T,bool>> filter);
        T GetById(object id);
        //bool IsDirty(T t, string propertyName);
    }
}
