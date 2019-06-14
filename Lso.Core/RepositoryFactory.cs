using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class RepositoryFactory
    {        
        public static ILsoCustomerRepository GetNewCustomerRepository()
        {
            return new LsoCustomerRepository();            
        }

        public static ILsoCustomerRepository GetNewCustomerRepository(string connectionString)
        {
            return new LsoCustomerRepository(connectionString);
        }


        public static IWebshipAccountRepository GetNewWebshipAccountRepository()
        {                
            return new WebShipAccountRepository();
        }

        public static IWebshipAccountRepository GetNewWebshipAccountRepository(string connectionString)
        {            
            return new WebShipAccountRepository(connectionString);
        }


        public static IPickupTimetableRepository GetNewPickupTimetableRepository()
        {           
            return new PickupTimetableRepository();
        }

        public static IPickupTimetableRepository GetNewPickupTimetableRepository(string connectionString)
        {
            return new PickupTimetableRepository(connectionString);
        }

        public static ILsoPackageRepository GetNewLsoPackageRepository()
        {
            return new LsoPackageRepository();
        }

        public static ILsoPackageRepository GetNewLsoPackageRepository(string connectionString)
        {
            return new LsoPackageRepository(connectionString);
        }

        public static ILsoIGroupRepository GetNewLsoIGroupRepository()
        {
            return new LsoIGroupRepository();
        }
        public static ILsoIGroupRepository GetNewLsoIGroupRepository(string connectionString)
        {
            return new LsoIGroupRepository(connectionString);
        }
    }
}
