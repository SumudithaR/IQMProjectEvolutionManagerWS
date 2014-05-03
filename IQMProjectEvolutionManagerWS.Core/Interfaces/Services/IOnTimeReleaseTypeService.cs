using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    public interface IOnTimeReleaseTypeService
    {
        IList<ReleaseType> GetAll();
        ReleaseType GetByName(string releaseTypeName);
    }
}
