using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACNHEncyclopedia.Droid.ServicesAndroid;
using ACNHEncyclopedia.Views.Customs;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageCustomRenderer))]
namespace ACNHEncyclopedia.Droid.ServicesAndroid
{
    public class NavigationPageCustomRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        public NavigationPageCustomRenderer(Context context) : base(context)
        {
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "Cabin-Regular.ttf");
                textView.Typeface = font;
                //toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.ActionMenuView))
            {
                var textView = (Android.Support.V7.Widget.ActionMenuView)e.Child;
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "Cabin-Regular.ttf");

                //toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}