using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoPackage
    {
        string AirbillNo { get; }
        // Whether trackingsteps should attempt to remove redundant steps
        bool FilterRedundantSteps { get; set; }

        // The time in minutes to delete redundant entries
        int FilterFuzzMinutes { get; set; }

        // This is at the moment deliberately stubbed out with almost nothing at the moment
        IList<ITrackingDatum> TrackingSteps { get; }
    }
}
