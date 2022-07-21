using System;
using System.Collections.Generic;
using System.Text;

namespace ACNHEncyclopedia.Models
{
    public enum MenuItemType
    {
        GameItems
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
