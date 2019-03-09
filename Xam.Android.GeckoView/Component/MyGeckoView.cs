
using Org.Mozilla.Gecko;
using Org.Mozilla.Geckoview;
using System;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Android.GeckoView
{
    public class MyGeckoView : GeckoViewManaged
    {
        public MyGeckoView(Org.Mozilla.Geckoview.GeckoView view) : base(view)
        {
        }

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

        /// <summary>
        /// The view's ability to go back has changed.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="canGoBack">The new value for the ability.</param>
        public override void OnCanGoBack(GeckoSession session, bool canGoBack)
        {
            Console.WriteLine($"{nameof(OnCanGoBack)}: {nameof(canGoBack)}{canGoBack}");
        }

        /// <summary>
        /// The view's ability to go forward has changed.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="canGoForward">The new value for the ability.</param>
        public override void OnCanGoForward(GeckoSession session, bool canGoForward)
        {
            Console.WriteLine($"{nameof(OnCanGoForward)}: {nameof(canGoForward)}{canGoForward}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="uri">The URI that failed to load.</param>
        /// <param name="error">A WebRequestError containing details about the error</param>
        /// <returns>A URI to display as an error. Returning null will halt the load entirely.</returns>
        public override GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            Console.WriteLine($"{nameof(OnLoadError)}: {nameof(uri)}{uri}");
            return GeckoResult.FromValue(null);
        }

        /// <summary>
        /// A request to open an URI. This is called before each top-level page load to allow custom behavior. For example, this can be used to override the behavior of TAGET_WINDOW_NEW requests, which defaults to requesting a new GeckoSession via onNewSession.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="request">The GeckoSession.NavigationDelegate.LoadRequest containing the request details.</param>
        /// <returns>A GeckoResult with a AllowOrDeny value which indicates whether or not the load was handled. If unhandled, Gecko will continue the load as normal. If handled (true value), Gecko will abandon the load. A null return value is interpreted as false (unhandled).</returns>
        public override GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            Console.WriteLine($"{nameof(OnLoadRequest)}: {nameof(request.Uri)}{request.Uri}, {nameof(request.IsRedirect)}{request.IsRedirect}");
            return GeckoResult.FromValue(null);
        }

        public override void OnLocationChange(GeckoSession session, string url)
        {
            Console.WriteLine($"{nameof(OnLocationChange)}: {url}");
            base.OnLocationChange(session, url);
        }

        /// <summary>
        /// A request has been made to open a new session. The URI is provided only for informational purposes. Do not call GeckoSession.loadUri() here. Additionally, the returned GeckoSession must be a newly-created one.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="uri">The URI to be loaded.</param>
        /// <returns>A GeckoResult which holds the returned GeckoSession. May be null, in which case the request for a new window by web content will fail. e.g., window.open() will return null.</returns>
        public override GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            Console.WriteLine($"{nameof(OnNewSession)}: {uri}");
            return base.OnNewSession(session, uri);
        }
    }
}