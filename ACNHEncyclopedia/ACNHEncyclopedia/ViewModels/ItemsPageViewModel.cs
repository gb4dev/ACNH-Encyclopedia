using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ACNHEncyclopedia.Models;
using ACNHEncyclopedia.Views;
using ACNHEncyclopedia.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;
using ACNHEncyclopedia.Entities;
using System.Linq;
using Prism.Commands;
using System.Globalization;

namespace ACNHEncyclopedia.ViewModels
{
    public class ItemsPageViewModel :  BindableBase, INavigatedAware
    {
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private List<Item> Items;
        

        private List<Item> _itemsDisplayed;
        public List<Item> ItemsDisplayed
        {
            get { return _itemsDisplayed; }
            set { SetProperty(ref _itemsDisplayed, value); }
        }

        private int _actualPage;
        public int ActualPage
        {
            get { return _actualPage; }
            set { SetProperty(ref _actualPage, value); }
        }
        //public int PreviousPage { get { return ActualPage - 1; } }
        //public int NextPage { get { return ActualPage + 1; } }
        private int _maxPage;
        public int MaxPage
        {
            get { return _maxPage; }
            set { SetProperty(ref _maxPage, value); }
        }
        public int ItemsPerPage { get; set; }
        public DelegateCommand PreviousPageCommad { get; private set; }
        public DelegateCommand NextPageCommand { get; private set; }

        private Kind _kindSelected;
        public Kind KindSelelected
        {
            get { return _kindSelected; }
            set { 
                SetProperty(ref _kindSelected, value);

                FilterItemsByKind(_kindSelected);
            }
        }

        private List<Kind> _kinds;
        public List<Kind> Kinds
        {
            get { return _kinds; }
            set { SetProperty(ref _kinds, value);}
        }

        GameDataLoader _gameDataLoader;
        public ItemsPageViewModel(GameDataLoader gameDataLoader)
        {
            _gameDataLoader = gameDataLoader;

            Title = "Game Items";
            Items = _gameDataLoader.GetItems();
            InitPagination();
            PreviousPageCommad = new DelegateCommand(PreviousPage);
            NextPageCommand = new DelegateCommand(NextPage);

            Kinds = _gameDataLoader.GetKinds().ToList();
            //KindSelelected = Kinds.First();
            //FilterItemsByKind(KindSelelected);

        }

        public void InitPagination()
        {
            ItemsPerPage = 10;
            ActualPage = 0;
            MaxPage = (int)Math.Ceiling((float)((float)Items.Count / (float)ItemsPerPage));
            NextPage();
        }

        public void PreviousPage()
        {
            if(ActualPage > 1 && Items.Any())
            {
                ActualPage--;
                int skip = (ActualPage - 1) * ItemsPerPage;
                ItemsDisplayed = Items.Skip(skip).Take(ItemsPerPage).ToList();
            }
        }

        public void NextPage()
        {
            if (ActualPage < MaxPage && Items.Any())
            {
                ActualPage++;
                int skip = (ActualPage - 1) * ItemsPerPage;
                ItemsDisplayed = Items.Skip(skip).Take(ItemsPerPage).ToList();
            }
        }

        public void FilterItemsByKind(Kind kind)
        {
            try
            {

                Items = _gameDataLoader.GetItems().Where(i =>i.Kind != null && i.Kind.Id == kind.Id).ToList();
                InitPagination();
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Errueur", "Echec du chargement des items", "Ok");
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}