using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification
{
    public interface ICalendarNotificationHandler
    {
        IList<SubscriberNotifier> GetReleaseCalendarNotifiers();
        void SetupReleaseCalendars(ref IList<SubscriberNotifier> releaseCalendarNotifiers);
        void UpdateCalendarsWithRelease(Release release);
    }
}