// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WSNotifyServiceModule.cs" company="IQm Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The ws notify service module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Notify.DependencyResolution.Modules
{
    using System.Diagnostics.CodeAnalysis;

    using IQMProjectEvolutionManagerWS.Notify.Domain;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;
    using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
    using IQMProjectEvolutionManagerWS.Notify.Services;

    using Ninject.Modules;

    /// <summary>
    /// The ws notify service module.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class WsNotifyServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IGenericCalendar>().To<GenericCalendar>();
            Bind<IGenericCalendarEvent>().To<GenericCalendarEvent>();
            Bind<IGenericSms>().To<GenericSms>();

            Bind<ICalendarService>().To<GoogleCalendarService>();
            Bind<ISmsService>().To<EsendexSmsService>();
        }
    }
}
