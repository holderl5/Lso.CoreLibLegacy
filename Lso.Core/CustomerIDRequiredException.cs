using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class CustomerIDRequiredException : Exception
    {
        public CustomerIDRequiredException(string message, Exception innerException)
            : base(message, innerException) {}
        public CustomerIDRequiredException(string message)
            : base(message) { }
    }
}
