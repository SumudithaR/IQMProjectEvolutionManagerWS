using IQMProjectEvolutionManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification
{
    public interface ISMSNotificationHandler
    {
        IList<SubscriberNotifier> GetReleaseSMSNotifiers();
        void SendSMSForRelease(Release release);
    }
}