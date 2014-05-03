using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    public class GenericCalendar : IGenericCalendar
    {
        public GenericCalendar()
        {
            TimeZone = "UTC";
            Location = "IQM Software";
        }

        public virtual string CalendarId { get; set; }
        public virtual string Description { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Location { get; set; }
        public virtual string TimeZone { get; set; }
    }
}
