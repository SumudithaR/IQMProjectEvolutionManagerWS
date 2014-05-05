// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseProcessTestFixture.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The base process test fixture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Tests.Business.Processes
{
    using IQM.Common.Test.Core;

    using IQMProjectEvolutionManagerWS.Business.Handlers;

    using NUnit.Framework;

    /// <summary>
    /// The base process test fixture.
    /// </summary>
    [TestFixture]
    public class BaseProcessTestFixture : NHibernateTestFixtureBase
    {
        /// <summary>
        /// The can initiate data management process.
        /// </summary>
        [Test]
        public void CanInitiateDataManagementProcess()
        {
            var baseProcess = new BaseHandler();
            baseProcess.Start();
        }

        /// <summary>
        /// The create initial data.
        /// </summary>
        public override void CreateInitialData()
        {
        }
    }
}
