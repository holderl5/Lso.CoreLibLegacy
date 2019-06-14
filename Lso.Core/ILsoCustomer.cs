﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoCustomer
    {
        // The properties here match exactly with the database so that we can automate the mapping process
        // ReSharper disable InconsistentNaming

        // Fields from Customer
        int CustID { get; set; }
        string CustName { get; set; }
        string CustContactName { get; set; }
        string CustContactPhone { get; set; }
        int PriceSchedule { get; set; }
        DateTime DateOpened { get; set; }
        int LSOContactEmpNum { get; set; }
        string SalesRepEmpNum { get; set; }
        DateTime RestartDate { get; set; }
        string SICCode { get; set; }
        int HowHearAbout { get; set; }
        string DOWBillingCode { get; set; }
        decimal CreditLimit { get; set; }
        decimal Balance { get; set; }
        decimal CreditAvail { get; set; }
        string AcctStatus { get; set; }
        int NetDueDays { get; set; }
        decimal InterestPercent { get; set; }
        char PayMethodID { get; set; }
        string CardType { get; set; }
        string CardNo { get; set; }
        short CardExpMonth { get; set; }
        int CardExpYear { get; set; }
        string NameOnCard { get; set; }
        string Comments { get; set; }
        short HeldCycleCount { get; set; }
        DateTime EditDateTime { get; set; }
        string LastEditEmp { get; set; }
        DateTime LastShipDate { get; set; }
        string BillToAttnName { get; set; }
        string BillToAddress1 { get; set; }
        string BillToAddress2 { get; set; }
        string BillToCity { get; set; }
        string BillToState { get; set; }
        string BillToZip { get; set; }
        string PhyAddress1 { get; set; }
        string PhyAddress2 { get; set; }
        string PhyCity { get; set; }
        string PhyState { get; set; }
        string PhyZip { get; set; }
        bool WaivePUFee { get; set; }
        string LocCode { get; set; }
        bool WaiveFuelFee { get; set; }
        short PageCount { get; set; }
        int BillToID { get; set; }
        bool EDICustomer { get; set; }
        DateTime LastStatusEditDate { get; set; }
        string LastStatusEditEmp { get; set; }
        bool OnHoldOverride { get; set; }
        decimal First60DayRev { get; set; }
        bool Hit { get; set; }
        int LinkedCustID { get; set; }
        bool WaiveResDelivFee { get; set; }
        bool StarCustomer { get; set; }
        bool WaiveDimWt { get; set; }
        bool KnownShipper { get; set; }
        string ReferralRep { get; set; }
        bool BillingRefRequired { get; set; }
        int ParentCustID { get; set; }
        bool InheritDiscounts { get; set; }
        string WaiveSignatureFee { get; set; }
        bool WaiveRemoteDeliveryFee { get; set; }
        bool WaiveManualProcessingFee { get; set; }
        bool WaiveOnCallFee { get; set; }
        bool DisallowSimplePricing { get; set; }
        Guid newCustomerReferralSourceID { get; set; }
        bool DeclaredValueSignature { get; set; }
        int TieredDiscountWindow { get; set; }
        bool WaiveSecuredFacilityFee { get; set; }
        bool WaivePickupEventFee { get; set; }

        // NewCustomerOption
        bool CCOptOut { get; set; }

        // ReSharper restore InconsistentNaming

        // Tell the underlying implementation to update the database objects
        void Update();
    }
}
