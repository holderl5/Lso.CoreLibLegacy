using System;

namespace Lso.Core
{
    public class LsoCustomer : DomainObjBase, ILsoCustomer
	{
        public LsoCustomer()
		{
			_CustName = "";
			_CustContactName = "";
			_CustContactPhone = "";
			_SalesRepEmpNum = "";
		    _SICCode = "";
		    _DOWBillingCode = "";
			_AcctStatus = "";
			_CardType = "";
			_CardNo = "";
			_NameOnCard = "";
			_Comments = "";
			_LastEditEmp = "";
			_BillToAttnName = "";
			_BillToAddress1 = "";
			_BillToAddress2 = "";
			_BillToCity = "";
		    _BillToState = "";
		    _BillToZip = "";
			_PhyAddress1 = "";
			_PhyAddress2 = "";
			_PhyCity = "";
		    _PhyState = "";
		    _PhyZip = "";
			_LocCode = "";
			_LastStatusEditEmp = "";
			_ReferralRep = "";
			_WaiveSignatureFee = "";
		}

        // LINQ to SQL object used to create this object
        internal Customer CreatedFrom;

		// The properties here match exactly with the database so that we can automate the mapping process
		// ReSharper disable InconsistentNaming
		private int _CustID;
		private string _CustName;
		private string _CustContactName;
		private string _CustContactPhone;
		private int _PriceSchedule;
		private DateTime _DateOpened;
		private int _LSOContactEmpNum;
		private string _SalesRepEmpNum;
		private DateTime _RestartDate;
		private string _SICCode;
		private int _HowHearAbout;
		private string _DOWBillingCode;
		private decimal _CreditLimit;
		private decimal _Balance;
		private decimal _CreditAvail;
		private string _AcctStatus;
		private int _NetDueDays;
		private decimal _InterestPercent;
		private char _PayMethodID;
		private string _CardType;
		private string _CardNo;
		private short _CardExpMonth;
		private int _CardExpYear;
		private string _NameOnCard;
		private string _Comments;
		private short _HeldCycleCount;
		private DateTime _EditDateTime;
		private string _LastEditEmp;
		private DateTime _LastShipDate;
		private string _BillToAttnName;
		private string _BillToAddress1;
		private string _BillToAddress2;
		private string _BillToCity;
		private string _BillToState;
		private string _BillToZip;
		private string _PhyAddress1;
		private string _PhyAddress2;
		private string _PhyCity;
		private string _PhyState;
		private string _PhyZip;
		private bool _WaivePUFee;
		private string _LocCode;
		private bool _WaiveFuelFee;
		private short _PageCount;
		private int _BillToID;
		private bool _EDICustomer;
		private DateTime _LastStatusEditDate;
		private string _LastStatusEditEmp;
		private bool _OnHoldOverride;
		private decimal _First60DayRev;
		private bool _Hit;
		private int _LinkedCustID;
		private bool _WaiveResDelivFee;
		private bool _StarCustomer;
		private bool _WaiveDimWt;
		private bool _KnownShipper;
		private string _ReferralRep;
		private bool _BillingRefRequired;
		private int _ParentCustID;
		private bool _InheritDiscounts;
		private string _WaiveSignatureFee;
		private bool _WaiveRemoteDeliveryFee;
		private bool _WaiveManualProcessingFee;
		private bool _WaiveOnCallFee;
        private bool _DisallowSimplePricing;
        private Guid _newCustomerReferralSourceID;
        private bool _DeclaredValueSignature;
        private int _TieredDiscountWindow;
        private bool _WaiveSecuredFacilityFee;
        private bool _WaivePickupEventFee;
        


        private bool _CCOptOut;
		// ReSharper restore InconsistentNaming

        public ILsoCustomer GetNewLsoCustomer()
        {
            return new LsoCustomer();
        }

