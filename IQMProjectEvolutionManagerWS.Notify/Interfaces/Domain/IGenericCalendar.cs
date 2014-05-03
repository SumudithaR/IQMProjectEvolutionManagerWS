using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    public interface IGenericCalendar
    {
        string CalendarId { get; set; }
        string Description { get; set; }
        string Summary { get; set; }
        string Location { get; set; }
        string TimeZone { get; set; }
    }
}
