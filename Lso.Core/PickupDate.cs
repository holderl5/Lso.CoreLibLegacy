using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class PickupDate
    {
        public DateTime DateOfPickup;
        public IList<TimeSpan> ReadyTimes;
        public IList<TimeSpan> CloseTimes;
    }
}
