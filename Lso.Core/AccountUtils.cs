using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using AutoMapper;


namespace Lso.Core
{
    
    public class AccountUtils
    {
        // TODO: Remove this once we have all domain objects needed
        // removes *all* non digit [0-9] characters from a string
        private static string ToDigitsOnly(string input)
        {
            string digitsOnly = "";
            if (input != null)
            {
                var regEx = new Regex(@"[^0-9]");
                digitsOnly = regEx.Replace(input, String.Empty);
            }
            return digitsOnly;
        }

        private static void ClearFields(AccountsSearchResponse item)
        {
            item.BillToAddress1 = "";
            item.CustContactName = "";
            item.CustContactPhone = "";
            item.CustomerId = 0;
            item.PhyAddress1 = "";            
        }
        

        public static IList<AccountsSearchResponse> PublicSearchForDuplicateAccounts(AccountsCreationRequest request)
        {
            // TODO: Possibly additional error checking here to make sure no info is leaked
            return SearchForDuplicateAccounts(request)
                .Select(resp => new AccountsSearchResponse()
                                    {
                                        SafeName = resp.SafeName,
                                        CustName = GetSubString(resp.CustName, 5)
                                    })
                .Take(30).ToArray();
        }

        public static IList<GroupSearchResponse> PublicSearchForDuplicateGroups(GroupCreationRequest request)
        {
            // TODO: Possibly additional error checking here to make sure no info is leaked
            return SearchForDuplicateGroups(request)
                .Select(resp => new GroupSearchResponse()
                                    {
                                        GroupId = resp.GroupId,
                                        GroupName = GetSubString(resp.GroupName, 5)
                                    })
                .Take(30).ToArray();
        }

        private static string GetSubString(string input, int count)
        {
            var retval = "";
            if (String.IsNullOrEmpty(input))
                return retval;
            retval = input.Substring(0, count > input.Length ? input.Length : count);
            return retval;
        }

        // TODO: This needs to probably be an entire class, the names in the DB are far too many
        // TODO: formats
        private static string GetSafeName(ILsoCustomer cust)
        {
            var retval = "";

            if (cust == null || String.IsNullOrEmpty(cust.CustContactName))
                return retval;

            var trimmedName = cust.CustContactName.Trim();

            var split = trimmedName.Split(' ');
            if (split.Length > 1)
            {
                retval = split[0] + " " + split[1].Substring(0, Math.Min(1, split[1].Length));
            } else
            {
                retval = cust.CustContactName;
            }

            return retval;
        }

        public static FullCustomerDataResponse GetFullCustomerData(FullCustomerDataRequest request)
        {       
            var response = new FullCustomerDataResponse();
            try
            {
                var customerRepository = RepositoryFactory.GetNewCustomerRepository();
                var webshipRepository = RepositoryFactory.GetNewWebshipAccountRepository();
                

                // Get Customer record
                var custresult = customerRepository.GetAllWithCustId(request.CustID);
                if (custresult.Any())
                {
                    response.CustRecord = custresult.First();
                }

                // Get WebshipAccounts
                var wsresult = webshipRepository.GetAllWithCustomerId(request.CustID);
                if (wsresult.Any())
                {
                    response.UserProfiles = new List<IWebshipAccount>();
                    response.UserProfiles.AddRange(wsresult);
                }

                // Get Base discounts  
                response.BaseDiscounts = new List<BaseDiscount>();                
                var baseres = customerRepository.GetBaseDiscounts(request.CustID);
                response.BaseDiscounts.AddRange(
                baseres.Select(LclConvert).ToList());

                // Get Tiered Discounts
                response.TieredRevenueDiscounts = new List<TieredRevenueDiscount>();
                var tierres = customerRepository.GetTieredRevenueDiscounts(request.CustID);
                response.TieredRevenueDiscounts.AddRange(
                    tierres.Select(LclConvert).ToList());

                // Get Weight Discounts
                response.WeightDiscounts = new List<WeightDiscount>();
                var wghtres = customerRepository.GetWeightDiscounts(request.CustID);
                response.WeightDiscounts.AddRange(
                    wghtres.Select(LclConvert).ToList());

                // Get Zone Discounts
                response.ZoneDiscounts = new List<ZoneDiscount>();
                var zoneres = customerRepository.GetZoneDiscounts(request.CustID);
                response.ZoneDiscounts.AddRange(
                    zoneres.Select(LclConvert).ToList());

                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ErrMessage = ex.Message + " " + ex.StackTrace;
            }
            return response;
        }

        // TODO: Could following 4 functions be refactored using template?

        // converts LINQ SQL type to generic DTO
        private static BaseDiscount LclConvert(sp_GetCustomerBaseDiscountsResult input)
        {
            Mapper.CreateMap<sp_GetCustomerBaseDiscountsResult, BaseDiscount>();

            var retval = new BaseDiscount();

            Mapper.Map<sp_GetCustomerBaseDiscountsResult, BaseDiscount>(input, retval);
            Mapper.AssertConfigurationIsValid();            

            return retval;
        }

        // converts LINQ SQL type to generic DTO
        private static TieredRevenueDiscount LclConvert(sp_GetCustomerTierRevenueDiscountsResult input)
        {
            Mapper.CreateMap<sp_GetCustomerTierRevenueDiscountsResult, TieredRevenueDiscount>();
                

            var retval = new TieredRevenueDiscount();

            Mapper.Map<sp_GetCustomerTierRevenueDiscountsResult, TieredRevenueDiscount>(input, retval);
            Mapper.AssertConfigurationIsValid();

            return retval;
        }

