using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ACNHEncyclopedia.Services;
using ACNHEncyclopedia.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System.Globalization;
using DLToolkit.Forms.Controls;
using ACNHEncyclopedia.Views.Dialogs;
using ACNHEncyclopedia.ViewModels;
using System.Linq;

namespace ACNHEncyclopedia
{
    public partial class App : PrismApplication
    {

        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialDesignIconsModule());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<GameDataLoader>();
            containerRegistry.RegisterSingleton<TranslateItemService>();
            containerRegistry.RegisterSingleton<ItemsFilterService>();
            containerRegistry.RegisterSingleton<UserDataService>();

            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<ItemsPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
            containerRegistry.RegisterForNavigation<AboutPage>();

            // Used to display lists of items
            containerRegistry.RegisterForNavigation<GenericListItemPage>();

            //Lists
            containerRegistry.RegisterForNavigation<WishListPage>();

            //Clothing

            //Dialogs
            containerRegistry.RegisterDialog<ItemDetailsDialog, ItemDetailsDialogViewModel>();
            containerRegistry.RegisterDialog<ItemsFilterDialog, ItemsFilterDialogViewModel>();

            //ViewModelLocationProvider.Register<UserBarView, UserBarViewViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //FlowListView.Init();
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
