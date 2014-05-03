using com.esendex.sdk.messaging;
using com.esendex.sdk.sent;
using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Services
{
    public class EsendexSMSService : ISMSService
    {
        private MessagingService GetMessagingService()
        {
            return new MessagingService(ConfigurationManager.AppSettings["EsendexUsername"], ConfigurationManager.AppSettings["EsendexPassword"]);
        }

        private SentService GetSentService()
        {
            return new SentService(ConfigurationManager.AppSettings["EsendexUsername"], ConfigurationManager.AppSettings["EsendexPassword"]);
        }

        public EsendexSMSService()
        {

        }

        public string SendSMS(GenericSMS sms)
        {
            var messagingService = GetMessagingService();
            var response = messagingService.SendMessage(new SmsMessage(sms.Mobile, sms.Message, ConfigurationManager.AppSettings["EsendexAccountRef"]));

            if (response != null)
            {
                return response.MessageIds.FirstOrDefault().Id.ToString();
            }

            return null;
        }

        public IList<GenericSMS> GetSentMessages(int pageSize)
        {
            var smsToReturn = new List<GenericSMS>();

            var sentMessages = GetSentService().GetMessages(ConfigurationManager.AppSettings["EsendexAccountRef"], 1, pageSize);

            foreach (var message in sentMessages.Messages)
            {
                if (message != null)
                {
                    smsToReturn.Add(new GenericSMS()
                        {
                            AccountRef = message.AccountReference,
                            Message = message.Body.BodyText,
                            Mobile = message.Recipient.PhoneNumber,
                            GenericSMSId = message.Id.ToString()
                        });
                }
            }

            return smsToReturn;
        }

        public GenericSMS GetSentMessage(string messageId, int pageSize)
        {
            foreach (var message in GetSentMessages(pageSize))
            {
                if (message != null && message.GenericSMSId.Equals(messageId))
                {
                    return message;
                }
            }

            return null;
        }
    }
}