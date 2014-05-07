// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericSMS.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the GenericSMS type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;

    /// <summary>
    /// The generic sms.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class GenericSms : IGenericSms
    {
        /// <summary>
        /// Gets or sets the generic sms id.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual string GenericSmsId { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the account ref.
        /// </summary>
        public virtual string AccountRef { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public virtual string Message { get; set; }
    }
}