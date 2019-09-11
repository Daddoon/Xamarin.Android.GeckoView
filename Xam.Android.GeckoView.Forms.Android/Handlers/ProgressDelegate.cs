using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using Xamarin.Forms;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Forms.Droid.Handlers
{
    public class ProgressDelegate : global::Java.Lang.Object, IProgressDelegate
    {
        protected GeckoViewRenderer _renderer;

        public ProgressDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        /// <summary>
        /// This is here for conveniencen, updated from the OnPageStart event. We should rely on something else in the future.
        /// </summary>
        string _currentURI = string.Empty;

        public virtual void OnPageStart(GeckoSession session, string url)
        {
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

        public virtual void OnPageStop(GeckoSession session, bool success)
        {
            NavigatedEvent(session, _currentURI, !success);
        }

        public virtual void OnProgressChange(GeckoSession session, int progress)
        {
        }

        public virtual void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
        {
        }
    }
}
