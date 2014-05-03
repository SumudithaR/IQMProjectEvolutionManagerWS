using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using IQMProjectEvolutionManager.Core.Interfaces.Domain;
using IQMProjectEvolutionManagerWS.Notify.Authentication;
using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Notify.Services
{
    public class GoogleCalendarService : ICalendarService
    {
        private readonly string serviceAccessName;

        private CalendarService GetService(UserCredential credential)
        {
            var calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ConfigurationManager.AppSettings["ApplicationName"],
            });

            return calendarService;
        }

        public GoogleCalendarService(string accessName)
        {
            serviceAccessName = accessName;
        }

        public string AddOrUpdateCalendar(GenericCalendar calendar)
        {
            var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var cal = (calendar.CalendarId != null && calendarService.CalendarList.List().Execute().Items.Any(dCal => dCal.Id.Equals(calendar.CalendarId)))
                ? calendarService.Calendars.Get(calendar.CalendarId).Execute() : null;

            if (cal != null)
            {
                cal.Description = calendar.Description;
                cal.Summary = calendar.Summary;
                cal.Location = calendar.Location;
                cal.TimeZone = calendar.TimeZone;

                cal = calendarService.Calendars.Update(cal, cal.Id).Execute();
            }
            else
            {
                cal = calendarService.Calendars.Insert(
                    new Calendar()
                    {
                        Description = calendar.Description,
                        Summary = calendar.Summary,
                        Location = calendar.Location,
                        TimeZone = calendar.TimeZone,
                    }).Execute();
            }

            return cal.Id;
        }

        public GenericCalendar GetCalendar(string calendarId)
        {
            if (!calendarId.Equals(string.Empty))
            {
                calendarId = calendarId.Trim();

                var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
                var calendarService = GetService(credential);

                var cal = (calendarService.CalendarList.List().Execute().Items.Any(dCal => dCal.Id.Equals(calendarId))) ? calendarService.Calendars.Get(calendarId).Execute() : null;
                if (cal != null)
                {
                    return new GenericCalendar()
                    {
                        CalendarId = cal.Id,
                        Description = cal.Description,
                        Location = cal.Location,
                        Summary = cal.Summary,
                        TimeZone = cal.TimeZone
                    };
                }
            }

            return null;
        }

        public bool DeleteCalendar(string calendarId)
        {
            if (!calendarId.Equals(string.Empty))
            {
                calendarId = calendarId.Trim();

                var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
                var calendarService = GetService(credential);

                var response = (calendarService.CalendarList.List().Execute().Items.Any(dCal => dCal.Id.Equals(calendarId))) ? calendarService.Calendars.Delete(calendarId).Execute() : null;

                if (response.Equals(string.Empty))
                {
                    return true;
                }
            }

            return false;
        }

        public string AddOrUpdateEvent(GenericCalendarEvent calendarEvent, string calendarId)
        {
            var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var calEvent = (calendarEvent.GenericCalendarEventId != null && calendarService.Events.List(calendarId).Execute().Items.Any(dEve => dEve.Id.Equals(calendarEvent.GenericCalendarEventId)))
                ? calendarService.Events.Get(calendarId, calendarEvent.GenericCalendarEventId).Execute() : null;

            if (calEvent != null)
            {
                var startDate = new EventDateTime();
                startDate.DateTime = (calendarEvent.StartDate != null) ? calendarEvent.StartDate.Value : DateTime.Now;
                startDate.TimeZone = calendarEvent.TimeZone;

                var endDate = new EventDateTime();
                endDate.DateTime = (calendarEvent.EndDate != null) ? calendarEvent.EndDate.Value : DateTime.Now;
                endDate.TimeZone = calendarEvent.TimeZone;

                calEvent.Description = calendarEvent.Description;
                calEvent.Summary = calendarEvent.Title;
                calEvent.Start = startDate;
                calEvent.End = endDate;
                calEvent.Visibility = calendarEvent.Visibility;
                calEvent.Status = calendarEvent.Status;

                try
                {
                    calEvent = calendarService.Events.Update(calEvent, calendarId, calendarEvent.GenericCalendarEventId).Execute();
                }
                catch (Exception e)
                {
                    var logger = log4net.LogManager.GetLogger(typeof(GoogleCalendarService));
                    logger.Error(e.Message);
                }
            }
            else
            {
                var startDate = new EventDateTime();
                startDate.DateTime = (calendarEvent.StartDate != null) ? calendarEvent.StartDate.Value : DateTime.Now;
                startDate.TimeZone = calendarEvent.TimeZone;

                var endDate = new EventDateTime();
                endDate.DateTime = (calendarEvent.EndDate != null) ? calendarEvent.EndDate.Value : DateTime.Now;
                endDate.TimeZone = calendarEvent.TimeZone;

                calEvent = new Event()
                    {
                        Description = calendarEvent.Description,
                        Summary = calendarEvent.Title,
                        Start = startDate,
                        End = endDate,
                        Visibility = calendarEvent.Visibility,
                        Status = calendarEvent.Status
                    };

                calEvent = calendarService.Events.Insert(calEvent, calendarId).Execute();
            }

            return calEvent.Id;
        }

        public GenericCalendarEvent GetEvent(string eventId, string calendarId)
        {
            if (!eventId.Equals(string.Empty) && !calendarId.Equals(string.Empty))
            {
                eventId = eventId.Trim();
                calendarId = calendarId.Trim();

                var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
                var calendarService = GetService(credential);

                var calEvent = (calendarService.Events.List(calendarId).Execute().Items.Any(dEve => dEve.Id.Equals(eventId))) ? calendarService.Events.Get(calendarId, eventId).Execute() : null;

                if (calEvent != null)
                {
                    return new GenericCalendarEvent()
                    {
                        Description = calEvent.Description,
                        EndDate = calEvent.End.DateTime,
                        Location = calEvent.Location,
                        StartDate = calEvent.Start.DateTime,
                        Title = calEvent.Summary,
                        TimeZone = calEvent.Start.TimeZone,
                        Visibility = calEvent.Visibility,
                    };
                }
            }

            return null;
        }

        public bool DeleteEvent(string eventId, string calendarId)
        {
            if (!eventId.Equals(string.Empty) && !calendarId.Equals(string.Empty))
            {
                var credential = GoogleAuthenticator.GetAuthenticatedCredential(serviceAccessName, new[] { CalendarService.Scope.Calendar });
                var calendarService = GetService(credential);

                var response = (calendarService.Events.List(calendarId).Execute().Items.Any(dEve => dEve.Id.Equals(eventId))) ? calendarService.Events.Delete(calendarId, eventId).Execute() : null;

                if (response.Equals(string.Empty))
                {
                    return true;
                }
            }

            return false;
        }
    }
}