using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    class LsoCustomerNewDefaults : ILsoCustomerNewDefaults
    {
        // Set sensible defaults that identify accounts
        // as being created by an automated process, etc
        public void SetDefaults(LsoCustomer item)
        {
            // TODO: Read this from XML or from the database

            // Program defaults
            if (!item.ChangedProperties.ContainsKey("CreditLimit"))
                item.CreditLimit = 100;
            if (!item.ChangedProperties.ContainsKey("CreditAvail"))
                item.CreditAvail = 100;
            if (!item.ChangedProperties.ContainsKey("PriceSchedule"))
                item.PriceSchedule = 0;
            if (!item.ChangedProperties.ContainsKey("DateOpened"))
                item.DateOpened = DateTime.Now;
            if (!item.ChangedProperties.ContainsKey("DeclaredValueSignature"))
                item.DeclaredValueSignature = true;
        }
    }    
}
