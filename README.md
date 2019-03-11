# Xamarin.Android GeckoView

Projects & Nuget packages in order to embed a [GeckoView](https://wiki.mozilla.org/Mobile/GeckoView) View & Engine into your Xamarin.Android App or Xamarin.Forms App (Android only of course)

## GeckoView Version

The current version is: **GeckoView 67.0.20190228011332**
From a nightly-try test build, as the multi-arch support is new.

## Compatibility

Usable from **Android 4.4** to **Android 9.0**

## Pre-requisite

**SDK:**
- MonoAndroid >= 8.0 (API 26) - Bare minimum from GeckoView native side, **MonoAndroid >= 8.1 (API 27) highly recommended** as a minimum as the compilation had some issue at the end of the build, even if everything seemed right.

**Nuget Dependencies:**
- Xamarin.Android.Support.v4 (>= 27.0.0.0) required by **GeckoView**
- Xamarin.Android.Support.v7.Palette (>= 27.0.0.0) required by **GeckoView**
- Xamarin.Android.Support.v7.AppCompat (>= 27.0.0.0) required by **Xamarin**

**Warning:**

The NuGet package is pretty big, as the GeckoView native library is **150 MB** because it's a multi-arch platform version.

If you build your own project with the **one APK per ABI option** checked, the library size will be reduced to **~50MB per APK architecture**

## Getting Started

### From Source

- The **Xam.Android.GeckoView.Binding** projects, contains the Bindings to the Android Library and the Library (AAR) itself. If you want to use it, you can just copy it into your Project.
- The **Xam.Android.GeckoView.Forms** & **Xam.Android.GeckoView.Forms.Android** projects, contains all the class logic in order to call GeckoView from a pur Xamarin.Forms context.

### From Nuget Package

- **GeckoView**: Download the [Xamarin.Android.GeckoView](https://www.nuget.org/packages/Xamarin.Android.GeckoView/) NuGet package from Nuget.org, or install it directly through Visual Studio **Nuget Package Manager**.
- **GeckoView.Forms**: Download the [Xamarin.Android.GeckoView.Forms](https://www.nuget.org/packages/Xamarin.Android.GeckoView.Forms/) NuGet package from Nuget.org, or install it directly through Visual Studio **Nuget Package Manager**.

### Usage guide for Xamarin.Android

This guide is for Xamarin.Android. **For Xamarin.Forms see the [usage guide for Xamarin.Forms](https://github.com/Daddoon/Xamarin.Android.GeckoView/#usage-guide-for-xamarinforms)**

As a first step, don't forget to add the **[Xamarin.Android.GeckoView](https://www.nuget.org/packages/Xamarin.Android.GeckoView/) NuGet package** if you are not working from sources.

An Example how to use the `Org.Mozilla.GeckoView`:

Modify or create an Android layout resource, like the one in **Resources > layout > activity_main.axml** like this:

```xml
<?xml version="1.0" encoding="utf-8"?>
<org.mozilla.geckoview.GeckoView
    xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:app="http://schemas.android.com/apk/res-auto"
    xmlns:tools="http://schemas.android.com/tools"
    android:id="@+id/geckoview"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent" />
```

And modify your MainActivity file like this:

```csharp
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Org.Mozilla.Geckoview;

namespace Xam.Android.MyGeckoApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Org.Mozilla.Geckoview.GeckoView view = (Org.Mozilla.Geckoview.GeckoView)FindViewById(Resource.Id.geckoview);
            GeckoSession session = new GeckoSession();
            GeckoRuntime runtime = GeckoRuntime.Create(this);
            session.Open(runtime);
            view.SetSession(session, runtime);

            //Use Mozilla GeckoView API with session object
            session.LoadUri("https://www.google.com/");
        }
    }
}
```

This is highly inspired from the [original documentation](https://wiki.mozilla.org/Mobile/GeckoView).

### Usage guide for Xamarin.Forms

- Create a **new Xamarin.Forms** project.
- Don't forget to add the **[Xamarin.Android.GeckoView.Forms](https://www.nuget.org/packages/Xamarin.Android.GeckoView.Forms/) NuGet package** if you are not working from sources, on your **Xamarin.Android** base project and your **Xamarin.Forms** shared project

Add **GeckoViewRenderer.Init(this)** in your **OnCreate** overrided in **MainActivity**.
If we take our sample Xamarin.Forms project it shoud look like something like this:

```csharp
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Xam.Droid.GeckoView.Forms.Droid.Renderers;

namespace Xam.Droid.GeckoView.Test.Forms.Droid
{
    [Activity(Label = "Xam.Droid.GeckoView.Test.Forms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            GeckoViewRenderer.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
```

Then on the **Xamarin.Forms** page where you want to add your GeckoView component, you only just have to instanciate a new **GeckoViewForms** object and add it to your current page.
If we take the example from the **MainPage.xaml**, things may look like this:

In **MainPage.xaml**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:Xam.Droid.GeckoView.Test.Forms"
             x:Class="Xam.Droid.GeckoView.Test.Forms.MainPage">

    <StackLayout x:Name="stackLayout" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
    </StackLayout>

</ContentPage>
```

In **MainPage.xaml.cs**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Droid.GeckoView.Forms;
using Xamarin.Forms;

namespace Xam.Droid.GeckoView.Test.Forms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var geckoForms = new GeckoViewForms()
            {
                Source = "https://www.google.com/"
            };
            geckoForms.HorizontalOptions = LayoutOptions.FillAndExpand;
            geckoForms.VerticalOptions = LayoutOptions.FillAndExpand;

            stackLayout.Children.Add(geckoForms);
        }
    }
}
```

## Additional notes:

- Javascript evaluation from/to Device is not supported at all by GeckoView
- It is highly recommended that you inherit your control from the **GeckoViewForms** class, and create your own **CustomRenderer** class inheriting **GeckoViewRenderer**, if you need to customize a lot of additional behaviors.
- "Standard" events and methods like the one we can find on the basic **Webview** control in Xamarin.Forms are also present.

Also, as some Xamarin.Forms events on the webview are based on the custom delegates set by this package when constructing the GeckoView object, some behavior may broke if you change them.
The most concern is about the various GeckoSession.*Something*Delegate objects.

**At the moment, if you want to add your own logic:**

On your own inherited CustomRenderer override **CreateNewSession**. Here is the original implementation:

```csharp
public virtual Tuple<GeckoSession, GeckoRuntime> CreateNewSession()
{
    GeckoSession _session = new GeckoSession();
    GeckoRuntime _runtime = GeckoRuntime.Create(Context);
    _session.Open(_runtime);
    _session.ProgressDelegate = new ProgressDelegate(this);
    _session.ContentDelegate = new ContentDelegate(this);

    return Tuple.Create(_session, _runtime);
}
```

To something in your class like:

```csharp
public override Tuple<GeckoSession, GeckoRuntime> CreateNewSession()
{
    GeckoSession _session = new GeckoSession();
    GeckoRuntime _runtime = GeckoRuntime.Create(Context);
    _session.Open(_runtime);
    _session.ProgressDelegate = new MyProgressDelegate(this);
    _session.ContentDelegate = new MyContentDelegate(this);
    _session.NavigationDelegate = new MyNavigationDelegate();

    return Tuple.Create(_session, _runtime);
}
```

So here, add your own additional Delegates on the **GeckoSession** object if needed, see the official GeckoView documentation for more info.
If you want to do your own logic but preserve the basic behaviors set for the Forms implementation, you just have to inherit from **ProgressDelegate** and **ContentDelegate** classes, and override any methods you want to use.

Your own **MyProgressDelegate** class can look like this:

```csharp
public class MyProgressDelegate : Xam.Droid.GeckoView.Forms.Droid.Handler.ProgressDelegate
{
    public MyProgressDelegate(GeckoViewRenderer renderer) : base(renderer)
    {
        //renderer is available as a protected object inherited in your subclass scope as _renderer
        //You may also remove this constructor if not needed
    }

    public override void OnPageStart(GeckoSession session, string url)
    {
        base.OnPageStart(session, url);
        //Do you own logic
    }

    public override void OnPageStop(GeckoSession session, bool success)
    {
        base.OnPageStop(session, sucess);
        //Do you own logic
    }

    public override void OnProgressChange(GeckoSession session, int progress)
    {
        base.OnProgressChange(session, progress);
        //Do you own logic
    }

    public override void OnSecurityChange(GeckoSession session, ProgressDelegateSecurityInformation securityInfo)
    {
        base.OnSecurityChange(session, securityInfo);
        //Do you own logic
    }
}
```


## Permissions

According to the current [AndroidManifest.xml](https://github.com/mozilla/gecko-dev/blob/master/mobile/android/geckoview/src/main/AndroidManifest.xml) in GeckoView read-only repository, the minimal Permissions required to run are:

```xml
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
<uses-permission android:name="android.permission.INTERNET"/>
<uses-permission android:name="android.permission.WAKE_LOCK"/>
```

If you want to access Hardware components, you have to add the permission accordingly to your `AndroidManifest.xml` file.

## Additional

**NOTE:** From the GeckoView library native dependencies, version **26.1.0.0** is the bare minimum, but it seem that Xamarin fail to compile with the library on this version.

Even if the JavaDoc is included on the source code, it seem that JavaDocToMd fail to generate the corresponding Bindings documentation. So there is no code documentation available for this moment.

Feel free to see [Mozilla: Documentation and Examples](https://wiki.mozilla.org/Mobile/GeckoView#Documentation_and_Examples)

## Authors

* **Guillaume ZAHRA** - [Daddoon](https://github.com/Daddoon)

## Inspiration

I took inspiration for this guide from [Kevin Gliewe](https://github.com/KevinGliewe) as his [CrossWalk binding project](https://github.com/KevinGliewe/Xamarin.Android.XWalk) was for the same type of purpose !

Thanks a lot for your work as a reference.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
