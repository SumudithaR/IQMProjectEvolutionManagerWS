// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyHandler.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the NotifyHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Handlers
{
    using IQMProjectEvolutionManager.Core.Domain;

    using IQMProjectEvolutionManagerWS.Business.Handlers.Notification;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.DependencyResolution.Resolver;
    using IQMProjectEvolutionManagerWS.Business.Interfaces.Handlers;

    /// <summary>
    /// The notify handler.
    /// </summary>
    public class NotifyHandler : INotifyHandler
    {
        /// <summary>
        /// The dependency resolver.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyHandler"/> class.
        /// </summary>
        /// <param name="resolver">
        /// The resolver.
        /// </param>
        public NotifyHandler(IDependencyResolver resolver)
        {
            this.dependencyResolver = resolver;
        }

        /// <summary>
        /// The handle updates.
        /// </summary>
        /// <param name="release">
        /// The release.
        /// </param>
        public void HandleUpdates(Release release)
        {
            var calendarNotificationHandler = new CalendarNotificationHandler(this.dependencyResolver);
            calendarNotificationHandler.UpdateCalendarsWithRelease(release);

            var smsNotificationHandler = new SmsNotificationHandler(this.dependencyResolver);
            smsNotificationHandler.SendSmsForRelease(release);
        }
    }
}