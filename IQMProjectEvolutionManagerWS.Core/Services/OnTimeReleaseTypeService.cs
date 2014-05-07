// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeReleaseTypeService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime ReleaseType repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Data;
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    /// <summary>
    /// The service to interact with the OnTime ReleaseType repository. 
    /// </summary>
    public class OnTimeReleaseTypeService : IOnTimeReleaseTypeService
    {
        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IOnTimeRepository<ReleaseType> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseTypeService"/> class.
        /// </summary>
        public OnTimeReleaseTypeService()
        {
            this.repository = new OnTimeRepository<ReleaseType>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<ReleaseType> GetAll()
        {
            return this.repository.GetAll();
        }

        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="releaseTypeName">
        /// The release type name.
        /// </param>
        /// <returns>
        /// The <see cref="ReleaseType"/>.
        /// </returns>
        public ReleaseType GetByName(string releaseTypeName)
        {
            if (releaseTypeName != string.Empty)
            {
                releaseTypeName = releaseTypeName.Trim();
            }

            // Get the ReleaseType that matches the releaseTypeName. 
            return this.GetAll().SingleOrDefault(rType => rType.Name.Equals(releaseTypeName));
        }
    }
}