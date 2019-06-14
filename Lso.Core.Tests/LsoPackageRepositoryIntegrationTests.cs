using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class LsoPackageRepositoryIntegrationTests
    {

        private ILsoPackageRepository _repository;

        [TestFixtureSetUp]
        public void InitTests()
        {                        
            // the repository used in the majority of tests
            _repository = new LsoPackageRepository();
            

        }

        [Test]
        public void FirstTest()
        {
            _repository.GetAllWithAirbillNo("32729513");
        }

        [Test]
        public void PkgHistoryTest()
        {
            _repository.GetAllWithAirbillNo("17426086");
        }

        [Test]
        public void RedundantDataTest()
        {
            var t = _repository.GetAllWithAirbillNo("17426086");
            t.First().FilterRedundantSteps = true;
            t.First().FilterFuzzMinutes = 9;
            var q = t.First().TrackingSteps;
            if (q.Count > 10)
            {
                
            }

        }
    }
}
