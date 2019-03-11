using Java.Interop;
using System;
using System.Threading.Tasks;
using static Org.Mozilla.Geckoview.GeckoResult;

namespace Xam.Droid.GeckoView.Forms.Droid.Helpers
{
    internal class GeckoResultOnValueListener<T> : Java.Lang.Object, IOnValueListener where T : class
    {
        TaskCompletionSource<T> _tcs;

        public GeckoResultOnValueListener(TaskCompletionSource<T> tcs)
        {
            _tcs = tcs;
        }

        private bool IsPrimitiveType(Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        private T Cast(object obj)
        {
            Type currentType = typeof(T);

            //Class object are correct for GeckoView API, but expected value type from bindings are from C# domains.
            //The returned result from GeckoResult objects are Java.Lang.* objects for value type.
            //We must try to convert them to C# primitive type
            if (IsPrimitiveType(currentType))
            {
                if (currentType == typeof(string))
                {
                    return Convert.ToString(obj) as T;
                }
                else if (currentType == typeof(int))
                {
                    return Convert.ToInt32(obj) as T;
                }
                else if (currentType == typeof(double))
                {
                    return Convert.ToDouble(obj) as T;
                }
                else if (currentType == typeof(decimal))
                {
                    return Convert.ToDecimal(obj) as T;
                }
                else if (currentType == typeof(DateTime))
                {
                    return Convert.ToDateTime(obj) as T;
                }
                else
                {
                    throw new NotSupportedException("Not supported Java to C# type");
                }
            }
            else
            {
                var propertyInfo = obj.GetType().GetProperty("Instance");
                return propertyInfo == null ? default(T) : propertyInfo.GetValue(obj, null) as T;
            }
        }

        public global::Org.Mozilla.Geckoview.GeckoResult OnValue(global::Java.Lang.Object p0)
        {
            try
            {
                _tcs.SetResult(Cast(p0));
            }
            catch (Exception ex)
            {
                _tcs.SetException(ex);
            }

            return null;
        }
    }
}