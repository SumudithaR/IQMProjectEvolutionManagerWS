using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using IQMProjectEvolutionManager.Core.Interfaces.Services;
using Ninject.Parameters;
using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Business.Handlers.Notification;

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    public class NotifyHandler : INotifyHandler
    {
        private readonly IDependencyResolver dependencyResolver;

        public NotifyHandler(IDependencyResolver resolver)
        {
            dependencyResolver = resolver;
        }

        public void HandleUpdates(Release release)
        {
            var calendarNotificationHandler = new CalendarNotificationHandler(dependencyResolver);
            calendarNotificationHandler.UpdateCalendarsWithRelease(release);

            var smsNotificationHandler = new SMSNotificationHandler(dependencyResolver);
            smsNotificationHandler.SendSMSForRelease(release);
        }
    }
}