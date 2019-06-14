using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class TrackingDatum : ITrackingDatum
    {        
        public string StatusCode { get; internal set; }
        public string StatusDescription { get; internal set; }
        public DateTime TrackingDate { get; internal set; }

        public string TrackingTableCode { get; internal set; }

        public string City { get; internal set; }

    }
}
