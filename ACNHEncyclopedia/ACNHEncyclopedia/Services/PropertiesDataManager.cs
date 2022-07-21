using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public static class PropertiesDataManager<T>
    {
        public static T GetValue(string key)
        {
            if (!Xamarin.Forms.Application.Current.Properties.ContainsKey(key)) return default(T);
            string json = Xamarin.Forms.Application.Current.Properties[key].ToString();
            T data = default(T);
            try
            {
                data = JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception ex) { }
            return data;
        }

        public static bool SetValue(string key, T value)
        {
            bool result = false;
            try
            {
                string json = JsonConvert.SerializeObject(value);
                Xamarin.Forms.Application.Current.Properties[key] = json;
                Xamarin.Forms.Application.Current.SavePropertiesAsync();
                result = true;
            }
            catch (Exception ex) { }
            return result;
        }
    }
}
