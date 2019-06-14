using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class LsoIGroupRepository : ILsoIGroupRepository
    {
        protected WebDbDataContext _Db;

        public LsoIGroupRepository()
        {
            string connectionString =
                "Data Source=10.0.5.14;Initial Catalog=LsoCoreTest;Persist Security Info=True;User ID=L5development;Password=L5SQLCool";
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["Lso.Core.LsoCoreSettings.WebDBConnString"].ConnectionString;
            }
            catch (Exception)
            {
                // this is a bandaid for unit testing so nothing for now
            }

            DoConstruction(connectionString);
        }

        public LsoIGroupRepository(string connectionString)
        {
            DoConstruction(connectionString);
        }

        protected void DoConstruction(string connectionString)
        {
            _Db = new WebDbDataContext(connectionString);

            if (!_Db.DatabaseExists())
            {
				// TODO, throw in .TOstring because this doesn't actually work right as it is
                throw new RepositoryDoesNotExistException("LsoIGroupRepository.DatabaseExists returns false with the following connection string: " + _Db.Connection);
            } 
        }

        public IList<ILsoIGroup> GetAllForUID(int uid)
        {
            // it appears executequery only returns IEnumerable, so working with a list at each point
            // where we check results, results should be limited to 1 row, so not a big waste of 
            // resources to do this
            IEnumerable<IGroup> igroups = from g in _Db.IGroups
                           where g.UID == uid
                           select g;
            var igroupsList = igroups.ToList();


            if (igroupsList.Count() > 0)
            {
                var converter = new LsoIGroupFactory();
                var domigroup = converter.Create(igroupsList.First());
                var actigroup = (LsoIGroup)domigroup;
                var retval = new List<ILsoIGroup>();
                retval.Add(actigroup);                
                return retval;
            }
            
            return new List<ILsoIGroup>();
        }

        public IList<ILsoIGroup> GetAllWithDataLike(int uid, string groupName, string groupDescr)
        {
            if (!String.IsNullOrEmpty(groupName) && !String.IsNullOrEmpty(groupDescr))
            {
                IQueryable<IGroup> groups = null;
                if (uid > 0)
                {
                    groups = (from p in _Db.IGroups
                              where uid == p.UID &&  
                              (System.Data.Linq.SqlClient.SqlMethods.Like(p.GroupName, DbUtils.ConvertToSqlLike(groupName))
                               || System.Data.Linq.SqlClient.SqlMethods.Like(p.GroupDescr, DbUtils.ConvertToSqlLike(groupDescr)))
                              select p).Take(1000);
                }
                else
                {
                    groups = (from p in _Db.IGroups
                              where System.Data.Linq.SqlClient.SqlMethods.Like(p.GroupName, DbUtils.ConvertToSqlLike(groupName))
                              || System.Data.Linq.SqlClient.SqlMethods.Like(p.GroupDescr, DbUtils.ConvertToSqlLike(groupDescr))
                              select p).Take(1000);                    
                }

                var converter = new LsoIGroupFactory();
                return groups.Select(group => converter.Create(group)).ToList();    
            }
            return new List<ILsoIGroup>();
        }

    }
}
