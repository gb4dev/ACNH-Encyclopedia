using ACNHEncyclopedia.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Controls
{
    public class TranslateItemConverter : IValueConverter
    {
        public TranslateItemService _translateItemService;
        public TranslateItemConverter(TranslateItemService translateItemService)
        {
            this._translateItemService = translateItemService;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _translateItemService.GetItemNameTranslated(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
