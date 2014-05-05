// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupDataFixture.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014
// </copyright>
// <summary>
//   The setup data fixture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using IQM.Common.Test.Core;

    using IQMProjectEvolutionManager.Core;
    using IQMProjectEvolutionManager.Core.Domain;
    using IQMProjectEvolutionManager.Core.Enums;

    using NUnit.Framework;

    /// <summary>
    /// The setup data fixture.
    /// </summary>
    [TestFixture]
    public class SetupDataFixture : NHibernateTestFixtureBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupDataFixture"/> class.
        /// </summary>
        public SetupDataFixture()
            : base(new CoreServiceModule())
        {
        }

        /// <summary>
        /// Setups the databases.
        /// </summary>
        [Test]
        public void SetupDatabase()
        {
            Assert.That(true, "Setup Database Complete.");
        }

        /// <summary>
        /// Creates the initial data.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1500:CurlyBracketsForMultiLineStatementsMustNotShareLine",
            Justification = "Reviewed. Suppression is OK here.")][SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed. Suppression is OK here.")]
        public override void CreateInitialData()
        {
            // Stores all the objects to be saved to the database. 
            var objectsToSave = new List<object>();

            #region Subscriber Notifier Purposes
            SubscriberNotifierPurpose[] subscriberNotifierPurposes = {
                                                       new SubscriberNotifierPurpose()
                                                       {
                                                            Name = "Release"
                                                       }
                                                   };
            objectsToSave.AddRange(subscriberNotifierPurposes);
            #endregion

            #region Subscriber Notifier Types
            SubscriberNotifierType[] subscriberNotifierTypes = {
                                                                   new SubscriberNotifierType()
                                                                   {
                                                                        Name = "Calendar"
                                                                   },
                                                                   new SubscriberNotifierType()
                                                                   {
                                                                        Name = "SMS"
                                                                   }
                                                               };
            objectsToSave.AddRange(subscriberNotifierTypes);
            #endregion

            #region DropDownItem
            #region PagingDataItems
            var defaultPaging = new MyManagedListItem { ListItemType = MyManagedListItemType.Paging, Name = "default-paging", MetaData = "10", Visible = true };
            var ajaxPaging = new MyManagedListItem { ListItemType = MyManagedListItemType.Paging, Name = "ajax-paging", MetaData = "100", Visible = true };

            objectsToSave.Add(defaultPaging);
            objectsToSave.Add(ajaxPaging);
            #endregion
            #endregion

            #region Saving Of Test Data
            var session = GetSession();
            using (var transaction = session.BeginTransaction())
            {
                foreach (var obj in objectsToSave)
                {
                    session.Save(obj);
                }

                transaction.Commit();
            }
            #endregion
        }

        /// <summary>
        /// The create database.
        /// </summary>
        [Test]
        public void CreateDatabase()
        {
        }
    }
}
