using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Views.Customs
{
    public class NavigationPageCustom : NavigationPage
    {
        public NavigationPageCustom(Page root) : base(root)
        {
            BarBackgroundColor = Color.DarkBlue;
            BarTextColor = Color.White;
        }
    }
}
