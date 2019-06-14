using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoIGroupRepositoryIntegrationTests
    {
        private ILsoIGroupRepository _repository;

        [TestFixtureSetUp]
        public void InitTests()
        {
            _repository = new LsoIGroupRepository();
        }

        [Test]
        public void GetTest()
        {
            var res = _repository.GetAllForUID(1);
            Assert.AreEqual(res.Count,1); 
        }
    }
}
