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

namespace Com.Google.Android.Exoplayer2.Text.Cea
{
    public sealed partial class CeaOutputBuffer
    {
        public unsafe override void ReleaseSubtitleOutputBuffer()
        {
            const string __id = "release.()V";
            try
            {
                _members.InstanceMethods.InvokeNonvirtualVoidMethod(__id, this, null);
            }
            finally
            {
            }
        }
    }
}