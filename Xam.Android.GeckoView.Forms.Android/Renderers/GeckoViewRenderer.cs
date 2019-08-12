using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Org.Mozilla.Geckoview;
using Android.Content;
using System.ComponentModel;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using Xam.Droid.GeckoView.Forms;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using Xam.Droid.GeckoView.Forms.Droid.Handlers;
using Android.App;

[assembly: ExportRenderer(typeof(GeckoViewForms), typeof(GeckoViewRenderer))]
namespace Xam.Droid.GeckoView.Forms.Droid.Renderers
{
    public class GeckoViewRenderer : ViewRenderer<GeckoViewForms, Org.Mozilla.Geckoview.GeckoView>, IWebViewDelegate
    {
        private static Activity _activity { get; set; }

        public Activity Activity {
            get
            {
                return _activity;
            }
        }

        public static void Init(Activity activity)
        {
            _activity = activity;
        }

        public GeckoViewRenderer() : base()
        {
            AutoPackage = false;
        }

        public GeckoViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        Org.Mozilla.Geckoview.GeckoView _view;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var geckoViewForms = Element;

                if (geckoViewForms != null)
                {
                    _view?.Session.Stop();

                    IWebViewController ElementController = geckoViewForms as IWebViewController;

                    ElementController.EvalRequested -= OnEvalRequested;
                    ElementController.GoBackRequested -= OnGoBackRequested;
                    ElementController.GoForwardRequested -= OnGoForwardRequested;
                    ElementController.ReloadRequested -= OnReloadRequested;

                    geckoViewForms.SourcePropertyChanged -= OnSourcePropertyChanged;

                    _view?.Session.Dispose();
                }
            }

            //GeckoView is disposed here
            base.Dispose(disposing);
        }

        public virtual Tuple<GeckoSession, GeckoRuntime> CreateNewSession()
        {
            GeckoSession _session = new GeckoSession();
            GeckoRuntime _runtime = GeckoRuntime.Create(Context);
            _session.Open(_runtime);
            _session.ProgressDelegate = new ProgressDelegate(this);
            _session.ContentDelegate = new ContentDelegate(this);

            return Tuple.Create(_session, _runtime);
        }

        private Org.Mozilla.Geckoview.GeckoView CreateGeckoViewControl()
        {
            _view = new Org.Mozilla.Geckoview.GeckoView(Context);

            var sessionObjects = CreateNewSession();

            var session = sessionObjects.Item1;
            var runtime = sessionObjects.Item2;

            _view.SetSession(session, runtime);

            return _view;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GeckoViewForms> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = CreateGeckoViewControl();
                webView.LayoutParameters = new global::Android.Widget.LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                webView.Session.Settings.AllowJavascript = true;
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

                e.NewElement.SourcePropertyChanged -= OnSourcePropertyChanged;
            }

            if (e.NewElement != null)
            {
                var newElementController = e.NewElement as IWebViewController;
                newElementController.EvalRequested += OnEvalRequested;
                newElementController.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
                newElementController.GoBackRequested += OnGoBackRequested;
                newElementController.GoForwardRequested += OnGoForwardRequested;
                newElementController.ReloadRequested += OnReloadRequested;

                e.NewElement.SourcePropertyChanged += OnSourcePropertyChanged;
            }

            Load();
        }

        private void OnSourcePropertyChanged(object sender, EventArgs e)
        {
            OnElementPropertyChanged(this, new PropertyChangedEventArgs("Source"));
        }

        protected virtual void OnReloadRequested(object sender, EventArgs e)
        {
            _view?.Session.Reload();
        }

        protected virtual void OnGoForwardRequested(object sender, EventArgs e)
        {
            _view?.Session.GoForward();
        }

        protected virtual void OnGoBackRequested(object sender, EventArgs e)
        {
            _view?.Session.GoBack();
        }

        protected virtual Task<string> OnEvaluateJavaScriptRequested(string script)
        {
            throw new NotImplementedException($"{nameof(OnEvaluateJavaScriptRequested)}: Javascript evaluation is not yet supported on GeckoView");
        }

        protected virtual void OnEvalRequested(object sender, EvalRequested e)
        {
            throw new NotImplementedException($"{nameof(OnEvalRequested)}: Javascript evaluation is not yet supported on GeckoView");
        }

        protected virtual void Load()
        {
            WebViewSource source = Element.Source;
            UrlWebViewSource uri = source as UrlWebViewSource;

            if (uri != null)
            {
                _view?.Session.LoadUri(uri.Url);
            }
            else
            {
                HtmlWebViewSource html = source as HtmlWebViewSource;
                if (html != null)
                {
                    _view?.Session.LoadString(html.Html, "text/html");
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

        public virtual void LoadHtml(string html, string baseUrl)
        {
            _view?.Session.LoadString(html, "text/html");
        }

        public virtual void LoadUrl(string url)
        {
            _view?.Session.LoadUri(url);
        }
    }
}