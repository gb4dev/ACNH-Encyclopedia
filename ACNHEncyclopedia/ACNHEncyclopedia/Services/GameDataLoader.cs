using ACNHEncyclopedia.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Services
{
    public class GameDataLoader
    {
        GameData GameData { get; set; }
        IFilesManager FileManager { get; set; }

        private Dictionary<string, string> TranslationApp { get; set; }
        private Dictionary<string, string> TranslationItems { get; set; }

        public GameDataLoader()
        {
            FileManager = DependencyService.Get<IFilesManager>();
            LoadData();
        }

        public void LoadData()
        {
            //string json = FileManager.GetTextFromAssetFile("Data/GameDataRelease.json");
            //GameData = JsonConvert.DeserializeObject<GameData>(json); // set data with json
            // set data from bson
            string bson = FileManager.GetTextFromAssetFile("Data/Data");
            byte[] data = Convert.FromBase64String(bson);

            try
            {
                MemoryStream ms = new MemoryStream(data);
                using (BsonReader reader = new BsonReader(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    GameData = serializer.Deserialize<GameData>(reader);
                }

            }
            catch(Exception e)
            {

            }
            GameData.Items.ForEach(i =>
            {
                i.Image = ImageSource.FromStream(() =>
                {
                    return GetImageStream($"{i.ImageName}.png");
                });
            });
            GameData.Items = GameData.Items.OrderBy(i=>i.ImageName).ToList();
            GameData.Crafts = GameData.Crafts.OrderBy(c => GameData.Items.FirstOrDefault(i=>i.Id == c.ItemIdToCraft)?.ImageName).ToList();
            GameData.Kinds = GameData.Kinds.OrderBy(k => k.Name).ToList();
            foreach(Item item in GameData.Items)
            {
                item.Kind = GameData.Kinds.FirstOrDefault(k => item.KindId == k.Id);
            }

            LoadTranslationApp();
            LoadTranslationItems();
        }

        public List<Item> GetItems()
        {
            return GameData.Items;
        }

        public List<Item> GetItemsByCategory(string categoryName)
        {
            Category cat = Category.Categories.FirstOrDefault(c => c.Name == categoryName);

            if (cat == null) return new List<Item>();
            return GameData.Items.Where(i=>i.Kind != null && cat.CategoryKinds.Contains(i.Kind.Name)).ToList();
        }
        

        public List<Kind> GetKinds()
        {
            return GameData.Kinds;
        }

        public Stream GetImageStream(string filename)
        {
            Stream stream = FileManager.GetImageAssetToStream(filename);
            if(stream == null)
            {
                stream = FileManager.GetImageAssetToStream($"Images/UnKnown.png");
            }
            return stream;
        }

        public Dictionary<string, string> LanguagesForDetectingCulture = new Dictionary<string, string>()
        {
            {"de", "EUde"} ,
            {"en", "EUen"} ,
            {"es", "EUes"} ,
            {"fr", "EUfr"} ,
            {"it", "EUit"} ,
            {"nl", "EUnl"} ,
            {"ru", "EUru"} ,
            {"ko", "KRko"} 
        };

        public Dictionary<string, string> LanguagesForDisplay = new Dictionary<string, string>()
        {
            {"Deutsch", "EUde"} ,
            {"English", "EUen"} ,
            {"Español", "EUes"} ,
            {"Français", "EUfr"} ,
            {"Italiano", "EUit"} ,
            {"Nederlands", "EUnl"} ,
            {"русский", "EUru"} ,
            {"韩国人", "KRko" }
        };

        public string GetCulture()
        {
            string tradId = "EUen";
            bool languageSet = Application.Current.Properties.ContainsKey("Language");
            if (!languageSet)
            {
                string cultureName = CultureInfo.CurrentCulture.Name.Split('-')[0];
                if (LanguagesForDetectingCulture.ContainsKey(cultureName)) tradId = LanguagesForDetectingCulture[cultureName];
                Application.Current.Properties["Language"] = tradId;
                Application.Current.SavePropertiesAsync();
            }
            else
            {
                tradId = Application.Current.Properties["Language"].ToString();
            }
            return tradId;
        }

        public void LoadTranslationItems()
        {
            //Get the culture in the properties

            string tradId = GetCulture();

            TranslationItems = new Dictionary<string, string>();

            //load the file for culture
            string dataTranslate = FileManager.GetTextFromAssetFile($"Traduction/Items/ACNHTraduction{tradId}.json");
            try
            {
                TranslationItems = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataTranslate);
            }
            catch(Exception ex)
            {

            }
        }

        public Dictionary<string, string> GetTranslationItems()
        {
            return TranslationItems;
        }

        public void LoadTranslationApp()
        {
            //Get the culture in the properties
            string tradId = GetCulture();


            TranslationApp = new Dictionary<string, string>(); // reset the translation
            //load the file for culture
            string dataTranslate = FileManager.GetTextFromAssetFile($"Traduction/App/Traduction{tradId}.json");
            try
            {
                TranslationApp = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataTranslate);
            }
            catch (Exception ex)
            {

            }
        }

        public Dictionary<string, string> GetTranslationApp()
        {
            return TranslationApp;
        }


        public List<Item> GetCrafts()
        {
            List<Item> itemsCraftable = new List<Item>();
            foreach(Craft craft in GameData.Crafts)
            {
                //craft.ItemToCraft = GameData.Items.FirstOrDefault(i => i.Id == craft.ItemIdToCraft);
                itemsCraftable.Add(GameData.Items.FirstOrDefault(i => i.Id == craft.ItemIdToCraft));
            }
            return itemsCraftable;
        }

        public Craft GetItemCraft(int itemId)
        {
            Craft itemsCraft = null;
            foreach (Craft craft in GameData.Crafts)
            {
                if(craft.ItemIdToCraft == itemId)
                {
                    itemsCraft = craft;
                    break;
                }
            }
            if(itemsCraft != null)
            {
                itemsCraft.ItemToCraft = GameData.Items.FirstOrDefault(i => i.Id == itemsCraft.ItemIdToCraft);
                List<RessourceCraft> ressourcesCraft = new List<RessourceCraft>();
                foreach(int itemForCraftId in itemsCraft.ItemsNeeded.Keys)
                {
                    Item itemForCraft = GameData.Items.FirstOrDefault(i => i.Id == itemForCraftId);
                    RessourceCraft rc = new RessourceCraft
                    {
                        Item = itemForCraft,
                        Count = itemsCraft.ItemsNeeded[itemForCraftId]
                    };
                    ressourcesCraft.Add(rc);
                }
                itemsCraft.ItemsForCraft = ressourcesCraft;
            }
            return itemsCraft;
        }

        public List<Item> GetItemsByIds(List<int> itemsIds)
        {
            List<Item> items = GetItems().Where(i => itemsIds.Contains(i.Id)).ToList();
            return items;
        }
    }

}
