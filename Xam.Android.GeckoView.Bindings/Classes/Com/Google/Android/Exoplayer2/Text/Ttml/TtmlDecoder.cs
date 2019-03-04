
namespace Com.Google.Android.Exoplayer2.Text.Ttml
{
    public sealed partial class TtmlDecoder
    {
        protected override ISubtitle Decode(byte[] p0, int p1, bool p2)
        {
            return DecodeTtmlSubtitle(p0, p1, p2);
        }

        protected override Java.Lang.Object Decode(Java.Lang.Object p0, Java.Lang.Object p1, bool p2)
        {
            //DvbSubtitle in ISubtitle interface actually inherit from Java.Lang.Object
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