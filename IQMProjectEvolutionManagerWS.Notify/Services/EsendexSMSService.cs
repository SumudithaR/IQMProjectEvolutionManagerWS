// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EsendexSMSService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The esendex sms service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using com.esendex.sdk.messaging;
    using com.esendex.sdk.sent;

    using IQMProjectEvolutionManagerWS.Notify.Domain;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;

    /// <summary>
    /// The esendex sms service.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class EsendexSmsService : ISmsService
    {
        /// <summary>
        /// The get sent messages.
        /// </summary>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Reviewed. Suppression is OK here.")]
        public IList<GenericSms> GetSentMessages(int pageSize)
        {
            var sentMessages = GetSentService().GetMessages(ConfigurationManager.AppSettings["EsendexAccountRef"], 1, pageSize);

            return (from message in sentMessages.Messages where message != null select new GenericSms() { AccountRef = message.AccountReference, Message = message.Body.BodyText, Mobile = message.Recipient.PhoneNumber, GenericSmsId = message.Id.ToString() }).ToList();
        }

        /// <summary>
        /// The get sent message.
        /// </summary>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="GenericSms"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Reviewed. Suppression is OK here.")]
        public GenericSms GetSentMessage(string messageId, int pageSize)
        {
            return this.GetSentMessages(pageSize).FirstOrDefault(message => message != null && message.GenericSmsId.Equals(messageId));
        }

        /// <summary>
        /// The send sms.
        /// </summary>
        /// <param name="sms">
        /// The sms.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string SendSms(GenericSms sms)
        {
            var messagingService = GetMessagingService();
            var response = messagingService.SendMessage(new SmsMessage(sms.Mobile, sms.Message, ConfigurationManager.AppSettings["EsendexAccountRef"]));

            var firstOrDefault = response.MessageIds.FirstOrDefault();

            return firstOrDefault != null ? firstOrDefault.Id.ToString() : null;
        }

        /// <summary>
        /// The get messaging service.
        /// </summary>
        /// <returns>
        /// The <see cref="MessagingService"/>.
        /// </returns>
        private static MessagingService GetMessagingService()
        {
            return new MessagingService(ConfigurationManager.AppSettings["EsendexUsername"], ConfigurationManager.AppSettings["EsendexPassword"]);
        }

        /// <summary>
        /// The get sent service.
        /// </summary>
        /// <returns>
        /// The <see cref="SentService"/>.
        /// </returns>
        private static SentService GetSentService()
        {
            return new SentService(ConfigurationManager.AppSettings["EsendexUsername"], ConfigurationManager.AppSettings["EsendexPassword"]);
        }
    }
}