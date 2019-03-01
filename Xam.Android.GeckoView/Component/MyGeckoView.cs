
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using System;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView
{
    public class MyGeckoView : GeckoViewManaged
    {
        public MyGeckoView(Org.Mozilla.Geckoview.GeckoView view, GeckoSession session, GeckoRuntime runtime) : base(view, session, runtime)
        {
        }

        public override void OnPageStart(GeckoSession session, string url)
        {
            Console.WriteLine($"GeckoView: OnPageStart: {url}");
        }
        public override void OnPageStop(GeckoSession session, bool success)
        {
            Console.WriteLine($"GeckoView: OnPageStop: {success}");
        }
        public override void OnProgressChange(GeckoSession session, int progress)
        {
            Console.WriteLine($"GeckoView: OnProgressChange: {progress}%");
        }

        public override void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
        {
            Console.WriteLine($"GeckoView: OnSecurityChange");
        }

        public override void OnMediaAdd(GeckoSession session, MediaElement element)
        {
            Console.WriteLine($"GeckoView: OnMediaAdd");
        }

        public override void OnMediaRemove(GeckoSession session, MediaElement element)
        {
            Console.WriteLine($"GeckoView: OnMediaRemove");
        }

        public override void OnCloseRequest(GeckoSession session)
        {
            Console.WriteLine($"GeckoView: OnCloseRequest");
        }

        public override void OnContextMenu(GeckoSession session, int screenX, int screenY, ContentDelegateContextElement element)
        {
            Console.WriteLine($"GeckoView: OnContextMenu");
        }

        public override void OnCrash(GeckoSession session)
        {
            Console.WriteLine($"GeckoView: OnCrash");
        }

        public override void OnExternalResponse(GeckoSession session, WebResponseInfo response)
        {
            Console.WriteLine($"GeckoView: OnExternalResponse");
        }

        public override void OnFirstComposite(GeckoSession session)
        {
            Console.WriteLine($"GeckoView: OnFirstComposite");
        }

        public override void OnFocusRequest(GeckoSession session)
        {
            Console.WriteLine($"GeckoView: OnFocusRequest");
        }

        public override void OnFullScreen(GeckoSession session, bool fullScreen)
        {
            Console.WriteLine($"GeckoView: OnFullScreen");
        }

        public override void OnTitleChange(GeckoSession session, string title)
        {
            Console.WriteLine($"GeckoView: OnTitleChange");
        }

        public override void OnScrollChanged(GeckoSession session, int scrollX, int scrollY)
        {
            Console.WriteLine($"GeckoView: OnScrollChanged");
        }
    }
}