using Android.App;
using Org.Mozilla.Geckoview;
using System;
using System.Threading.Tasks;
using Xam.Droid.GeckoView.Extension.Handlers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Org.Mozilla.Gecko
{
    public abstract class GeckoViewManaged
    {
        private readonly GeckoView _view = null;

        private void SetSession(GeckoSession session, GeckoRuntime runtime)
        {
            CloseSession();
            _view.SetSession(session, runtime);
        }

        public virtual Tuple<GeckoSession, GeckoRuntime> CreateSession(bool setAsCurrentSession)
        {
            var result = CreateSession();

            if (setAsCurrentSession)
            {
                SetSession(result.Item1, result.Item2);
            }
            return result;
        }

        public virtual Tuple<GeckoSession, GeckoRuntime> CreateSession()
        {
            GeckoSession _session = new GeckoSession();
            GeckoRuntime _runtime = GeckoRuntime.Create(Application.Context);
            _session.Open(_runtime);

            _session.ProgressDelegate = new ProgressDelegate(this);
            _session.MediaDelegate = new MediaDelegate(this);
            _session.ContentDelegate = new ContentDelegate(this);
            _session.ScrollDelegate = new ScrollDelegate(this);
            _session.NavigationDelegate = new NavigationDelegate(this);

            return Tuple.Create(_session, _runtime);
        }

        private void CloseSession()
        {
            try
            {
                if (_view.Session != null)
                {
                    _view.ReleaseSession();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during session Release: {ex.Message}");
            }
        }

        private void InitGeckoView(GeckoView view, GeckoSession session, GeckoRuntime runtime)
        {
            SetSession(session, runtime);
        }

        public GeckoViewManaged(GeckoView view)
        {
            _view = view;

            var newSession = CreateSession();
            InitGeckoView(view, newSession.Item1, newSession.Item2);
        }

        public GeckoViewManaged(GeckoView view, GeckoSession session, GeckoRuntime runtime)
        {
            if (view == null)
            {
                throw new NullReferenceException($"{nameof(view)} is null");
            }

            if (session == null)
            {
                throw new NullReferenceException($"{ nameof(session) } is null");
            }

            if (runtime == null)
            {
                throw new NullReferenceException($"{nameof(runtime)} is null");
            }

            _view = view;
            InitGeckoView(view, session, runtime);
        }

        #region Properties

        public GeckoSession Session
        {
            get
            {
                return _view.Session;
            }
        }

        public GeckoView View
        {
            get
            {
                return _view;
            }
        }

        #endregion Properties

        /// <summary>
        /// Load the given URI.
        /// </summary>
        /// <param name="uri"></param>
        public virtual void LoadUri(string uri)
        {
            Session?.LoadUri(uri);
        }

        /// <summary>
        /// Load the given URI with the specified referrer and load type.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="flags"></param>
        public virtual void LoadUri(string uri, int flags)
        {
            Session?.LoadUri(uri, flags);
        }

        /// <summary>
        /// Load the given URI with the specified referrer and load type.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="flags"></param>
        public virtual void LoadUri(string uri, string referrer, int flags)
        {
            Session?.LoadUri(uri, referrer, flags);
        }

        /// <summary>
        /// Load the given URI.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="flags"></param>
        public virtual void LoadUri(Uri uri)
        {
            Session?.LoadUri(Android.Net.Uri.Parse(uri.ToString()));
        }

        /// <summary>
        /// Load the given URI with the specified referrer and load type.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="flags"></param>
        public virtual void LoadUri(Uri uri, int flags)
        {
            Session?.LoadUri(Android.Net.Uri.Parse(uri.ToString()), flags);
        }

        /// <summary>
        /// Load the given URI with the specified referrer and load type.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="flags"></param>
        public virtual void LoadUri(Uri uri, Uri referrer, int flags)
        {
            Session?.LoadUri(Android.Net.Uri.Parse(uri.ToString()), Android.Net.Uri.Parse(referrer.ToString()), flags);
        }

        /// <summary>
        /// Load the specified String data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mimeType"></param>
        public virtual void LoadString(string data, string mimeType)
        {
            Session?.LoadString(data, mimeType);
        }


        /// <summary>
        /// Load the specified bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="mimeType"></param>
        public virtual void LoadData(byte[] bytes, string mimeType)
        {
            Session?.LoadData(bytes, mimeType);
        }

        /// <summary>
        /// Stop loading.
        /// </summary>
        public virtual void Stop()
        {
            Session?.Stop();
        }

        /// <summary>
        /// Reload the current URI.
        /// </summary>
        public virtual void Reload()
        {
            Session?.Reload();
        }

        /// <summary>
        /// Restore a saved state to this GeckoSession; only data that is saved (history, scroll position, zoom, and form data) will be restored.
        /// WARNING: Not yet tested
        /// </summary>
        /// <param name="sessionState"></param>
        public virtual void RestoreState(SessionState sessionState)
        {
            Session?.RestoreState(sessionState);
        }

        /// <summary>
        /// Save the current browsing session state of this GeckoSession.
        /// WARNING: Not yet tested
        /// </summary>
        /// <returns></returns>
        public virtual Task<SessionState> SaveState()
        {
            TaskCompletionSource<SessionState> tcs = new TaskCompletionSource<SessionState>();

            try
            {
                var onSuccess = new GeckoResultOnValueListener<SessionState>(tcs);
                var onError = new GeckoResultOnExceptionListener<SessionState>(tcs);

                var data = Session?.SaveState();
                data.Then(onSuccess, onError);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        #region Event Handlers

        /// <summary>
        /// A View has started loading content from the network.
        /// </summary>
        /// <param name="session">GeckoSession that initiated the callback.</param>
        /// <param name="url">The resource being loaded.</param>
        public virtual void OnPageStart(GeckoSession session, string url)
        {
        }

        /// <summary>
        /// A View has finished loading content from the network.
        /// </summary>
        /// <param name="session">GeckoSession that initiated the callback.</param>
        /// <param name="success">Whether the page loaded successfully or an error occurred.</param>
        public virtual void OnPageStop(GeckoSession session, bool success)
        {
        }

        /// <summary>
        /// Page loading has progressed.
        /// </summary>
        /// <param name="session">GeckoSession that initiated the callback.</param>
        /// <param name="progress">Current page load progress value [0, 100].</param>
        public virtual void OnProgressChange(GeckoSession session, int progress)
        {
        }

        /// <summary>
        /// The security status has been updated.
        /// </summary>
        /// <param name="session">GeckoSession that initiated the callback.</param>
        /// <param name="securityInfo">The new security information.</param>
        public virtual void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
        {
        }

        /// <summary>
        /// An HTMLMediaElement has been created.
        /// </summary>
        /// <param name="session">Session instance.</param>
        /// <param name="element">The media element that was just created.</param>
        public virtual void OnMediaAdd(GeckoSession session, MediaElement element)
        {
        }

        /// <summary>
        /// An HTMLMediaElement has been unloaded.
        /// </summary>
        /// <param name="session">Session instance.</param>
        /// <param name="element">The media element that was unloaded.</param>
        public virtual void OnMediaRemove(GeckoSession session, MediaElement element)
        {
        }

        /// <summary>
        /// A page has requested to close
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        public virtual void OnCloseRequest(GeckoSession session)
        {
        }

        /// <summary>
        /// A user has initiated the context menu via long-press. This event is fired on links, (nested) images and (nested) media elements.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="screenX">The screen coordinates of the press.</param>
        /// <param name="screenY">The screen coordinates of the press.</param>
        /// <param name="element">The details for the pressed element.</param>
        public virtual void OnContextMenu(GeckoSession session, int screenX, int screenY, ContentDelegateContextElement element)
        {
        }

        /// <summary>
        /// The content process hosting this GeckoSession has crashed. The GeckoSession is now closed and unusable. You may call GeckoSession.open(GeckoRuntime) to recover the session, but no state is preserved. Most applications will want to call GeckoSession.loadUri(Uri) or GeckoSession.restoreState(SessionState) at this point.
        /// </summary>
        /// <param name="session">The GeckoSession that crashed.</param>
        public virtual void OnCrash(GeckoSession session)
        {
        }

        /// <summary>
        /// This is fired when there is a response that cannot be handled by Gecko (e.g., a download).
        /// </summary>
        /// <param name="session">the GeckoSession that received the external response.</param>
        /// <param name="response">the WebResponseInfo for the external response</param>
        public virtual void OnExternalResponse(GeckoSession session, WebResponseInfo response)
        {
        }

        /// <summary>
        /// Notification that the first content composition has occurred. This callback is invoked for the first content composite after either a start or a restart of the compositor.
        /// </summary>
        /// <param name="session">The GeckoSession that had a first paint event.</param>
        public virtual void OnFirstComposite(GeckoSession session)
        {
        }

        /// <summary>
        /// A page has requested focus. Note that window.focus() in content will not result in this being called.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        public virtual void OnFocusRequest(GeckoSession session)
        {
        }

        /// <summary>
        /// A page has entered or exited full screen mode. Typically, the implementation would set the Activity containing the GeckoSession to full screen when the page is in full screen mode.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="fullScreen">True if the page is in full screen mode.</param>
        public virtual void OnFullScreen(GeckoSession session, bool fullScreen)
        {
        }

        /// <summary>
        /// A page title was discovered in the content or updated after the content loaded.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="title">The title sent from the content.</param>
        public virtual void OnTitleChange(GeckoSession session, string title)
        {
        }

        /// <summary>
        /// The scroll position of the content has changed.
        /// </summary>
        /// <param name="session">GeckoSession that initiated the callback.</param>
        /// <param name="scrollX">The new horizontal scroll position in pixels.</param>
        /// <param name="scrollY">The new vertical scroll position in pixels.</param>
        public virtual void OnScrollChanged(GeckoSession session, int scrollX, int scrollY)
        {
        }

        #region NavigationDelegate

        /// <summary>
        /// The view's ability to go back has changed.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="canGoBack">The new value for the ability.</param>
        public virtual void OnCanGoBack(GeckoSession session, bool canGoBack)
        {
        }

        /// <summary>
        /// The view's ability to go forward has changed.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="canGoForward">The new value for the ability.</param>
        public virtual void OnCanGoForward(GeckoSession session, bool canGoForward)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="uri">The URI that failed to load.</param>
        /// <param name="error">A WebRequestError containing details about the error</param>
        /// <returns>A URI to display as an error. Returning null will halt the load entirely.</returns>
        public virtual GeckoResult OnLoadError(GeckoSession session, string uri, WebRequestError error)
        {
            return GeckoResult.FromValue(null);
        }

        /// <summary>
        /// A request to open an URI. This is called before each top-level page load to allow custom behavior. For example, this can be used to override the behavior of TAGET_WINDOW_NEW requests, which defaults to requesting a new GeckoSession via onNewSession.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="request">The GeckoSession.NavigationDelegate.LoadRequest containing the request details.</param>
        /// <returns>A GeckoResult with a AllowOrDeny value which indicates whether or not the load was handled. If unhandled, Gecko will continue the load as normal. If handled (true value), Gecko will abandon the load. A null return value is interpreted as false (unhandled).</returns>
        public virtual GeckoResult OnLoadRequest(GeckoSession session, avigationDelegateClassLoadRequest request)
        {
            return GeckoResult.FromValue(null);
        }

        public virtual void OnLocationChange(GeckoSession session, string url)
        {
        }

        /// <summary>
        /// A request has been made to open a new session. The URI is provided only for informational purposes. Do not call GeckoSession.loadUri() here. Additionally, the returned GeckoSession must be a newly-created one.
        /// </summary>
        /// <param name="session">The GeckoSession that initiated the callback.</param>
        /// <param name="uri">The URI to be loaded.</param>
        /// <returns>A GeckoResult which holds the returned GeckoSession. May be null, in which case the request for a new window by web content will fail. e.g., window.open() will return null.</returns>
        public virtual GeckoResult OnNewSession(GeckoSession session, string uri)
        {
            return GeckoResult.FromValue(CreateSession().Item1);
        }

        #endregion NavigationDelegate

        #endregion Event Handlers
    }
}
