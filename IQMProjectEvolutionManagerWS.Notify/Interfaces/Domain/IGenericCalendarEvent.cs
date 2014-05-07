// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericCalendarEvent.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The GenericCalendarEvent interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManagerWS.Notify.Enums;

    /// <summary>
    /// The GenericCalendarEvent interface.
    /// </summary>
    public interface IGenericCalendarEvent
    {
        /// <summary>
        /// Gets or sets the generic calendar event id.
        /// </summary>
        string GenericCalendarEventId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        string Visibility { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the read only.
        /// </summary>
        bool? ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the notification colour.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        NotificationColours NotificationColour { get; set; }
    }
}
