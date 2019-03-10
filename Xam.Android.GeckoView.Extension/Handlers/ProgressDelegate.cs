using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Extension.Handlers
{
    internal class ProgressDelegate : global::Java.Lang.Object, IProgressDelegate
    {
        private GeckoViewManaged _viewManaged;

        public ProgressDelegate(GeckoViewManaged managedGeckoView)
        {
            _viewManaged = managedGeckoView;
        }

        public void OnPageStart(GeckoSession p0, string p1)
        {
            _viewManaged.OnPageStart(p0, p1);
        }

        public void OnPageStop(GeckoSession p0, bool p1)
        {
            _viewManaged.OnPageStop(p0, p1);
        }

        public void OnProgressChange(GeckoSession p0, int p1)
        {
            _viewManaged.OnProgressChange(p0, p1);
        }

        public void OnSecurityChange(GeckoSession p0, ProgressDelegateSecurityInformation p1)
        {
            _viewManaged.OnSecurityChange(p0, p1);
        }
    }
}
