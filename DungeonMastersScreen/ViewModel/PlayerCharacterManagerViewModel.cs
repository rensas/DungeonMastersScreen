using DungeonMastersScreen.Model.Creatures;
using DungeonMastersScreen.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DungeonMastersScreen.ViewModel
{
    public class PlayerCharacterManagerViewModel : ViewModelBase
    {
        #region Services
        private INavigationService _navigationService;
        private ILocalStorageService _localStorageService;

        #endregion

        public PlayerCharacterManagerViewModel(INavigationService navService, ILocalStorageService localStorageService)
        {
            _navigationService = navService;
            _localStorageService = localStorageService;

            LoadPlayerData();
        }

        #region Properties

        private List<PlayerCharacter> _playerCharacters;
        public List<PlayerCharacter> PlayerCharacters
        {
            get
            {
                return _playerCharacters;
            }
            set
            {
                if (_playerCharacters == value)
                    return;
                _playerCharacters = value;
                RaisePropertyChanged("PlayerCharacters");
            }
        }

        private string _newCharName;
        public string NewCharName
        {
            get
            {
                return _newCharName;
            }
            set
            {
                if (_newCharName == value)
                    return;
                _newCharName = value;
                RaisePropertyChanged("NewCharName");
            }
        }

        private int _newCharMaxHP;
        public int NewCharMaxHP
        {
            get
            {
                return _newCharMaxHP;
            }
            set
            {
                if (_newCharMaxHP == value)
                    return;
                _newCharMaxHP = value;
                RaisePropertyChanged("NewCharMaxHP");
            }
        }

        /*
        private T _name;
        public T Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        */



        #endregion

        #region Methods

        private void LoadPlayerData()
        {
            PlayerCharacters = _localStorageService.RetrievePlayerCharacters();
        }

        private void SaveNewPlayerCharacter()
        {
            if (!String.IsNullOrWhiteSpace(NewCharName) && NewCharMaxHP > 0)
            {
                PlayerCharacter pc = new PlayerCharacter()
                {
                    Name = NewCharName,
                    MaxHP = NewCharMaxHP
                };
                _localStorageService.StorePlayerCharacter(pc);
                NewCharMaxHP = 0;
                NewCharName = "";
                LoadPlayerData();
            }
        }

        #endregion

        #region Commands

        public ICommand SaveNewPlayerCharCommand
        {
            get
            {
                return new RelayCommand(SaveNewPlayerCharacter);
            }
        }

        #endregion

    }
}
