// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeReleaseStatusTypeService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime ReleaseStatusType repository.
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
    /// The service to interact with the OnTime ReleaseStatusType repository. 
    /// </summary>
    public class OnTimeReleaseStatusTypeService : IOnTimeReleaseStatusTypeService
    {
        /// <summary>
        /// The _repository.
        /// </summary>
        private readonly IOnTimeRepository<ReleaseStatusType> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeReleaseStatusTypeService"/> class.
        /// </summary>
        public OnTimeReleaseStatusTypeService()
        {
            this.repository = new OnTimeRepository<ReleaseStatusType>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<ReleaseStatusType> GetAll()
        {
            return this.repository.GetAll();
        }

        /// <summary>
        /// The get by name and release type.
        /// </summary>
        /// <param name="releaseStatusTypeName">
        /// The release status type name.
        /// </param>
        /// <param name="releaseTypeId">
        /// The release type id.
        /// </param>
        /// <returns>
        /// The <see cref="ReleaseStatusType"/>.
        /// </returns>
        public ReleaseStatusType GetByNameAndReleaseType(string releaseStatusTypeName, int releaseTypeId)
        {
            // Remove any leading or trailing spaces prior to comparison. 
            if (releaseStatusTypeName != string.Empty)
            {
                releaseStatusTypeName = releaseStatusTypeName.Trim();
            }

            // Get the ReleaseStatusType that matches the releaseStatusTypeName and releaseTypeId. 
            return this.GetAll().SingleOrDefault(rSType => rSType.Name.Equals(releaseStatusTypeName) && rSType.ReleaseTypeId == releaseTypeId);
        }
    }
}