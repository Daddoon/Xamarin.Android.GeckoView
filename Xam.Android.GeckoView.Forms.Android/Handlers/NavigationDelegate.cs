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
            return GeckoResult.FromValue(null);
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

            var args = new WebNavigatingEventArgs(navEvent, new UrlWebViewSource { Url = request.Uri }, request.Uri);
            _renderer.ForwardSendNavigating(args);

            return args.Cancel ? GeckoResult.Deny : GeckoResult.Allow;
        }

        public virtual void OnLocationChange(GeckoSession session, string url)
        {

        }

        public virtual GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            return GeckoResult.FromValue(_renderer.CreateNewSession().Item1);
        }
    }
}