using System;
using System.Collections.Generic;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public class TranslateAppService
    {
        private GameDataLoader _gameDataLoader;
        private Dictionary<string, string> TranslationItems { get { return _gameDataLoader.GetTranslationApp(); } }
        public TranslateAppService(GameDataLoader gameDataLoader)
        {
            this._gameDataLoader = gameDataLoader;

            //Load translation file
            //LoadTranslationsApp();
        }

        public string GetAppNameTranslated(string translationId)
        {
            string text = "";
            try
            {
                if (TranslationItems.ContainsKey(translationId)) text = TranslationItems[translationId];
            }
            catch(Exception ex)
            {

            }
            return text;
        }

        //public void LoadTranslationsApp()
        //{
        //    TranslationItems = _gameDataLoader.GetTranslationApp();
        //}
    }
}
