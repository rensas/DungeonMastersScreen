using DungeonMastersScreen.Model.Creatures;
using DungeonMastersScreen.Model.Types;
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

            //LoadPlayerData();
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

        private int _newCharCurrentHP;
        public int NewCharCurrentHP
        {
            get
            {
                return _newCharCurrentHP;
            }
            set
            {
                if (_newCharCurrentHP == value)
                    return;
                _newCharCurrentHP = value;
                RaisePropertyChanged("NewCharCurrentHP");
            }
        }

        private PlayerCharacter _selectedPlayerCharacter;
        public PlayerCharacter SelectedPlayerCharacter
        {
            get
            {
                return _selectedPlayerCharacter;
            }
            set
            {
                if (_selectedPlayerCharacter == value)
                    return;
                _selectedPlayerCharacter = value;
                RaisePropertyChanged("SelectedPlayerCharacter");
                RaisePropertyChanged("EditPCVisible");
            }
        }

        public bool EditPCVisible
        {
            get
            {
                return SelectedPlayerCharacter != null;
            }
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            LoadPlayerData();
        }

        private void DoLongRest()
        {
            foreach (PlayerCharacter pc in PlayerCharacters)
            {
                pc.CurrentHP = pc.MaxHP;
                _localStorageService.StorePlayerCharacter(pc);
            }
            LoadPlayerData();
        }
        private void LoadPlayerData()
        {
            PlayerCharacters = _localStorageService.RetrievePlayerCharacters();
            RaisePropertyChanged("PlayerCharacters");
        }

        private void SaveNewPlayerCharacter()
        {
            if (!String.IsNullOrWhiteSpace(NewCharName) && NewCharMaxHP > 0 && NewCharCurrentHP > 0)
            {
                PlayerCharacter pc = new PlayerCharacter()
                {
                    Name = NewCharName,
                    MaxHP = NewCharMaxHP,
                    CurrentHP = NewCharCurrentHP,
                    DispositionToPCs = DispositionType.Friendly
                };
                _localStorageService.StorePlayerCharacter(pc);
                NewCharMaxHP = 0;
                NewCharCurrentHP = 0;
                NewCharName = "";
                LoadPlayerData();
            }
        }

        private void SaveExistingPlayerCharacter()
        {
            _localStorageService.StorePlayerCharacter(SelectedPlayerCharacter);
            SelectedPlayerCharacter = null;
            LoadPlayerData();
        }

        private void DeletePlayerCharacter(object state)
        {
            PlayerCharacter pc = state as PlayerCharacter;
            _localStorageService.DeletePlayerCharacter(pc);
            LoadPlayerData();
        }

        #endregion

        #region Commands

        public ICommand LoadDataCommand
        {
            get
            {
                return new RelayCommand(LoadData);
            }
        }

        public ICommand LongRestCommand
        {
            get
            {
                return new RelayCommand(DoLongRest);
            }
        }

        public ICommand SaveNewPlayerCharCommand
        {
            get
            {
                return new RelayCommand(SaveNewPlayerCharacter);
            }
        }

        public ICommand SaveExistingPlayerCharCommand
        {
            get
            {
                return new RelayCommand(SaveExistingPlayerCharacter);
            }
        }

        public ICommand DeletePlayerCharacterCommand
        {
            get
            {
                return new RelayCommand<object>(DeletePlayerCharacter);
            }
        }

        #endregion

    }
}
