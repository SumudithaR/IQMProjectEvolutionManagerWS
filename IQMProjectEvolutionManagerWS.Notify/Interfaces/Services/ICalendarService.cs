// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICalendarService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The CalendarService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Services
{
    using IQMProjectEvolutionManagerWS.Notify.Domain;

    /// <summary>
    /// The CalendarService interface.
    /// </summary>
    public interface ICalendarService
    {
        /// <summary>
        /// The add or update calendar.
        /// </summary>
        /// <param name="calendar">
        /// The calendar.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string AddOrUpdateCalendar(GenericCalendar calendar);

        /// <summary>
        /// The get calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="GenericCalendar"/>.
        /// </returns>
        GenericCalendar GetCalendar(string calendarId);

        /// <summary>
        /// The delete calendar.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DeleteCalendar(string calendarId);

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
        string AddOrUpdateEvent(GenericCalendarEvent calendarEvent, string calendarId);

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
        GenericCalendarEvent GetEvent(string eventId, string calendarId);

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
        bool DeleteEvent(string eventId, string calendarId);
    }
}