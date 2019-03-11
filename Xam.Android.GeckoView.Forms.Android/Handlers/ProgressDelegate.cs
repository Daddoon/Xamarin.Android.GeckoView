using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Forms.Droid.Handlers
{
    public class ProgressDelegate : global::Java.Lang.Object, IProgressDelegate
    {
        private GeckoViewRenderer _renderer;

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
            _currentURI = url;
            _renderer.Element.SendNavigating(new Xamarin.Forms.WebNavigatingEventArgs(Xamarin.Forms.WebNavigationEvent.NewPage, _currentURI, _currentURI));
        }

        public virtual void OnPageStop(GeckoSession session, bool success)
        {
            var result = success ? Xamarin.Forms.WebNavigationResult.Success : Xamarin.Forms.WebNavigationResult.Failure;
            _renderer.Element.SendNavigated(new Xamarin.Forms.WebNavigatedEventArgs(Xamarin.Forms.WebNavigationEvent.NewPage, _currentURI, _currentURI, result));
        }

        public virtual void OnProgressChange(GeckoSession session, int progress)
        {
        }

        public virtual void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
        {
        }
    }
}
