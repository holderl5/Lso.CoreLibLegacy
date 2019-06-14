using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoMapper;

namespace Lso.Core
{
    public class WebShipAccountRepository : IWebshipAccountRepository
    {
        protected WebDbDataContext _Db;

        public WebShipAccountRepository()
        {
            string connectionString =
                "Data Source=10.0.5.14;Initial Catalog=LsoCoreTest;Persist Security Info=True;User ID=L5development;Password=L5SQLCool";
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Lso.Core.LsoCoreSettings.WebDBConnString"].ConnectionString;
            }
            catch (Exception ex)
            {
                // this is a bandaid for unit testing so nothing for now
            }

            DoConstruction(connectionString);
        }

        public WebShipAccountRepository(string connectionString)
        {
            DoConstruction(connectionString);
        }

        protected void DoConstruction(string connectionString)
        {
            _Db = new WebDbDataContext(connectionString);

            if (!_Db.DatabaseExists())
            {
                throw new RepositoryDoesNotExistException("WebshipAccountRepository.DatabaseExists returns false with the following connection string: " + _Db.Connection);
            } 
        }
        
        public IList<IWebshipAccount> GetAllWithUid(int UID)
        {            
            var accounts = from p in _Db.UserProfiles
                           where p.UID == UID
                           select p;
            var converter = new WebshipAccountFactory();
            return accounts.Select(userProfile => converter.Create(userProfile)).ToList();
            
        }

        public IList<IWebshipAccount> GetAllWithLoginNameCustId(string loginName, int custId)
        {
            var accounts = from p in _Db.UserProfiles
                           where p.LoginName.Trim() == loginName && p.CustID == custId
                           select p;
            var converter = new WebshipAccountFactory();
            return accounts.Select(userProfile => converter.Create(userProfile)).ToList();
        }

        public IList<IWebshipAccount> GetAllWithCustomerId(int custId)
        {
            var accounts = from p in _Db.UserProfiles
                           where p.CustID == custId
                           select p;
            var converter = new WebshipAccountFactory();
            return accounts.Select(userProfile => converter.Create(userProfile)).ToList();
        }

        public void Add(IWebshipAccount item)
        {            
            //
            // by convention new users are added to the newusers table, and
            // then added to the userprofile table
            // I believe newusers served a role in a manual process before, but am unsure
            //
            Mapper.Reset();
            Mapper.CreateMap<IWebshipAccount, NewUser>()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.RequestDate, opt => opt.Ignore());

            var newuser = Mapper.Map<IWebshipAccount, NewUser>(item);
            Mapper.AssertConfigurationIsValid();
            
            // Fill in data not automapped            
            newuser.CreatedDate = DateTime.Now;
            newuser.RequestDate = DateTime.Now;
            newuser.Created = true;
            // Per Bill set this to easily recognizable value
            newuser.CreatedBy = -1;

            _Db.NewUsers.InsertOnSubmit(newuser);


            //
            // The userprofile row is where the "webship account" is stored
            //
            var userprofile = new UserProfile();

            // We need access to the underlying implementation
            var actualitem = (WebshipAccount)item;
            actualitem.CreatedFrom = userprofile;  // this empty userprofile will be updated in the factory below                                   
            new WebshipAccountNewDefaults().SetDefaults(actualitem);
            WebshipAccountFactory.Update(actualitem);

            // queue for storage in DB
            _Db.UserProfiles.InsertOnSubmit(userprofile);

                       
            // there must be a row in customerProfiles to schedule a pickup
            // for now, leaving this awkwardness here since moving it would
            // make error handling that much harder
            customerProfile cp = null;
            var cprofile = from p in _Db.customerProfiles
                           where p.CustID == item.CustID
                           select p;
            if (!cprofile.Any())
            {
                // create customerprofile row
                cp = new customerProfile
                {
                    AccountLockout = false,
                    CustContactPhone = item.CompanyPhone,
                    CustID = item.CustID,
                    CustName = item.CompanyName,
                    PhyAddress1 = item.CompanyAddress1,
                    PhyAddress2 = item.CompanyAddress2,
                    KnownShipper = false,
                    PhyCity = item.CompanyCity,
                    PhyState = item.CompanyState,
                    PhyZip = item.CompanyZip
                };
                _Db.customerProfiles.InsertOnSubmit(cp);
            }

            // check to see if this object will fit into the DB columns
            try
            {
                _Db.TestSubmitChanges();
            }
            catch (InvalidOperationException ex)
            {
                // discard the changes
                _Db.DiscardInsertsAndDeletes();
                _Db.SubmitChanges();

                // rethrow the exception for the benefit of our caller);
                throw;
            }

            // If we made it to here, we can proceed safely
            // Get a new UID from the DB
            int? newUid = 0;
            _Db.sp_getuid(ref newUid);

            if (!newUid.HasValue || newUid == 0)
            {
                // discard the changes
                _Db.DiscardInsertsAndDeletes();
                _Db.SubmitChanges();
                throw new CannotCreateNewIdException("WebshipAccountRepository.Add: sp_getuid returned null or zero for the next UID");
            }

            // store customer id into objects before storing them in the DB
            newuser.UID = newUid.Value;
            item.UID = newUid.Value;
            actualitem.CreatedFrom.UID = newUid.Value;      
                                    
           
            _Db.SubmitChanges();



        }

        public void Update(IWebshipAccount item)
        {
            // TODO: make sure subsequent code additions do not change this
            // Although submitchanges turns in all changes, since we don't touch the
            // LINQ to SQL objects directly until we update them here, we should
            // effectively be updating one object at a time as the repository interface 
            // specifies            
            item.Update();
            _Db.SubmitChanges();
        }
    }
}
