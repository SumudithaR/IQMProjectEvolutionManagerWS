// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeWorkLogService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The interface of the service to interact with the OnTime WorkLog repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    using System.Collections.Generic;

    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The interface of the service to interact with the OnTime WorkLog repository. 
    /// </summary>
    public interface IOnTimeWorkLogService
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<WorkLog> GetAll();

        /// <summary>
        /// The get last week by task.
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<WorkLog> GetLastWeekByTask(Task task);
    }
}