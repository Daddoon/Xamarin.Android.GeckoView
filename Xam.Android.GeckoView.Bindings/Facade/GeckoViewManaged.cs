using Org.Mozilla.Geckoview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Android.GeckoView.Bindings.Handlers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Org.Mozilla.Gecko
{
    public abstract class GeckoViewManaged
    {
        private readonly GeckoSession _session = null;
        private readonly GeckoRuntime _runtime = null;
        private readonly GeckoView _view = null;

        public GeckoViewManaged(GeckoView view, GeckoSession session, GeckoRuntime runtime)
        {
            if (view == null || session == null || runtime == null)
            {
                throw new NullReferenceException("GeckoView, GeckoSession or GeckoRuntime is null");
            }

            _session = session;
            _runtime = runtime;
            _view = view;
            _session.ProgressDelegate = new ProgressDelegate(this);
            _session.MediaDelegate = new MediaDelegate(this);
            _session.ContentDelegate = new ContentDelegate(this);
            _session.ScrollDelegate = new ScrollDelegate(this);
        }

        #region Properties

        public GeckoSession GeckoSession
        {
            get
            {
                return _session;
            }
        }

        public GeckoRuntime GeckoRuntime
        {
            get
            {
                return _runtime;
            }
        }

        public GeckoView GeckoView
        {
            get
            {
                return _view;
            }
        }

        #endregion Properties

        public virtual void LoadUri(string uri)
        {
            _session.LoadUri(uri);
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

        #endregion Event Handlers
    }
}
