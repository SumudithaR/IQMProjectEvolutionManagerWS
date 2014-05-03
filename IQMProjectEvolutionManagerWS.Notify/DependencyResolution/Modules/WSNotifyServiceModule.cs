using IQMProjectEvolutionManagerWS.Notify.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;
using IQMProjectEvolutionManagerWS.Notify.Interfaces.Services;
using IQMProjectEvolutionManagerWS.Notify.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.DependencyResolution.Modules
{
    public class WSNotifyServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IGenericCalendar>().To<GenericCalendar>();
            Bind<IGenericCalendarEvent>().To<GenericCalendarEvent>();
            Bind<IGenericSMS>().To<GenericSMS>();

            Bind<ICalendarService>().To<GoogleCalendarService>();
            Bind<ISMSService>().To<EsendexSMSService>();
        }
    }
}
