using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime ReleaseType repository. 
    /// </summary>
    public class OnTimeReleaseTypeService : IOnTimeReleaseTypeService
    {
        private readonly IOnTimeRepository<ReleaseType> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseTypeService"/> class.
        /// </summary>
        public OnTimeReleaseTypeService()
        {
            _repository = new OnTimeRepository<ReleaseType>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<ReleaseType> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets the single ReleaseType by its name.
        /// </summary>
        /// <param name="releaseTypeName">Name of the release type.</param>
        /// <returns></returns>
        public ReleaseType GetByName(string releaseTypeName)
        {
            if (releaseTypeName != string.Empty)
            {
                releaseTypeName = releaseTypeName.Trim();
            }

            // Get the ReleaseType that matches the releaseTypeName. 
            return GetAll().SingleOrDefault(rType => rType.Name.Equals(releaseTypeName));
        }
    }
}