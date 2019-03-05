using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Google.Android.Exoplayer2.Upstream.Cache
{
    public sealed partial class LeastRecentlyUsedCacheEvictor
    {
        public int Compare(Java.Lang.Object o1, Java.Lang.Object o2)
        {
            return Compare((CacheSpan)o1, (CacheSpan)o2);
        }
    }
}
