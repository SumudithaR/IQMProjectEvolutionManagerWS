// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeUnitCalculator.cs" company="IQM Software">
//   Suuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the TimeUnitCalculator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Business.Utility
{
    using IQMProjectEvolutionManagerWS.Data;

    /// <summary>
    /// The time unit calculator.
    /// </summary>
    public static class TimeUnitCalculator
    {
        /// <summary>
        /// The get hours.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="unitType">
        /// The unit type.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float GetHours(float value, TimeUnitType unitType)
        {
            return (value * unitType.ConversionFactor) / 60;
        }
    }
}