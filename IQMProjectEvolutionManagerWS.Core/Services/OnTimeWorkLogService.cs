using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    /// <summary>
    /// The service to interact with the OnTime WorkLog repository. 
    /// </summary>
    public class OnTimeWorkLogService : IOnTimeWorkLogService
    {
        private readonly IOnTimeRepository<WorkLog> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeWorkLogService"/> class.
        /// </summary>
        public OnTimeWorkLogService()
        {
            _repository = new OnTimeRepository<WorkLog>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<WorkLog> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Gets a list of last week WorkLogs recorded in the last week by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public IList<WorkLog> GetLastWeekByTask(Task task)
        {
            // If task is not null, then get the collection of WorkLog objects recorded in the last week. They must be linked to the task. 
            return task != null ? GetAll().Where(wLog => wLog.Task.TaskId == task.TaskId && wLog.WorkLogDateTime >= DateTime.Today.AddDays(-7)).ToList() : null;
        }
    }
}