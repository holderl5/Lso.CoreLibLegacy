using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace Lso.Core
{
    public class WebshipAccount : DomainObjBase, IWebshipAccount
    {
        public WebshipAccount()
        {
            _loginName = "";
            _UserFirstName = "";
            _UserLastName = "";
            _UserEmail = "";     
            _UserPWD = "";
            _CompanyName = "";
            _CompanyPhone = "";
            _CompanyAddress1 = "";
            _CompanyAddress2 = "";
            _CompanyCity = "";
            _CompanyState = "";
            _CompanyZip = "";
            _HardCodedBillingRefValue = "";
            _HardCodedBillingRefValue2 = "";
            _ActivityNotes = "";

        }
        // LINQ to SQL object used to create this object
        internal UserProfile CreatedFrom;

        // ReSharper disable InconsistentNaming
        private int _UID;
        private string _loginName;
        private string _UserFirstName;
        private string _UserLastName;
        private string _UserEmail;     
        private string _UserPWD;
        private bool _administrator;
        private int _CustID;   
        private string _CompanyName;
        private string _CompanyPhone;
        private string _CompanyAddress1;
        private string _CompanyAddress2;
        private string _CompanyCity;
        private string _CompanyState;
        private string _CompanyZip;
        private DateTime _LastLoginDate;
        private int _LoginsToDate;
        private int _LoginAttemptsToday;
        private int _LoginFailedToday;        
        private DateTime _PWDChangedLast;
        private DateTime _PWDResentLast;
        private int _PWDResentCount;
        private short _PWDResentToday;
        private int _ABEntryToday;
        private int _CreatedBy;
        private DateTime _CreateDate;
        private bool _AccountLockout;
        private int _PrintToLabel;
        private char _DefaultService;
        private bool _BillingRefRequired;        
        private bool _EmailPOD;       
        private bool _SuperWebShipper;
        private bool _UseLocBillingRef;
        private decimal _HandlingFee;
        private bool _PrintPublishedRates;
        private bool _IsUserAdmin;
        private bool _Active;
        private bool _ShowOnlyUserShipments;
        private bool _DisableBillingReferenceRequired;
        private bool _HardCodeBillingRef;
        private string _HardCodedBillingRefValue;
        private bool _HardCodeBillingRef2;
        private string _HardCodedBillingRefValue2;
        private Guid _newCustomerReferralSourceID;
        private string _ActivityNotes;

        // ReSharper restore InconsistentNaming

        public IWebshipAccount GetNewWebshipAccount()
        {
            return new WebshipAccount();
        }

        public int UID
        {
            get { return _UID; }
            set { SetProp("UID", ref _UID, value,
               () => { CreatedFrom.UID = value; });
            }
        }
        public string LoginName
        {
            get { return SafeTrim(_loginName); }
            set { SetProp("LoginName", ref _loginName, value,
                () => { CreatedFrom.LoginName = value; });
            }
        }
        public string UserFirstName
        {
            get { return _UserFirstName; }
            set { SetProp("UserFirstName", ref _UserFirstName, value,
                () => { CreatedFrom.UserFirstName = value; });
            }
        }
        public string UserLastName
        {
            get { return _UserLastName; }
            set { SetProp("UserLastName", ref _UserLastName, value,
                () => { CreatedFrom.UserLastName = value; });
            }
        }
        public string UserEmail
        {
            get { return _UserEmail; }
            set { SetProp("UserEmail", ref _UserEmail, value,
                () => { CreatedFrom.UserEmail = value; });
            }
        }
        
        // ReSharper disable InconsistentNaming
        public string UserPWD
        {
            get { return _UserPWD; }
            set { SetProp("UserPWD", ref _UserPWD, value,
                () => { CreatedFrom.UserPWD = value; });
            }
        }

        public bool Administrator
        {
            get { return _administrator; }
            set
            {
                // If we are about to set administrator to true (and it is not already)
                if (!_administrator && value)
                {
                    if (_CustID == 0 && !AllowAutomapExceptions)
                    {                        
                        throw new CustomerIDRequiredException("Cannot set Administrator on a webship account without a custID");
                    }

                    
                    CheckCustomerIdPrivileged();
                }

                SetProp("Administrator", ref _administrator, value,
                    () => { CreatedFrom.Administrator = value; });
            }
        }

        public int CustID
        {
            get { return _CustID; }
            set { SetProp("CustID", ref _CustID, value,
                () => { CreatedFrom.CustID = value; });
            }
        }
        
        // ReSharper restore InconsistentNaming
        public string CompanyName
        {
            get { return _CompanyName; }
            set { SetProp("CompanyName", ref _CompanyName, value,
                () => { CreatedFrom.CompanyName = value; });
            }
        }
        public string CompanyPhone
        {
            get { return _CompanyPhone; }
            set
            {
                var digitsOnly = ToDigitsOnly(value);                
                SetProp("CompanyPhone", ref _CompanyPhone, digitsOnly,
                    () => { CreatedFrom.CompanyPhone = digitsOnly; });
            }
        }
        public string CompanyAddress1
        {
            get { return _CompanyAddress1; }
            set { SetProp("CompanyAddress1", ref _CompanyAddress1, value,
                () => { CreatedFrom.CompanyAddress1 = value; });
            }
        }

        // ReSharper disable InconsistentNaming
        public string CompanyAddress2
        {
            get { return _CompanyAddress2; }
            set { SetProp("CompanyAddress2", ref _CompanyAddress2, value,
                () => { CreatedFrom.CompanyAddress2 = value; });
            }
        }

        public string CompanyCity
        {
            get { return _CompanyCity; }
            set { SetProp("CompanyCity", ref _CompanyCity, value,
                () => { CreatedFrom.CompanyCity = value; });
            }
        }
        public string CompanyState
        {
            get { return _CompanyState; }
            set { SetProp("CompanyState", ref _CompanyState, value,
                () => { CreatedFrom.CompanyState = value; });
            }
        }
        public string CompanyZip
        {
            get { return _CompanyZip; }
            set { SetProp("CompanyZip", ref _CompanyZip, value,
                () => { CreatedFrom.CompanyZip = value; });
            }
        }
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { SetProp("LastLoginDate", ref _LastLoginDate, value,
                () => { CreatedFrom.LastLoginDate = value; });
            }
        }

        public int LoginsToDate
        {
            get { return _LoginsToDate; }
            set { SetProp("LoginsToDate", ref _LoginsToDate, value,
                () => { CreatedFrom.LoginsToDate = value; });
            }
        }
        public int LoginAttemptsToday
        {
            get { return _LoginAttemptsToday; }
            set { SetProp("LoginAttemptsToday", ref _LoginAttemptsToday, value,
                () => { CreatedFrom.LoginAttemptsToday = (short)value; });
            }
        }
        public int LoginFailedToday
        {
            get { return _LoginFailedToday; }
            set { SetProp("LoginFailedToday", ref _LoginFailedToday, value,
                () => { CreatedFrom.LoginFailedToday = (short)value; });
            }
        }
        public DateTime PWDChangedLast
        {
            get { return _PWDChangedLast; }
            set { SetProp("PWDChangedLast", ref _PWDChangedLast, value,
                () => { CreatedFrom.PWDChangedLast = value; });
            }
        }
        public DateTime PWDResentLast
        {
            get { return _PWDResentLast; }
            set { SetProp("PWDResentLast", ref _PWDResentLast, value,
                () => { CreatedFrom.PWDResentLast = value; });
            }
        }
        public int PWDResentCount
        {
            get { return _PWDResentCount; }
            set { SetProp("PWDResentCount", ref _PWDResentCount, value,
                () => { CreatedFrom.PWDResentCount = value; });
            }
        }
        public short PWDResentToday
        {
            get { return _PWDResentToday; }
            set { SetProp("PWDResentToday", ref _PWDResentToday, value,
                () => { CreatedFrom.PWDResentToday = value; });
            }
        }
        public int ABEntryToday
        {
            get { return _ABEntryToday; }
            set { SetProp("ABEntryToday", ref _ABEntryToday, value,
                () => { CreatedFrom.ABEntryToday = value; });
            }
        }
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { SetProp("CreatedBy", ref _CreatedBy, value,
                () => { CreatedFrom.CreatedBy = value; });
            }
        }


        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { SetProp("CreateDate", ref _CreateDate, value,
                () => { CreatedFrom.CreateDate = value; });
            }
        }
        public bool AccountLockout
        {
            get { return _AccountLockout; }
            set { SetProp("AccountLockout", ref _AccountLockout, value,
                () => { CreatedFrom.AccountLockout = value; });
            }
        }
        public int PrintToLabel
        {
            get { return _PrintToLabel; }
            set { SetProp("PrintToLabel", ref _PrintToLabel, value,
                () => { CreatedFrom.PrintToLabel = value; });
            }
        }
        public char DefaultService
        {
            get { return _DefaultService; }
            set { SetProp("DefaultService", ref _DefaultService, value,
                () => { CreatedFrom.DefaultService = value; });
            }
        }
        public bool BillingRefRequired
        {
            get { return _BillingRefRequired; }
            set { SetProp("BillingRefRequired", ref _BillingRefRequired, value,
                () => { CreatedFrom.BillingRefRequired = value; });
            }
        }
        public bool EmailPOD
        {
            get { return _EmailPOD; }
            set { SetProp("EmailPOD", ref _EmailPOD, value,
                () => { CreatedFrom.EmailPOD = value; });
            }
        }
        public bool SuperWebShipper
        {
            get { return _SuperWebShipper; }
            set { SetProp("SuperWebShipper", ref _SuperWebShipper, value,
                () => { CreatedFrom.SuperWebShipper = value; });
            }
        }
        public bool UseLocBillingRef
        {
            get { return _UseLocBillingRef; }
            set { SetProp("UseLocBillingRef", ref _UseLocBillingRef, value,
                () => { CreatedFrom.UseLocBillingRef = value; });
            }
        }
        public decimal HandlingFee
        {
            get { return _HandlingFee; }
            set { SetProp("HandlingFee", ref _HandlingFee, value,
                () => { CreatedFrom.HandlingFee = value; });
            }
        }
        public bool PrintPublishedRates
        {
            get { return _PrintPublishedRates; }
            set { SetProp("PrintPublishedRates", ref _PrintPublishedRates, value,
                () => { CreatedFrom.PrintPublishedRates = value; });
            }
        }
        public bool IsUserAdmin
        {
            get { return _IsUserAdmin; }
            set { SetProp("IsUserAdmin", ref _IsUserAdmin, value,
                () => { CreatedFrom.IsUserAdmin = value; });
            }
        }
        public bool Active
        {
            get { return _Active; }
            set { SetProp("Active", ref _Active, value,
                () => { CreatedFrom.Active = value; });
            }
        }
        public bool ShowOnlyUserShipments
        {
            get { return _ShowOnlyUserShipments; }
            set { SetProp("ShowOnlyUserShipments", ref _ShowOnlyUserShipments, value,
                () => { CreatedFrom.ShowOnlyUserShipments = value; });
            }
        }
        public bool DisableBillingReferenceRequired
        {
            get { return _DisableBillingReferenceRequired; }
            set { SetProp("DisableBillingReferenceRequired", ref _DisableBillingReferenceRequired, value,
                () => { CreatedFrom.DisableBillingReferenceRequired = value; });
            }
        }
        public bool HardCodeBillingRef
        {
            get { return _HardCodeBillingRef; }
            set { SetProp("HardCodeBillingRef", ref _HardCodeBillingRef, value,
                () => { CreatedFrom.HardCodeBillingRef = value; });
            }
        }
        public string HardCodedBillingRefValue
        {
            get { return _HardCodedBillingRefValue; }
            set { SetProp("HardCodedBillingRefValue", ref _HardCodedBillingRefValue, value,
                () => { CreatedFrom.HardCodedBillingRefValue = value; });
            }
        }
        public bool HardCodeBillingRef2
        {
            get { return _HardCodeBillingRef2; }
            set { SetProp("HardCodeBillingRef2", ref _HardCodeBillingRef2, value,
                () => { CreatedFrom.HardCodeBillingRef2 = value; });
            }
        }
        public string HardCodedBillingRefValue2
        {
            get { return _HardCodedBillingRefValue2; }
            set { SetProp("HardCodedBillingRefValue2", ref _HardCodedBillingRefValue2, value,
                () => { CreatedFrom.HardCodedBillingRefValue2 = value; });
            }
        }

        public Guid newCustomerReferralSourceID
        {
            get { return _newCustomerReferralSourceID; }
            set
            {
                SetProp("newCustomerReferralSourceID", ref _newCustomerReferralSourceID, value,
                    () => { CreatedFrom.newCustomerReferralSourceID = value; });
            }
        }

        public string ActivityNotes
        {
            get { return _ActivityNotes; }
            set
            {
                SetProp("ActivityNotes", ref _ActivityNotes, value,
                    () => { CreatedFrom.ActivityNotes = value; });
            }
        }

        // There are no administrator accounts that are not the LSO Customer ID (which is 1)
        // This is a safety net to prevent abuse
        private void CheckCustomerIdPrivileged()
        {
            if (_CustID != 1 && !AllowAutomapExceptions)
            {
                throw new PrivilegedCustomerIDRequiredException("Account cannot be set to Administrator for an unprivileged CustID");
            }
        }

    }
}
