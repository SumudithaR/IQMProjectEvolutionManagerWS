using IQM.Common.Test.Core;
using IQMProjectEvolutionManager.Core;
using IQMProjectEvolutionManager.Core.Domain;
using IQMProjectEvolutionManager.Core.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMProjectEvolutionManagerWS.Tests
{
    /// <summary>
    /// 
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

        [Test]
        public void CreateDatabase()
        {
        }
    }
}
