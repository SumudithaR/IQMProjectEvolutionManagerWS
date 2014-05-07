// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericCalendarEvent.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the GenericCalendarEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManagerWS.Notify.Enums;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;

    /// <summary>
    /// The generic calendar event.
    /// </summary>
    public sealed class GenericCalendarEvent : IGenericCalendarEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCalendarEvent"/> class.
        /// </summary>
        public GenericCalendarEvent()
        {
            this.Location = "IQM Software";
            this.TimeZone = "UTC";
            this.Visibility = "private";
            this.Status = "confirmed";
        }

        /// <summary>
        /// Gets or sets the generic calendar event id.
        /// </summary>
        public string GenericCalendarEventId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the read only.
        /// </summary>
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the notification colour.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public NotificationColours NotificationColour { get; set; }
    }
}