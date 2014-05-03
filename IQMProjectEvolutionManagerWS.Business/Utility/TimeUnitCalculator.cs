using IQMProjectEvolutionManagerWS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Business.Utility
{
    public static class TimeUnitCalculator
    {
        public static float GetHours(float value, TimeUnitType unitType)
        {
            return (value * unitType.ConversionFactor) / 60;
        }
    }
}