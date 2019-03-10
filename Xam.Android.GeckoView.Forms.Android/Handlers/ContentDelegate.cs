using System;
using Android.Content;
using Android.Net;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Forms.Droid.Handlers
{
    public class ContentDelegate : global::Java.Lang.Object, IContentDelegate
    {
        private GeckoViewRenderer _renderer;

        public ContentDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public void OnCloseRequest(GeckoSession session)
        {
        }

        public void OnContextMenu(GeckoSession session, int screenX, int screenY, ContentDelegateContextElement element)
        {
        }

        public void OnCrash(GeckoSession session)
        {
        }

        /// <summary>
        /// This is fired when there is a response that cannot be handled by Gecko (e.g., a download).
        /// </summary>
        /// <param name="session">the GeckoSession that received the external response.</param>
        /// <param name="response">the WebResponseInfo for the external response</param>
        public void OnExternalResponse(GeckoSession session, WebResponseInfo response)
        {
            //We actually use a simple version using the DownloadManager here
            //As GeckoView is not a WebView, we cannot get Cookies credential from current request to put it in DownloadManager

            //If a download with Cookies credential from current session is needed, it should be called from the GeckoView Streaming Download API
            //But this will be managed directly from this app and not externally from the DownloadManager. This can be problematic if
            //the download progress want to be shown by the integrated DownloadManager.

            //See https://bugzilla.mozilla.org/show_bug.cgi?id=1522705

            //try
            //{
            //    Intent intent = new Intent(Intent.ActionView);
            //    intent.SetDataAndTypeAndNormalize(new Android.Net.Uri(, response.ContentType);
            //    startActivity(intent);
            //}
            //catch (ActivityNotFoundException e)
            //{
            //    downloadFile(response);
            //}
        }

        public void OnFirstComposite(GeckoSession session)
        {
        }

        public void OnFocusRequest(GeckoSession session)
        {
        }

        public void OnFullScreen(GeckoSession session, bool fullScreen)
        {
        }

        public void OnTitleChange(GeckoSession session, string title)
        {
        }
    }
}
