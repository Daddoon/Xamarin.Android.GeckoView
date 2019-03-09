using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView.Extension.Handlers
{
    public class NavigationDelegate : global::Java.Lang.Object, NavigationDelegateClass
    {
        private GeckoViewManaged _viewManaged;

        public NavigationDelegate(GeckoViewManaged viewManaged)
        {
            _viewManaged = viewManaged;
        }

        public void OnCanGoBack(GeckoSession session, bool canGoBack)
        {
            _viewManaged.OnCanGoBack(session, canGoBack);
        }

        public void OnCanGoForward(GeckoSession session, bool canGoForward)
        {
            _viewManaged.OnCanGoForward(session, canGoForward);
        }

        public GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            return _viewManaged.OnLoadError(session, uri, error);
        }

        public GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            return _viewManaged.OnLoadRequest(session, request);
        }

        public void OnLocationChange(GeckoSession session, string url)
        {
            _viewManaged.OnLocationChange(session, url);
        }

        public GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            return _viewManaged.OnNewSession(session, uri);
        }
    }
}