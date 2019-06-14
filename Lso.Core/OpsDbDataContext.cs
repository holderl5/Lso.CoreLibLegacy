using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    partial class OpsDbDataContext : System.Data.Linq.DataContext
    {
        public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        {
            // Test submit changes should throw if it encounters a problem
            TestSubmitChanges();
            base.SubmitChanges(failureMode);
        }//override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)

        public void TestSubmitChanges()
        {
            //get hold of the change set
            ChangeSet cs = this.GetChangeSet();

            //types that we will check for maxlength
            Type[] typesToCheck = new Type[] { typeof(string), typeof(System.Data.Linq.Binary) };

            //get pending inserts and updates along with the members that can have a max length
            var insertsUpdates = (
            from i in cs.Inserts.Union(cs.Updates)
            join m in this.Mapping.GetTables() on i.GetType() equals m.RowType.Type
            select new { Entity = i, Members = m.RowType.DataMembers.Where(dm => typesToCheck.Contains(dm.Type)).ToList() }
            ).Where(m => m.Members.Any()).ToList();

            //check all pending inserts and updates for members containing a value longer than the allowed max length for the db column
            foreach (var ins in insertsUpdates)
            {
                foreach (System.Data.Linq.Mapping.MetaDataMember mm in ins.Members)
                {
                    int maxLength = GetMaxLength(mm.DbType);
                    if (mm.MemberAccessor.HasValue(ins.Entity))
                    {
                        int memberValueLength = GetMemberValueLength(mm.MemberAccessor.GetBoxedValue(ins.Entity));
                        if (maxLength > 0 && memberValueLength > maxLength)
                        {
                            InvalidOperationException iex = new InvalidOperationException("Member '" + mm.Name + "' in '" + mm.DeclaringType.Name + "' has a value that will not fit into column '" + mm.MappedName + "' (" + mm.DbType + ")");
                            throw iex;
                        }//if (maxLength > 0 && memberValueLength > maxLength)
                    }//if (mm.MemberAccessor.HasValue(ins.Entity))
                }//foreach (System.Data.Linq.Mapping.MetaDataMember mm in ins.Members)
            }//foreach (var ins in insertsUpdates)            
        }

        public void DiscardInsertsAndDeletes()
        {
            //get hold of the change set
            ChangeSet cs = this.GetChangeSet();

            foreach (var insertion in cs.Inserts)
            {
                this.GetTable(insertion.GetType()).DeleteOnSubmit(insertion);
            }

            foreach (var deletion in cs.Deletes)
            {
                this.GetTable(deletion.GetType()).InsertOnSubmit(deletion);
            }

        }

        private int GetMaxLength(string dbType)
        {
            int maxLength = 0;

            if (dbType.Contains("(")) { dbType = dbType.Substring(dbType.IndexOf("(") + 1); }
            if (dbType.Contains(")")) { dbType = dbType.Substring(0, dbType.IndexOf(")")); }
            int.TryParse(dbType, out maxLength);
            return maxLength;
        }//int GetMaxLength(string dbType)

        private int GetMemberValueLength(object value)
        {
            if (value == null) { return 0; }
            if (value.GetType() == typeof(string)) { return ((string)value).Length; }
            else if (value.GetType() == typeof(Binary)) { return ((Binary)value).Length; }
            else { throw new ArgumentException("Unknown type."); }
        }//int GetMemberValueLength(object value)
    }
}
