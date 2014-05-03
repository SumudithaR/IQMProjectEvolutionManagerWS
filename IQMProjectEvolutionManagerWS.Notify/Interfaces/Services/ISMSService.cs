using IQMProjectEvolutionManagerWS.Notify.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Services
{
    public interface ISMSService
    {
        string SendSMS(GenericSMS sms);
        IList<GenericSMS> GetSentMessages(int pageSize);
        GenericSMS GetSentMessage(string messageId, int pageSize);
    }
}