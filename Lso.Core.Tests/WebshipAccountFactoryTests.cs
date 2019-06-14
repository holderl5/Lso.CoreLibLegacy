using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class WebshipAccountFactoryTests
    {
        private UserProfile _testprofile;
        private WebshipAccount _testwebshipaccount;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _testprofile = new UserProfile();

            _testprofile.CustID = 1;
            _testprofile.ABEntryToday = 10;
            _testprofile.AccountLockout = false;
            _testprofile.Active = true;
            _testprofile.Administrator = true;
            _testprofile.BillingRefRequired = false;
            _testprofile.CompanyAddress1 = "123 Any St.";
            _testprofile.CompanyAddress2 = "Suite 100";
            _testprofile.CompanyName = "LSO";
            _testprofile.CompanyCity = "Austin";
            _testprofile.LoginName = "testlogin";
            _testprofile.UserFirstName = "Blah";
            _testprofile.UserLastName = "Brand";
            _testprofile.UserEmail = "bloppo@blopblop.com";
            _testprofile.LastLoginDate = new DateTime(2010, 10, 11);
            _testprofile.IsUserAdmin = true;

            _testwebshipaccount = new WebshipAccount();

            _testwebshipaccount.CustID = 1;
            _testwebshipaccount.ABEntryToday = 10;
            _testwebshipaccount.AccountLockout = false;
            _testwebshipaccount.Active = true;
            _testwebshipaccount.Administrator = true;
            _testwebshipaccount.BillingRefRequired = false;
            _testwebshipaccount.CompanyAddress1 = "123 Any St.";
            _testwebshipaccount.CompanyAddress2 = "Suite 100";
            _testwebshipaccount.CompanyName = "LSO";
            _testwebshipaccount.CompanyCity = "Austin";
            _testwebshipaccount.LoginName = "testlogin";
            _testwebshipaccount.UserFirstName = "Blah";
            _testwebshipaccount.UserLastName = "Brand";
            _testwebshipaccount.UserEmail = "bloppo@blopblop.com";
            _testwebshipaccount.LastLoginDate = new DateTime(2010, 10, 11);
            _testwebshipaccount.IsUserAdmin = true;

        }

        [Test]
        public void Can_Create_WebshipAccount_From_AccountsCreationRequest()
        {
            var request = new AccountsCreationRequest()
            {
                Comments = "Testing comment",
                CompanyBillAddress1 = "123 Any St",
                CompanyBillAddress2 = "Suite 101",
                CompanyBillCity = "Austin",
                CompanyBillState = "TX",
                CompanyBillZip = "78727",
                CompanyName = "IWidgets Inc",
                CompanyPhone = "5125555555",
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
                RequesterPhone = "5125555555",
                UserPWD = "ParticularWord123"

            };

            var factory = new WebshipAccountFactory();
            var webshipAccount = factory.Create(request);

            Assert.AreEqual(request.CompanyName, webshipAccount.CompanyName);
            Assert.AreEqual(request.CompanyPhyAddress1, webshipAccount.CompanyAddress1);
            Assert.AreEqual(request.CompanyPhyAddress2, webshipAccount.CompanyAddress2);
            Assert.AreEqual(request.CompanyPhyCity, webshipAccount.CompanyCity);
            Assert.AreEqual(request.CompanyPhyState, webshipAccount.CompanyState);
            Assert.AreEqual(request.CompanyPhyZip, webshipAccount.CompanyZip);
            Assert.AreEqual(request.RequesterFirstName, webshipAccount.UserFirstName);
            Assert.AreEqual(request.RequesterLastName, webshipAccount.UserLastName);
            Assert.AreEqual(request.RequesterEmail, webshipAccount.UserEmail);
            Assert.AreEqual(request.LoginName, webshipAccount.LoginName);
            Assert.AreEqual(request.UserPWD, webshipAccount.UserPWD);
        }

        
        //public void Check_IWebshipAccount_Has_All_Data_Class_Fields()
        

        [Test]
        public void Check_WebshipAccount_Has_All_Data_Class_Fields()
        {
            Mapper.CreateMap<WebshipAccount, UserProfile>();

            var testval = new UserProfile();

            Mapper.Map<WebshipAccount, UserProfile>(_testwebshipaccount, testval);
            Mapper.AssertConfigurationIsValid();            
        }

        [Test]
        public void Can_AutoMap_From_Data_Class_To_WebshipAccount()
        {
            var factory = new WebshipAccountFactory();

            var webshipaccount = factory.Create(_testprofile);

            Assert.AreEqual(_testprofile.CustID, webshipaccount.CustID);
            Assert.AreEqual(_testprofile.ABEntryToday, webshipaccount.ABEntryToday);
            Assert.AreEqual(_testprofile.AccountLockout, webshipaccount.AccountLockout);
            Assert.AreEqual(_testprofile.Active, webshipaccount.Active);
            Assert.AreEqual(_testprofile.Administrator, webshipaccount.Administrator);
            Assert.AreEqual(_testprofile.BillingRefRequired, webshipaccount.BillingRefRequired);
            Assert.AreEqual(_testprofile.CompanyAddress1, webshipaccount.CompanyAddress1);
            Assert.AreEqual(_testprofile.CompanyAddress2, webshipaccount.CompanyAddress2);
            Assert.AreEqual(_testprofile.CompanyName, webshipaccount.CompanyName);
            Assert.AreEqual(_testprofile.CompanyCity, webshipaccount.CompanyCity);           
            Assert.AreEqual(_testprofile.LoginName, webshipaccount.LoginName);
            Assert.AreEqual(_testprofile.UserFirstName, webshipaccount.UserFirstName);
            Assert.AreEqual(_testprofile.UserLastName, webshipaccount.UserLastName);
            Assert.AreEqual(_testprofile.UserEmail, webshipaccount.UserEmail);
            Assert.AreEqual(_testprofile.LastLoginDate, webshipaccount.LastLoginDate);
            Assert.AreEqual(_testprofile.IsUserAdmin, webshipaccount.IsUserAdmin);
            

        }

        [Test]
        public void Can_Update_Data_Class_From_WebshipAccount()
        {
            var factory = new WebshipAccountFactory();

            

            var webshipaccount = factory.Create(_testprofile);

            webshipaccount.CustID = 1000;
            webshipaccount.ABEntryToday = 11;
            webshipaccount.AccountLockout = true;
            webshipaccount.Active = false;
            webshipaccount.Administrator = false;
            webshipaccount.BillingRefRequired = true;
            webshipaccount.CompanyAddress1 = "123 Other St.";
            webshipaccount.CompanyAddress2 = "Suite 1001";
            webshipaccount.CompanyName = "LSO2";
            webshipaccount.CompanyCity = "Dallas";
            webshipaccount.LoginName = "newtestlogin";
            webshipaccount.UserFirstName = "ABlah";
            webshipaccount.UserLastName = "ABrand";
            webshipaccount.UserEmail = "Abloppo@blopblop.com";
            webshipaccount.LastLoginDate = new DateTime(2010, 10, 12);
            webshipaccount.IsUserAdmin = false;

            WebshipAccountFactory.Update(webshipaccount);

            Assert.AreEqual(webshipaccount.CustID, _testprofile.CustID);
            Assert.AreEqual(webshipaccount.ABEntryToday, _testprofile.ABEntryToday);
            Assert.AreEqual(webshipaccount.AccountLockout, _testprofile.AccountLockout);
            Assert.AreEqual(webshipaccount.Active, _testprofile.Active);
            Assert.AreEqual(webshipaccount.Administrator, _testprofile.Administrator);
            Assert.AreEqual(webshipaccount.BillingRefRequired, _testprofile.BillingRefRequired);
            Assert.AreEqual(webshipaccount.CompanyAddress1, _testprofile.CompanyAddress1);
            Assert.AreEqual(webshipaccount.CompanyAddress2, _testprofile.CompanyAddress2);
            Assert.AreEqual(webshipaccount.CompanyName, _testprofile.CompanyName);
            Assert.AreEqual(webshipaccount.CompanyCity, _testprofile.CompanyCity);            
            Assert.AreEqual(webshipaccount.LoginName, _testprofile.LoginName);
            Assert.AreEqual(webshipaccount.UserFirstName, _testprofile.UserFirstName);
            Assert.AreEqual(webshipaccount.UserLastName, _testprofile.UserLastName);
            Assert.AreEqual(webshipaccount.UserEmail, _testprofile.UserEmail);
            Assert.AreEqual(webshipaccount.LastLoginDate, _testprofile.LastLoginDate);
            Assert.AreEqual(webshipaccount.IsUserAdmin, _testprofile.IsUserAdmin);            
        }
    }
}
