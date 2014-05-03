using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IQM.Common.Test.Core;
using NUnit.Framework;
using IQMProjectEvolutionManagerWS.Business.Handlers;

namespace IQMProjectEvolutionManagerWS.Tests.Business.Processes
{
    [TestFixture]
    public class BaseProcessTestFixture : NHibernateTestFixtureBase
    {
        [Test]
        public void CanInitiateDataManagementProcess()
        {
            var baseProcess = new BaseHandler();
            baseProcess.Start();
        }

        public override void CreateInitialData()
        {
        }
    }
}
