using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManagerWS.Notify.Enums;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    public class GenericCalendarEvent : IGenericCalendarEvent
    {
        public GenericCalendarEvent()
        {
            Location = "IQM Software";
            TimeZone = "UTC";
            Visibility = "private";
            Status = "confirmed";
        }

        public virtual string GenericCalendarEventId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Location { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string Visibility { get; set; }
        public virtual string Status { get; set; }
        public virtual bool? ReadOnly { get; set; }
        public virtual NotificationColours NotificationColour { get; set; }
    }
}