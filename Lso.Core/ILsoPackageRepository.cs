using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoPackageRepository
    {
        IList<ILsoPackage> GetAllWithAirbillNo(string AirbillNo);
    }


}
