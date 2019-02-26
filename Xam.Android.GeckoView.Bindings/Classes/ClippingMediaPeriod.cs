using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Com.Google.Android.Exoplayer2.Source
{
    public sealed partial class ClippingMediaPeriod
    {
        public void OnContinueLoadingRequested(Java.Lang.Object p0)
        {
            OnContinueLoadingRequested((IMediaPeriod)p0);
        }
    }
}