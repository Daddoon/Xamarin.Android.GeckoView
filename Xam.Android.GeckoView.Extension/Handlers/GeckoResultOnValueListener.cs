
using System;
using System.Threading.Tasks;
using static Org.Mozilla.Geckoview.GeckoResult;

namespace Xam.Android.GeckoView.Bindings.Handlers
{
    internal class GeckoResultOnValueListener<T> : Java.Lang.Object, IOnValueListener
    {
        TaskCompletionSource<T> _tcs;

        public GeckoResultOnValueListener(TaskCompletionSource<T> tcs)
        {
            _tcs = tcs;
        }

        public global::Org.Mozilla.Geckoview.GeckoResult OnValue(global::Java.Lang.Object p0)
        {
            try
            {
                _tcs.SetResult((T)((object)p0));
            }
            catch (Exception ex)
            {
                _tcs.SetException(ex);
            }

            return null;
        }
    }
}