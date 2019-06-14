using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class PrivilegedCustomerIDRequiredException : Exception
    {
        public PrivilegedCustomerIDRequiredException(string message, Exception innerException)
            : base(message, innerException) {}
        public PrivilegedCustomerIDRequiredException(string message)
            : base(message) { }
    }
}
