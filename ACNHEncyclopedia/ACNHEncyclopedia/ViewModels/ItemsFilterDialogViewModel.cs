using ACNHEncyclopedia.Entities;
using ACNHEncyclopedia.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.ViewModels
{
    public class ItemsFilterDialogViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        private List<Item> _items;
        public List<Item> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Items = parameters.GetValue<List<Item>>("Items");
            OrderSort = false;
        }

        public List<KeyValuePair<string, string>> _availableSorts;
        public List<KeyValuePair<string, string>> AvailableSorts
        {
            get => _availableSorts;
            set => SetProperty(ref _availableSorts, value);
        }

        private KeyValuePair<string, string> _selectedSort;
        public KeyValuePair<string, string> SelectedSort
        {
            get => _selectedSort;
            set => SetProperty(ref _selectedSort, value);
        }

        private const bool _decroissant = true;
        private bool _orderSort;
        public bool OrderSort
        {
            get => _orderSort;
            set => SetProperty(ref _orderSort, value);
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }

        private ItemsFilterService _filterService;
        TranslateAppService _translateAppService;

        public ItemsFilterDialogViewModel(ItemsFilterService filterService, TranslateAppService translateAppService)
        {
            _filterService = filterService;
            _translateAppService = translateAppService;
            CancelCommand = new DelegateCommand(Cancel);
            FilterCommand = new DelegateCommand(Filter);

            _availableSorts = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Name", _translateAppService.GetAppNameTranslated("Filter_ItemName")), // id , trad
            new KeyValuePair<string, string>("Price", _translateAppService.GetAppNameTranslated("Filter_ItemPrice")),
            new KeyValuePair<string, string>("Sell price", _translateAppService.GetAppNameTranslated("Filter_ItemSellPrice"))
        };
        }

        public void Cancel()
        {
            RequestClose(null);
        }
        public void Filter()
        {
            if(_selectedSort.Key != null)
            {
                switch (_selectedSort.Key)
                {
                    case("Name"):
                        Items = _filterService.OrderByName(Items);
                        break;
                    case ("Price"):
                        Items = _filterService.OrderByPrice(Items);
                        break;
                    case ("Sell price"):
                        Items = _filterService.OrderBySellPrice(Items);
                        break;
                }
                if (_orderSort == _decroissant) Items.Reverse();
            }

            IDialogParameters parameters = new DialogParameters()
            {
                { "Items", Items }
            };
            RequestClose(parameters);
        }
    }
}
