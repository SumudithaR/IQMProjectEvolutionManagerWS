using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    public class OnTimeReleaseTypeService : IOnTimeReleaseTypeService
    {
        private readonly IOnTimeRepository<ReleaseType> repository;

        public OnTimeReleaseTypeService()
        {
            repository = new OnTimeRepository<ReleaseType>();
        }

        public IList<ReleaseType> GetAll()
        {
            return repository.GetAll();
        }

        public ReleaseType GetByName(string releaseTypeName)
        {
            if (releaseTypeName != string.Empty)
            {
                releaseTypeName = releaseTypeName.Trim();
            }

            return GetAll().SingleOrDefault(rType => rType.Name.Equals(releaseTypeName));
        }
    }
}