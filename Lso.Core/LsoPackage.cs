using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class LsoPackage : DomainObjBase, ILsoPackage
    {
        // TODO: This violates principles doing it this way, but the
        // TODO: public facing interface is sound
        private Func<string, List<TrackingDatum>> _GetTrackingDetail;

        public string AirbillNo { get; internal set; }

        // Whether tracking steps should be filtered
        public bool FilterRedundantSteps { get; set; }

        // The time in minutes to delete redundant entries
        public int FilterFuzzMinutes { get; set; }

        internal Func<string, List<TrackingDatum>> GetTrackingDetail
        {
            set { _GetTrackingDetail = value; }
        }

        

        private void SortSteps(List<TrackingDatum> steps)
        {
            // sort all steps in reverse chronological order
            steps.Sort(
                delegate(TrackingDatum step1, TrackingDatum step2)
                {
                    int retVal = -1;
                    if (step2.TrackingDate > step1.TrackingDate)
                        retVal = 0;
                    else if (step2.TrackingDate == step1.TrackingDate)
                        retVal = step1.StatusCode.CompareTo(step2.StatusCode);

                    return retVal;
                }
            );
        }

        // If two steps have the same status code and are closer than fuzz factor in minutes,
        // eliminate the redundant step
        private void EliminateRedundantSteps(List<TrackingDatum> steps)
        {
            int fuzzMinutes = (FilterFuzzMinutes > 0) ? FilterFuzzMinutes : 5;

            int i = 0;
            while (i < steps.Count)
            {
                if (i > 0)
                {
                    var timediff = steps[i - 1].TrackingDate.Subtract(steps[i].TrackingDate);
                    var remLast = (steps[i - 1].StatusCode == steps[i].TrackingTableCode);
                    var remCurr = (steps[i - 1].TrackingTableCode == steps[i].StatusCode);

                    if (timediff.TotalMinutes < fuzzMinutes && (remCurr || remLast)) 
                    {
                        if (remCurr)
                            steps.Remove(steps[i]);
                        if (remLast)
                            steps.Remove(steps[i - 1]);
                        
                    } 
                    else
                    {
                        i++;
                    }

                }
                else
                {
                    i++;
                }
            }

        }

        public IList<ITrackingDatum> TrackingSteps
        {
            get
            {                
                var data = _GetTrackingDetail(AirbillNo);                

                if (FilterRedundantSteps)
                {
                    SortSteps(data);
                    EliminateRedundantSteps(data);
                }
                

                return data.Cast<ITrackingDatum>().ToList();
            }            
        }
    }
}
