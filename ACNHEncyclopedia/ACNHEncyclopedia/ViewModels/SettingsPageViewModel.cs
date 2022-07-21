using ACNHEncyclopedia.Services;
using ACNHEncyclopedia.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private GameDataLoader _gameDataLoader;
        TranslateItemService _translateItemService;
        TranslateAppService _translateAppService;
        private INavigationService _navigationService;

        private List<KeyValuePair<string, string>> _langs;
        public List<KeyValuePair<string,string>> Langs
        {
            get { return _langs; }
            set { SetProperty(ref _langs, value); }
        }
        private KeyValuePair<string, string> _selectedLang;
        public KeyValuePair<string, string> SelectedLang
        {
            get { return _selectedLang; }
            set {
                SetProperty(ref _selectedLang, value);
                SetLanguage(value);
            }
        }
        public SettingsPageViewModel(GameDataLoader gameDataLoader, INavigationService navigationService)
        {
            this._gameDataLoader = gameDataLoader;
            _navigationService = navigationService;
            this.Langs = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Deutsch", "EUde"), // id , trad
                new KeyValuePair<string, string>("English", "EUen"),
                new KeyValuePair<string, string>("Español", "EUes"),
                new KeyValuePair<string, string>("Français", "EUfr"),
                new KeyValuePair<string, string>("Italiano", "EUit"),
                new KeyValuePair<string, string>("Nederlands", "EUnl"),
                new KeyValuePair<string, string>("русский", "EUru"),
                new KeyValuePair<string, string>("にほんご", "JPja"),
                new KeyValuePair<string, string>("韩国人", "KRko")
            };
            //if (Application.Current.Properties.ContainsKey("Language")) _selectedLang = Application.Current.Properties["Language"].ToString();
        }

        public void SetLanguage(KeyValuePair<string, string> langKey)
        {
            if (langKey.Value == null) return;
            string tradId = langKey.Value;
            Application.Current.Properties["Language"] = tradId;
            Application.Current.SavePropertiesAsync();
            //_translateItemService.LoadTranslationsItems();
            //_translateAppService.LoadTranslationsApp();
            _gameDataLoader.LoadTranslationItems();
            _gameDataLoader.LoadTranslationApp();
            _navigationService.NavigateAsync("/MainPage");
        }
    }
}
