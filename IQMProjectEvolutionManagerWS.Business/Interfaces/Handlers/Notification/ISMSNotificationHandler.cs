// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISMSNotificationHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the ISMSNotificationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers.Notification
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManager.Core.Domain;

    /// <summary>
    /// The SmsNotificationHandler interface.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public interface ISmsNotificationHandler
    {
        /// <summary>
        /// The get release sms notifiers.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        IList<SubscriberNotifier> GetReleaseSmsNotifiers();

        /// <summary>
        /// The send sms for release.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        void SendSmsForRelease(Release release);
    }
}