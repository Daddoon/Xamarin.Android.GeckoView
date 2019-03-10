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
    internal class ScrollDelegate : global::Java.Lang.Object, IScrollDelegate
    {
        private GeckoViewManaged _viewManaged;

        public ScrollDelegate(GeckoViewManaged managedGeckoView)
        {
            _viewManaged = managedGeckoView;
        }

        public void OnScrollChanged(GeckoSession p0, int p1, int p2)
        {
            _viewManaged.OnScrollChanged(p0, p1, p2);
        }
    }
}
