# Xamarin.Android GeckoView Binding

An example Project to embed a [GeckoView](https://wiki.mozilla.org/Mobile/GeckoView) View & Engine into your Android App.

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

The Xamarin.Android.GeckoView.Binding projects variation depending your MonoAndroid version, contains the Bindings to the Android Library and the Library (AAR) itself. If you want to use it, you can just copy it into your Project.

### From Nuget Package

Download the [Xamarin.Android.GeckoView](https://www.nuget.org/packages/Xamarin.Android.GeckoView/) NuGet package from Nuget.org, or install it directly through Visual Studio **Nuget Package Manager**.

### Usage

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
            session.LoadUri("https://www.google.fr");
        }
    }
}
```

This is highly inspired from the [original documentation](https://wiki.mozilla.org/Mobile/GeckoView).

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

**Additional:** Even if the JavaDoc is included on the source code, it seem that JavaDocToMd fail to generate the corresponding Bindings documentation. So there is no code documentation available for this moment.

Feel free to see [Mozilla: Documentation and Examples](https://wiki.mozilla.org/Mobile/GeckoView#Documentation_and_Examples)

## Authors

* **Guillaume ZAHRA** - [Daddoon](https://github.com/Daddoon)

## Inspiration

I took inspiration for this guide from [Kevin Gliewe](https://github.com/KevinGliewe) as his [CrossWalk binding project](https://github.com/KevinGliewe/Xamarin.Android.XWalk) was for the same type of purpose !

Thanks a lot for your work as a reference.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
