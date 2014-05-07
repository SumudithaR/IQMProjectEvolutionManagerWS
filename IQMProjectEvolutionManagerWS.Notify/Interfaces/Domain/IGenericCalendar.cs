// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericCalendar.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The GenericCalendar interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    /// <summary>
    /// The GenericCalendar interface.
    /// </summary>
    public interface IGenericCalendar
    {
        /// <summary>
        /// Gets or sets the calendar id.
        /// </summary>
        string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        string TimeZone { get; set; }
    }
}
