using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public class TranslateItemService
    {
        private GameDataLoader _gameDataLoader;
        private Dictionary<string, string> TranslationItems {get { return _gameDataLoader.GetTranslationItems(); } }
        public TranslateItemService(GameDataLoader gameDataLoader)
        {
            this._gameDataLoader = gameDataLoader;

            //Load translation file
            //LoadTranslationsItems();
        }

        public string GetItemNameTranslated(string translationId)
        {
            string text = "";
            try
            {
                if (TranslationItems.ContainsKey(translationId))
                {
                    text = TranslationItems[translationId];
                    if (text.Length == 1)
                    {
                        text = text.ToUpper();
                    }
                    else if (text.Length > 1)
                    {
                        text = text[0].ToString().ToUpper() + text.Substring(1);
                    }
                }

            }
            catch(Exception ex)
            {

            }
            return text;
        }

        //public void LoadTranslationsItems()
        //{
        //    TranslationItems = _gameDataLoader.GetTranslationItems();
        //}
    }
}
