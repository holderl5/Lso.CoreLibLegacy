using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    class RepositoryDoesNotExistException : Exception
    {
        public RepositoryDoesNotExistException(string message, Exception innerException)
            : base(message, innerException) {}
        public RepositoryDoesNotExistException(string message)
            : base(message) { }
    }
}
