using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lso.Core;
using Ninject;
using NUnit.Framework;


namespace Lso.Core.Tests
{
    // TODO: I was originally going to isolate testing from the DB testing
    // but the repository code came out so short and concise, it didn't seem
    // worthwhile to do this testing at this time, so I skipped on to the integration 
    // testing which I consider far more important since MS dropped SQL server 2000 support
    class WebshipAccountRepositoryTests
    {
        //private IWebshipAccountRepository _repository;
        
        [TestFixtureSetUp]
        public void InitTests()
        {
            //var kernel = Buildkernel();
            //var repository = kernel.Get<IWebshipAccountRepository>();

            //var account1 = new WebshipAccount();
            //account1.UID = 101;
            //account1.LoginName = "testuser1";
            //account1.UserFirstName = "test";
            //account1.UserLastName = "user1";
            //account1.CustID = 110;

            //var account2 = new WebshipAccount();
            //account2.UID = 102;
            //account2.LoginName = "testuser2";
            //account2.UserFirstName = "test";
            //account2.UserLastName = "user2";
            //account2.CustID = 111;

            //_repository = new FakeWebshipAccountRepository(account1, account2);
        }

        public static IKernel Buildkernel()
        {
            return new StandardKernel();
        }

        [Test]
        public void Repository_Returns_Account1_When_Requested()
        {
            
            


        }
        
    }
}
