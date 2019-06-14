using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    class CannotCreateNewIdException : Exception
    {
        public CannotCreateNewIdException(string message, Exception innerException)
            : base(message, innerException) {}
        public CannotCreateNewIdException(string message)
            : base(message) { }
    }
}
