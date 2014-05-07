// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICalendarNotificationHandler.cs" company="IQM Software">
//   Sumuditha Ranawka 2014.
// </copyright>
// <summary>
//   Defines the ICalendarNotificationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Domain;

    /// <summary>
    /// The CalendarNotificationHandler interface.
    /// </summary>
    public interface ICalendarNotificationHandler
    {
        /// <summary>
        /// The get release calendar notifiers.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        IList<SubscriberNotifier> GetReleaseCalendarNotifiers();

        /// <summary>
        /// The setup release calendars.
        /// </summary>
        /// <param name="releaseCalendarNotifiers">
        /// The release calendar notifiers.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        void SetupReleaseCalendars(ref IList<SubscriberNotifier> releaseCalendarNotifiers);

        /// <summary>
        /// The update calendars with release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        void UpdateCalendarsWithRelease(Release release);
    }
}