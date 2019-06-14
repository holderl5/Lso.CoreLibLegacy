using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoIGroupTests
    {
        [Test]
        public void SettingClassPropertiesRaisesEvent()
        {
            var group = new LsoIGroup();
            bool eventRaised = false;
            group.PropertyChanged +=
                delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    eventRaised = true;
                    Assert.AreEqual("GroupName", e.PropertyName);
                };

            group.GroupName = "Bob";
            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void ClassChangeTrackingInterfaceWorks()
        {
            var group = new LsoIGroup();
            group.AcceptChanges();
            Assert.IsFalse(group.IsChanged);
            group.GroupDescr = "bozo";
            Assert.IsTrue(group.IsChanged);
        }

        [Test]
        public void ClassPropertiesChangeTrackingListWorks()
        {
            var group = new LsoIGroup();
            group.AcceptChanges();

            Assert.AreEqual(0, group.ChangedPropertiesCount);

            group.UID = 2;
            group.GroupName = "el groupo";
            group.GroupDescr = "blah";

            Assert.AreEqual(3, group.ChangedPropertiesCount);
        }

    }
}
