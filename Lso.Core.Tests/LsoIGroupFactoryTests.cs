using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoIGroupFactoryTests
    {
        private IGroup _testgroup;
        private LsoIGroup _testlsogroup;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _testgroup = new IGroup();

            _testgroup.UID = 1;
            _testgroup.GroupName = "TestGroup1";
            _testgroup.GroupDescr = "TestGroup1Description";

            _testlsogroup = new LsoIGroup();

            _testlsogroup.UID = 1;
            _testlsogroup.GroupName = "TestGroup1";
            _testlsogroup.GroupDescr = "TestGroup1Description";

        }

        [Test]
        public void CheckILsoGroupHasAllIGroupFields()
        {
            var data = new IGroup();


            Mapper.CreateMap<LsoIGroup, IGroup>();
            Mapper.Map<LsoIGroup, IGroup>(_testlsogroup, data);
            Mapper.AssertConfigurationIsValid();

        }
        [Test]
        public void CanAutoMapFromDataClassToIGroup()
        {
            var factory = new LsoIGroupFactory();

            var lsogroup = factory.Create(_testgroup);

            Assert.AreEqual(_testlsogroup.GroupId, lsogroup.GroupId);
            Assert.AreEqual(_testlsogroup.UID, lsogroup.UID);
            Assert.AreEqual(_testlsogroup.GroupName, lsogroup.GroupName);
            Assert.AreEqual(_testlsogroup.GroupDescr, lsogroup.GroupDescr);
        }

        [Test]
        public void CanUpdateDataClassFromLsogroup()
        {
            var factory = new LsoIGroupFactory();
            var lsogroup = factory.Create(_testgroup);

            lsogroup.UID = 2;
            lsogroup.GroupName = "TestGroup2";
            lsogroup.GroupDescr = "TestGroup2Descr";

            LsoIGroupFactory.Update(lsogroup);

            Assert.AreEqual(lsogroup.UID, _testgroup.UID);
            Assert.AreEqual(lsogroup.GroupDescr, _testgroup.GroupDescr);
            Assert.AreEqual(lsogroup.GroupName, _testgroup.GroupName);
        }
    }
}
