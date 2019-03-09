using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Android.GeckoView.Forms.Android.Renderers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView.Forms.Android.Handlers
{
    public class NavigationDelegate : global::Java.Lang.Object, NavigationDelegateClass
    {
        private GeckoViewRenderer _renderer;

        public NavigationDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public void OnCanGoBack(GeckoSession session, bool canGoBack)
        {
           
        }

        public void OnCanGoForward(GeckoSession session, bool canGoForward)
        {

        }

        public GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            return GeckoResult.FromValue(null);
        }

        public GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            _renderer.Element.SendNavigating(new Xamarin.Forms.WebNavigatingEventArgs(Xamarin.Forms.WebNavigationEvent.NewPage, request.Uri, request.Uri));
            return GeckoResult.FromValue(null);
        }

        public void OnLocationChange(GeckoSession session, string url)
        {

        }

        public GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            return GeckoResult.FromValue(_renderer.CreateNewSession().Item1);
        }
    }
}