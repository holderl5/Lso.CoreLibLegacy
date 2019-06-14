using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface IPickupTimetable
    {
        int CustID { get; }
        string PhoneNumber { get; }
        string PhoneExtension { get; }
        IList<PickupDate> PickupDates { get; }
    }
}
