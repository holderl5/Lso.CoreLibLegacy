using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    // provides public access to internal or private data
    public class LsoCustomerTestingGlass
    {
        public static HashSet<string> GetChangedProperties(LsoCustomer item)
        {
            return new HashSet<string>();
        }
    }
}
