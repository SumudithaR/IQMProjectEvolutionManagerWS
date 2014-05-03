using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    public interface IOnTimeProjectService
    {
        IList<Project> GetAll(bool onlyActive);
    }
}
