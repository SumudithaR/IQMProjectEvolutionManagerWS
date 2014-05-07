// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeWorkLogService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The service to interact with the OnTime WorkLog repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Data;
    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
    using IQMProjectEvolutionManagerWS.Data.Repository;

    /// <summary>
    /// The service to interact with the OnTime WorkLog repository. 
    /// </summary>
    public class OnTimeWorkLogService : IOnTimeWorkLogService
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IOnTimeRepository<WorkLog> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeWorkLogService"/> class.
        /// </summary>
        public OnTimeWorkLogService()
        {
            this.repository = new OnTimeRepository<WorkLog>();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<WorkLog> GetAll()
        {
            return this.repository.GetAll();
        }

        /// <summary>
        /// The get last week by task.
        /// </summary>
        /// <param name="task">
        /// The task.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<WorkLog> GetLastWeekByTask(Task task)
        {
            // If task is not null, then get the collection of WorkLog objects recorded in the last week. They must be linked to the task. 
            return task != null ? this.GetAll().Where(wLog => wLog.Task.TaskId == task.TaskId && wLog.WorkLogDateTime >= DateTime.Today.AddDays(-7)).ToList() : null;
        }
    }
}