        public int CustID
		{
			get { return _CustID;}
            set { SetProp("CustID", ref _CustID, value,
                () => { CreatedFrom.CustID = value; });
            }
		}
		public string CustName
		{
			get { return _CustName;}
			set { SetProp("CustName", ref _CustName, value,
                () => { CreatedFrom.CustName = value; });
            }
		}
		public string CustContactName
		{
			get { return _CustContactName;}
			set { SetProp("CustContactName", ref _CustContactName, value,
                () => { CreatedFrom.CustContactName = value; });
            }
		}
		public string CustContactPhone
		{
			get { return _CustContactPhone;}
			set
			{
                var digitsOnly = ToDigitsOnly(value);                
                SetProp("CustContactPhone", ref _CustContactPhone, digitsOnly,
                    () => { CreatedFrom.CustContactPhone = digitsOnly; });
			}
		}
		public int PriceSchedule
		{
			get { return _PriceSchedule;}
			set { SetProp("PriceSchedule", ref _PriceSchedule, value,
                () => { CreatedFrom.PriceSchedule = value; });
            }
		}
		public DateTime DateOpened
		{
			get { return _DateOpened;}
			set { SetProp("DateOpened", ref _DateOpened, value,
                () => { CreatedFrom.DateOpened = value; });
            }
		}
		public int LSOContactEmpNum
		{
			get { return _LSOContactEmpNum;}
			set { SetProp("LSOContactEmpNum", ref _LSOContactEmpNum, value,
                () => { CreatedFrom.LSOContactEmpNum = value; });
            }
		}
		public string SalesRepEmpNum
		{
			get { return _SalesRepEmpNum;}
			set { SetProp("SalesRepEmpNum", ref _SalesRepEmpNum, value,
                () => { CreatedFrom.SalesRepEmpNum = value; });
            }
		}
		public DateTime RestartDate
		{
			get { return _RestartDate;}
			set { SetProp("RestartDate", ref _RestartDate, value,
                () => { CreatedFrom.RestartDate = value; });
            }
		}
		public string SICCode
		{
			get { return SafeTrim(_SICCode);}
			set { SetProp("SICCode", ref _SICCode, value,
                () => { CreatedFrom.SICCode = value; });
            }
		}
		public int HowHearAbout
		{
			get { return _HowHearAbout;}
			set { SetProp("HowHearAbout", ref _HowHearAbout, value,
                () => { CreatedFrom.HowHearAbout = value; });
            }
		}
		public string DOWBillingCode
		{
            get { return SafeTrim(_DOWBillingCode); }
			set { SetProp("DOWBillingCode", ref _DOWBillingCode, value,
                () => { CreatedFrom.DOWBillingCode = value; });
            }
		}
		public decimal CreditLimit
		{
			get { return _CreditLimit;}
			set { SetProp("CreditLimit", ref _CreditLimit, value,
                () => { CreatedFrom.CreditLimit = value; });
            }
		}
		public decimal Balance
		{
			get { return _Balance;}
			set { SetProp("Balance", ref _Balance, value,
                () => { CreatedFrom.Balance = value; });
            }
		}
		public decimal CreditAvail
		{
			get { return _CreditAvail;}
			set { SetProp("CreditAvail", ref _CreditAvail, value,
                () => { CreatedFrom.CreditAvail = value; });
            }
		}
		public string AcctStatus
		{
			get { return _AcctStatus;}
			set { SetProp("AcctStatus", ref _AcctStatus, value,
                () => { CreatedFrom.AcctStatus = value; });
            }
		}
		public int NetDueDays
		{
			get { return _NetDueDays;}
			set { SetProp("NetDueDays", ref _NetDueDays, value,
                () => { CreatedFrom.NetDueDays = value; });
            }
		}
		public decimal InterestPercent
		{
			get { return _InterestPercent;}
			set { SetProp("InterestPercent", ref _InterestPercent, value,
                () => { CreatedFrom.InterestPercent = value; });
            }
		}
		public char PayMethodID
		{
			get { return _PayMethodID;}
			set { SetProp("PayMethodID", ref _PayMethodID, value,
                () => { CreatedFrom.PayMethodID = value; });
            }
		}
		public string CardType
		{
			get { return _CardType;}
			set { SetProp("CardType", ref _CardType, value,
                () => { CreatedFrom.CardType = value; });
            }
		}
		public string CardNo
		{
			get { return _CardNo;}
			set { SetProp("CardNo", ref _CardNo, value,
                () => { CreatedFrom.CardNo = value; });
            }
		}
		public short CardExpMonth
		{
			get { return _CardExpMonth;}
			set { SetProp("CardExpMonth", ref _CardExpMonth, value,
                () => { CreatedFrom.CardExpMonth = value; });
            }
		}
		public int CardExpYear
		{
			get { return _CardExpYear;}
			set { SetProp("CardExpYear", ref _CardExpYear, value,
                () => { CreatedFrom.CardExpYear = value; });
            }
		}
		public string NameOnCard
		{
			get { return _NameOnCard;}
			set { SetProp("NameOnCard", ref _NameOnCard, value,
                () => { CreatedFrom.NameOnCard = value; });
            }
		}
		public string Comments
		{
			get { return _Comments;}
			set { SetProp("Comments", ref _Comments, value,
                () => { CreatedFrom.Comments = value; });
            }
		}
		public short HeldCycleCount
		{
			get { return _HeldCycleCount;}
			set { SetProp("HeldCycleCount", ref _HeldCycleCount, value,
                () => { CreatedFrom.HeldCycleCount = value; });
            }
		}
		public DateTime EditDateTime
		{
			get { return _EditDateTime;}
			set { SetProp("EditDateTime", ref _EditDateTime, value,
                () => { CreatedFrom.EditDateTime = value; });
            }
		}
		public string LastEditEmp
		{
			get { return _LastEditEmp;}
			set { SetProp("LastEditEmp", ref _LastEditEmp, value,
                () => { CreatedFrom.LastEditEmp = value; });
            }
		}
		public DateTime LastShipDate
		{
			get { return _LastShipDate;}
			set { SetProp("LastShipDate", ref _LastShipDate, value,
                () => { CreatedFrom.LastShipDate = value; });
            }
		}
		public string BillToAttnName
		{
			get { return _BillToAttnName;}
			set { SetProp("BillToAttnName", ref _BillToAttnName, value,
                () => { CreatedFrom.BillToAttnName = value; });
            }
		}
		public string BillToAddress1
		{
			get { return _BillToAddress1;}
			set { SetProp("BillToAddress1", ref _BillToAddress1, value,
                () => { CreatedFrom.BillToAddress1 = value; });
            }
		}
		public string BillToAddress2
		{
			get { return _BillToAddress2;}
			set { SetProp("BillToAddress2", ref _BillToAddress2, value,
                () => { CreatedFrom.BillToAddress2 = value; });
            }
		}
		public string BillToCity
		{
			get { return _BillToCity;}
			set { SetProp("BillToCity", ref _BillToCity, value,
                () => { CreatedFrom.BillToCity = value; });
            }
		}
		public string BillToState
		{
            get { return SafeTrim(_BillToState); }
			set { SetProp("BillToState", ref _BillToState, value,
                () => { CreatedFrom.BillToState = value; });
            }
		}
		public string BillToZip
		{
            get { return SafeTrim(_BillToZip); }
			set
			{
                var digitsOnly = ToDigitsOnly(value);
                SetProp("BillToZip", ref _BillToZip, digitsOnly,
                    () => { CreatedFrom.BillToZip = digitsOnly; });
			}
		}
		public string PhyAddress1
		{
			get { return _PhyAddress1;}
			set { SetProp("PhyAddress1", ref _PhyAddress1, value,
                () => { CreatedFrom.PhyAddress1 = value; });
            }
		}
		public string PhyAddress2
		{
			get { return _PhyAddress2;}
			set { SetProp("PhyAddress2", ref _PhyAddress2, value,
                () => { CreatedFrom.PhyAddress2 = value; });
            }
		}
		public string PhyCity
		{
			get { return _PhyCity;}
			set { SetProp("PhyCity", ref _PhyCity, value,
                () => { CreatedFrom.PhyCity = value; });
            }
		}
		public string PhyState
		{
            get { return _PhyState; }
			set { SetProp("PhyState", ref _PhyState, value,
                () => { CreatedFrom.PhyState = value; });
            }
		}
		public string PhyZip
		{
            get { return SafeTrim(_PhyZip); }
			set
			{
                var digitsOnly = ToDigitsOnly(value);
                SetProp("PhyZip", ref _PhyZip, digitsOnly,
                    () => { CreatedFrom.PhyZip = value; });
			}
		}
		public bool WaivePUFee
		{
			get { return _WaivePUFee;}
			set { SetProp("WaivePUFee", ref _WaivePUFee, value,
                () => { CreatedFrom.WaivePUFee = value; });
            }
		}
		public string LocCode
		{
			get { return _LocCode;}
            set
            {
                SetProp("LocCode", ref _LocCode, value,
                    () => { CreatedFrom.LocCode = value; });
            }
		}
		public bool WaiveFuelFee
		{
			get { return _WaiveFuelFee;}
			set { SetProp("WaiveFuelFee", ref _WaiveFuelFee, value,
                () => { CreatedFrom.WaiveFuelFee = value; });
            }
		}
		public short PageCount
		{
			get { return _PageCount;}
			set { SetProp("PageCount", ref _PageCount, value,
                () => { CreatedFrom.PageCount = value; });
            }
		}
		public int BillToID
		{
			get { return _BillToID;}
			set { SetProp("BillToID", ref _BillToID, value,
                () => { CreatedFrom.BillToID = value; });
            }
		}
		public bool EDICustomer
		{
			get { return _EDICustomer;}
			set { SetProp("EDICustomer", ref _EDICustomer, value,
                () => { CreatedFrom.EDICustomer = value; });
            }
		}
		public DateTime LastStatusEditDate
		{
			get { return _LastStatusEditDate;}
			set { SetProp("LastStatusEditDate", ref _LastStatusEditDate, value,
                () => { CreatedFrom.LastStatusEditDate = value; });
            }
		}
		public string LastStatusEditEmp
		{
			get { return _LastStatusEditEmp;}
			set { SetProp("LastStatusEditEmp", ref _LastStatusEditEmp, value,
                () => { CreatedFrom.LastStatusEditEmp = value; });
            }
		}
		public bool OnHoldOverride
		{
			get { return _OnHoldOverride;}
			set { SetProp("OnHoldOverride", ref _OnHoldOverride, value,
                () => { CreatedFrom.OnHoldOverride = value; });
            }
		}
		public decimal First60DayRev
		{
			get { return _First60DayRev;}
			set { SetProp("First60DayRev", ref _First60DayRev, value,
                () => { CreatedFrom.First60DayRev = value; });
            }
		}
		public bool Hit
		{
			get { return _Hit;}
			set { SetProp("Hit", ref _Hit, value,
                () => { CreatedFrom.Hit = value; });
            }
		}
		public int LinkedCustID
		{
			get { return _LinkedCustID;}
			set { SetProp("LinkedCustID", ref _LinkedCustID, value,
                () => { CreatedFrom.LinkedCustID = value; });
            }
		}
		public bool WaiveResDelivFee
		{
			get { return _WaiveResDelivFee;}
			set { SetProp("WaiveResDelivFee", ref _WaiveResDelivFee, value,
                () => { CreatedFrom.WaiveResDelivFee = value; });
            }
		}
		public bool StarCustomer
		{
			get { return _StarCustomer;}
			set { SetProp("StarCustomer", ref _StarCustomer, value,
                () => { CreatedFrom.StarCustomer = value; });
            }
		}
		public bool WaiveDimWt
		{
			get { return _WaiveDimWt;}
			set { SetProp("WaiveDimWt", ref _WaiveDimWt, value,
                () => { CreatedFrom.WaiveDimWt = value; });
            }
		}
		public bool KnownShipper
		{
			get { return _KnownShipper;}
			set { SetProp("KnownShipper", ref _KnownShipper, value,
                () => { CreatedFrom.KnownShipper = value; });
            }
		}
		public string ReferralRep
		{
			get { return _ReferralRep;}
			set { SetProp("ReferralRep", ref _ReferralRep, value,
                () => { CreatedFrom.ReferralRep = value; });
            }
		}
		public bool BillingRefRequired
		{
			get { return _BillingRefRequired;}
			set { SetProp("BillingRefRequired", ref _BillingRefRequired, value,
                () => { CreatedFrom.BillingRefRequired = value; });
            }
		}
		public int ParentCustID
		{
			get { return _ParentCustID;}
			set { SetProp("ParentCustID", ref _ParentCustID, value,
                () => { CreatedFrom.ParentCustID = value; });
            }
		}
		public bool InheritDiscounts
		{
			get { return _InheritDiscounts;}
			set { SetProp("InheritDiscounts", ref _InheritDiscounts, value,
                () => { CreatedFrom.InheritDiscounts = value; });
            }
		}
		public string WaiveSignatureFee
		{
			get { return _WaiveSignatureFee;}
			set { SetProp("WaiveSignatureFee", ref _WaiveSignatureFee, value,
                () => { CreatedFrom.WaiveSignatureFee = value; });
            }
		}
		public bool WaiveRemoteDeliveryFee
		{
			get { return _WaiveRemoteDeliveryFee;}
			set { SetProp("WaiveRemoteDeliveryFee", ref _WaiveRemoteDeliveryFee, value,
                () => { CreatedFrom.WaiveRemoteDeliveryFee = value; });
            }
		}
		public bool WaiveManualProcessingFee
		{
			get { return _WaiveManualProcessingFee;}
			set { SetProp("WaiveManualProcessingFee", ref _WaiveManualProcessingFee, value,
                () => { CreatedFrom.WaiveManualProcessingFee = value; });
            }
		}
		public bool WaiveOnCallFee
		{
			get { return _WaiveOnCallFee;}
			set { SetProp("WaiveOnCallFee", ref _WaiveOnCallFee, value,
                () => { CreatedFrom.WaiveOnCallFee = value; });
            }
		}

