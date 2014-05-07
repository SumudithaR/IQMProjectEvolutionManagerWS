// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISMSService.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the ISMSService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    using IQMProjectEvolutionManagerWS.Notify.Domain;

    /// <summary>
    /// The SmsService interface.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public interface ISmsService
    {
        /// <summary>
        /// The send sms.
        /// </summary>
        /// <param name="sms">
        /// The sms.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string SendSms(GenericSms sms);

        /// <summary>
        /// The get sent messages.
        /// </summary>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<GenericSms> GetSentMessages(int pageSize);

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
        GenericSms GetSentMessage(string messageId, int pageSize);
    }
}