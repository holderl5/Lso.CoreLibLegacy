using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class GroupSearchResponse
    {
        public bool Success;
        public int GroupId;
        public int UID;
        public string GroupName;
        public string GroupDescr;

        // We need Equals to eliminate duplicate items from lists
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            var p = obj as GroupSearchResponse;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return GroupId == p.GroupId;
        }

        public bool Equals(GroupSearchResponse p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            return GroupId == p.GroupId;
        }

        public override int GetHashCode()
        {
            return GroupId;
        }

    }
}
