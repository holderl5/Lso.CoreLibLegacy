using System;
using AutoMapper;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoCustomerFactoryTests
    {
        private Customer _testcustomer;
        private LsoCustomer _testlsocustomer;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _testcustomer = new Customer();

            _testcustomer.CustID = 1;
			_testcustomer.AcctStatus = "Testing";
            _testcustomer.Balance = 107;			
            _testcustomer.BillingRefRequired = false;
            _testcustomer.BillToAddress1 = "123 Any St.";
            _testcustomer.BillToAddress2 = "Suite 100";
            _testcustomer.BillToAttnName = "Clark Kent";            
            _testcustomer.BillToCity = "Austin";
            _testcustomer.BillToID = 1001;
            _testcustomer.BillToState = "TX";
            _testcustomer.BillToZip = "78727";
            _testcustomer.CardExpMonth = 9;
			_testcustomer.CardExpYear = 2015;
			_testcustomer.CardNo = "4111111111111111";
            _testcustomer.CardType = "VISA";
            _testcustomer.Comments = "delicious";
            _testcustomer.CreditAvail = 10;
            _testcustomer.CustContactName = "Clark Kent";
			_testcustomer.CustContactPhone = "5555555555";
			_testcustomer.CustName = "Clarky";
			_testcustomer.DateOpened = new DateTime(1971, 10, 10);
			_testcustomer.DOWBillingCode = "idunno";
			_testcustomer.EDICustomer = false;
			_testcustomer.PayMethodID = 'A';
			_testcustomer.PhyAddress1 = "123 Other St.";
			_testcustomer.PhyAddress2 = "Suite 10000-A";

            _testlsocustomer = new LsoCustomer();

            _testlsocustomer.CustID = 1;
            _testlsocustomer.AcctStatus = "Testing";
            _testlsocustomer.Balance = 107;
            _testlsocustomer.BillingRefRequired = false;
            _testlsocustomer.BillToAddress1 = "123 Any St.";
            _testlsocustomer.BillToAddress2 = "Suite 100";
            _testlsocustomer.BillToAttnName = "Clark Kent";
            _testlsocustomer.BillToCity = "Austin";
            _testlsocustomer.BillToID = 1001;
            _testlsocustomer.BillToState = "TX";
            _testlsocustomer.BillToZip = "78727";
            _testlsocustomer.CardExpMonth = 9;
            _testlsocustomer.CardExpYear = 2015;
            _testlsocustomer.CardNo = "4111111111111111";
            _testlsocustomer.CardType = "VISA";
            _testlsocustomer.Comments = "delicious";
            _testlsocustomer.CreditAvail = 10;
            _testlsocustomer.CustContactName = "Clark Kent";
            _testlsocustomer.CustContactPhone = "5555555555";
            _testlsocustomer.CustName = "Clarky";
            _testlsocustomer.DateOpened = new DateTime(1971, 10, 10);
            _testlsocustomer.DOWBillingCode = "idunno";
            _testlsocustomer.EDICustomer = false;
            _testlsocustomer.PayMethodID = 'A';
            _testlsocustomer.PhyAddress1 = "123 Other St.";
            _testlsocustomer.PhyAddress2 = "Suite 10000-A";	

            

        }

        [Test]
        public void Check_LsoCustomer_Has_All_Data_Class_Fields()
        {
            // TODO: We also need to check the NewCustomerOptions mappings as well
            // but it is unclear if the version of automap will support this.  
            // it is not needed *right* now, so putting it off as a TODO
            var data = new Customer();

            Mapper.CreateMap<LsoCustomer, Customer>()
                .ForMember(dest => dest.NewCustomerOptions, opt => opt.Ignore());
            
            Mapper.Map<LsoCustomer, Customer>(_testlsocustomer, data);
            Mapper.AssertConfigurationIsValid();
                      
        }

        [Test]
        public void Can_AutoMap_From_Data_Class_To_LsoCustomer()
        {
            var factory = new LsoCustomerFactory();

            var lsocustomer = factory.Create(_testcustomer);			

			Assert.AreEqual(_testcustomer.CustID, lsocustomer.CustID);
			Assert.AreEqual(_testcustomer.AcctStatus, lsocustomer.AcctStatus);
            Assert.AreEqual(_testcustomer.Balance, lsocustomer.Balance);
            Assert.AreEqual(_testcustomer.BillingRefRequired, lsocustomer.BillingRefRequired);
            Assert.AreEqual(_testcustomer.BillToAddress1, lsocustomer.BillToAddress1);
            Assert.AreEqual(_testcustomer.BillToAddress2, lsocustomer.BillToAddress2);
            Assert.AreEqual(_testcustomer.BillToAttnName, lsocustomer.BillToAttnName);
            Assert.AreEqual(_testcustomer.BillToCity, lsocustomer.BillToCity);
            Assert.AreEqual(_testcustomer.BillToID, lsocustomer.BillToID);
            Assert.AreEqual(_testcustomer.BillToState, lsocustomer.BillToState);
            Assert.AreEqual(_testcustomer.BillToZip, lsocustomer.BillToZip);
            Assert.AreEqual(_testcustomer.CardExpMonth, lsocustomer.CardExpMonth);
			Assert.AreEqual(_testcustomer.CardExpYear, lsocustomer.CardExpYear);
			Assert.AreEqual(_testcustomer.CardNo, lsocustomer.CardNo);
            Assert.AreEqual(_testcustomer.CardType, lsocustomer.CardType);
            Assert.AreEqual(_testcustomer.Comments, lsocustomer.Comments);
            Assert.AreEqual(_testcustomer.CreditAvail, lsocustomer.CreditAvail);
            Assert.AreEqual(_testcustomer.CustContactName, lsocustomer.CustContactName);
			Assert.AreEqual(_testcustomer.CustContactPhone, lsocustomer.CustContactPhone);
			Assert.AreEqual(_testcustomer.CustName, lsocustomer.CustName);
			Assert.AreEqual(_testcustomer.DateOpened, lsocustomer.DateOpened);
			Assert.AreEqual(_testcustomer.DOWBillingCode, lsocustomer.DOWBillingCode);
			Assert.AreEqual(_testcustomer.EDICustomer, lsocustomer.EDICustomer);
			Assert.AreEqual(_testcustomer.PayMethodID, lsocustomer.PayMethodID);
			Assert.AreEqual(_testcustomer.PhyAddress1, lsocustomer.PhyAddress1);
			Assert.AreEqual(_testcustomer.PhyAddress2, lsocustomer.PhyAddress2);

            


        }

        [Test]
        public void Can_Update_Data_Class_From_lsocustomer()
        {
            var factory = new LsoCustomerFactory();

            var lsocustomer = factory.Create(_testcustomer);

            lsocustomer.CustID = 100;
			lsocustomer.AcctStatus = "Testingaa";
            lsocustomer.Balance = 100;			
            lsocustomer.BillingRefRequired = true;
            lsocustomer.BillToAddress1 = "1203 Any St.";
            lsocustomer.BillToAddress2 = "Suite 10";
            lsocustomer.BillToAttnName = "Clarky Kenty";            
            lsocustomer.BillToCity = "Dallas";
            lsocustomer.BillToID = 1010;
            lsocustomer.BillToState = "TX";
            lsocustomer.BillToZip = "78704";
            lsocustomer.CardExpMonth = 10;
			lsocustomer.CardExpYear = 2013;
			lsocustomer.CardNo = "4111111111111112";
            lsocustomer.CardType = "MASTERCARD";
            lsocustomer.Comments = "MAZUI";
            lsocustomer.CreditAvail = 11;
            lsocustomer.CustContactName = "Clarky Kentelly";
			lsocustomer.CustContactPhone = "5555555552";
            // skipping the following field to test that nothing happens
			//lsocustomer.CustName = "Clarkums";
			lsocustomer.DateOpened = new DateTime(1979, 9, 9);
			lsocustomer.DOWBillingCode = "something";
			lsocustomer.EDICustomer = true;
			lsocustomer.PayMethodID = 'B';
			lsocustomer.PhyAddress1 = "1203 Other St.";
			lsocustomer.PhyAddress2 = "Suite 100-A";			

            LsoCustomerFactory.Update(lsocustomer);

            Assert.AreEqual(lsocustomer.CustID, _testcustomer.CustID);
			Assert.AreEqual(lsocustomer.AcctStatus, _testcustomer.AcctStatus);
            Assert.AreEqual(lsocustomer.Balance, _testcustomer.Balance);
            Assert.AreEqual(lsocustomer.BillingRefRequired, _testcustomer.BillingRefRequired);
            Assert.AreEqual(lsocustomer.BillToAddress1, _testcustomer.BillToAddress1);
            Assert.AreEqual(lsocustomer.BillToAddress2, _testcustomer.BillToAddress2);
            Assert.AreEqual(lsocustomer.BillToAttnName, _testcustomer.BillToAttnName);
            Assert.AreEqual(lsocustomer.BillToCity, _testcustomer.BillToCity);
            Assert.AreEqual(lsocustomer.BillToID, _testcustomer.BillToID);
            Assert.AreEqual(lsocustomer.BillToState, _testcustomer.BillToState);
            Assert.AreEqual(lsocustomer.BillToZip, _testcustomer.BillToZip);
            Assert.AreEqual(lsocustomer.CardExpMonth, _testcustomer.CardExpMonth);
			Assert.AreEqual(lsocustomer.CardExpYear, _testcustomer.CardExpYear);
			Assert.AreEqual(lsocustomer.CardNo, _testcustomer.CardNo);
            Assert.AreEqual(lsocustomer.CardType, _testcustomer.CardType);
            Assert.AreEqual(lsocustomer.Comments, _testcustomer.Comments);
            Assert.AreEqual(lsocustomer.CreditAvail, _testcustomer.CreditAvail);
            Assert.AreEqual(lsocustomer.CustContactName, _testcustomer.CustContactName);
			Assert.AreEqual(lsocustomer.CustContactPhone, _testcustomer.CustContactPhone);
			Assert.AreEqual(lsocustomer.CustName, _testcustomer.CustName);
			Assert.AreEqual(lsocustomer.DateOpened, _testcustomer.DateOpened);
			Assert.AreEqual(lsocustomer.DOWBillingCode, _testcustomer.DOWBillingCode);
			Assert.AreEqual(lsocustomer.EDICustomer, _testcustomer.EDICustomer);
			Assert.AreEqual(lsocustomer.PayMethodID, _testcustomer.PayMethodID);
			Assert.AreEqual(lsocustomer.PhyAddress1, _testcustomer.PhyAddress1);
			Assert.AreEqual(lsocustomer.PhyAddress2, _testcustomer.PhyAddress2);
        }

        [Test]
        public void Can_Create_Customer_From_AccountsCreationRequest()
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
                                  CCOptOut = true

                              };

            var factory = new LsoCustomerFactory();
            var lsocustomer = factory.Create(request);

            Assert.AreEqual(request.Comments, lsocustomer.Comments);
            Assert.AreEqual(request.CompanyBillAddress1, lsocustomer.BillToAddress1);
            Assert.AreEqual(request.CompanyBillAddress2, lsocustomer.BillToAddress2);
            Assert.AreEqual(request.CompanyBillCity, lsocustomer.BillToCity);
            Assert.AreEqual(request.CompanyBillState, lsocustomer.BillToState);
            Assert.AreEqual(request.CompanyBillZip, lsocustomer.BillToZip);
            Assert.AreEqual(request.CompanyName, lsocustomer.CustName);
            // Note there is no company phone in the DB at this time
            Assert.AreEqual(request.CompanyPhone, lsocustomer.CustContactPhone);
            Assert.AreEqual(request.CompanyPhyAddress1, lsocustomer.PhyAddress1);
            Assert.AreEqual(request.CompanyPhyAddress2, lsocustomer.PhyAddress2);
            Assert.AreEqual(request.CompanyPhyCity, lsocustomer.PhyCity);
            Assert.AreEqual(request.CompanyPhyState, lsocustomer.PhyState);
            Assert.AreEqual(request.CompanyPhyZip, lsocustomer.PhyZip);            
            Assert.AreEqual(request.HowHearAbout, lsocustomer.HowHearAbout);
            Assert.AreEqual(request.RequesterFirstName + " " + request.RequesterLastName, lsocustomer.CustContactName);
            Assert.AreEqual(request.RequesterPhone, lsocustomer.CustContactPhone);
            Assert.AreEqual(request.CCOptOut, lsocustomer.CCOptOut);
        }
    }
}
