using ACNHEncyclopedia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public class UserDataService
    {
        private const string WishListKey = "WishList";
        private const string PossesionListKey = "PossesionList";
        public UserDataService()
        {
        }

        public bool AddOrRemoveItemToWhishList(int itemId)
        {
            bool result = false;
            try
            {
                if(!Xamarin.Forms.Application.Current.Properties.ContainsKey(WishListKey))
                {
                    PropertiesDataManager<List<int>>.SetValue(WishListKey, new List<int>());
                }
                List<int> furnitureWishList = PropertiesDataManager<List<int>>.GetValue(WishListKey);
                if (!furnitureWishList.Contains(itemId)) furnitureWishList.Add(itemId);
                else
                {
                    furnitureWishList.Remove(itemId);
                }
                PropertiesDataManager<List<int>>.SetValue(WishListKey, furnitureWishList);
                result = true;
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public bool IsInWishList(int itemId)
        {
            bool result = false;
            try
            {
                if (!Xamarin.Forms.Application.Current.Properties.ContainsKey(WishListKey))
                {
                    return false;
                }
                List<int> furnitureWishList = PropertiesDataManager<List<int>>.GetValue(WishListKey);
                if (furnitureWishList.Contains(itemId)) result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<int> GetWishListItems()
        {
            List<int> itemsIds = new List<int>();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey(WishListKey))
            {
                itemsIds = PropertiesDataManager<List<int>>.GetValue(WishListKey);

            }
            return itemsIds;
        }


        public bool AddOrRemoveItemToPossesionList(int itemId)
        {
            bool result = false;
            try
            {
                if (!Xamarin.Forms.Application.Current.Properties.ContainsKey(PossesionListKey))
                {
                    PropertiesDataManager<List<int>>.SetValue(PossesionListKey, new List<int>());
                }
                List<int> furnitureWishList = PropertiesDataManager<List<int>>.GetValue(PossesionListKey);
                if (!furnitureWishList.Contains(itemId)) furnitureWishList.Add(itemId);
                else
                {
                    furnitureWishList.Remove(itemId);
                }
                PropertiesDataManager<List<int>>.SetValue(PossesionListKey, furnitureWishList);
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool IsInPossesionList(int itemId)
        {
            bool result = false;
            try
            {
                if (!Xamarin.Forms.Application.Current.Properties.ContainsKey(PossesionListKey))
                {
                    return false;
                }
                List<int> furnitureWishList = PropertiesDataManager<List<int>>.GetValue(PossesionListKey);
                if (furnitureWishList.Contains(itemId)) result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<int> GetPossesionListItems()
        {
            List<int> itemsIds = new List<int>();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey(PossesionListKey))
            {
                itemsIds = PropertiesDataManager<List<int>>.GetValue(PossesionListKey);

            }
            return itemsIds;
        }
    }
}
