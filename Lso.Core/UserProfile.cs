using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    // Sets defaults for a new instance of the LINQ to SQL class
    // Some defaults here are to account for the fact that LINQ toSQL does
    // not handle auto inserted values correctly
    public partial class UserProfile
    {
        partial void OnCreated()
        {
            LoginsToDate = 0;
            LoginAttemptsToday = 0;
            LoginFailedToday = 0;
            PWDResentCount = 0;
            PWDResentToday = 0;
            ABEntryToday = 0;
            AccountLockout = false;
            PrintToLabel = 0;
            BillingRefRequired = false;
            EmailPOD = false;
            SuperWebShipper = false;
            UseLocBillingRef = false;
        }
    }
}
