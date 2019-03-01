﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Org.Mozilla.Geckoview;
using Android;
using Org.Mozilla.Gecko;

namespace Xam.Android.GeckoView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_geckoview);

            Org.Mozilla.Geckoview.GeckoView view = (Org.Mozilla.Geckoview.GeckoView)FindViewById(Resource.Id.geckoview);
            GeckoSession session = new GeckoSession();
            GeckoRuntime runtime = GeckoRuntime.Create(this);
            session.Open(runtime);
            view.SetSession(session, runtime);

            MyGeckoView managed = new MyGeckoView(view, session, runtime);

            managed.LoadUri("https://www.google.fr"); // Or any other URL...

            //session.LoadUri("https://www.google.fr"); // Or any other URL...
            //managed.LoadUri("https://lupblazorclockcanvas.z20.web.core.windows.net/"); // Or any other URL...
        }
    }
}