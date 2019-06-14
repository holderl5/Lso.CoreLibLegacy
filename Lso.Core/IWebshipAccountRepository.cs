using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface IWebshipAccountRepository
    {
        IList<IWebshipAccount> GetAllWithUid(int UID);
        IList<IWebshipAccount> GetAllWithLoginNameCustId(string loginName, int custId);
        IList<IWebshipAccount> GetAllWithCustomerId(int custId);
        void Add(IWebshipAccount item);
        void Update(IWebshipAccount item);
    }
}
