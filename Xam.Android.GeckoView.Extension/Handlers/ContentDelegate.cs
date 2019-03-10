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
    internal class ContentDelegate : global::Java.Lang.Object, IContentDelegate
    {
        private GeckoViewManaged _viewManaged;

        public ContentDelegate(GeckoViewManaged managedGeckoView)
        {
            _viewManaged = managedGeckoView;
        }

        public void OnCloseRequest(GeckoSession p0)
        {
            _viewManaged.OnCloseRequest(p0);
        }

        public void OnContextMenu(GeckoSession p0, int p1, int p2, ContentDelegateContextElement p3)
        {
            _viewManaged.OnContextMenu(p0, p1, p2, p3);
        }

        public void OnCrash(GeckoSession p0)
        {
            _viewManaged.OnCrash(p0);
        }

        public void OnExternalResponse(GeckoSession p0, WebResponseInfo p1)
        {
            _viewManaged.OnExternalResponse(p0, p1);
        }

        public void OnFirstComposite(GeckoSession p0)
        {
            _viewManaged.OnFirstComposite(p0);
        }

        public void OnFocusRequest(GeckoSession p0)
        {
            _viewManaged.OnFocusRequest(p0);
        }

        public void OnFullScreen(GeckoSession p0, bool p1)
        {
            _viewManaged.OnFullScreen(p0, p1);
        }

        public void OnTitleChange(GeckoSession p0, string p1)
        {
            _viewManaged.OnTitleChange(p0, p1);
        }
    }
}
