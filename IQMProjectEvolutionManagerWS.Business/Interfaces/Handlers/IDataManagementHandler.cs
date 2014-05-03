using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers
{
    public interface IDataManagementHandler
    {
        void InsertReleasesByPreference();
        void InsertStaffMembers();
        void InsertReleaseTypes();
        void InsertReleaseStatusTypes();
    }
}