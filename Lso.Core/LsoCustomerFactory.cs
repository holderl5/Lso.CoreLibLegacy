using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Lso.Core
{
    public class LsoCustomerFactory
    {
        // Creates a LsoCustomer from the LINQtoSQL data class
        public ILsoCustomer Create(Customer data)
        {
            Mapper.CreateMap<Customer, LsoCustomer>()
                .ForMember(dest => dest.CCOptOut, opt => opt.MapFrom(src => src.NewCustomerOptions == null ? false : src.NewCustomerOptions.CCOptOut));
            var retval = new LsoCustomer();
            // TODO: This needs to be changed into a more general way of checking the object
            retval.AllowAutomapExceptions = true;
            Mapper.Map<Customer, LsoCustomer>(data, retval);
            Mapper.AssertConfigurationIsValid();

            retval.AllowAutomapExceptions = false;

            // save the LINQ to SQL object into this domain object to make updates easier
            retval.CreatedFrom = data;

            // reset change tracking
            retval.AcceptChanges();

            return retval;
        }

        public ILsoCustomer Create(AccountsCreationRequest data)
        {
            var cust = new LsoCustomer(); 
			string contactName = data.RequesterFirstName + " " + data.RequesterLastName;

            Mapper.Reset();

            // TODO: customer object does not have a company phone
            Mapper.CreateMap<AccountsCreationRequest, LsoCustomer>()
                .ForMember(dest => dest.CustContactName, opt => opt.UseValue(contactName))
                .ForMember(dest => dest.CustContactPhone, opt => opt.MapFrom(src => src.RequesterPhone))
                .ForMember(dest => dest.CustName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.PhyAddress1, opt => opt.MapFrom(src => src.CompanyPhyAddress1))
                .ForMember(dest => dest.PhyAddress2, opt => opt.MapFrom(src => src.CompanyPhyAddress2))
                .ForMember(dest => dest.PhyCity, opt => opt.MapFrom(src => src.CompanyPhyCity))
                .ForMember(dest => dest.PhyState, opt => opt.MapFrom(src => src.CompanyPhyState))
                .ForMember(dest => dest.PhyZip, opt => opt.MapFrom(src => src.CompanyPhyZip))
                .ForMember(dest => dest.BillToAttnName, opt => opt.UseValue(contactName))
                .ForMember(dest => dest.BillToAddress1, opt => opt.MapFrom(src => src.CompanyBillAddress1))
                .ForMember(dest => dest.BillToAddress2, opt => opt.MapFrom(src => src.CompanyBillAddress2))
                .ForMember(dest => dest.BillToCity, opt => opt.MapFrom(src => src.CompanyBillCity))
                .ForMember(dest => dest.BillToState, opt => opt.MapFrom(src => src.CompanyBillState))
                .ForMember(dest => dest.BillToZip, opt => opt.MapFrom(src => src.CompanyBillZip))
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CCType))
                .ForMember(dest => dest.CardNo, opt => opt.MapFrom(src => src.CCNumber))
                .ForMember(dest => dest.CardExpMonth, opt => opt.MapFrom(src => src.CCExpMonth))
                .ForMember(dest => dest.CardExpYear, opt => opt.MapFrom(src => src.CCExpYear))
                .ForMember(dest => dest.NameOnCard, opt => opt.MapFrom(src => src.CCName))
                .ForMember(dest => dest.CustID, opt => opt.Ignore())
                .ForMember(dest => dest.PriceSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.DateOpened, opt => opt.Ignore())
                .ForMember(dest => dest.LSOContactEmpNum, opt => opt.Ignore())
                .ForMember(dest => dest.SalesRepEmpNum, opt => opt.Ignore())
                .ForMember(dest => dest.RestartDate, opt => opt.Ignore())
                .ForMember(dest => dest.SICCode, opt => opt.Ignore())
                .ForMember(dest => dest.DOWBillingCode, opt => opt.Ignore())
                .ForMember(dest => dest.CreditLimit, opt => opt.Ignore())
                .ForMember(dest => dest.Balance, opt => opt.Ignore())
                .ForMember(dest => dest.CreditAvail, opt => opt.Ignore())
                .ForMember(dest => dest.AcctStatus, opt => opt.Ignore())
                .ForMember(dest => dest.NetDueDays, opt => opt.Ignore())
                .ForMember(dest => dest.InterestPercent, opt => opt.Ignore())
                .ForMember(dest => dest.PayMethodID, opt => opt.Ignore())
                .ForMember(dest => dest.HeldCycleCount, opt => opt.Ignore())
                .ForMember(dest => dest.EditDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastEditEmp, opt => opt.Ignore())
                .ForMember(dest => dest.LastShipDate, opt => opt.Ignore())
                .ForMember(dest => dest.WaivePUFee, opt => opt.Ignore())
                .ForMember(dest => dest.LocCode, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveFuelFee, opt => opt.Ignore())
                .ForMember(dest => dest.PageCount, opt => opt.Ignore())
                .ForMember(dest => dest.BillToID, opt => opt.Ignore())
                .ForMember(dest => dest.EDICustomer, opt => opt.Ignore())
                .ForMember(dest => dest.LastStatusEditDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastStatusEditEmp, opt => opt.Ignore())
                .ForMember(dest => dest.OnHoldOverride, opt => opt.Ignore())
                .ForMember(dest => dest.First60DayRev, opt => opt.Ignore())
                .ForMember(dest => dest.Hit, opt => opt.Ignore())
                .ForMember(dest => dest.LinkedCustID, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveResDelivFee, opt => opt.Ignore())
                .ForMember(dest => dest.StarCustomer, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveDimWt, opt => opt.Ignore())
                .ForMember(dest => dest.KnownShipper, opt => opt.Ignore())
                .ForMember(dest => dest.ReferralRep, opt => opt.Ignore())
                .ForMember(dest => dest.BillingRefRequired, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCustID, opt => opt.Ignore())
                .ForMember(dest => dest.InheritDiscounts, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveSignatureFee, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveRemoteDeliveryFee, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveManualProcessingFee, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveOnCallFee, opt => opt.Ignore())
                .ForMember(dest => dest.DisallowSimplePricing, opt => opt.Ignore())
                .ForMember(dest => dest.newCustomerReferralSourceID, opt => opt.Ignore())
                .ForMember(dest => dest.DeclaredValueSignature, opt => opt.Ignore())
                .ForMember(dest => dest.TieredDiscountWindow, opt => opt.Ignore())
                .ForMember(dest => dest.WaiveSecuredFacilityFee, opt => opt.Ignore())
                .ForMember(dest => dest.WaivePickupEventFee, opt => opt.Ignore())
                .ForMember(dest => dest.CCOptOut, opt => opt.MapFrom(src => src.CCOptOut));


            cust.AllowAutomapExceptions = true;
            Mapper.Map<AccountsCreationRequest, LsoCustomer>(data, cust);

            Mapper.AssertConfigurationIsValid();
            cust.AllowAutomapExceptions = false;
			
            return cust;
        }

        // Update the database
        static public void Update(ILsoCustomer item)
        {
            item.Update();
        }
    }
}
