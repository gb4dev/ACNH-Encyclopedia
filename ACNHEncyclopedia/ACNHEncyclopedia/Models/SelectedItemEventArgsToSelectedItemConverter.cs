using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Models
{
	public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object item = null;
			if(parameter is ListView)
			{
				ListView listView = (ListView)parameter;
				item = listView.SelectedItem;
				listView.SelectedItem = null;
			}
			var eventArgs = value as ItemTappedEventArgs;
			if(eventArgs.Item != null) item = eventArgs.Item;
			return item;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
