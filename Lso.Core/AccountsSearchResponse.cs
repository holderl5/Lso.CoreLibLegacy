using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class AccountsSearchResponse
    {
        public bool Success;
        public int CustomerId;
        public string CustName;
        public string CustContactName;
        public string CustContactPhone;
        public string BillToAddress1;
        public string PhyAddress1;

        public string SafeName;

        // We need Equals to eliminate duplicate items from lists
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            var p = obj as AccountsSearchResponse;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return CustomerId == p.CustomerId;
        }

        public bool Equals(AccountsSearchResponse p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            return CustomerId == p.CustomerId;
        }

        public override int GetHashCode()
        {
            return CustomerId;
        }

    }
}
