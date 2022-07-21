using Prism.Navigation;
using System;

namespace ACNHEncyclopedia.Models
{
    public class ItemMenu
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string NavigationUri { get; set; }
        public NavigationParameters Parameters { get; set; }

    }
}