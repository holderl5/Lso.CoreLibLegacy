using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoCustomerTests
    {
        [Test]
        public void Setting_Class_Properties_Raises_Event()
        {
            var customer = new LsoCustomer();
            bool eventRaised = false;
            customer.PropertyChanged +=
                delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    eventRaised = true;
                    Assert.AreEqual("CustContactName", e.PropertyName);
                };

            customer.CustContactName = "Cooter";
            Assert.IsTrue(eventRaised);

        }

        [Test]
        public void Class_Change_Tracking_Interface_Works()
        {
            var customer = new LsoCustomer();
            customer.AcceptChanges();
            Assert.IsFalse(customer.IsChanged);
            customer.CustContactPhone = "5555555555";
            Assert.IsTrue(customer.IsChanged);
        }

        [Test]
        public void Class_Properties_Change_Tracking_List_Works()
        {
            var customer = new LsoCustomer();
            customer.AcceptChanges();

            Assert.AreEqual(0, customer.ChangedPropertiesCount);

            customer.CustContactPhone = "5555555555";
            customer.CustID = 501;
            customer.DateOpened = DateTime.Now;
            customer.Balance = 0;
            customer.BillingRefRequired = false;
            customer.CardExpMonth = 0;
            customer.CreditAvail = 0;
            customer.PriceSchedule = 0;

            Assert.AreEqual(8, customer.ChangedPropertiesCount);
        }
    }
}
