using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Org.Mozilla.Geckoview;
using Android.Content;
using System.ComponentModel;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using Xam.Android.GeckoView.Forms;
using Xam.Android.GeckoView.Forms.Android.Renderers;
using Android.Views;
using Android.Util;
using Android.Runtime;

[assembly: ExportRenderer(typeof(GeckoViewForms), typeof(GeckoViewRenderer))]
namespace Xam.Android.GeckoView.Forms.Android.Renderers
{
    public class GeckoViewRenderer : ViewRenderer<GeckoViewForms, Org.Mozilla.Geckoview.GeckoView>, IWebViewDelegate
    {
        public static void Init()
        {
            //No-op
        }

        public GeckoViewRenderer() : base()
        {
            AutoPackage = false;
        }

        public GeckoViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    _session.Stop();

                    IWebViewController ElementController = Element as IWebViewController;

                    ElementController.EvalRequested -= OnEvalRequested;
                    ElementController.GoBackRequested -= OnGoBackRequested;
                    ElementController.GoForwardRequested -= OnGoForwardRequested;
                    ElementController.ReloadRequested -= OnReloadRequested;

                    _session.Dispose();
                    _runtime.Dispose();
                }
            }

            //GeckoView is disposed here
            base.Dispose(disposing);
        }

        GeckoSession _session = null;
        GeckoRuntime _runtime = null;
        Org.Mozilla.Geckoview.GeckoView _view;

        private Org.Mozilla.Geckoview.GeckoView CreateGeckoViewControl()
        {
            _view = new Org.Mozilla.Geckoview.GeckoView(Context);

            _session = new GeckoSession();
            _runtime = GeckoRuntime.Create(Context);
            _session.Open(_runtime);
            _view.SetSession(_session, _runtime);

            return _view;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GeckoViewForms> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = CreateGeckoViewControl();
                webView.LayoutParameters = new global::Android.Widget.LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                _session.Settings.AllowJavascript = true;
                //webView.Settings.DomStorageEnabled = true;

                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                var oldElementController = e.OldElement as IWebViewController;
                oldElementController.EvalRequested -= OnEvalRequested;
                oldElementController.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
                oldElementController.GoBackRequested -= OnGoBackRequested;
                oldElementController.GoForwardRequested -= OnGoForwardRequested;
                oldElementController.ReloadRequested -= OnReloadRequested;
            }

            if (e.NewElement != null)
            {
                var newElementController = e.NewElement as IWebViewController;
                newElementController.EvalRequested += OnEvalRequested;
                newElementController.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
                newElementController.GoBackRequested += OnGoBackRequested;
                newElementController.GoForwardRequested += OnGoForwardRequested;
                newElementController.ReloadRequested += OnReloadRequested;
            }

            Load();
        }

        private void OnReloadRequested(object sender, EventArgs e)
        {
            _session.Reload();
        }

        private void OnGoForwardRequested(object sender, EventArgs e)
        {
            _session.GoForward();
        }

        private void OnGoBackRequested(object sender, EventArgs e)
        {
            _session.GoBack();
        }

        private Task<string> OnEvaluateJavaScriptRequested(string script)
        {
            throw new NotImplementedException($"{nameof(OnEvaluateJavaScriptRequested)}: Javascript evaluation is not yet supported on GeckoView");
        }

        private void OnEvalRequested(object sender, EvalRequested e)
        {
            throw new NotImplementedException($"{nameof(OnEvalRequested)}: Javascript evaluation is not yet supported on GeckoView");
        }

        private void Load()
        {
            WebViewSource source = Element.Source;
            UrlWebViewSource uri = source as UrlWebViewSource;

            if (uri != null)
            {
                _session.LoadUri(uri.Url);
            }
            else
            {
                HtmlWebViewSource html = source as HtmlWebViewSource;
                if (html != null)
                {
                    _session.LoadString(html.Html, "text/html");
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Source":
                    Load();
                    break;
            }
        }

        public void LoadHtml(string html, string baseUrl)
        {
            _session.LoadString(html, "text/html");
        }

        public void LoadUrl(string url)
        {
            _session.LoadUri(url);
        }
    }
}