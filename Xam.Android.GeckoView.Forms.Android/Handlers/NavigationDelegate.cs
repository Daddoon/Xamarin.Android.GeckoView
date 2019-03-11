using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
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
           
        }

        public virtual void OnCanGoForward(GeckoSession session, bool canGoForward)
        {

        }

        public virtual GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            return GeckoResult.FromValue(null);
        }

        public virtual GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            return GeckoResult.FromValue(null);
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