
using Org.Mozilla.Geckoview;
using System;
using System.Threading.Tasks;

namespace Xam.Droid.GeckoView.Forms.Droid.Helpers
{
    public static class GeckoResultHelper
    {
        public static Task<T> GetResult<T>(GeckoResult futureData) where T : class
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

            try
            {
                var onSuccess = new GeckoResultOnValueListener<T>(tcs);
                var onError = new GeckoResultOnExceptionListener<T>(tcs);

                futureData.Then(onSuccess, onError);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }
    }
}