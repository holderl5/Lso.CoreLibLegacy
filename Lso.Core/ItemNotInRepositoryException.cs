using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class ItemNotInRepositoryException : Exception
    {
        public ItemNotInRepositoryException(string message, Exception innerException)
            : base(message, innerException) {}
        public ItemNotInRepositoryException(string message)
            : base(message) { }
    }
}
