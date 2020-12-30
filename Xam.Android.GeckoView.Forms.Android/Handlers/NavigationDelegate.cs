using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using System.Collections.Generic;
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

        public GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
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

        public void OnLocationChange(GeckoSession session, string url)
        {
        }

        private List<GeckoSession> _notGCSessionList = new List<GeckoSession>();

        public virtual GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            //Can assume result precisely here compared to Xamarin.Forms API
            var navEvent = WebNavigationEvent.NewPage;

            WebViewSource source;
            if (_renderer.Element != null && _renderer.Element.Source != null)
            {
                source = _renderer.Element.Source;
            }
            else
            {
                source = new UrlWebViewSource() { Url = uri };
            }

            var args = new WebNavigatingEventArgs(navEvent, source, uri);
            _renderer.Element.SendNavigating(args);

            if (args.Cancel)
            {
                return GeckoResult.FromValue(null);
            }
            else
            {
                //From documentation: A GeckoResult which holds the returned GeckoSession. May be null, in which case the request for a new window by web content will fail. e.g., window.open() will return null. The implementation of onNewSession is responsible for maintaining a reference to the returned object, to prevent it from being garbage collected.
                //See here: https://mozilla.github.io/geckoview/javadoc/mozilla-central/org/mozilla/geckoview/GeckoSession.NavigationDelegate.html#onNewSession-org.mozilla.geckoview.GeckoSession-java.lang.String-

                GeckoSession newSession = _renderer.CreateNewSession(false, uri).Item1;
                _notGCSessionList.Add(newSession);

                return GeckoResult.FromValue(newSession);
            }
        }
    }
}