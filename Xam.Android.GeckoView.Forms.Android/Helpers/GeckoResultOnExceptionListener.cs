
using System;
using System.Threading.Tasks;
using Java.Lang;
using Org.Mozilla.Geckoview;
using static Org.Mozilla.Geckoview.GeckoResult;

namespace Xam.Droid.GeckoView.Forms.Droid.Helpers
{
    internal class GeckoResultOnExceptionListener<T> : Java.Lang.Object, IOnExceptionListener
    {
        TaskCompletionSource<T> _tcs;

        public GeckoResultOnExceptionListener(TaskCompletionSource<T> tcs)
        {
            _tcs = tcs;
        }

        public GeckoResult OnException(Throwable p0)
        {
            try
            {
                _tcs.SetException(p0.GetBaseException());
            }
            catch (System.Exception ex)
            {
                _tcs.SetException(ex);
            }

            return null;
        }
    }
}