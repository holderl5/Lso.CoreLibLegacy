using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Lso.Core.Tests
{
    class PickupTimetableTests
    {
        private IPickupTimetableRepository _repo;

        [TestFixtureSetUp]
        public void InitTests()
        {
            // TODO: set the connection string from a setting would be better I think
            var db = new WebDbDataContext("Data Source=LSO360OPS;Initial Catalog=LsoCoreTest;Persist Security Info=True;User ID=L5development;Password=L5SQLCool");
            db.spRestorePickupTableTestState();

            _repo = RepositoryFactory.GetNewPickupTimetableRepository();

        }

        [Test]
        public void Can_Get_PickupTimetable_Object()
        {

            var ptt  = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "78727")).FirstOrDefault();            
            
           
            Assert.AreEqual(1, ptt.CustID);
            Assert.AreEqual("5555555555", ptt.PhoneNumber);
            Assert.AreEqual("576", ptt.PhoneExtension);

        }

        [Test]
        public void Request_With_Normal_Workday_Test_Date_Before_Day_Start_Returns_Good_Result()
        {
            var ptt = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "78727")).FirstOrDefault();

            // Set testing value on class
            var actualptt = (PickupTimetable)ptt;
            actualptt.TestingParameter_CurrentDateTime = new DateTime(2010, 12, 13, 6, 19, 0);

            var pickups = ptt.PickupDates;

            // Request Details
            // Date of Request: 2010/12/13 6:19:00AM
            // Customer: 1
            // Pickup Zip: 78727
            // Expected Response
            // Pickup Dates: (6 Ttl) 2010/12/13, 2010/12/14, 2010/12/15, 2010/12/16, 2010/12/17, 2010/12/20
            // Ready Times: [Request Day] 7:00AM - 5:00PM  [Future Day] 7:00AM - 5:00PM
            // Close Times: [Request Day] 4:00PM - 11:00PM  [Future Day] 4:00PM - 11:00PM

            Assert.AreEqual(6, pickups.Count);
            Assert.AreEqual(new DateTime(2010, 12, 13), pickups[0].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 14), pickups[1].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 15), pickups[2].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 16), pickups[3].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 17), pickups[4].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 20), pickups[5].DateOfPickup);

            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[0].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[0].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[1].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[1].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[2].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[2].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[3].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[3].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[4].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[4].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[5].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[5].ReadyTimes.Last());

            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[0].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[0].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[1].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[1].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[2].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[2].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[3].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[3].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[4].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[4].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[5].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[5].CloseTimes.Last());
        }

        [Test]
        public void Request_With_Normal_Workday_Test_Date_After_Day_Start_Returns_Good_Result()
        {
            var ptt = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "73301")).FirstOrDefault();

            // Set testing value on class
            var actualptt = (PickupTimetable)ptt;
            actualptt.TestingParameter_CurrentDateTime = new DateTime(2017,02, 22, 15, 19, 0);

            var pickups = ptt.PickupDates;

            // Request Details
            // Date of Request: 2010/12/13 8:19:00AM
            // Customer: 1
            // Pickup Zip: 78727
            // Expected Response
            // Pickup Dates: (6 Ttl) 2010/12/13, 2010/12/14, 2010/12/15, 2010/12/16, 2010/12/17, 2010/12/20
            // Ready Times: [Request Day] 8:30AM - 5:00PM  [Future Day] 7:00AM - 5:00PM
            // Close Times: [Request Day] 4:00PM - 11:00PM  [Future Day] 4:00PM - 11:00PM

            Assert.AreEqual(6, pickups.Count);
            Assert.AreEqual(new DateTime(2010, 12, 13), pickups[0].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 14), pickups[1].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 15), pickups[2].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 16), pickups[3].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 17), pickups[4].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 20), pickups[5].DateOfPickup);

            Assert.AreEqual(new TimeSpan(0, 8, 30, 0), pickups[0].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[0].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[1].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[1].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[2].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[2].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[3].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[3].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[4].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[4].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[5].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[5].ReadyTimes.Last());

            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[0].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[0].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[1].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[1].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[2].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[2].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[3].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[3].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[4].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[4].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[5].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[5].CloseTimes.Last());
        }

        [Test]
        public void Request_With_Normal_Workday_Test_Date_After_Day_Start_Round_Up_Returns_Good_Result()
        {
            var ptt = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "78727")).FirstOrDefault();

            // Set testing value on class
            var actualptt = (PickupTimetable)ptt;
            actualptt.TestingParameter_CurrentDateTime = new DateTime(2010, 12, 13, 8, 41, 0);

            var pickups = ptt.PickupDates;

            // Request Details
            // Date of Request: 2010/12/13 8:41:00AM
            // Customer: 1
            // Pickup Zip: 78727
            // Expected Response
            // Pickup Dates: (6 Ttl) 2010/12/13, 2010/12/14, 2010/12/15, 2010/12/16, 2010/12/17, 2010/12/20
            // Ready Times: [Request Day] 9:00AM - 5:00PM  [Future Day] 7:00AM - 5:00PM
            // Close Times: [Request Day] 4:00PM - 11:00PM  [Future Day] 4:00PM - 11:00PM

            Assert.AreEqual(6, pickups.Count);
            Assert.AreEqual(new DateTime(2010, 12, 13), pickups[0].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 14), pickups[1].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 15), pickups[2].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 16), pickups[3].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 17), pickups[4].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 20), pickups[5].DateOfPickup);

            Assert.AreEqual(new TimeSpan(0, 9, 0, 0), pickups[0].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[0].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[1].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[1].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[2].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[2].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[3].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[3].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[4].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[4].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[5].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[5].ReadyTimes.Last());

            Assert.AreEqual(new TimeSpan(0, 16, 00, 0), pickups[0].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[0].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[1].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[1].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[2].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[2].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[3].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[3].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[4].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[4].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[5].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[5].CloseTimes.Last());
        }

        [Test]
        public void Request_Before_Holiday_Returns_Good_Result()
        {
            var ptt = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "78727")).FirstOrDefault();

            // Set testing value on class
            var actualptt = (PickupTimetable)ptt;
            actualptt.TestingParameter_CurrentDateTime = new DateTime(2010, 12, 23, 8, 41, 0);

            var pickups = ptt.PickupDates;

            // Request Details
            // Date of Request: 2010/12/23 8:41:00AM
            // Customer: 1
            // Pickup Zip: 78727
            // Expected Response
            // Pickup Dates: (6 Ttl) 2010/12/23, 2010/12/24, 2010/12/27, 2010/12/28, 2010/12/29, 2010/12/30
            // Ready Times: [Request Day] 9:00AM - 5:00PM  [Future Day] 7:00AM - 5:00PM
            // Close Times: [Request Day] 4:00PM - 11:00PM  [Future Day] 4:00PM - 11:00PM

            Assert.AreEqual(6, pickups.Count);
            Assert.AreEqual(new DateTime(2010, 12, 23), pickups[0].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 24), pickups[1].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 27), pickups[2].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 28), pickups[3].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 29), pickups[4].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 30), pickups[5].DateOfPickup);

            Assert.AreEqual(new TimeSpan(0, 9, 0, 0), pickups[0].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[0].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[1].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[1].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[2].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[2].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[3].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[3].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[4].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[4].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[5].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 17, 0, 0), pickups[5].ReadyTimes.Last());

            Assert.AreEqual(new TimeSpan(0, 16, 00, 0), pickups[0].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[0].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[1].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[1].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[2].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[2].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[3].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[3].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[4].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[4].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[5].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[5].CloseTimes.Last());
        }

        [Test]
        public void Next_Day_Pickup_Returns_Good_Result()
        {
            var ptt = (_repo.GetAllWithCustIdPhoneExtensionZip(1, "5555555555", "576", "78754")).FirstOrDefault();

            // Set testing value on class
            var actualptt = (PickupTimetable)ptt;
            actualptt.TestingParameter_CurrentDateTime = new DateTime(2010, 12, 16, 20, 42, 0);

            var pickups = ptt.PickupDates;

            // Request Details
            // Date of Request: 2010/12/16 8:42:00PM
            // Customer: 1
            // Pickup Zip: 78754
            // Expected Response
            // Pickup Dates: (5 Ttl) 2010/12/17, 2010/12/20, 2010/12/21, 2010/12/22, 2010/12/23
            // Ready Times: [Future Day] 7:00AM - 8:00PM
            // Close Times: [Future Day] 4:00PM - 11:00PM

            Assert.AreEqual(5, pickups.Count);
            Assert.AreEqual(new DateTime(2010, 12, 17), pickups[0].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 20), pickups[1].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 21), pickups[2].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 22), pickups[3].DateOfPickup);
            Assert.AreEqual(new DateTime(2010, 12, 23), pickups[4].DateOfPickup);
            

            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[0].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 20, 30, 0), pickups[0].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[1].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 20, 30, 0), pickups[1].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[2].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 20, 30, 0), pickups[2].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[3].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 20, 30, 0), pickups[3].ReadyTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 7, 0, 0), pickups[4].ReadyTimes.First());
            Assert.AreEqual(new TimeSpan(0, 20, 30, 0), pickups[4].ReadyTimes.Last());            

            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[0].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[0].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[1].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[1].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[2].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[2].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[3].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[3].CloseTimes.Last());
            Assert.AreEqual(new TimeSpan(0, 16, 0, 0), pickups[4].CloseTimes.First());
            Assert.AreEqual(new TimeSpan(0, 23, 0, 0), pickups[4].CloseTimes.Last());            
        }
    }
    
}
