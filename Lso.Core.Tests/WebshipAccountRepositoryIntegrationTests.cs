using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    // Full integration test against test SQL DB
    class WebshipAccountRepositoryIntegrationTests
    {
        private IWebshipAccountRepository _repository;

        [TestFixtureSetUp]
        public void InitTests()
        {
            // TODO: set the connection string from a setting would be better I think
            var db = new WebDbDataContext("Data Source=10.0.5.14;Initial Catalog=LsoCoreTest;Persist Security Info=True;User ID=L5development;Password=L5SQLCool");
            db.spRestoreWebshipAccountRepositoryTestState();

            // the repository used in the majority of tests
            _repository = new WebShipAccountRepository();

        }

        
        [Test]
        public void GetAllWithUID_4_Returns_Single__wilsona_Record()
        {
            var accounts = _repository.GetAllWithUid(4);

            Assert.AreEqual(1, accounts.Count);

            var wilsona = accounts[0];
            Assert.AreEqual("wilsona", wilsona.LoginName);
            Assert.AreEqual("Wilson", wilsona.UserFirstName);
            Assert.AreEqual("Waldrop", wilsona.UserLastName);
            Assert.AreEqual("wwaldrop@lso.com", wilsona.UserEmail);
        }
        
        [Test]
        public void GetAllWithLoginName_wilsona_Returns_Single_wilsona_Record()
        {
            var accounts = _repository.GetAllWithLoginNameCustId("wilsona", 1);
            Assert.AreEqual(1, accounts.Count);

            var wilsona = accounts[0];
            Assert.AreEqual("wilsona", wilsona.LoginName);
            Assert.AreEqual("Wilson", wilsona.UserFirstName);
            Assert.AreEqual("Waldrop", wilsona.UserLastName);
            Assert.AreEqual("wwaldrop@lso.com", wilsona.UserEmail);
        }

        [Test]
        public void Can_Update_WebshipAccount_Objects_In_Repository()
        {
            var accounts = _repository.GetAllWithLoginNameCustId("wilson", 1);
            Assert.AreEqual(1, accounts.Count);

            var wilson = accounts[0];
            var newloginname = "wilson_test";
            wilson.LoginName = newloginname;
            _repository.Update(wilson);

            accounts = _repository.GetAllWithLoginNameCustId(newloginname, 1);
            Assert.AreEqual(1, accounts.Count);
            Assert.AreEqual(newloginname, wilson.LoginName);
        }

        [Test]
        public void Can_Add_WebshipAccount_Objects_To_Repository()
        {
            var newaccount = new WebshipAccount();
            newaccount.LoginName = "SillyTest";
            newaccount.UserFirstName = "Silly";
            newaccount.UserLastName = "Test";
            newaccount.UserEmail = "silly@example.com";
            newaccount.IsUserAdmin = false;
            newaccount.CustID = 1;

            _repository.Add(newaccount);

            var accountsearch = _repository.GetAllWithLoginNameCustId("SillyTest", 1);
            Assert.AreEqual(1, accountsearch.Count);
        }
    }
}
