using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ITrackingDatum
    {        
        string StatusCode { get; }
        string StatusDescription { get;  }
        DateTime TrackingDate { get;  }
        // Scanner codes create entries in the tracking table
        string TrackingTableCode { get; }
        string City { get;  }
    }
}
