using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Org.Mozilla.Geckoview;
using Xam.Droid.GeckoView.Forms.Droid.Consts;
using Xam.Droid.GeckoView.Forms.Droid.Helpers;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;
using Xamarin.Forms;
using static Org.Mozilla.Geckoview.GeckoSession;

namespace Xam.Droid.GeckoView.Forms.Droid.Handlers
{
    public class ContentDelegate : global::Java.Lang.Object, IContentDelegate
    {
        protected GeckoViewRenderer _renderer;

        public ContentDelegate(GeckoViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public virtual void OnCloseRequest(GeckoSession session)
        {
        }

        public virtual void OnContextMenu(GeckoSession session, int screenX, int screenY, ContentDelegateContextElement element)
        {
        }

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
            //We actually use a simple version using the DownloadManager here
            //As GeckoView is not a WebView, we cannot get Cookies credential from current request to put it in DownloadManager

            //If a download with Cookies credential from current session is needed, it should be called from the GeckoView Streaming Download API
            //But this will be managed directly from this app and not externally from the DownloadManager. This can be problematic if
            //the download progress want to be shown by the integrated DownloadManager.

            //See https://bugzilla.mozilla.org/show_bug.cgi?id=1522705

            try
            {
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndTypeAndNormalize(Android.Net.Uri.Parse(response.Uri) , response.ContentType);
                _renderer.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException e)
            {
                DownloadFile(session, response);
            }
        }

        private void DownloadFile(GeckoSession session, WebResponseInfo response)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    string userAgent = await GeckoResultHelper.GetResult<string>(session.UserAgent);
                    DownloadFile(response, userAgent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(DownloadFile)} - {nameof(ex)}: {ex.Message}");
                }
            });
        }

        private List<WebResponseInfo> _pendingDownloads = new List<WebResponseInfo>();

        private void DownloadFile(WebResponseInfo response, string userAgent)
        {
            if (ContextCompat.CheckSelfPermission(_renderer.Context,
            Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                _pendingDownloads.Add(response);
                ActivityCompat.RequestPermissions(_renderer.Activity,
                        new string[] { Manifest.Permission.WriteExternalStorage },
                        PermissionRequestCode.REQUEST_WRITE_EXTERNAL_STORAGE);
                return;
            }

            Android.Net.Uri uri = Android.Net.Uri.Parse(response.Uri);
            string filename = response.Filename != null ? response.Filename : uri.LastPathSegment;

            DownloadManager manager = (DownloadManager)_renderer.Context.GetSystemService(Android.Content.Context.DownloadService);
            DownloadManager.Request req = new DownloadManager.Request(uri);
            req.SetMimeType(response.ContentType);
            req.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, filename);
            req.SetNotificationVisibility(DownloadVisibility.Visible);
            req.AddRequestHeader("User-Agent", userAgent);
            manager.Enqueue(req);
        }

        /// <summary>
        /// This code will never be called.
        /// It would be possible if we can trigger the granted permission event and then call things back here
        /// But it should be about the final user implementation in it's app
        /// </summary>
        private void ContinueDownloads()
        {
            List<WebResponseInfo> downloads = _pendingDownloads;
            _pendingDownloads = new List<WebResponseInfo>();

            foreach (WebResponseInfo response in downloads)
            {
                DownloadFile(_renderer.Control.Session, response);
            }
        }

        public virtual void OnFirstComposite(GeckoSession session)
        {
        }

        public virtual void OnFocusRequest(GeckoSession session)
        {
        }

        public virtual void OnFullScreen(GeckoSession session, bool fullScreen)
        {
        }

        public virtual void OnTitleChange(GeckoSession session, string title)
        {
        }
    }
}
