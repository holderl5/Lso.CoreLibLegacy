using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoCustomerRepositoryIntegrationTests
    {
        private ILsoCustomerRepository _repository;

        [TestFixtureSetUp]
        public void InitTests()
        {
            // TODO: set the connection string from a setting would be better I think
            var db = new OpsDbDataContext("Data Source=10.0.5.14;Initial Catalog=LsoCoreTestOps;Persist Security Info=True;User ID=L5development;Password=L5SQLCool");
            db.CommandTimeout = 90;
            db.spRestoreLsoCustomerRepositoryTestState();

            // the repository used in the majority of tests
            _repository = new LsoCustomerRepository();

        }


        [Test]
        public void GetAllWithCustId_6_Returns_Single_Wilson_Record()
        {
            var accounts = _repository.GetAllWithCustId(6);

            Assert.AreEqual(1, accounts.Count);

            var wilsona = accounts[0];
            Assert.AreEqual("LSO SUPPLIES/COOKIES-INTERNAL USE ONLY", wilsona.CustName);
            Assert.AreEqual("WILSON WALDROP", wilsona.CustContactName);
            Assert.AreEqual("8008008984", wilsona.CustContactPhone);
            Assert.AreEqual("OTHER", wilsona.SICCode);
        }

        [Test]
        public void GetAllWithCustName_Lonestar_Returns_Two_Records()
        {
            var accounts = _repository.GetAllWithCustName("lone star overnight");

            // Should return 2
            Assert.AreEqual(2, accounts.Count);

            var lonestar = accounts[0];
            Assert.AreEqual(1, lonestar.CustID);
            Assert.AreEqual("LONE STAR OVERNIGHT", lonestar.CustName);
            Assert.AreEqual("Gary Gunter Jr", lonestar.CustContactName);
            Assert.AreEqual("5128738067", lonestar.CustContactPhone);
            Assert.AreEqual("TRANS", lonestar.SICCode);

            lonestar = accounts[1];
            Assert.AreEqual(139795, lonestar.CustID);
            Assert.AreEqual("LONE STAR OVERNIGHT", lonestar.CustName);
            Assert.AreEqual("BILL BARTLEY", lonestar.CustContactName);
            Assert.AreEqual("5128738067", lonestar.CustContactPhone);
            Assert.AreEqual("OTHER", lonestar.SICCode);
        }

        [Test]
        public void Can_Update_Customer_Objects_In_Repository()
        {
            var accounts = _repository.GetAllWithCustId(1);
            Assert.AreEqual(1, accounts.Count);

            var wilson = accounts[0];
            var newcontactname = "Gary Gunter Jr";
            wilson.CustContactName = newcontactname;
            wilson.CCOptOut = true;
            _repository.Update(wilson);

            accounts = _repository.GetAllWithCustId(1);
            Assert.AreEqual(1, accounts.Count);
            Assert.AreEqual(newcontactname, accounts[0].CustContactName);
            Assert.AreEqual(true, accounts[0].CCOptOut);
        }

        [Test]
        public void Can_Add_Customer_Objects_To_Repository()
        {
            var newaccount = new LsoCustomer();
            
            newaccount.AcctStatus = "TST";
            newaccount.Balance = 107;
            newaccount.BillingRefRequired = false;
            newaccount.BillToAddress1 = "123 Any St.";
            newaccount.BillToAddress2 = "Suite 100";
            newaccount.BillToAttnName = "Clark Kent";
            newaccount.BillToCity = "Austin";
            newaccount.BillToID = 1001;
            newaccount.BillToState = "TX";
            newaccount.BillToZip = "78727";
            newaccount.CardExpMonth = 9;
            newaccount.CardExpYear = 2015;
            newaccount.CardNo = "4111111111111111";
            newaccount.CardType = "VISA";
            newaccount.Comments = "delicious";
            newaccount.CreditAvail = 10;
            newaccount.CustContactName = "Clark Kent";
            newaccount.CustContactPhone = "5555555555";
            newaccount.CustName = "Clarky";
            newaccount.DateOpened = new DateTime(1971, 10, 10);
            newaccount.DOWBillingCode = "ABCD";
            newaccount.EDICustomer = false;
            newaccount.PayMethodID = 'A';
            newaccount.PhyAddress1 = "123 Other St.";
            newaccount.PhyAddress2 = "Suite 10000-A";
            newaccount.newCustomerReferralSourceID = new Guid();
            newaccount.TieredDiscountWindow = 4;            
            newaccount.WaiveSecuredFacilityFee = true;
            newaccount.WaivePickupEventFee = true;
            

            newaccount.CCOptOut = true;

            _repository.Add(newaccount);

            // Test Data on server should return 25001 for first new customer after restore state
            var accountsearch = _repository.GetAllWithCustId(25001);
            Assert.AreEqual(1, accountsearch.Count);

            var dbsearch = accountsearch[0];            
            Assert.AreEqual(25001, dbsearch.CustID);
            Assert.AreEqual(newaccount.CustID, dbsearch.CustID);
            Assert.AreEqual(newaccount.AcctStatus, dbsearch.AcctStatus);
            Assert.AreEqual(newaccount.Balance, dbsearch.Balance);
            Assert.AreEqual(newaccount.BillingRefRequired, dbsearch.BillingRefRequired);
            Assert.AreEqual(newaccount.BillToAddress1, dbsearch.BillToAddress1);
            Assert.AreEqual(newaccount.BillToAddress2, dbsearch.BillToAddress2);
            Assert.AreEqual(newaccount.BillToAttnName, dbsearch.BillToAttnName);
            Assert.AreEqual(newaccount.BillToCity, dbsearch.BillToCity);
            Assert.AreEqual(newaccount.BillToID, dbsearch.BillToID);
            Assert.AreEqual(newaccount.BillToState, dbsearch.BillToState);
            Assert.AreEqual(newaccount.BillToZip, dbsearch.BillToZip);
            Assert.AreEqual(newaccount.CardExpMonth, dbsearch.CardExpMonth);
            Assert.AreEqual(newaccount.CardExpYear, dbsearch.CardExpYear);
            Assert.AreEqual(newaccount.CardNo, dbsearch.CardNo);
            Assert.AreEqual(newaccount.CardType, dbsearch.CardType);
            Assert.AreEqual(newaccount.Comments, dbsearch.Comments);
            Assert.AreEqual(newaccount.CreditAvail, dbsearch.CreditAvail);
            Assert.AreEqual(newaccount.CustContactName, dbsearch.CustContactName);
            Assert.AreEqual(newaccount.CustContactPhone, dbsearch.CustContactPhone);
            Assert.AreEqual(newaccount.CustName, dbsearch.CustName);
            Assert.AreEqual(newaccount.DateOpened, dbsearch.DateOpened);
            Assert.AreEqual(newaccount.DOWBillingCode, dbsearch.DOWBillingCode);
            Assert.AreEqual(newaccount.EDICustomer, dbsearch.EDICustomer);
            Assert.AreEqual(newaccount.PayMethodID, dbsearch.PayMethodID);
            Assert.AreEqual(newaccount.PhyAddress1, dbsearch.PhyAddress1);
            Assert.AreEqual(newaccount.PhyAddress2, dbsearch.PhyAddress2);
            Assert.AreEqual(newaccount.newCustomerReferralSourceID, dbsearch.newCustomerReferralSourceID);
            Assert.AreEqual(newaccount.TieredDiscountWindow, dbsearch.TieredDiscountWindow);            
            Assert.AreEqual(newaccount.WaiveSecuredFacilityFee, dbsearch.WaiveSecuredFacilityFee);
            Assert.AreEqual(newaccount.WaivePickupEventFee, dbsearch.WaivePickupEventFee);
            Assert.AreEqual(newaccount.CCOptOut, dbsearch.CCOptOut);
        }
    }
}
