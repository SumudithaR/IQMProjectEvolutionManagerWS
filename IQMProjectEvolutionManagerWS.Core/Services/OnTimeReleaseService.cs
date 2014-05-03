using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    public class OnTimeReleaseService : IOnTimeReleaseService
    {
        private readonly IOnTimeRepository<Release> repository;

        public OnTimeReleaseService()
        {
            repository = new OnTimeRepository<Release>();
        }

        public IList<Release> GetAll(bool onlyActive)
        {
            if (onlyActive)
            {
                Expression<Func<Release, bool>> expression = rele => rele.IsActive;
                return repository.GetAll(expression);
            }
            else
            {
                return repository.GetAll();
            }
        }

        public IList<Release> GetReleasesByCriteria(ReleaseType releaseType, ReleaseStatusType releaseStatusType, bool onlyActive)
        {
            if (releaseStatusType != null || releaseType != null)
            {
                return GetAll(onlyActive).Where(rele => rele.ReleaseType.ReleaseTypeId == releaseType.ReleaseTypeId
                    && rele.ReleaseStatusType.ReleaseStatusTypeId == releaseStatusType.ReleaseStatusTypeId).ToList();
            }
            return null;
        }

        public Release GetParentRelease(Release childRelease, bool onlyActive)
        {
            if (childRelease != null)
            {
                return GetAll(onlyActive).SingleOrDefault(rele => rele.ReleaseId == childRelease.ParentReleaseId);
            }
            return null;
        }

        public Release GetReleaseOfProject(Release originRelease)
        {
            if (originRelease != null)
            {
                var currentRelease = originRelease;

                while (GetParentRelease(currentRelease, false) != null)
                {
                    currentRelease = GetParentRelease(currentRelease, false);
                }

                return currentRelease;
            }

            return null;
        }
    }
}