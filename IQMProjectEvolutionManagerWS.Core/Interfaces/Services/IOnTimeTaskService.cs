using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Core.Interfaces.Services
{
    public interface IOnTimeTaskService
    {
        IList<Task> GetAll();
        IList<Task> GetByRelease(Release release);
    }
}