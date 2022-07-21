using ACNHEncyclopedia.Entities;
using ACNHEncyclopedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.ViewModels
{
    public class GenericListItemPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<Item> _itemsReference;
        public List<Item> ItemsReference
        {
            get { return _itemsReference; }
            set { SetProperty(ref _itemsReference, value); }
        }

        private List<Item> _items;
        public List<Item> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private List<Item> _itemsDisplayed;
        public List<Item> ItemsDisplayed
        {
            get { return _itemsDisplayed; }
            set { SetProperty(ref _itemsDisplayed, value); }
        }


        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                SearchItemByName(value);
            }
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
        public DelegateCommand<object> ItemTappedCommand { get; private set; }
        public DelegateCommand OpenFilterDialogCommand { get; private set; }

        GameDataLoader _gameDataLoader;
        IDialogService _dialogService;
        UserDataService _userDataService;
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            // Get the items
            ItemsReference = parameters.GetValue<List<Item>>("Items");
            Items = GetItems();
            InitPagination();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public GenericListItemPageViewModel(GameDataLoader gameDataLoader, IDialogService dialogService, UserDataService userDataService)
        {
            _gameDataLoader = gameDataLoader;
            _dialogService = dialogService;
            _userDataService = userDataService;

            PreviousPageCommad = new DelegateCommand(PreviousPage);
            NextPageCommand = new DelegateCommand(NextPage);
            ItemTappedCommand = new DelegateCommand<object>(DisplayItemDetails);
            OpenFilterDialogCommand = new DelegateCommand(OpenFilterDialog);
            Title = "";
        }

        public List<Item> GetItems()
        {
            return ItemsReference;//_gameDataLoader.GetItemsByCategory("Mobilier");
        }

        public void InitPagination()
        {
            ItemsPerPage = 12;
            ActualPage = 1;
            MaxPage = (int)Math.Ceiling((float)((float)Items.Count / (float)ItemsPerPage));
            ItemsDisplayed = Items.Take(ItemsPerPage).ToList();
        }

        public void PreviousPage()
        {
            if (ActualPage > 1)
            {
                ActualPage--;
                int skip = (ActualPage - 1) * ItemsPerPage;
                ItemsDisplayed = Items.Skip(skip).Take(ItemsPerPage).ToList();
            }
        }

        public void NextPage()
        {
            if (ActualPage < MaxPage)
            {
                ActualPage++;
                int skip = (ActualPage - 1) * ItemsPerPage;
                ItemsDisplayed = Items.Skip(skip).Take(ItemsPerPage).ToList();
            }
        }

        public void DisplayItemDetails(object item)
        {
            Item itemCast = (Item)item;
            Craft itemCraft = _gameDataLoader.GetItemCraft(itemCast.Id);
            var parameters = new DialogParameters()
            {
                { "Item", itemCast },
                { "Craft", itemCraft }
            };
            _dialogService.ShowDialog("ItemDetailsDialog", parameters);
        }

        public async void SearchItemByName(string search)
        {
            if (search.Length == 0)
            {
                Items = GetItems(); // reset
                InitPagination();
            }
            else
            {
                var trads = _gameDataLoader.GetTranslationItems();
                List<int> itemsIdTextFiltered = new List<int>();
                foreach (string key in trads.Keys)
                {
                    string text = trads[key];
                    if (text.Contains(search))
                    {
                        var tradId = key.Split('_');
                        if (tradId.Count() > 1)
                        {
                            itemsIdTextFiltered.Add(int.Parse(tradId[1]));

                        }
                    }
                }
                try
                {
                    Items = new List<Item>();
                    if (itemsIdTextFiltered.Any()) Items = GetItems().Where(i => itemsIdTextFiltered.Contains(i.Id)).ToList();
                    InitPagination();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void OpenFilterDialog()
        {
            var parameters = new DialogParameters()
            {
                { "Items", Items }
            };
            _dialogService.ShowDialog("ItemsFilterDialog", parameters, ApplyFilter);
        }

        public void ApplyFilter(IDialogResult result)
        {
            bool hasItems = result.Parameters.ContainsKey("Items");
            if (!hasItems) return;
            Items = result.Parameters.GetValue<List<Item>>("Items");
            InitPagination();
        }
    }
}
