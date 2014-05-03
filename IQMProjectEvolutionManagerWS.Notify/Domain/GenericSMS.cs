using IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Notify.Domain
{
    public class GenericSMS : IGenericSMS
    {
        public virtual string GenericSMSId { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string AccountRef { get; set; }
        public virtual string Message { get; set; }
    }
}