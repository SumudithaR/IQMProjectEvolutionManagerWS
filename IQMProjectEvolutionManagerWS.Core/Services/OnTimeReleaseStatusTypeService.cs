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
    public class OnTimeReleaseStatusTypeService : IOnTimeReleaseStatusTypeService
    {
        private readonly IOnTimeRepository<ReleaseStatusType> repository;

        public OnTimeReleaseStatusTypeService()
        {
            repository = new OnTimeRepository<ReleaseStatusType>();
        }

        public IList<ReleaseStatusType> GetAll()
        {
            return repository.GetAll();
        }

        public ReleaseStatusType GetByNameAndReleaseType(string releaseStatusTypeName, int releaseTypeId)
        {
            if (releaseStatusTypeName != string.Empty)
            {
                releaseStatusTypeName = releaseStatusTypeName.Trim();
            }

            return GetAll().SingleOrDefault(rSType => rSType.Name.Equals(releaseStatusTypeName) && rSType.ReleaseTypeId == releaseTypeId);
        }
    }
}