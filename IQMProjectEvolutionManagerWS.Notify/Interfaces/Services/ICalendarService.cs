using IQMProjectEvolutionManagerWS.Notify.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Services
{
    public interface ICalendarService
    {
        string AddOrUpdateCalendar(GenericCalendar calendar);
        GenericCalendar GetCalendar(string calendarId);
        bool DeleteCalendar(string calendarId);
        string AddOrUpdateEvent(GenericCalendarEvent calendarEvent, string calendarId);
        GenericCalendarEvent GetEvent(string eventId, string calendarId);
        bool DeleteEvent(string eventId, string calendarId);
    }
}