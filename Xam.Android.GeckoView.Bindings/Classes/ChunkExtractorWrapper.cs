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
using Com.Google.Android.Exoplayer2.Extractor;

namespace Com.Google.Android.Exoplayer2.Source.Chunk
{
    public sealed partial class ChunkExtractorWrapper
    {
        void IExtractorOutput.SeekMap(ISeekMap p0)
        {
            InvokeSeekMap(p0);
        }
    }
}