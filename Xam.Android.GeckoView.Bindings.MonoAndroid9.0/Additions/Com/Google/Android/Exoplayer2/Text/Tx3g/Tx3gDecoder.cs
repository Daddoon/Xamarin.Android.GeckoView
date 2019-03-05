using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Google.Android.Exoplayer2.Text.Tx3g
{
    public sealed partial class Tx3gDecoder
    {
        protected override Java.Lang.Object Decode(Java.Lang.Object p0, Java.Lang.Object p1, bool p2)
        {
            return (global::Java.Lang.Object)Decode((byte[])p0, (int)p1, (bool)p2);
        }

        protected override Java.Lang.Object CreateInputBuffer()
        {
            return CreateInputBufferBase();
        }

        protected override Java.Lang.Object CreateOutputBuffer()
        {
            return CreateOutputBufferBase();
        }
    }
}
