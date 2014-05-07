// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotifyHandler.cs" company="IQM SoftwareS">
//   Sumuditha Ranawka 2014.
// </copyright>
// <summary>
//   Defines the INotifyHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers
{
    using IQMProjectEvolutionManager.Core.Domain;

    /// <summary>
    /// The NotifyHandler interface.
    /// </summary>
    public interface INotifyHandler
    {
        /// <summary>
        /// The handle updates.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        void HandleUpdates(Release release);
    }
}