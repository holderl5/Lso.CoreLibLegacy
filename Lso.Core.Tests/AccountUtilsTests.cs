using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class AccountUtilsTests
    {
        private AccountsCreationRequest _request1;
        private AccountsCreationRequest _request2;
        private AccountsCreationRequest _badRequest1;
        private AccountsCreationRequest _goodRequest1;
        private AccountsCreationRequest _search1;
        private AccountsCreationRequest _badSearch;
        private GroupCreationRequest _groupSearchUid;
        private GroupCreationRequest _groupSearchNoUid;
        private GroupCreationRequest _groupSearchUidBad;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _request1 = new AccountsCreationRequest()
                            {
                                CCName = "John Doe",
                                CCNumber = "4111111111111111",
                                CCExpMonth = 9,
                                CCExpYear = 2010,
                                CCType = "Mastercard",
                                Comments = "Testing comment",
                                CompanyBillAddress1 = "123 Any St",
                                CompanyBillAddress2 = "Suite 101",
                                CompanyBillCity = "Austin",
                                CompanyBillState = "TX",
                                CompanyBillZip = "78727",
                                CompanyName = "IWidgets Inc",
                                CompanyPhone = "512-555-5555",
                                CompanyPhyAddress1 = "5901 Physical Way",
                                CompanyPhyAddress2 = "Suite 11",
                                CompanyPhyCity = "Austin",
                                CompanyPhyState = "TX",
                                CompanyPhyZip = "78727",
                                HowHearAbout = 16,
                                LoginName = "Foobar",
                                RequesterFirstName = "John",
                                RequesterLastName = "Doe",
                                RequesterEmail = "a@example.com",
                                RequesterPhone = "(512) 555-5555",
                                UserPWD = "FOO",
                                CCOptOut = true
                            };

            _search1 = new AccountsCreationRequest()
            {
                CCName = "charlotte POweLL",
                CCNumber = "4111111111111111",
                CCExpMonth = 9,
                CCExpYear = 2010,
                CCType = "Mastercard",
                Comments = "Testing comment",
                CompanyBillAddress1 = "5408 -A Bell Street",
                CompanyBillAddress2 = "",
                CompanyBillCity = "amarillo",
                CompanyBillState = "TX",
                CompanyBillZip = "79109",
                CompanyName = "Great Nation INVEstment corp",
                CompanyPhone = "8063536767",
                CompanyPhyAddress1 = "5408 -A Bell Street",
                CompanyPhyAddress2 = "",
                CompanyPhyCity = "amarillo",
                CompanyPhyState = "TX",
                CompanyPhyZip = "79109",
                HowHearAbout = 16,
                LoginName = "Foobar",
                RequesterFirstName = "CHarlotte",
                RequesterLastName = "powell",                
                RequesterEmail = "a@example.com",
                RequesterPhone = "5124487791",
                UserPWD = "FOO"
            };

            _badSearch = new AccountsCreationRequest() {CCName = "YO MAMMA"};


            _request2 = new AccountsCreationRequest()
            {
                Comments = "Testing comment2",
                CompanyBillAddress1 = "124 Any St",
                CompanyBillAddress2 = "Suite 102",
                CompanyBillCity = "Austin",
                CompanyBillState = "TX",
                CompanyBillZip = "78727-5170",
                CompanyName = "IWidgets Inc",
                CompanyPhone = "512-555-5555",
                //CompanyPhone = "5125555555",
                CompanyPhyAddress1 = "5902 Physical Way",
                CompanyPhyAddress2 = "Suite 12",
                CompanyPhyCity = "Austin",
                CompanyPhyState = "TX",
                CompanyPhyZip = "78727-5170",
                HowHearAbout = 16,
                LoginName = "Foobar2",
                RequesterFirstName = "John2",
                RequesterLastName = "Doe2",
                RequesterEmail = "a2@example.com",
                RequesterPhone = "(512)-555-5555",
                //RequesterPhone = "5125555555",
                UserPWD = "FOO2",
                CCExpMonth = 8,
                CCName = "John A Holder",
                CCNumber = "41111111111111",
                CCExpYear = 2010,
                CCType = "Visa",
                VerificationCode = "556",
                BillingZip = "78727-5170",
                CCOptOut = true
                
            };

            _badRequest1 = new AccountsCreationRequest()
            {
                Comments = "Bad Testing comment2",
                CompanyBillAddress1 = "124 Bad St",
                CompanyBillAddress2 = "Suite BAD 102",
                CompanyBillCity = "AustinBad",
                CompanyBillState = "TX",
                CompanyBillZip = "78727",
                CompanyName = "IWidgets BAD Inc",
                CompanyPhone = "512-555-5555",
                //CompanyPhone = "5125555555",
                CompanyPhyAddress1 = "5902 BAD Physical Way",
                CompanyPhyAddress2 = "Suite BAD12",
                CompanyPhyCity = "AustinBAD",
                CompanyPhyState = "TX",
                CompanyPhyZip = "78727",
                HowHearAbout = 15,
                LoginName = "Foobar2Bad",
                RequesterFirstName = "John2Bad",
                RequesterLastName = "Doe2Bad",
                RequesterEmail = "a2Bad@example.com",
                RequesterPhone = "1-(512)-555-5555",
                UserPWD = "BAD It doesn;t accept a very long password to totally mess this up"

            };

            _goodRequest1 = new AccountsCreationRequest()
            {
                Comments = "GD Testing comment2",
                CompanyBillAddress1 = "124 GD Any St",
                CompanyBillAddress2 = "Suite GD102",
                CompanyBillCity = "AustinGD",
                CompanyBillState = "TX",
                CompanyBillZip = "78727",
                CompanyName = "IWidgets GDInc",
                CompanyPhone = "512-555-5555",
                //CompanyPhone = "5125555555",
                CompanyPhyAddress1 = "5902 GDPhysical Way",
                CompanyPhyAddress2 = "Suite GD12",
                CompanyPhyCity = "AustinGD",
                CompanyPhyState = "TX",
                CompanyPhyZip = "78727",
                HowHearAbout = 16,
                LoginName = "Foobar2GD",
                RequesterFirstName = "John2GD",
                RequesterLastName = "Doe2GD",
                RequesterEmail = "a2GD@example.com",
                RequesterPhone = "(512)-555-5555",
                //RequesterPhone = "5125555555",
                UserPWD = "GDFOO2"

            };

            _groupSearchUid = new GroupCreationRequest
            {
                UID = 1,
                GroupDescr = "Test",
                GroupName = "Test"
            }; 

            _groupSearchNoUid = new GroupCreationRequest
            {
                UID = -1,
                GroupDescr = "Test",
                GroupName = "Test"
            };
            _groupSearchUidBad = new GroupCreationRequest
            {
                UID = 1,
                GroupDescr = "Test Im Not There",
                GroupName = "Test Nor There"
            };
        }

        [Test]
        public void Can_Create_Accounts()
        {
            var response = Core.AccountUtils.CreateAccounts(_request1);
            var response2 = Core.AccountUtils.CreateAccounts(_request2);

            Assert.AreEqual(true, response.Success);
            Assert.AreNotEqual(0, response.CustomerId);
            Assert.AreEqual(_request1.LoginName, response.LoginName);

            Assert.AreEqual(response.CustomerId + 1, response2.CustomerId);
            Assert.AreEqual(true, response2.Success);
            Assert.AreNotEqual(0, response2.CustomerId);
            Assert.AreEqual(_request2.LoginName, response2.LoginName);

            var warepo = RepositoryFactory.GetNewWebshipAccountRepository();
            var webacct = warepo.GetAllWithLoginNameCustId(response.LoginName, response.CustomerId);
            var webacct2 = warepo.GetAllWithLoginNameCustId(response2.LoginName, response2.CustomerId);
         
            Assert.IsTrue(webacct.Any());
            Assert.AreEqual(_request1.UserPWD, webacct.First().UserPWD);
            Assert.AreEqual(webacct.First().UID + 1, webacct2.First().UID);

            var crepo = RepositoryFactory.GetNewCustomerRepository();
            var cust = crepo.GetAllWithCustId(response.CustomerId);
            var cust2 = crepo.GetAllWithCustId(response2.CustomerId);

            Assert.IsTrue(cust.Any());
            Assert.AreEqual(_request1.CCNumber, cust.First().CardNo);
            Assert.AreEqual(_request1.CCExpYear, cust.First().CardExpYear);
            Assert.AreEqual(_request1.CCExpMonth, cust.First().CardExpMonth);
            Assert.AreEqual(_request1.CCType, cust.First().CardType);
            Assert.AreEqual(_request1.CCOptOut, cust.First().CCOptOut);
            Assert.AreNotEqual(cust.First().CustContactName, cust2.First().CustContactName);
  
            Assert.AreEqual(0, cust.First().PriceSchedule);

        }

        [Test]
        public void Bad_Data_Test()
        {
            // It was possible in the original implementation to crash the create and get some half
            // created garbage back.  This test is intended to make sure that cannot happen again
            // Due to LINQ to SQL limitations, we don't currently have a good way around the bad data
            // other than returning a general failure.  This limitation should go away once we have 
            // more time to devote to this issue, for now, the website code just limits inputs to match
            // DB lengths, which is simple and just works


            AccountsCreationResponse responseBad = null;

            try
            {
                responseBad = Core.AccountUtils.CreateAccounts(_badRequest1);
                
            }
            catch (Exception)
            {
                
            }



            AccountsCreationResponse responseGood = null;

            try
            {
                responseGood = Core.AccountUtils.CreateAccounts(_goodRequest1);
            }
            catch (Exception)
            {
                
                
            }

            Assert.IsNotNull(responseBad);
            Assert.IsNotNull(responseGood);

            Assert.AreEqual(false, responseBad.Success);
            Assert.AreEqual(true, responseGood.Success);            
            
            
            

            var warepo = RepositoryFactory.GetNewWebshipAccountRepository();
            var webacct = warepo.GetAllWithLoginNameCustId(responseGood.LoginName, responseGood.CustomerId);

            Assert.IsTrue(webacct.Any());
            // TODO: should we check?

            

            var crepo = RepositoryFactory.GetNewCustomerRepository();
            var cust = crepo.GetAllWithCustId(responseGood.CustomerId);

            Assert.AreEqual(_goodRequest1.CompanyName, cust.First().CustName);
            Assert.AreEqual(_goodRequest1.RequesterFirstName + ' ' + _goodRequest1.RequesterLastName, cust.First().CustContactName);            
            
        }

        [Test]
        public void GetFullCustomerData_Function_Works()
        {
            // See discussion about technical debt in other files
            // TODO: For the moment, the discounts data is being pull directly from the live DB
            // TODO: This test is at best then only quasi permanent...
            var request = new FullCustomerDataRequest()
                              {
                                  CustID = 1
                              };
            var response = AccountUtils.GetFullCustomerData(request);
            
            Assert.IsTrue(response.Success);
            Assert.LessOrEqual(1, response.UserProfiles.Count);
            Assert.LessOrEqual(1, response.BaseDiscounts.Count);
            Assert.LessOrEqual(1, response.TieredRevenueDiscounts.Count);
            Assert.LessOrEqual(1, response.WeightDiscounts.Count);
            Assert.LessOrEqual(1, response.ZoneDiscounts.Count); 

        }

        [Test]
        public void TestAccountSearch()
        {
            // This search should return 2 results
            var result = Core.AccountUtils.SearchForDuplicateAccounts(_search1);
            Assert.AreEqual(2, result.Count);

            // Bad search can kill if you are not handling things correctly
            try
            {
                result = null;
                result = Core.AccountUtils.SearchForDuplicateAccounts(_badSearch);
            }
            catch (Exception)
            {
                
                
            }
            


            var result2 = Core.AccountUtils.PublicSearchForDuplicateAccounts(_search1);
            Assert.AreEqual(2, result2.Count);

            try
            {
                result2 = null;
                result2 = Core.AccountUtils.PublicSearchForDuplicateAccounts(_badSearch);                 
            }
            catch (Exception)
            {
                                
            }

            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);

            // TODO: For now we don't allow a search to proceed unless all variables are set (mimicing the page that uses this code)
            // TODO: While this test exactly doesn't reflect that, it is at least documented here in this file
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, result2.Count);
            

        }

        [Test]
        public void TestGroupsSearch()
        {
            // This search should return 2 results
            var result = Core.AccountUtils.SearchForDuplicateGroups(_groupSearchUid);
            Assert.AreEqual(1, result.Count);

            result = Core.AccountUtils.SearchForDuplicateGroups(_groupSearchNoUid);
            Assert.Less(1, result.Count); // Originaly 32.

            result = Core.AccountUtils.SearchForDuplicateGroups(_groupSearchUidBad);
            Assert.AreEqual(0, result.Count);
        }
    }
}
