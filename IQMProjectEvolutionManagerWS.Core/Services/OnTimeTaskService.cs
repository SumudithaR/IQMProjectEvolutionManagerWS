using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    public class OnTimeTaskService : IOnTimeTaskService
    {
        private readonly IOnTimeRepository<Task> repository;

        public OnTimeTaskService()
        {
            repository = new OnTimeRepository<Task>();
        }

        public IList<Task> GetAll()
        {
            return repository.GetAll();
        }

        public IList<Task> GetByRelease(Release release)
        {
            if (release != null)
            {
                return GetAll().Where(tsk => tsk.Release.ReleaseId == release.ReleaseId).ToList();
            }
            return null;
        }
    }
}