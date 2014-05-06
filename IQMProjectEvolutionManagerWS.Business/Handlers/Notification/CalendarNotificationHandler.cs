using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification;

namespace IQMProjectEvolutionManagerWS.Business.Handlers.Notification
{
    public class CalendarNotificationHandler : ICalendarNotificationHandler
    {
        private readonly IDependencyResolver dependencyResolver;

        public CalendarNotificationHandler(IDependencyResolver resolver)
        {
            dependencyResolver = resolver;
        }

        public IList<SubscriberNotifier> GetReleaseCalendarNotifiers()
        {
            var notifiersToReturn = new List<SubscriberNotifier>();

            var userNotifierService = dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

            var subscribers = dependencyResolver.GetKernel().Get<ISubscriberService>().GetCalendarSubscribers(true);
            var userNotifierPurpose = dependencyResolver.GetKernel().Get<ISubscriberNotifierPurposeService>().GetByName("Release");
            var userNotifierType = dependencyResolver.GetKernel().Get<ISubscriberNotifierTypeService>().GetByName("Calendar");

            if (subscribers != null && userNotifierPurpose != null && userNotifierType != null)
            {
                foreach (var subscriber in subscribers)
                {
                    notifiersToReturn.Add(userNotifierService.GetNotifier(subscriber, userNotifierPurpose, userNotifierType));
                }
            }

            return notifiersToReturn;
        }

        public void SetupReleaseCalendars(ref IList<SubscriberNotifier> releaseCalendarNotifiers)
        {
            if (releaseCalendarNotifiers != null)
            {
                var subscriberNotifierService = dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

                foreach (var notifier in releaseCalendarNotifiers)
                {
                    var calendarService = dependencyResolver.GetKernel().Get<ICalendarService>(new ConstructorArgument("accessName", notifier.Subscriber.Email));
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
                    else
                    {
                        calendar = calendarService.GetCalendar(notifier.AccessId);
                    }
                }
            }
        }

        public void UpdateCalendarsWithRelease(Release release)
        {
            if (release != null)
            {
                var userNotifierLogService = dependencyResolver.GetKernel().Get<ISubscriberNotifierLogService>();

                var calendarNotifiers = GetReleaseCalendarNotifiers();
                SetupReleaseCalendars(ref calendarNotifiers);

                foreach (var notifier in calendarNotifiers)
                {
                    var calendarService = dependencyResolver.GetKernel().Get<ICalendarService>(new ConstructorArgument("accessName", notifier.Subscriber.Email));
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
}