using ACNHEncyclopedia.Entities;
using ACNHEncyclopedia.Models;
using ACNHEncyclopedia.Services;
using ACNHEncyclopedia.Views;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ACNHEncyclopedia.ViewModels
{
    public class MainPageViewModel : BindableBase
    {

        private INavigationService _navigationService;
        public DelegateCommand<object> OnNavigateCommand { get; set; }

        private List<ItemMenu> _menus;
        public List<ItemMenu> Menus
        {
            get { return _menus; }
            set { SetProperty(ref _menus, value); }
        }

        GameDataLoader _gameDataLoader;
        TranslateAppService _translateAppService;

        public MainPageViewModel(INavigationService navigationService, GameDataLoader gameDataLoader, TranslateAppService translateAppService)
        {
            _navigationService = navigationService;
            _gameDataLoader = gameDataLoader;
            _translateAppService = translateAppService;


            OnNavigateCommand = new DelegateCommand<object>(NavigateAsync);
            Menus = new List<ItemMenu>()
            {
                /*new ItemMenu()
                {
                    Title = "All Game Items",
                    ImageName = "",
                    NavigationUri = "NavigationPage/ItemsPage",
                },*/
                new ItemMenu()
                {
                    Title = "Pages_Title_FurniturePage",
                    ImageName = "leaf.png",
                    NavigationUri = "NavigationPage/GenericListItemPage",
                    Parameters = new NavigationParameters()
                    {
                        {"Items",  _gameDataLoader.GetItemsByCategory("Mobilier") },
                        {"Title", "Pages_Title_FurniturePage" }
                    }
                },
                new ItemMenu()
                {
                    Title = "Pages_Title_CraftsPage",
                    ImageName = "diy.png",
                    NavigationUri = "NavigationPage/GenericListItemPage",
                    Parameters = new NavigationParameters()
                    {
                        {"Items",  _gameDataLoader.GetCrafts() },
                        {"Title", "Pages_Title_CraftsPage" }
                    }
                },
                new ItemMenu()
                {
                    Title = "Pages_Title_ClothesPage",
                    ImageName = "vetements.png",
                    NavigationUri = "NavigationPage/GenericListItemPage",
                    Parameters = new NavigationParameters()
                    {
                        {"Items",  _gameDataLoader.GetItemsByCategory("Vetements") },
                        {"Title", "Pages_Title_ClothesPage" }
                    }
                },
                new ItemMenu()
                {
                    Title = "Pages_Title_WishListPage",
                    ImageName = "heart.png",
                    NavigationUri = "NavigationPage/WishListPage"
                },
                new ItemMenu()
                {
                    Title = "Pages_Title_SettingsPage",
                    ImageName = "settings.png",
                    NavigationUri = "NavigationPage/SettingsPage"
                },
                new ItemMenu()
                {
                    Title = "Pages_Title_AboutPage",
                    ImageName = "info.png",
                    NavigationUri = "NavigationPage/AboutPage"
                }
            };
        }

        async void NavigateAsync(object sender)
        {
            ItemMenu item = (ItemMenu)sender;
            if(item.Parameters != null) await _navigationService.NavigateAsync(item.NavigationUri, item.Parameters);
            else await _navigationService.NavigateAsync(item.NavigationUri);
        }
    }
}
