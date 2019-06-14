using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Configuration;
using System.Text;
using AutoMapper;
using Configuration = System.Configuration.Configuration;

namespace Lso.Core
{
    public class LsoCustomerRepository : ILsoCustomerRepository
    {
		protected OpsDbDataContext _Db;

        public LsoCustomerRepository()
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

        public LsoCustomerRepository(string connectionString)
		{
            DoConstruction(connectionString);
		}

        protected void DoConstruction(string connectionString)
        {
            // TODO: We want to make sure that we get created with TEST parameters for now
            _Db = new OpsDbDataContext(connectionString);

            if (!_Db.DatabaseExists())
            {
                throw new RepositoryDoesNotExistException("LsoCustomerRepository.DatabaseExists returns false with the following connection string: " + _Db.Connection);
            }
        }

        public ILsoCustomerRepository GetRepository()
        {
            return new LsoCustomerRepository();
        }

        public IList<ILsoCustomer> GetAllWithCustId(int custId)
		{            
			var accounts = from p in _Db.Customers
						   where p.CustID == custId
						   select p;
			var converter = new LsoCustomerFactory();
			return accounts.Select(customer => converter.Create(customer)).ToList();

		}

        public IList<ILsoCustomer> GetAllWithCustName(string custName)
        {
            // TODO: Names should be normalized to prevent entries with extra spaces from failing the search
            var accounts = from p in _Db.Customers
                           where p.CustName.ToLower() == custName.ToLower()
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithCustIdZipcode(int custId, string zipcode)
        {
            var accounts = from p in _Db.Customers
                           where p.CustID == custId && p.PhyZip == zipcode
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithDataLike(string custName, string CustContactName, string BillToAddress1, string CustContactPhone, string PhyAddress1)
        {
            // TODO: we are using a hard coded take on this right now as a web client
            // doesn't normally need to see many records.  instead we should rework the interface to
            // work in both use cases

            // For the moment it makes the most sense if we require all of these to be present because the
            // primary place where this will be used will require them
            // later it might be nice to use only one or 2, but that can wait
            if (!String.IsNullOrEmpty(custName) && !String.IsNullOrEmpty(CustContactName) && !String.IsNullOrEmpty(BillToAddress1) && !String.IsNullOrEmpty(CustContactPhone) && !String.IsNullOrEmpty(PhyAddress1))
            {
                var accounts = (from p in _Db.Customers
                               where System.Data.Linq.SqlClient.SqlMethods.Like(p.CustName, DbUtils.ConvertToSqlLike(custName))
                               || System.Data.Linq.SqlClient.SqlMethods.Like(p.CustContactName, DbUtils.ConvertToSqlLike(CustContactName))
                               || System.Data.Linq.SqlClient.SqlMethods.Like(p.BillToAddress1, DbUtils.ConvertToSqlLike(BillToAddress1))
                               || System.Data.Linq.SqlClient.SqlMethods.Like(p.CustContactPhone, DbUtils.ConvertToSqlLike(CustContactPhone))
                               || System.Data.Linq.SqlClient.SqlMethods.Like(p.PhyAddress1, DbUtils.ConvertToSqlLike(PhyAddress1))
                               select p).Take(1000);
                var converter = new LsoCustomerFactory();
                return accounts.Select(customer => converter.Create(customer)).ToList();    
            }
            return new List<ILsoCustomer>();
        }

        public IList<ILsoCustomer> GetAllWithCustNameLike(string custName)
        {
            var searchstring = DbUtils.ConvertToSqlLike(custName);
            var accounts = from p in _Db.Customers
                           where System.Data.Linq.SqlClient.SqlMethods.Like(p.CustName, searchstring)
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithCustContactNameLike(string CustContactName)
        {
            var searchstring = DbUtils.ConvertToSqlLike(CustContactName);
            var accounts = from p in _Db.Customers
                           where System.Data.Linq.SqlClient.SqlMethods.Like(p.CustContactName, searchstring)
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithBillToAddress1Like(string BillToAddress1)
        {
            var searchstring = DbUtils.ConvertToSqlLike(BillToAddress1);
            var accounts = from p in _Db.Customers
                           where System.Data.Linq.SqlClient.SqlMethods.Like(p.BillToAddress1, searchstring)
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithPhyAddress1Like(string PhyAddress1)
        {
            var searchstring = DbUtils.ConvertToSqlLike(PhyAddress1);
            var accounts = from p in _Db.Customers
                           where System.Data.Linq.SqlClient.SqlMethods.Like(p.PhyAddress1, searchstring)
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public IList<ILsoCustomer> GetAllWithCustContactPhone(string CustContactPhone)
        {
            var searchstring = DbUtils.ConvertToSqlLike(CustContactPhone);
            var accounts = from p in _Db.Customers
                           where System.Data.Linq.SqlClient.SqlMethods.Like(p.PhyAddress1, searchstring)
                           select p;
            var converter = new LsoCustomerFactory();
            return accounts.Select(customer => converter.Create(customer)).ToList();
        }

        public void Add(ILsoCustomer item)
		{
			// create the customer row and update it using the passed in value
			var customer = new Customer();

            /* Note: Code generally relies on Linq to create the associated NewCustomerOption,
             *       but there are two cases that need to be handled
             * First, many customers exist that do not have a NewCustomerOption yet. That case is handled in Customer.cs
             * Second, a completely new customer. That case is handled below.
             */
            customer.NewCustomerOptions = new NewCustomerOption();
            var actualitem = (LsoCustomer)item; // We need access to the underlying implementation
            actualitem.CreatedFrom = customer;  // This empty customer will get updated in the factory below

            // set defaults if those fields are not already set
            new LsoCustomerNewDefaults().SetDefaults(actualitem);
            
            // perform copy to LINQ to SQL class
            LsoCustomerFactory.Update(actualitem);
                        		              
            // linq to sql is ready to push to DB, but we need to test that first
            _Db.Customers.InsertOnSubmit(customer);

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
            // Get a new customer ID from the DB
            int? newCustId = 0;
            _Db.sp_GetCustID(ref newCustId);

            if (!newCustId.HasValue || newCustId == 0)
            {
                // discard the changes
                _Db.DiscardInsertsAndDeletes();
                _Db.SubmitChanges();
                throw new CannotCreateNewIdException("LsoCustomerRepository.Add: sp_GetCustID returned null or zero for the next customer ID");
            }

            // populate the customerID now that we have it and are clear to save in the DB
            item.CustID = newCustId.Value;
            actualitem.CreatedFrom.CustID = newCustId.Value;
            			            
            // flush DB            
			_Db.SubmitChanges();
		}

		public void Update(ILsoCustomer item)
		{
			// TODO: make sure subsequent code additions do not change this
			// Although submitchanges turns in all changes, since we don't touch the
			// LINQ to SQL objects directly until we update them here, we should
			// effectively be updating one object at a time as the repository interface 
			// specifies            
            item.Update();
			_Db.SubmitChanges();
		}

        // TODO: TECHNICAL DEBT - Due to time constraints these SPs are being called from here
        // TODO: though they completely violate the repository pattern
        // TODO: Get rid of this ASAP
        public ISingleResult<Lso.Core.sp_GetCustomerBaseDiscountsResult> GetBaseDiscounts(int CustID)
        {
            return _Db.sp_GetCustomerBaseDiscounts(CustID);
        }

        // TODO: TECHNICAL DEBT - Due to time constraints these SPs are being called from here
        // TODO: though they completely violate the repository pattern
        // TODO: Get rid of this ASAP
        public ISingleResult<Lso.Core.sp_GetCustomerTierRevenueDiscountsResult> GetTieredRevenueDiscounts(int CustID)
        {
            return _Db.sp_GetCustomerTierRevenueDiscounts(CustID);
        }

        // TODO: TECHNICAL DEBT - Due to time constraints these SPs are being called from here
        // TODO: though they completely violate the repository pattern
        // TODO: Get rid of this ASAP
        public ISingleResult<Lso.Core.sp_GetCustomerWeightDiscountsResult> GetWeightDiscounts(int CustID)
        {
            return _Db.sp_GetCustomerWeightDiscounts(CustID);
        }

        // TODO: TECHNICAL DEBT - Due to time constraints these SPs are being called from here
        // TODO: though they completely violate the repository pattern
        // TODO: Get rid of this ASAP
        public ISingleResult<Lso.Core.sp_GetCustomerZoneDiscountsResult> GetZoneDiscounts(int CustID)
        {
            return _Db.sp_GetCustomerZoneDiscounts(CustID);
        }

    }
}
