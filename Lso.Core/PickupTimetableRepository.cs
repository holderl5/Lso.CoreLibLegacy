using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lso.Core
{
    public class PickupTimetableRepository : IPickupTimetableRepository
    {
        protected WebDbDataContext _Db;

        public PickupTimetableRepository()
        {
            string connectionString =
                "Data Source=10.0.5.14;Initial Catalog=LsoCoreTest;Persist Security Info=True;User ID=L5development;Password=L5SQLCool";
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Lso.Core.LsoCoreSettings.WebDBConnString"].ConnectionString;
            }
            catch (Exception)
            {
                // this is a bandaid for unit testing so nothing for now
            }

            DoConstruction(connectionString);
        }

        public PickupTimetableRepository(string connectionString)
        {
            DoConstruction(connectionString);
        }

        protected void DoConstruction(string connectionString)
        {
            _Db = new WebDbDataContext(connectionString);

            if (!_Db.DatabaseExists())
            {
                throw new RepositoryDoesNotExistException("WebshipAccountRepository.DatabaseExists returns false with the following connection string: " + _Db.Connection);
            }
        }

        public IList<IPickupTimetable> GetAllWithCustIdPhoneExtensionZip(int custId, string phoneNumber, string phoneExtension, string zipCode)
        {
            var retval = new List<IPickupTimetable>();

            var custrepo = RepositoryFactory.GetNewCustomerRepository();
            var custlist = custrepo.GetAllWithCustId(custId);
            if (custlist.Any())
            {
                var latestTimeCall = _Db.sp_getLatestTime(zipCode).First();               
                var cust = custlist.FirstOrDefault();
                var earliestCloseTime = _Db.sp_getEarliestClosingTime(cust.CustID).First();
                var timetable = new PickupTimetable
                                    {
                                        CustID = cust.CustID,
                                        PhoneNumber = phoneNumber,
                                        PhoneExtension = phoneExtension,
                                        LastPickupTime = TimeSpan.Parse(latestTimeCall.LastPickUpTime),
                                        LatestCallInTime = TimeSpan.Parse(latestTimeCall.callInTime),
                                        EarliestClosingTime = TimeSpan.Parse(earliestCloseTime.EarliestTime),
                                        PickupSchedulingLatest = GetPickupSchedulingLatest(zipCode),
                                        PickupSchedulingSoonest = GetPickupSchedulingSoonest(zipCode),
                                        PickupZipcode = zipCode,
                                        CheckMaxPickups = checkdate => _Db.sp_checkMaxLocPU(cust.CustID, phoneNumber, phoneExtension, checkdate),
                                        CheckValidPickupDay = checkholiday => ValidPickupDate(checkholiday)
                                    };           
                retval.Add(timetable);
            }

            return retval;

        }

        
//---------------------------------------------------------------------------------------------------------
// Checks if date passed in is a date on which LSO can pick up a package.
// args:
//   DateToCheck - the date to check
//   NextDelivDate - if date is not a valid pickup date, this argument will contain the next valid date
// returns:
//   True if DateToCheck is valid pickup date, else False
//---------------------------------------------------------------------------------------------------------
    private DateTime ValidPickupDate(DateTime dateToCheck)
    {
        // assume incoming date is good
        DateTime nextDelivDate = dateToCheck.Date;

        // check for saturday or sunday - no deliveries then
        while (nextDelivDate.DayOfWeek == DayOfWeek.Saturday || nextDelivDate.DayOfWeek == DayOfWeek.Sunday)
        {
            nextDelivDate = nextDelivDate.AddDays(1);
        }

        // can't pickup on a holiday
        // we have to check the original incoming date AND the date we calculated by 
        // skipping the weekend.  If a holiday falls on the weekend and has a next delivery
        // date of Tuesday, we would miss by only checking for Monday being a holiday
        var holidaynextdelivdate = from p in _Db.Holidays
                                   where p.Holiday1 == dateToCheck || p.Holiday1 == nextDelivDate
                                   select p;
        if (holidaynextdelivdate.Any())
        {
            foreach (var holiday in holidaynextdelivdate)
            {
                if (holiday.NextDelivDate != null && holiday.NextDelivDate > nextDelivDate)
                {
                    nextDelivDate = holiday.NextDelivDate.Value;
                }
                
            }            
        }

        

        return nextDelivDate;

    }

        //---------------------------------------------------------------------------------------------------------
        //Returns how many days into the future that a user is allowed to schedule a pickup.
        //args:
        //   ZipCode - the zipcode for which the scheduling lead time should be obtained
        //returns:
        //   The number of days, including the current day, into the future that a pickup can be scheduled.
        //   For example, a value of 1 indicates they can schedule a pickup for that day or the next day.
        //---------------------------------------------------------------------------------------------------------
        private int GetPickupSchedulingLatest(string zipCode)
        {
            var schedules = from p in _Db.ServiceByZipCodes
                           where p.ZipCode == zipCode
                           select p;                      
            return schedules.FirstOrDefault().PickupSchedulingLatest;                      
        }

        private int GetPickupSchedulingSoonest(string zipCode)
        {
            var schedules = from p in _Db.ServiceByZipCodes
                            where p.ZipCode == zipCode
                            select p;
            return schedules.FirstOrDefault().PickupSchedulingSoonest;                      
        }

    }
}
