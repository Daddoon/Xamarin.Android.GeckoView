using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView.Extension.Handlers
{
    internal class MediaDelegate : global::Java.Lang.Object, IMediaDelegate
    {
        private GeckoViewManaged _viewManaged;

        public MediaDelegate(GeckoViewManaged managedGeckoView)
        {
            _viewManaged = managedGeckoView;
        }

        public void OnMediaAdd(GeckoSession p0, MediaElement p1)
        {
            _viewManaged.OnMediaAdd(p0, p1);
        }

        public void OnMediaRemove(GeckoSession p0, MediaElement p1)
        {
            _viewManaged.OnMediaRemove(p0, p1);
        }
    }
}
