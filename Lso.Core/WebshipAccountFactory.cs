using System;
using System.Reflection;
using AutoMapper;

namespace Lso.Core
{
    public class WebshipAccountFactory
    {
        // Creates a webshipaccount from the LINQtoSQL data class
        public IWebshipAccount Create(UserProfile data)
        {                                    
            Mapper.CreateMap<UserProfile, WebshipAccount>();
            var retval = new WebshipAccount();
            // TODO: This needs to be changed into a more general way of checking the object
            retval.AllowAutomapExceptions = true;
            Mapper.Map<UserProfile, WebshipAccount>(data, retval);            
            Mapper.AssertConfigurationIsValid();

            retval.AllowAutomapExceptions = false;

            // save the LINQ to SQL object into this domain object to make updates easier
            retval.CreatedFrom = data;

            // reset change tracking
            retval.AcceptChanges();

            return retval;
        }

        public IWebshipAccount Create(AccountsCreationRequest data)
        {
            var webacct = new WebshipAccount();
            Mapper.Reset();
            Mapper.CreateMap<AccountsCreationRequest, WebshipAccount>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.RequesterFirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.RequesterLastName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.RequesterEmail))
                .ForMember(dest => dest.CompanyAddress1, opt => opt.MapFrom(src => src.CompanyPhyAddress1))
                .ForMember(dest => dest.CompanyAddress2, opt => opt.MapFrom(src => src.CompanyPhyAddress2))
                .ForMember(dest => dest.CompanyCity, opt => opt.MapFrom(src => src.CompanyPhyCity))
                .ForMember(dest => dest.CompanyState, opt => opt.MapFrom(src => src.CompanyPhyState))
                .ForMember(dest => dest.CompanyZip, opt => opt.MapFrom(src => src.CompanyPhyZip))
                .ForMember(dest => dest.UID, opt => opt.Ignore())                
                .ForMember(dest => dest.Administrator, opt => opt.Ignore())
                .ForMember(dest => dest.CustID, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginDate, opt => opt.Ignore())
                .ForMember(dest => dest.LoginsToDate, opt => opt.Ignore())
                .ForMember(dest => dest.LoginAttemptsToday, opt => opt.Ignore())
                .ForMember(dest => dest.LoginFailedToday, opt => opt.Ignore())
                .ForMember(dest => dest.PWDChangedLast, opt => opt.Ignore())
                .ForMember(dest => dest.PWDResentLast, opt => opt.Ignore())
                .ForMember(dest => dest.PWDResentCount, opt => opt.Ignore())
                .ForMember(dest => dest.PWDResentToday, opt => opt.Ignore())
                .ForMember(dest => dest.ABEntryToday, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.AccountLockout, opt => opt.Ignore())
                .ForMember(dest => dest.PrintToLabel, opt => opt.Ignore())
                .ForMember(dest => dest.DefaultService, opt => opt.Ignore())
                .ForMember(dest => dest.BillingRefRequired, opt => opt.Ignore())
                .ForMember(dest => dest.EmailPOD, opt => opt.Ignore())
                .ForMember(dest => dest.SuperWebShipper, opt => opt.Ignore())
                .ForMember(dest => dest.UseLocBillingRef, opt => opt.Ignore())
                .ForMember(dest => dest.HandlingFee, opt => opt.Ignore())
                .ForMember(dest => dest.PrintPublishedRates, opt => opt.Ignore())
                .ForMember(dest => dest.IsUserAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.ShowOnlyUserShipments, opt => opt.Ignore())
                .ForMember(dest => dest.DisableBillingReferenceRequired, opt => opt.Ignore())
                .ForMember(dest => dest.HardCodeBillingRef, opt => opt.Ignore())
                .ForMember(dest => dest.HardCodedBillingRefValue, opt => opt.Ignore())
                .ForMember(dest => dest.HardCodeBillingRef2, opt => opt.Ignore())
                .ForMember(dest => dest.HardCodedBillingRefValue2, opt => opt.Ignore())
                .ForMember(dest => dest.newCustomerReferralSourceID, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityNotes, opt => opt.Ignore());
                            
            webacct.AllowAutomapExceptions = true;
            Mapper.Map<AccountsCreationRequest, WebshipAccount>(data, webacct);
            Mapper.AssertConfigurationIsValid();
            webacct.AllowAutomapExceptions = false;

            return webacct;


        }

        // Update the database
        static public void Update(IWebshipAccount item)
        {
            item.Update();
        }
    }
}
