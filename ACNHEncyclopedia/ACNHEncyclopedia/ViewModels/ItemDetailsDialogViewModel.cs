using ACNHEncyclopedia.Entities;
using ACNHEncyclopedia.Services;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACNHEncyclopedia.ViewModels
{
    public class ItemDetailsDialogViewModel : BindableBase, IDialogAware
    {
        public event Action<IDialogParameters> RequestClose;
        private bool _canClose;
        public bool CanClose
        {
            get => _canClose;
            set => SetProperty(ref _canClose, value);
        }

        private bool _closeOnTap;
        public bool CloseOnTap
        {
            get => _closeOnTap;
            set => SetProperty(ref _closeOnTap, value);
        }
        public bool CanCloseDialog() => true;

        private void OnCloseTapped()
        {
            RequestClose(new DialogParameters());
        }


        public void OnDialogClosed()
        {
            Console.WriteLine("The Demo Dialog has been closed...");
        }

        private Craft _craft;
        public Craft Craft
        {
            get => _craft;
            set => SetProperty(ref _craft, value);
        }
        private bool _hasCraft;
        public bool HasCraft
        {
            get => _hasCraft;
            set => SetProperty(ref _hasCraft, value);
        }

        private int _craftListHeight;
        public int CraftListHeight
        {
            get => _craftListHeight;
            set => SetProperty(ref _craftListHeight, value);
        }

        private Item _item;
        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private bool _isInWhishList;
        public bool IsInWishList
        {
            get => _isInWhishList;
            set => SetProperty(ref _isInWhishList, value);
        }

        private DelegateCommand _addToWishListCommand;
        public DelegateCommand AddToWishListCommand
        {
            get => _addToWishListCommand;
            set => SetProperty(ref _addToWishListCommand, value);
        }


        private bool _isInPossesionList;
        public bool IsInPossesionList
        {
            get => _isInPossesionList;
            set => SetProperty(ref _isInPossesionList, value);
        }
        private DelegateCommand _addToPossesionCommand;
        public DelegateCommand AddToPossesionCommand
        {
            get => _addToPossesionCommand;
            set => SetProperty(ref _addToPossesionCommand, value);
        }

        private DelegateCommand<object> _openRessourceDetailsCommand;
        public DelegateCommand<object> OpenRessourceDetailsCommand
        {
            get => _openRessourceDetailsCommand;
            set => SetProperty(ref _openRessourceDetailsCommand, value);
        }


        UserDataService _userDataService;
        IDialogService _dialogService;
        GameDataLoader _gameDataLoader;

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // If not using IAutoInitialize you would need to set the Message property here.
            Item = parameters.GetValue<Item>("Item");
            Craft = parameters.GetValue<Craft>("Craft");
            HasCraft = Craft != null;
            if (HasCraft) CraftListHeight = Craft.ItemsForCraft.Count * 51 - Craft.ItemsForCraft.Count/3;
            string category = parameters.GetValue<string>("Category");

            AddToWishListCommand = new DelegateCommand(AddOrRemoveToWhishList);
            AddToPossesionCommand = new DelegateCommand(AddOrRemoveToPossesionList);
            OpenRessourceDetailsCommand = new DelegateCommand<object>(OpenCraftRessourceDetails);

            IsInWishList = _userDataService.IsInWishList(Item.Id);
            IsInPossesionList = _userDataService.IsInPossesionList(Item.Id);
        }


        public ItemDetailsDialogViewModel(UserDataService userDataService, GameDataLoader gameDataLoader, IDialogService dialogService)
        {

            _userDataService = userDataService;
            _dialogService = dialogService;
            _gameDataLoader = gameDataLoader;
            
        }

        public void AddOrRemoveToWhishList()
        {
            bool result = _userDataService.AddOrRemoveItemToWhishList(Item.Id);
            if (result) IsInWishList = _userDataService.IsInWishList(Item.Id);
        }

        public void AddOrRemoveToPossesionList()
        {
            bool result = _userDataService.AddOrRemoveItemToPossesionList(Item.Id);
            if (result) IsInPossesionList = _userDataService.IsInPossesionList(Item.Id);
        }

        public async void OpenCraftRessourceDetails(object ressourceCraft)
        {
            RessourceCraft rc = (RessourceCraft)ressourceCraft;
            Craft itemCraft = _gameDataLoader.GetItemCraft(rc.Item.Id);
            var parameters = new DialogParameters()
            {
                { "Item", rc.Item },
                { "Craft", itemCraft }
            };
            _dialogService.ShowDialog("ItemDetailsDialog", parameters);
        }
    }
}
