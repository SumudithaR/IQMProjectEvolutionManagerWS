using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using Ninject.Parameters;
using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification;

namespace IQMProjectEvolutionManagerWS.Business.Handlers.Notification
{
    public class SMSNotificationHandler : ISMSNotificationHandler
    {
        private readonly IDependencyResolver dependencyResolver;

        public SMSNotificationHandler(IDependencyResolver resolver)
        {
            dependencyResolver = resolver;
        }

        public IList<SubscriberNotifier> GetReleaseSMSNotifiers()
        {
            var notifiersToReturn = new List<SubscriberNotifier>();

            var userNotifierService = dependencyResolver.GetKernel().Get<ISubscriberNotifierService>();

            var subscribers = dependencyResolver.GetKernel().Get<ISubscriberService>().GetSMSubscribers(true);
            var userNotifierPurpose = dependencyResolver.GetKernel().Get<ISubscriberNotifierPurposeService>().GetByName("Release");
            var userNotifierType = dependencyResolver.GetKernel().Get<ISubscriberNotifierTypeService>().GetByName("SMS");

            if (subscribers != null && userNotifierPurpose != null && userNotifierType != null)
            {
                foreach (var subscriber in subscribers)
                {
                    notifiersToReturn.Add(userNotifierService.GetNotifier(subscriber, userNotifierPurpose, userNotifierType));
                }
            }

            return notifiersToReturn;
        }

        public void SendSMSForRelease(Release release)
        {
            if (release != null)
            {
                var userNotifierLogService = dependencyResolver.GetKernel().Get<ISubscriberNotifierLogService>();

                var smsNotifiers = GetReleaseSMSNotifiers();

                foreach (var notifier in smsNotifiers)
                {
                    if (notifier != null && (release.DueDate - DateTime.Now).Days == notifier.Subscriber.SMSNotificationPeriod)
                    {
                        var proceed = true;

                        var smsService = dependencyResolver.GetKernel().Get<ISMSService>();
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

                        if (proceed)
                        {
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

                            var smsMessage = new GenericSMS()
                            {
                                Message = "The release: " + release.Name + ", linked to the project(s): " + linkedProjects + ", is due on: " + release.DueDate.ToShortDateString() + 
                                ". The release statistics are as follows. The percentage complete is: " + release.PercentageComplete + ", The total hours worked " + release.HoursWorked + 
                                ", The total hours remaining is: " + release.HoursRemaining,
                                Mobile = notifier.AccessName
                            };

                            smsMessage.GenericSMSId = smsService.SendSMS(smsMessage);

                            userNotifierLogService.InsertOrUpdate(
                                new SubscriberNotifierLog()
                                {
                                    TransactionId = smsMessage.GenericSMSId,
                                    RegisteredForId = release.ReleaseId,
                                    EndDate = release.DueDate,
                                    Message = smsMessage.Message,
                                    SentSuccess = (smsMessage.GenericSMSId != null && !smsMessage.GenericSMSId.Equals(string.Empty)) ? true : false,
                                    StartDate = release.DueDate,
                                    SubscriberNotifier = notifier,
                                });
                        }
                    }
                }
            }
        }
    }
}