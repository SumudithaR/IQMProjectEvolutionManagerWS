using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime ReleaseStatusType repository. 
    /// </summary>
    public class OnTimeReleaseStatusTypeService : IOnTimeReleaseStatusTypeService
    {
        private readonly IOnTimeRepository<ReleaseStatusType> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseStatusTypeService"/> class.
        /// </summary>
        public OnTimeReleaseStatusTypeService()
        {
            _repository = new OnTimeRepository<ReleaseStatusType>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<ReleaseStatusType> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets the single ReleaseStatusType by its name and the Id of its 
        /// associated ReleaseType.
        /// </summary>
        /// <param name="releaseStatusTypeName">Name of the release status type.</param>
        /// <param name="releaseTypeId">The release type identifier.</param>
        /// <returns></returns>
        public ReleaseStatusType GetByNameAndReleaseType(string releaseStatusTypeName, int releaseTypeId)
        {
            // Remove any leading or trailing spaces prior to comparison. 
            if (releaseStatusTypeName != string.Empty)
            {
                releaseStatusTypeName = releaseStatusTypeName.Trim();
            }

            // Get the ReleaseStatusType that matches the releaseStatusTypeName and releaseTypeId. 
            return GetAll().SingleOrDefault(rSType => rSType.Name.Equals(releaseStatusTypeName) && rSType.ReleaseTypeId == releaseTypeId);
        }
    }
}