using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using Xamarin.Forms;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Forms.Droid.Handlers
{
    public class NavigationDelegate : global::Java.Lang.Object, NavigationDelegateClass
    {
        protected GeckoViewRenderer _renderer;

        public NavigationDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public virtual void OnCanGoBack(GeckoSession session, bool canGoBack)
        {
            _renderer.UpdateCanGoBackForward(canGoBack, null);
        }

        public virtual void OnCanGoForward(GeckoSession session, bool canGoForward)
        {
            _renderer.UpdateCanGoBackForward(null, canGoForward);
        }

        public virtual GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            NavigatedEvent(session, uri, true);
            return null;
        }

        public virtual GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            //Can assume result precisely here compared to Xamarin.Forms API
            var navEvent = WebNavigationEvent.NewPage;
            
            if (request.IsRedirect)
            {
                navEvent = WebNavigationEvent.NewPage;
            }
            else if (request.Uri.Equals(request.TriggerUri, System.StringComparison.OrdinalIgnoreCase))
            {
                navEvent = WebNavigationEvent.Refresh;
            }

            WebViewSource source;
            if (_renderer.Element != null && _renderer.Element.Source != null)
            {
                source = _renderer.Element.Source;
            }
            else
            {
                source = new UrlWebViewSource() { Url = request.Uri };
            }

            var args = new WebNavigatingEventArgs(navEvent, source, request.Uri);
            _renderer.ForwardSendNavigating(args);

            return args.Cancel ? GeckoResult.Deny : GeckoResult.Allow;
        }

        private void NavigatedEvent(GeckoSession session, string url, bool isError)
        {
            WebViewSource source;
            if (_renderer.Element != null && _renderer.Element.Source != null)
            {
                source = _renderer.Element.Source;
            }
            else
            {
                source = new UrlWebViewSource() { Url = url };
            }

            WebNavigationResult navigationResult;

            if (isError)
            {
                navigationResult = WebNavigationResult.Failure;
            }
            else
            {
                navigationResult = WebNavigationResult.Success;
            }

            var args = new WebNavigatedEventArgs(WebNavigationEvent.NewPage, source, url, navigationResult);

            _renderer.ForwardSendNavigated(args);
        }

        public virtual void OnLocationChange(GeckoSession session, string url)
        {
            NavigatedEvent(session, url, false);
        }

        public virtual GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            return GeckoResult.FromValue(_renderer.CreateNewSession().Item1);
        }
    }
}