        // converts LINQ SQL type to generic DTO
        private static WeightDiscount LclConvert(sp_GetCustomerWeightDiscountsResult input)
        {
            Mapper.CreateMap<sp_GetCustomerWeightDiscountsResult, WeightDiscount>();

            var retval = new WeightDiscount();

            Mapper.Map<sp_GetCustomerWeightDiscountsResult, WeightDiscount>(input, retval);
            Mapper.AssertConfigurationIsValid();

            return retval;
        }
        
        // converts LINQ SQL type to generic DTO
        private static ZoneDiscount LclConvert(sp_GetCustomerZoneDiscountsResult input)
        {
            Mapper.CreateMap<sp_GetCustomerZoneDiscountsResult, ZoneDiscount>();

            var retval = new ZoneDiscount();

            Mapper.Map<sp_GetCustomerZoneDiscountsResult, ZoneDiscount>(input, retval);
            Mapper.AssertConfigurationIsValid();

            return retval;
        }


        public static IList<AccountsSearchResponse> SearchForDuplicateAccounts(AccountsCreationRequest request)
        {
            var result = new List<AccountsSearchResponse>();
            var customerRepository = RepositoryFactory.GetNewCustomerRepository();

            
            result.AddRange(
                customerRepository.GetAllWithDataLike(request.CompanyName, request.RequesterFirstName + " " + request.RequesterLastName, request.CompanyBillAddress1, request.RequesterPhone, request.CompanyPhyAddress1)
                    .Select(cust => new AccountsSearchResponse()
                    {
                        SafeName = GetSafeName(cust),
                        BillToAddress1 = cust.BillToAddress1,
                        CustContactName = cust.CustContactName,
                        CustContactPhone = cust.CustContactPhone,
                        CustName = cust.CustName,
                        CustomerId = cust.CustID,
                        PhyAddress1 = cust.PhyAddress1,
                        Success = true
                    }));
                              
            // return unique customerIDs only
            return result.Distinct().ToList();
        }

        public static IList<GroupSearchResponse> SearchForDuplicateGroups(GroupCreationRequest request)
        {
            var result = new List<GroupSearchResponse>();
            var groupRepository = RepositoryFactory.GetNewLsoIGroupRepository();

            result.AddRange(
                groupRepository.GetAllWithDataLike(request.UID, request.GroupName, request.GroupDescr)
                    .Select(group => new GroupSearchResponse()
                    {
                        UID = group.UID,
                        GroupId = group.GroupId,
                        GroupDescr = group.GroupDescr,
                        GroupName = group.GroupName,
                        Success = true
                    }));

            // return unique groupIds only
            return result.Distinct().ToList();
        }


        // Creates Customer account and on success creates a new webship account for it
        public static AccountsCreationResponse CreateAccounts(AccountsCreationRequest request)
        {
            // TODO: Some of the tables updated in webship account creation do not have domain objects
            // TODO: So they lack code to prevent killing the database insert with bad data
            // TODO: For now, just scrub the fields in question here
            request.BillingZip = ToDigitsOnly(request.BillingZip);
            request.CompanyBillZip = ToDigitsOnly(request.CompanyBillZip);
            request.CCNumber = ToDigitsOnly(request.CCNumber);
            request.RequesterPhone = ToDigitsOnly(request.RequesterPhone);
            request.CompanyPhone = ToDigitsOnly(request.CompanyPhone);
            request.CompanyPhyZip = ToDigitsOnly(request.CompanyPhyZip);
            
            var response = new AccountsCreationResponse();
            var custfactory = new LsoCustomerFactory();
            var webacctfactory = new WebshipAccountFactory();
            
            var cust = custfactory.Create(request);
            var webacct = webacctfactory.Create(request);
            
            
            // TODO: make a validator class that looks at all data in these new
            // domain instances and either run it on them, or weave it into the 
            // property set code in each domain object

            var customerRepository = RepositoryFactory.GetNewCustomerRepository();
            var webshipAccountRepository = RepositoryFactory.GetNewWebshipAccountRepository();

            response.Success = true;

            // TODO: We don't have validation in place, so bad data errors will be lost with this current implementation
            try
            {
                // TODO: With different databases, and the repository pattern we don't get transactions
                // TODO: We will need to modify this code to catch errors and remove spurious accounts
                // TODO: Or add a transaction pattern
                customerRepository.Add(cust);
                webacct.CustID = cust.CustID;                
                webshipAccountRepository.Add(webacct);
                response.CustomerId = webacct.CustID;
                response.LoginName = webacct.LoginName;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors = new ValidationErrors();
                response.Errors.ErrorsMessage = new List<string>();
                var trace = new System.Diagnostics.StackTrace(ex, true);
               
                response.Errors.ErrorsMessage.Add(ex.Message + " " + trace.GetFrame(0).GetMethod().Name + " " + "Line: " + trace.GetFrame(0).GetFileLineNumber() + " " + "Column: " + trace.GetFrame(0).GetFileColumnNumber());
                if (ex.InnerException != null)
                {
                    var trace2 = new System.Diagnostics.StackTrace(ex.InnerException, true);
                    response.Errors.ErrorsMessage.Add(ex.InnerException.Message + " " + trace2.GetFrame(0).GetMethod().Name + " " + "Line: " + trace2.GetFrame(0).GetFileLineNumber() + " " + "Column: " + trace2.GetFrame(0).GetFileColumnNumber());
                }
            }
                                       
            return response;
        }        

        
    }
}
