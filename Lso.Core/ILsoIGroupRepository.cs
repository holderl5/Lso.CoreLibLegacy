using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoIGroupRepository
    {
        IList<ILsoIGroup> GetAllForUID(int UID);
        IList<ILsoIGroup> GetAllWithDataLike(int uid, string groupName, string groupDescr);
    }


}
