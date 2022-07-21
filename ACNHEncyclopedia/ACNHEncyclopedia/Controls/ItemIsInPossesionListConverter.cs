using ACNHEncyclopedia.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Controls
{
    public class ItemIsInPossesionListConverter : IValueConverter
    {
        public UserDataService _userDataService;
        public List<int> itemsIds;
        public ItemIsInPossesionListConverter(UserDataService userDataService)
        {
            this._userDataService = userDataService;
            itemsIds = userDataService.GetPossesionListItems();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;
            return itemsIds.Contains(id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
