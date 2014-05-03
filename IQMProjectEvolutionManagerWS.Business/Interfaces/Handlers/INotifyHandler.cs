using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers
{
    public interface INotifyHandler
    {
        void HandleUpdates(Release release);
    }
}