// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericCalendar.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the GenericCalendar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;

    /// <summary>
    /// The generic calendar.
    /// </summary>
    public sealed class GenericCalendar : IGenericCalendar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCalendar"/> class.
        /// </summary>
        public GenericCalendar()
        {
            this.TimeZone = "UTC";
            this.Location = "IQM Software";
        }

        /// <summary>
        /// Gets or sets the calendar id.
        /// </summary>
        public string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        public string TimeZone { get; set; }
    }
}
