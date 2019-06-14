using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Lso.Core;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    [TestFixture]
    public class WebshipAccountTests
    {
        [TestFixtureSetUp]
        public void InitTests()
        {            
            
        }

        [Test]
        public void Can_Create_New_Webship_Account()
        {
            TestDelegate del = () => { new WebshipAccount(); };
            Assert.DoesNotThrow(del);         
        }

        [Test]
        public void Cannot_Set_Administrator_Without_CustomerID()
        {
            var webshipaccount = new WebshipAccount();
            TestDelegate del = () => { webshipaccount.Administrator = true; };
            Assert.Throws(typeof(CustomerIDRequiredException), del);

        }

        [Test]
        public void Cannot_Set_Administrator_Unless_CustomerID_Is_Privileged()
        {
            var webshipaccount = new WebshipAccount();
            webshipaccount.CustID = 1000;
            TestDelegate del = () => { webshipaccount.Administrator = true; };
            Assert.Throws(typeof(PrivilegedCustomerIDRequiredException), del);
        }

        [Test]
        public void Administrator_Get_Set_Property_Works()
        {
            var webshipaccount = new WebshipAccount();
            // Only account 1 is privileged at this time
            webshipaccount.CustID = 1;
            webshipaccount.Administrator = true;
            Assert.AreEqual(true, webshipaccount.Administrator);
            webshipaccount.Administrator = false;
            Assert.AreEqual(false, webshipaccount.Administrator);
        }

        [Test]
        public void Setting_Class_Properties_Raises_Event()
        {
            var webshipaccount = new WebshipAccount();
            bool eventRaised = false;
            webshipaccount.PropertyChanged +=
                delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    eventRaised = true;
                    Assert.AreEqual("LoginName", e.PropertyName);
                };

            webshipaccount.LoginName = "Cooter";
            Assert.AreEqual(true, eventRaised);
           
        }

        [Test]
        public void Class_Change_Tracking_Interface_Works()
        {
            var webshipaccount = new WebshipAccount();
            webshipaccount.AcceptChanges();
            Assert.AreEqual(false, webshipaccount.IsChanged);
            webshipaccount.LoginName = "Food";
            Assert.AreEqual(true, webshipaccount.IsChanged);
        }
     
    }
}
