using Com.Google.Android.Exoplayer2.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Google.Android.Exoplayer2.Trackselection
{
    public partial class AdaptiveTrackSelection
    {
        public sealed partial class Factory
        {
            ITrackSelection ITrackSelectionFactory.CreateTrackSelection(TrackGroup p0, params int[] p1)
            {
                return CreateTrackSelection(p0, p1);
            }
        }
    }
}
