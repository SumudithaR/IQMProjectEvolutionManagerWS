using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    public interface IOnTimeReleaseService
    {
        IList<Release> GetAll(bool onlyActive);
        IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive);
        Release GetParentRelease(Release childRelease, bool onlyActive);
        Release GetReleaseOfProject(Release originRelease);
    }
}