using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xam.Android.GeckoView.Forms
{
    public class GeckoViewForms : ContentView, IWebViewController
    {
        public static readonly BindableProperty SourceProperty;
        //public static readonly BindableProperty CanGoBackProperty;
        //public static readonly BindableProperty CanGoForwardProperty;

        //
        // Résumé :
        //     Gets or sets the Xamarin.Forms.WebViewSource object that represents the location
        //     that this Xamarin.Forms.WebView object displays.
        //
        // Remarques :
        //     To be added.
        [TypeConverter(typeof(WebViewSourceTypeConverter))]
        public WebViewSource Source { get; set; }
        //
        // Résumé :
        //     Gets a value that indicates whether the user can navigate forward.
        //
        // Remarques :
        //     To be added.
        //public bool CanGoForward { get; }
        //
        // Résumé :
        //     Gets a value that indicates whether the user can navigate to previous pages.
        //
        // Remarques :
        //     To be added.
        //public bool CanGoBack { get; }
        bool IWebViewController.CanGoBack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IWebViewController.CanGoForward { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GeckoViewForms()
        {
            Source = "about:blank";

            if (Device.RuntimePlatform != Device.Android)
            {
                throw new InvalidOperationException("GeckoView is supported only on Android");
            }
        }

        event EventHandler<EvalRequested> _EvalRequested;

        event EventHandler<EvalRequested> IWebViewController.EvalRequested
        {
            add
            {
                _EvalRequested += value;
            }

            remove
            {
                _EvalRequested -= value;
            }
        }

        event EvaluateJavaScriptDelegate _EvaluateJavaScriptRequested;

        event EvaluateJavaScriptDelegate IWebViewController.EvaluateJavaScriptRequested
        {
            add
            {
                _EvaluateJavaScriptRequested += value;
            }

            remove
            {
                _EvaluateJavaScriptRequested -= value;
            }
        }

        event EventHandler _GoBackRequested;

        event EventHandler IWebViewController.GoBackRequested
        {
            add
            {
                _GoBackRequested += value;
            }

            remove
            {
                _GoBackRequested -= value;
            }
        }

        event EventHandler _GoForwardRequested;

        event EventHandler IWebViewController.GoForwardRequested
        {
            add
            {
                _GoForwardRequested += value;
            }

            remove
            {
                _GoForwardRequested -= value;
            }
        }

        event EventHandler _ReloadRequested;
        event EventHandler IWebViewController.ReloadRequested
        {
            add
            {
                _ReloadRequested += value;
            }

            remove
            {
                _ReloadRequested -= value;
            }
        }

        void IWebViewController.SendNavigated(WebNavigatedEventArgs args)
        {
            throw new NotImplementedException();
        }

        void IWebViewController.SendNavigating(WebNavigatingEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            _ReloadRequested.Invoke(this, EventArgs.Empty);
        }
    }
}