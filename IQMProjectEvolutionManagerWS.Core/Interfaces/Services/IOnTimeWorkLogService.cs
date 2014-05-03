using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    public interface IOnTimeWorkLogService
    {
        IList<WorkLog> GetAll();
        IList<WorkLog> GetLastWeekByTask(Task task);
    }
}