// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SMSNotificationHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the SMSNotificationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers.Notification
{
    using System;
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

    /// <summary>
    /// The sms notification handler.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class SmsNotificationHandler : ISmsNotificationHandler
    {
        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsNotificationHandler"/> class.
        /// </summary>
        /// <param name="resolver">
        /// The resolver.
        /// </param>
        public SmsNotificationHandler(IDependencyResolver resolver)
        {
            this.dependencyResolver = resolver;
        }

        /// <summary>
        /// The get release sms notifiers.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<SubscriberNotifier> GetReleaseSmsNotifiers()
        {
            var notifiersToReturn = new List<SubscriberNotifier>();

            var userNotifierService = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

            var subscribers = this.dependencyResolver.GetKernel().Get<ISubscriberService>().GetSmsSubscribers(true);
            var userNotifierPurpose = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierPurposeService>().GetByName("Release");
            var userNotifierType = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierTypeService>().GetByName("SMS");

            if (subscribers == null || userNotifierPurpose == null || userNotifierType == null)
            {
                return notifiersToReturn;
            }

            notifiersToReturn.AddRange(subscribers.Select(subscriber => userNotifierService.GetNotifier(subscriber, userNotifierPurpose, userNotifierType)));

            return notifiersToReturn;
        }

        /// <summary>
        /// The send sms for release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        public void SendSmsForRelease(Release release)
        {
            if (release == null)
            {
                return;
            }

            var userNotifierLogService = this.dependencyResolver.GetKernel().Get<ISubscriberNotifierLogService>();

            var smsNotifiers = this.GetReleaseSmsNotifiers();

            foreach (var notifier in smsNotifiers)
            {
                if (notifier == null
                    || DateTime.Now.AddDays(notifier.Subscriber.SmsNotificationPeriod) != release.DueDate)
                {
                    continue;
                }

                var proceed = true;

                var smsService = this.dependencyResolver.GetKernel().Get<ISmsService>();
                var logEntry = userNotifierLogService.GetByRegisteredForId(notifier, release.ReleaseId);

                if (logEntry != null && !logEntry.TransactionId.Equals(string.Empty) && smsService.GetSentMessage(logEntry.TransactionId, userNotifierLogService.LogsCount()) != null)
                {
                    if (logEntry.EndDate != null && logEntry.EndDate == release.DueDate)
                    {
                        proceed = false;
                    }
                    else
                    {
                        userNotifierLogService.Delete(logEntry);
                    }
                }

                if (!proceed)
                {
                    continue;
                }

                var linkedProjects = string.Empty;

                foreach (var name in release.ReleaseProjects.Select(p => p.Project.Name))
                {
                    if (linkedProjects.Equals(string.Empty))
                    {
                        linkedProjects = name;
                    }
                    else
                    {
                        linkedProjects += ", " + name;
                    }
                }

                var smsMessage = new GenericSms()
                                     {
                                         Message = "The release: " + release.Name + ", linked to the project(s): " + linkedProjects + ", is due on: " + release.DueDate.ToShortDateString() +
                                                   ". The release statistics are as follows. The percentage complete is: " + release.PercentageComplete + "%, The total hours worked is: " + release.HoursWorked +
                                                   ", The total hours remaining is: " + release.HoursRemaining + ".",
                                         Mobile = notifier.Subscriber.Mobile
                                     };

                smsMessage.GenericSmsId = smsService.SendSms(smsMessage);

                userNotifierLogService.InsertOrUpdate(
                    new SubscriberNotifierLog()
                        {
                            TransactionId = smsMessage.GenericSmsId,
                            RegisteredForId = release.ReleaseId,
                            EndDate = release.DueDate,
                            Message = smsMessage.Message,
                            SentSuccess = (smsMessage.GenericSmsId != null && !smsMessage.GenericSmsId.Equals(string.Empty)) ? true : false,
                            StartDate = release.DueDate,
                            SubscriberNotifier = notifier,
                        });
            }
        }
    }
}