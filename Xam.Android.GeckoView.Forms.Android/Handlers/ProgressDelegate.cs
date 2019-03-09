using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using Xam.Android.GeckoView.Forms.Android.Renderers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView.Forms.Android.Handlers
{
    public class ProgressDelegate : global::Java.Lang.Object, IProgressDelegate
    {
        private GeckoViewRenderer _renderer;

        public ProgressDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        string _currentURI = string.Empty;

        public void OnPageStart(GeckoSession session, string url)
        {
            _currentURI = url;
            _renderer.Element.SendNavigating(new Xamarin.Forms.WebNavigatingEventArgs(Xamarin.Forms.WebNavigationEvent.NewPage, _currentURI, _currentURI));
        }

        public void OnPageStop(GeckoSession session, bool success)
        {
            var result = success ? Xamarin.Forms.WebNavigationResult.Success : Xamarin.Forms.WebNavigationResult.Failure;
            _renderer.Element.SendNavigated(new Xamarin.Forms.WebNavigatedEventArgs(Xamarin.Forms.WebNavigationEvent.NewPage, _currentURI, _currentURI, result));
        }

        public void OnProgressChange(GeckoSession session, int progress)
        {
        }

        public void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
        {
        }
    }
}
