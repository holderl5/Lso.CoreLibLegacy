using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface IUserProfile
    {
        int UID { get; set; }
        string LoginName { get; set; }
        string UserFirstName { get; set; }
        string UserLastName { get; set; }
        string UserEmail { get; set; }
        // ReSharper disable InconsistentNaming
        string UserPWD { get; set; }
        bool Administrator { get; set; }
        int CustID { get; set; }
        // ReSharper restore InconsistentNaming
        string CompanyName { get; set; }
        string CompanyPhone { get; set; }
        string CompanyAddress1 { get; set; }
        string CompanyAddress2 { get; set; }
        string CompanyCity { get; set; }
        string CompanyState { get; set; }
        string CompanyZip { get; set; }
        DateTime LastLoginDate { get; set; }
        int LoginsToDate { get; set; }
        int LoginAttemptsToday { get; set; }
        int LoginFailedToday { get; set; }
        // ReSharper disable InconsistentNaming
        DateTime PWDChangedLast { get; set; }
        DateTime PWDResentLast { get; set; }
        int PWDResentCount { get; set; }
        short PWDResentToday { get; set; }
        int ABEntryToday { get; set; }
        // ReSharper restore InconsistentNaming
        int CreatedBy { get; set; }
        DateTime CreateDate { get; set; }
        bool AccountLockout { get; set; }
        int PrintToLabel { get; set; }
        char DefaultService { get; set; }
        bool BillingRefRequired { get; set; }
        // ReSharper disable InconsistentNaming
        bool EmailPOD { get; set; }
        // ReSharper restore InconsistentNaming
        bool SuperWebShipper { get; set; }
        bool UseLocBillingRef { get; set; }
        decimal HandlingFee { get; set; }
        bool PrintPublishedRates { get; set; }
        bool IsUserAdmin { get; set; }
        bool Active { get; set; }
        bool ShowOnlyUserShipments { get; set; }
        bool DisableBillingReferenceRequired { get; set; }
        bool HardCodeBillingRef { get; set; }
        string HardCodedBillingRefValue { get; set; }
        bool HardCodeBillingRef2 { get; set; }
        string HardCodedBillingRefValue2 { get; set; }
    }
}