        public bool DisallowSimplePricing
        {
            get { return _DisallowSimplePricing; }
            set
            {
                SetProp("DisallowSimplePricing", ref _DisallowSimplePricing, value,
                    () => { CreatedFrom.DisallowSimplePricing = value; });
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

        public bool DeclaredValueSignature
        {
            get { return _DeclaredValueSignature; }
            set
            {
                SetProp("DeclaredValueSignature", ref _DeclaredValueSignature, value,
                    () => { CreatedFrom.DeclaredValueSignature = value; });
            }
        }

        public int TieredDiscountWindow
        {
            get { return _TieredDiscountWindow; }
            set
            {
                SetProp("TieredDiscountWindow", ref _TieredDiscountWindow, value,
                    () => { CreatedFrom.TieredDiscountWindow = value; });
            }
        }

        public bool WaiveSecuredFacilityFee
        {
            get { return _WaiveSecuredFacilityFee; }
            set
            {
                SetProp("WaiveSecuredFacilityFee", ref _WaiveSecuredFacilityFee, value,
                    () => { CreatedFrom.WaiveSecuredFacilityFee = value; });
            }
        }

        public bool WaivePickupEventFee
        {
            get { return _WaivePickupEventFee; }
            set
            {
                SetProp("WaivePickupEventFee", ref _WaivePickupEventFee, value,
                    () => { CreatedFrom.WaivePickupEventFee = value; });
            }
        }
                       

        public bool CCOptOut
        {
            get { return _CCOptOut; }
            set {
                /* Note: Code generally relies on Linq to create the associated NewCustomerOption,
                 *       but there are two cases that need to be handled
                 * First, many customers exist that do not have a NewCustomerOption yet. That case is handled below.
                 * Second, a completely new customer. That case is handled in LsoCustomerFactory.
                 */
                if (CreatedFrom != null && CreatedFrom.NewCustomerOptions == null)
                {
                    CreatedFrom.NewCustomerOptions = new NewCustomerOption()
                    {
                        CustID = this.CustID,
                        CCOptOut = false
                    };
                }
                SetProp("CCOptOut", ref _CCOptOut, value,
                        () => { CreatedFrom.NewCustomerOptions.CCOptOut = value; });
            }
        }

	}
}
