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
                Source = "https://lupblazorpaint.z20.web.core.windows.net/"
            };
            geckoForms.HorizontalOptions = LayoutOptions.FillAndExpand;
            geckoForms.VerticalOptions = LayoutOptions.FillAndExpand;

            stackLayout.Children.Add(geckoForms);
        }
    }
}
