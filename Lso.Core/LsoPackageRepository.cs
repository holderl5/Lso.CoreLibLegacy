using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class LsoPackageRepository : ILsoPackageRepository
    {
        protected OpsDbDataContext _Db;

        public LsoPackageRepository()
        {
            string connectionString =
                "Data Source=10.0.5.14;Initial Catalog=LsoCoreTestOps;Persist Security Info=True;User ID=L5development;Password=L5SQLCool";
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Lso.Core.LsoCoreSettings.OpsDBConnString"].ConnectionString;
            }
            catch (Exception ex)
            {
                // this is a bandaid for unit testing so nothing for now
            }

            DoConstruction(connectionString);
        }

        public LsoPackageRepository(string connectionString)
        {
            DoConstruction(connectionString);
        }

        protected void DoConstruction(string connectionString)
        {
            _Db = new OpsDbDataContext(connectionString);

            if (!_Db.DatabaseExists())
            {
				// TODO, throw in .TOstring because this doesn't actually work right as it is
                throw new RepositoryDoesNotExistException("LsoPackageRepository.DatabaseExists returns false with the following connection string: " + _Db.Connection);
            } 
        }

        public IList<ILsoPackage> GetAllWithAirbillNo(string AirbillNo)
        {
            // it appears executequery only returns IEnumerable, so working with a list at each point
            // where we check results, results should be limited to 1 row, so not a big waste of 
            // resources to do this
            IEnumerable<Package> packages = from p in _Db.Packages
                           where p.AirbillNo == AirbillNo
                           select p;
            var packagesList = packages.ToList();

            // If we got no packages back, we need to try the PkgHistory table next            
            if (packagesList.Count() == 0)
            {                
                // Query PkgHistory and return type as a Package by omitting extra fields and
                // fixing a few unintentional incompatibilities
                // This is to avoid doing a lot of new domain class work at this time
                packages = _Db.ExecuteQuery<Package>(
                    @"SELECT AirbillNo,PickUpLocID,DestinationLocID,BillToCustID,
                                  PaymentType,CardType,ExpMonth,ExpYear,CardNumber,NameOnCard,
                                  ServiceType,ReleaseSignature,AuthorizedReleaseName,
                                  LetterType,PkgWeight,Pieces,Length,Width,Height,WgtVolume,
                                  DeclaredValue,BillingRef,RegCharge,AddInsCharge,ServiceCharge,
                                  TotalCharge,PickupHubCity,PickupDate,PickupTime,PickupBoxID,
                                  PickupRoute,PickupEmpNum,PickupComments,PickupInputDateTime,
                                  PickupInputEmpNum,Transmitted,Carrier,
                                  CONVERT(smallint,DelivRouteStop) as DelivRouteStop,
                                  DelivRoute,DelivStatus,ArriveHubCode,ArriveDateTime,
                                  ArriveEmpNum,DelivDateTime,DelivSignatureName,DelivEmpNum,
                                  DelivComments,DelivInputDateTime,DelivInputEmpNum,
                                  SrvFailLateCode,OtherChargesOne,OtherChargesTwo,ToAttnName,
                                  FromAttnName,Comments,ToCoName,ToAddress1,ToAddress2,ToCity,
                                  ToState,ToZip,ToPhone,FromcoName,FromAddress1,FromAddress2,
                                  FromCity,FromState,FromZip,FromPhone,User1,User2,User3,User4,
                                  User5,User6,User7,User8,ResidentialDelivery,Zone,PickupFee,
                                  ResDelivFee,LSOPackingUsed,BillingRef2,BillingRef3,
                                  BillingRef4,SystemGenerated,Discount,PromoCode,
                                  SignatureRequirement,SignatureFee,RemoteDeliveryFee,
                                  ManualProcessingFee,OnCallFeeAssessed,DispatchID,SecurityFee,
                                  UseSimplePricing
                                  FROM PkgHistory
                                  WHERE AirbillNo = {0}", AirbillNo);
                packagesList = packages.ToList();
            }

            if (packagesList.Count() > 0)
            {
                var converter = new LsoPackageFactory();
                var dompackage = converter.Create(packagesList.First());
                var actpackage = (LsoPackage)dompackage;
                actpackage.GetTrackingDetail = airbillno => GetTrackingData(airbillno);
                var retval = new List<ILsoPackage>();
                retval.Add(actpackage);                
                return retval;
            }
            
            return new List<ILsoPackage>();
        }

        private List<TrackingDatum> GetTrackingData(string AirbillNo)
        {
            var retval = new List<TrackingDatum>();

            var rawdata = _Db.sp_GetTrackingData(AirbillNo);
            
            foreach (var spGetTrackingDataResult in rawdata)
            {
                DateTime t = (spGetTrackingDataResult.EventTimeStamp.HasValue) ? spGetTrackingDataResult.EventTimeStamp.Value : new DateTime(2011,01,01,13,0,0);
                
                retval.Add(new TrackingDatum()
                {
                    StatusCode = spGetTrackingDataResult.StatusCode.ToString(),
                    StatusDescription = spGetTrackingDataResult.StatusDescription,
                    TrackingDate =  t,
                    TrackingTableCode = spGetTrackingDataResult.TrackingCode,
                    City = spGetTrackingDataResult.City
                });
            }                
            

            return retval;

            
        }
    }
}
