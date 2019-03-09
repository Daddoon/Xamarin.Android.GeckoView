using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xam.Android.GeckoView.Forms
{
    public class GeckoViewForms : View, IWebViewController
    {
        public static readonly BindableProperty SourceProperty;
        public static readonly BindableProperty CanGoBackProperty;
        public static readonly BindableProperty CanGoForwardProperty;

        [TypeConverter(typeof(WebViewSourceTypeConverter))]
        public WebViewSource Source { get; set; }

        public void Eval(string script)
        {
            EvalRequested.Invoke(this, new EvalRequested(script));
        }

        public Task<string> EvaluateJavaScriptAsync(string script)
        {
            return EvaluateJavaScriptRequested.Invoke(script);
        }

        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }

        public void GoBack()
        {
            if (CanGoBack)
            {
                GoBackRequested.Invoke(this, EventArgs.Empty);
            }
        }

        public void GoForward()
        {
            if (CanGoForward)
            {
                GoForwardRequested.Invoke(this, EventArgs.Empty);
            }
        }

        public GeckoViewForms()
        {
            Source = "about:blank";
            CanGoBack = true;
            CanGoForward = true;

            if (Device.RuntimePlatform != Device.Android)
            {
                throw new InvalidOperationException("GeckoView is supported only on Android");
            }
        }

        public event EventHandler<EvalRequested> EvalRequested;

        public event EvaluateJavaScriptDelegate EvaluateJavaScriptRequested;

        public event EventHandler GoBackRequested;

        public event EventHandler GoForwardRequested;

        public event EventHandler ReloadRequested;

        public event EventHandler<WebNavigatingEventArgs> Navigating;
        
        public event EventHandler<WebNavigatedEventArgs> Navigated;

        public void SendNavigated(WebNavigatedEventArgs args)
        {
            Navigated.Invoke(this, args);
        }

        public void SendNavigating(WebNavigatingEventArgs args)
        {
            Navigating.Invoke(this, args);
        }

        public void Reload()
        {
            ReloadRequested.Invoke(this, EventArgs.Empty);
        }
    }
}