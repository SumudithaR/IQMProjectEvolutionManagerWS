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
    public class OnTimeReleaseProjectService : IOnTimeReleaseProjectService
    {
        private readonly IOnTimeRepository<ReleaseProject> repository;

        public OnTimeReleaseProjectService()
        {
            repository = new OnTimeRepository<ReleaseProject>();
        }

        public IList<ReleaseProject> GetAll()
        {
            return repository.GetAll();
        }

        public IList<Project> GetAssociatedProjects(Release release)
        {
            if (release != null)
            {
                Expression<Func<ReleaseProject, bool>> clause = rProj => rProj.ReleaseId == release.ReleaseId;
                return repository.GetAll(clause).Select(rP => rP.Project).ToList();
            }

            return null;
        }
    }
}