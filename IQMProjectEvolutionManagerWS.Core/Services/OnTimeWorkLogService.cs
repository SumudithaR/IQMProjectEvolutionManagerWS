using IQMProjectEvolutionManagerWS.Core.Interfaces;
using IQMProjectEvolutionManagerWS.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Data;
using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;
using IQMProjectEvolutionManagerWS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Core.Services
{
    public class OnTimeWorkLogService : IOnTimeWorkLogService
    {
        private readonly IOnTimeRepository<WorkLog> repository;

        public OnTimeWorkLogService()
        {
            repository = new OnTimeRepository<WorkLog>();
        }

        public IList<WorkLog> GetAll()
        {
            return repository.GetAll();
        }

        public IList<WorkLog> GetLastWeekByTask(Task task)
        {
            if (task != null)
            {
                return GetAll().Where(wLog => wLog.Task.TaskId == task.TaskId && wLog.WorkLogDateTime >= DateTime.Today.AddDays(-7)).ToList();
            }

            return null;
        }
    }
}