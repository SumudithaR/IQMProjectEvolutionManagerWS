// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarNotificationHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the CalendarNotificationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers.Notification
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Interfaces.Services;

    using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification;
    using IQMProjectEvolutionManagerWS.Notify.Domain;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;

    using Ninject;
    using Ninject.Parameters;

    /// <summary>
    /// The calendar notification handler.
    /// </summary>
    public class CalendarNotificationHandler : ICalendarNotificationHandler
    {
        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarNotificationHandler"/> class.
        /// </summary>
        /// <param name="resolver">
        /// The resolver.
        /// </param>
        public CalendarNotificationHandler(IDependencyResolver resolver)
        {
            this.dependencyResolver = resolver;
        }

        /// <summary>
        /// The get release calendar notifiers.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public IList<SubscriberNotifier> GetReleaseCalendarNotifiers()
        {
            var notifiersToReturn = new List<SubscriberNotifier>();

            var userNotifierService = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

            var subscribers = this.dependencyResolver.GetKernel().Get<ISubscriberService>().GetCalendarSubscribers(true);
            var userNotifierPurpose = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierPurposeService>().GetByName("Release");
            var userNotifierType = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierTypeService>().GetByName("Calendar");

            if (subscribers == null || userNotifierPurpose == null || userNotifierType == null)
            {
                return notifiersToReturn;
            }

            notifiersToReturn.AddRange(subscribers.Select(subscriber => userNotifierService.GetNotifier(subscriber, userNotifierPurpose, userNotifierType)));

            return notifiersToReturn;
        }

        /// <summary>
        /// The setup release calendars.
        /// </summary>
        /// <param name="releaseCalendarNotifiers">
        /// The release calendar notifiers.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public void SetupReleaseCalendars(ref IList<SubscriberNotifier> releaseCalendarNotifiers)
        {
            if (releaseCalendarNotifiers == null)
            {
                return;
            }

            var subscriberNotifierService = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

            foreach (var notifier in releaseCalendarNotifiers)
            {
                var calendarService = this.dependencyResolver.GetKernel().Get<ICalendarService>(new ConstructorArgument("accessName", notifier.Subscriber.Email));
                var calendar = new GenericCalendar();

                if (notifier.AccessId == null || notifier.AccessId.Equals(string.Empty) || calendarService.GetCalendar(notifier.AccessId) == null)
                {
                    calendar.Summary = "IQM PEM Releases";
                    calendar.Description = "Releases of projects.";
                    calendar.Location = "IQM";

                    calendar.CalendarId = calendarService.AddOrUpdateCalendar(calendar);

                    notifier.AccessId = calendar.CalendarId;

                    subscriberNotifierService.InsertOrUpdate(notifier);
                }
            }
        }

        /// <summary>
        /// The update calendars with release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        public void UpdateCalendarsWithRelease(Release release)
        {
            if (release == null)
            {
                return;
            }

            var userNotifierLogService = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierLogService>();

            var calendarNotifiers = this.GetReleaseCalendarNotifiers();
            this.SetupReleaseCalendars(ref calendarNotifiers);

            foreach (var notifier in calendarNotifiers)
            {
                var calendarService = this.dependencyResolver.GetKernel().Get<ICalendarService>(new ConstructorArgument("accessName", notifier.Subscriber.Email));
                var logEntry = userNotifierLogService.GetByRegisteredForId(notifier, release.ReleaseId);

                if (logEntry != null && !logEntry.TransactionId.Equals(string.Empty) && calendarService.GetEvent(logEntry.TransactionId, notifier.AccessId) != null)
                {
                    var eventOnProvider = calendarService.GetEvent(logEntry.TransactionId, notifier.AccessId);

                    eventOnProvider.GenericCalendarEventId = logEntry.TransactionId;
                    eventOnProvider.Title = release.Name;
                    eventOnProvider.Description = "Due Date: " + release.DueDate.ToShortDateString();
                    eventOnProvider.StartDate = release.DueDate;
                    eventOnProvider.EndDate = release.DueDate;
                    eventOnProvider.NotificationColour = Notify.Enums.NotificationColours.Blue;
                    eventOnProvider.ReadOnly = true;

                    logEntry.TransactionId = calendarService.AddOrUpdateEvent(eventOnProvider, notifier.AccessId);

                    userNotifierLogService.InsertOrUpdate(logEntry);
                }
                else
                {
                    if (logEntry != null)
                    {
                        userNotifierLogService.Delete(logEntry);
                    }

                    var calEvent = new GenericCalendarEvent();

                    calEvent.Title = release.Name;
                    calEvent.Description = "Due Date: " + release.DueDate.ToShortDateString();
                    calEvent.StartDate = release.DueDate;
                    calEvent.EndDate = release.DueDate;
                    calEvent.NotificationColour = Notify.Enums.NotificationColours.Blue;
                    calEvent.ReadOnly = true;

                    calEvent.GenericCalendarEventId = calendarService.AddOrUpdateEvent(calEvent, notifier.AccessId);

                    userNotifierLogService.InsertOrUpdate(
                        new SubscriberNotifierLog()
                            {
                                TransactionId = calEvent.GenericCalendarEventId,
                                RegisteredForId = release.ReleaseId,
                                EndDate = calEvent.EndDate,
                                Location = calEvent.Location,
                                Message = calEvent.Description,
                                SentSuccess = true,
                                StartDate = calEvent.StartDate,
                                Subject = calEvent.Title,
                                SubscriberNotifier = notifier,
                            });
                }
            }
        }
    }
}