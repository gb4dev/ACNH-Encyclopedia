using ACNHEncyclopedia.Entities;
using ACNHEncyclopedia.Services;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.ViewModels
{
    public class WishListPageViewModel : BindableBase
    {
        private List<Item> Items;
        private List<int> _itemsIds;
        public bool HaveItems { get; set; } = false;

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


        UserDataService _userDataService;
        IDialogService _dialogService;
        GameDataLoader _gameDataLoader;

        public event EventHandler IsActiveChanged;

        public WishListPageViewModel(IDialogService dialogService, UserDataService userDataService, GameDataLoader gameDataLoader)
        {
            _dialogService = dialogService;
            _userDataService = userDataService;
            _gameDataLoader = gameDataLoader;

            PreviousPageCommad = new DelegateCommand(PreviousPage);
            NextPageCommand = new DelegateCommand(NextPage);
            ItemTappedCommand = new DelegateCommand<object>(DisplayItemDetails);
            OpenFilterDialogCommand = new DelegateCommand(OpenFilterDialog);

            RefreshDataAfterTabSwitch();
        }

        public void RefreshDataAfterTabSwitch()
        {
            _itemsIds = _userDataService.GetWishListItems();
            Items = GetItems();
            HaveItems = Items.Any();
            InitPagination();
        }

        public List<Item> GetItems()
        {
            return _gameDataLoader.GetItemsByIds(_itemsIds); // get for wishlist
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
            Craft craft = _gameDataLoader.GetItemCraft(itemCast.Id);
            var parameters = new DialogParameters()
            {
                { "Item", itemCast},
                {"Craft", craft }
            };
            _dialogService.ShowDialog("ItemDetailsDialog", parameters, RefreshWishList);
        }

        public void RefreshWishList(IDialogResult result)
        {
            RefreshDataAfterTabSwitch();
        }

        public async void SearchItemByName(string search)
        {
            /*if (search.Length == 0)
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
                    if (itemsIdTextFiltered.Any()) Items = _gameDataLoader.GetItems().Where(i => itemsIdTextFiltered.Contains(i.Id)).ToList();
                    InitPagination();
                }
                catch (Exception ex)
                {

                }
            }*/
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
