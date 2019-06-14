using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface IPickupTimetableRepository
    {
        IList<IPickupTimetable> GetAllWithCustIdPhoneExtensionZip(int custId, string phoneNumber, string phoneExtension, string zipCode);        
    }
}
