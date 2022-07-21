using ACNHEncyclopedia.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Controls
{
    class TranslateAppConverter : IValueConverter
    {
        public TranslateAppService _translateAppService;
        public TranslateAppConverter(TranslateAppService translateAppService)
        {
            this._translateAppService = translateAppService;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && parameter == null)
            {
                return _translateAppService.GetAppNameTranslated(value.ToString());
            }
            return _translateAppService.GetAppNameTranslated(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
