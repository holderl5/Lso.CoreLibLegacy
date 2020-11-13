using System;
using System.Collections.Generic;

namespace Lso.Core
{
    public class PickupTimetable : DomainObjBase, IPickupTimetable
    {
        // ReSharper disable InconsistentNaming
        private int _CustID;
        private string _PhoneNumber;
        private string _PhoneExtension;
        private string _PickupZipcode;
        private TimeSpan _EarliestClosingTime;
        private TimeSpan _LatestCallInTime;
        private TimeSpan _LastPickupTime;
        private int _PickupSchedulingSoonest;
        private int _PickupSchedulingLatest;
        private Func<DateTime, int> _CheckMaxPickups;
        private Func<DateTime, DateTime> _CheckValidPickupDay;
        // ReSharper restore InconsistentNaming

        public PickupTimetable()
        {
            _PhoneNumber = "";
            _PhoneExtension = "";
            _PickupZipcode = "";
        }

        public int CustID
        {
            get { return _CustID; }
            internal set { SetProp("CustID", ref _CustID, value, NullDelegate); }
        }     

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            internal set
            {
                var digitsOnly = ToDigitsOnly(value);
                SetProp("PhoneNumber", ref _PhoneNumber, digitsOnly, NullDelegate);
            }
        }

        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            internal set { SetProp("PhoneExtension", ref _PhoneExtension, value, NullDelegate); }
        }

        public string PickupZipcode
        {
            get { return _PickupZipcode; }
            internal set { SetProp("PickupZipcode", ref _PickupZipcode, value, NullDelegate); }
        }

        // Min number of days into the future a pickup can be scheduled for pickup zipcode
        internal int PickupSchedulingSoonest
        {
            get { return _PickupSchedulingSoonest; }
            set { SetProp("PickupSchedulingSoonest", ref _PickupSchedulingSoonest, value, NullDelegate); }            
        }

        // Max number of days into the future a pickup can be scheduled for pickup zipcode
        internal int PickupSchedulingLatest
        {
            get { return _PickupSchedulingLatest; }
            set { SetProp("PickupSchedulingLatest", ref _PickupSchedulingLatest, value, NullDelegate); }
        }

        // Latest call in time for the pickup zipcode
        internal TimeSpan LatestCallInTime
        {
            get { return _LatestCallInTime; }
            set { SetProp("LatestCallInTime", ref _LatestCallInTime, value, NullDelegate); }
        }

        // Last pickup time for the pickup zipcode
        internal TimeSpan LastPickupTime
        {
            get { return _LastPickupTime; }
            set { SetProp("LastPickupTime", ref _LastPickupTime, value, NullDelegate); }
        }

        internal TimeSpan EarliestClosingTime
        {
            get { return _EarliestClosingTime; }
            set { SetProp("EarliestClosingTime", ref _EarliestClosingTime, value, NullDelegate); }
        }

        internal Func<DateTime, int> CheckMaxPickups 
        {
            set { _CheckMaxPickups = value; }
        }

        internal Func<DateTime, DateTime> CheckValidPickupDay
        {
            set { _CheckValidPickupDay = value; }
        }

        // Requires PickupSchedulingSoonest, PickupSchedulingLatest, LatestCallInTime, LastPickupTime
        // returns the days available for pickup
        protected IList<DateTime> PickupDays
        {
            get
            {
                var retval = new List<DateTime>();


                DateTime now = GetCurrentTime();

                // Calculate the first day we could schedule a pickup for this zipcode
                DateTime pickupday = now.Date.AddDays(PickupSchedulingSoonest);
                // push pickup to tomorrow if today is already too late
                if (!CanPickupToday() && pickupday == now.Date)
                {
                    pickupday = pickupday.AddDays(1);
                }

                DateTime latestPickupDate = now.Date.AddDays(PickupSchedulingLatest);
                while (pickupday <= latestPickupDate)
                {
                    // Check that this customer/location still has pickup slots available
                    if (_CheckMaxPickups(pickupday) == 0)
                    {
                        // checks for sat/sun/holidays and returns next available pickup day
                        DateTime nextvalidpickupday = _CheckValidPickupDay(pickupday);
                        if (nextvalidpickupday == pickupday)
                        {
                            retval.Add(pickupday);
                        }
                        else
                        {
                            // skip all the holidays/weekends
                            pickupday = nextvalidpickupday;
                            continue;
                        }
                    }

                    pickupday = pickupday.AddDays(1);
                }

                return retval;
            }
        }

        protected bool CanPickupToday()
        {
            DateTime now = GetCurrentTime();            
            var lastReadyTime = LastPickupTime.Subtract(new TimeSpan(0, 2, 0, 0));      // change to 2 hours for 2020 peak                
            // Found this bug while testing peak changes:  LastCallInTime is specified in servicebyzipcode to protect certain zipcodes, but the PickupService does not ever look at it
            // because of peak changes, is possible that lastReadyTime might be earlier than LatestCallInTime
            var readyTimeEnd = EarliestTime(LatestCallInTime, lastReadyTime);
            return (now.TimeOfDay < readyTimeEnd);
        }

        public IList<PickupDate> PickupDates
        {
            get
            {
                // ready time always starts at 7AM, close time always starts at 8AM
                var readyTimeStart = new TimeSpan(0, 7, 0, 0);
                var closeTimeStart = EarliestClosingTime;
                // Close time is always 30 minutes later than ready time when day is today
                var closeTimeExtra = new TimeSpan(0, 0, 30, 0);
                // last close time is always 11:00
                var closeTimeEnd = new TimeSpan(0, 23, 0, 0);

                // last ready time is 1 hour before last pickup time
                var lastReadyTime = LastPickupTime.Subtract(new TimeSpan(0, 2, 0, 0));      // change to 2 hours for 2020 peak                

                var retval = new List<PickupDate>();
                DateTime now = GetCurrentTime();

                foreach (var pickupDay in PickupDays)
                {
                    var pickup = new PickupDate();

                    pickup.DateOfPickup = pickupDay.Date;

                    // Today has special time processing
                    if (now.Date == pickupDay.Date)
                    {                        
                        var rstart = LatestTime(now.TimeOfDay, readyTimeStart);
                        var cstart = LatestTime(now.TimeOfDay, closeTimeStart);
                        cstart = (now.TimeOfDay >= closeTimeStart) ? cstart + closeTimeExtra : cstart;

                        // Found this bug while testing peak changes:  LastCallInTime is specified in servicebyzipcode to protect certain zipcodes, but the PickupService does not ever look at it
                        // because of peak changes, is possible that lastReadyTime might be earlier than LatestCallInTime
                        var readyTimeEnd = EarliestTime(LatestCallInTime, lastReadyTime);
                                        
                        pickup.ReadyTimes = FillTimes(rstart, readyTimeEnd);
                        pickup.CloseTimes = FillTimes(cstart, closeTimeEnd);
                    }
                    else
                    {
                        pickup.ReadyTimes = FillTimes(readyTimeStart, lastReadyTime);
                        pickup.CloseTimes = FillTimes(closeTimeStart, closeTimeEnd);
                    }

                    retval.Add(pickup);
                }
                return retval;
            }

        }

        protected TimeSpan LatestTime(TimeSpan ts1, TimeSpan ts2)
        {
            if (ts1 > ts2)
            {
                return ts1;
            }

            return ts2;
        }

        protected TimeSpan EarliestTime(TimeSpan ts1, TimeSpan ts2)
        {
            if (ts1 < ts2)
            {
                return ts1;
            }
            return ts2;
        }

        protected IList<TimeSpan> FillTimes(TimeSpan start, TimeSpan end)
        {
            var retval = new List<TimeSpan>();

            var increment = new TimeSpan(0, 0, 30, 0);

            // round up to next hour for 30 minutes or more past hour, otherwise round down to 0 minutes past
            // leave exact hours or half hours alone
            TimeSpan timectr;
            switch (start.Minutes)
            {
                case 0:
                case 30:
                    timectr = start;
                    break;
                default:
                    timectr = (start.Minutes >= 30)
                              ? new TimeSpan(0, start.Hours + 1, 0, 0)
                              : new TimeSpan(0, start.Hours, 30, 0);
                    break;

            }
            
            
            
            while (timectr <= end)
            {
                retval.Add(timectr);
                timectr = timectr.Add(increment);
            }
            return retval;
        }
        

        // TODO: make this internal and only accessible by a special adapter class in different project (So unit testing class stays pure)
        // Testing parameter that will override the current time if it is set
        public DateTime TestingParameter_CurrentDateTime {get; set; }

       
        
        // private function to get DateTime.Now that can be overridden with a test value
        protected DateTime GetCurrentTime()
        {
            // Return testing value if it is not the default datetime 
            if (TestingParameter_CurrentDateTime != new DateTime(0001, 01, 01))
            {
                return TestingParameter_CurrentDateTime;
            }

            return DateTime.Now;
        }
    }
}
