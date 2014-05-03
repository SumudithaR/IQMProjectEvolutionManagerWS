using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQMProjectEvolutionManagerWS.Notify.Interfaces.Domain
{
    public interface IGenericSMS
    {
        string GenericSMSId { get; set; }
        string Mobile { get; set; }
        string AccountRef { get; set; }
        string Message { get; set; }
    }
}