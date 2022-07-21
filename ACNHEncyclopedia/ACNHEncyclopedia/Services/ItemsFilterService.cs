using ACNHEncyclopedia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public class ItemsFilterService
    {
        GameDataLoader _gameDataLoader;
        public ItemsFilterService(GameDataLoader gameDataLoader)
        {
            _gameDataLoader = gameDataLoader;
        }

        public List<Item> OrderByName(List<Item> itemsToSort)
        {
            var trads = _gameDataLoader.GetTranslationItems();
            List<int> itemsIdTextFiltered = new List<int>();
            foreach(Item item in itemsToSort)
            {
                if (trads.ContainsKey(item.TranslateId)) item.TranslatedName = trads[item.TranslateId];
            }
            itemsToSort = itemsToSort.OrderBy(i => i.TranslatedName).ToList();
            return itemsToSort;
        }

        public List<Item> OrderByPrice(List<Item> itemsToSort)
        {
            return itemsToSort.OrderBy(i => i.Price).ToList();
        }

        public List<Item> OrderBySellPrice(List<Item> itemsToSort)
        {
            return itemsToSort.OrderBy(i => i.SellPrice).ToList();
        }
    }
}
