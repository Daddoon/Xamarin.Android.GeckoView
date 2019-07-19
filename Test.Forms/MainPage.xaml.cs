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
                //Test WASM performance (local app)
                Source = "http://192.168.1.114:5080/login"

                //Test for basic file download with download manager with no auth
                //Source = "https://github.com/Daddoon/Xamarin.Android.GeckoView/releases/tag/0.1.0"

                //Test for WASM support
                //Source = "https://lupblazorpaint.z20.web.core.windows.net/"
            };
            geckoForms.HorizontalOptions = LayoutOptions.FillAndExpand;
            geckoForms.VerticalOptions = LayoutOptions.FillAndExpand;

            stackLayout.Children.Add(geckoForms);
        }
    }
}
