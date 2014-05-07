// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoogleCalendarService.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The google calendar service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Services
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Http;
    using Google.Apis.Services;

    using IQMProjectEvolutionManagerWS.Notify.Authentication;
    using IQMProjectEvolutionManagerWS.Notify.Domain;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;

    /// <summary>
    /// The google calendar service.
    /// </summary>
    public class GoogleCalendarService : ICalendarService
    {
        /// <summary>
        /// The service access name.
        /// </summary>
        private readonly string serviceAccessName;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleCalendarService"/> class.
        /// </summary>
        /// <param name="accessName">
        /// The access name.
        /// </param>
        public GoogleCalendarService(string accessName)
        {
            this.serviceAccessName = accessName;
        }

        /// <summary>
        /// The add or update calendar.
        /// </summary>
        /// <param name="calendar">
        /// The calendar.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string AddOrUpdateCalendar(GenericCalendar calendar)
        {
            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
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

        /// <summary>
        /// The get calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="GenericCalendar"/>.
        /// </returns>
        public GenericCalendar GetCalendar(string calendarId)
        {
            if (calendarId.Equals(string.Empty))
            {
                return null;
            }

            calendarId = calendarId.Trim();

            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var cal = calendarService.CalendarList.List().Execute().Items.Any(dCal => dCal.Id.Equals(calendarId)) ? calendarService.Calendars.Get(calendarId).Execute() : null;
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

            return null;
        }

        /// <summary>
        /// The delete calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DeleteCalendar(string calendarId)
        {
            if (calendarId.Equals(string.Empty))
            {
                return false;
            }

            calendarId = calendarId.Trim();

            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var response = calendarService.CalendarList.List().Execute().Items.Any(dCal => dCal.Id.Equals(calendarId)) ? calendarService.Calendars.Delete(calendarId).Execute() : null;

            return response != null && response.Equals(string.Empty);
        }

        /// <summary>
        /// The add or update event.
        /// </summary>
        /// <param name="calendarEvent">
        /// The calendar event.
        /// </param>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string AddOrUpdateEvent(GenericCalendarEvent calendarEvent, string calendarId)
        {
            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
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

        /// <summary>
        /// The get event.
        /// </summary>
        /// <param name="eventId">
        /// The event id.
        /// </param>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="GenericCalendarEvent"/>.
        /// </returns>
        public GenericCalendarEvent GetEvent(string eventId, string calendarId)
        {
            if (eventId.Equals(string.Empty) || calendarId.Equals(string.Empty))
            {
                return null;
            }

            eventId = eventId.Trim();
            calendarId = calendarId.Trim();

            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var calEvent = calendarService.Events.List(calendarId).Execute().Items.Any(dEve => dEve.Id.Equals(eventId)) ? calendarService.Events.Get(calendarId, eventId).Execute() : null;

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

            return null;
        }

        /// <summary>
        /// The delete event.
        /// </summary>
        /// <param name="eventId">
        /// The event id.
        /// </param>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool DeleteEvent(string eventId, string calendarId)
        {
            if (eventId.Equals(string.Empty) || calendarId.Equals(string.Empty))
            {
                return false;
            }

            var credential = GoogleAuthenticator.GetAuthenticatedCredential(this.serviceAccessName, new[] { CalendarService.Scope.Calendar });
            var calendarService = GetService(credential);

            var response = calendarService.Events.List(calendarId).Execute().Items.Any(dEve => dEve.Id.Equals(eventId)) ? calendarService.Events.Delete(calendarId, eventId).Execute() : null;

            return response.Equals(string.Empty);
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="credential">
        /// The credential.
        /// </param>
        /// <returns>
        /// The <see cref="CalendarService"/>.
        /// </returns>
        private static CalendarService GetService(IConfigurableHttpClientInitializer credential)
        {
            var calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ConfigurationManager.AppSettings["ApplicationName"],
            });

            return calendarService;
        }
    }
}