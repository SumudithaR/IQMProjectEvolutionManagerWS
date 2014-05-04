using IQMProjectEvolutionManagerWS.Data;
using System.Collections.Generic;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    /// <summary>
    /// The interface of the service to interact with the OnTime WorkLog repository. 
    /// </summary>
    public interface IOnTimeWorkLogService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<WorkLog> GetAll();

        /// <summary>
        /// Gets a list of last week WorkLogs recorded in the last week by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        IList<WorkLog> GetLastWeekByTask(Task task);
    }
}