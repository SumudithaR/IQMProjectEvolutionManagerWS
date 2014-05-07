// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericSMS.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the IGenericSMS type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The GenericSms interface.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public interface IGenericSms
    {
        /// <summary>
        /// Gets or sets the generic sms id.
        /// </summary>
        string GenericSmsId { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the account ref.
        /// </summary>
        string AccountRef { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        string Message { get; set; }
    }
}