using IQMProjectEvolutionManagerWS.Notify.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    public interface IGenericCalendarEvent
    {
        string GenericCalendarEventId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Location { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        string TimeZone { get; set; }
        string Visibility { get; set; }
        string Status { get; set; }
        bool? ReadOnly { get; set; }
        NotificationColours NotificationColour { get; set; }
    }
}
