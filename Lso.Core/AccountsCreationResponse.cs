using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class AccountsCreationResponse
    {
        public bool Success;
        public int CustomerId;
        public string LoginName;
        public ValidationErrors Errors;        
    }
}
