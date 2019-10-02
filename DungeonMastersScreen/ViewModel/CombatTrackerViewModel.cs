using DungeonMastersScreen.Model.Creatures;
using DungeonMastersScreen.Model.Types;
using DungeonMastersScreen.Service.Interface;
using DungeonMastersScreen.ViewModel.ChildViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DungeonMastersScreen.ViewModel
{
    public class CombatTrackerViewModel : ViewModelBase
    {
        #region Services
        private INavigationService _navigationService;
        private ILocalStorageService _localStorageService;

        #endregion


        public CombatTrackerViewModel(INavigationService navigationService, ILocalStorageService storageService)
        {
            _navigationService = navigationService;
            _localStorageService = storageService;
            InitiativeCreatures = new List<InitiativeCreatureViewModel>();

            MonsterColorQueue = new Queue<InitiativeColorType>();
            MonsterColorQueue.Enqueue(InitiativeColorType.R);
            MonsterColorQueue.Enqueue(InitiativeColorType.G);
            MonsterColorQueue.Enqueue(InitiativeColorType.Y);
            MonsterColorQueue.Enqueue(InitiativeColorType.B);

            PageTitle = "Combat tracker page";
            NumberOfNewCreature = 1;
        }

        #region Properties
        private string _pageTitle;
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                if (_pageTitle == value)
                    return;
                _pageTitle = value;
                RaisePropertyChanged("ExampleValue");
            }
        }

        private Queue<InitiativeColorType> _colorQueue;
        public Queue<InitiativeColorType> MonsterColorQueue
        {
            get
            {
                return _colorQueue;
            } set
            {
                if (_colorQueue == value)
                    return;
                _colorQueue = value;
                RaisePropertyChanged("ColorQueue");
            }
        }

        public InitiativeColorType NextMonsterColor
        {
            get
            {
                return GetNextMonsterColor();
            }
        }

        private ObservableCollection<Creature> _initiableCreatures;
        public ObservableCollection<Creature> InitiableCreatures
        {
            get
            {
                return _initiableCreatures;
            }
            set
            {
                if (_initiableCreatures == value)
                    return;
                _initiableCreatures = value;
                RaisePropertyChanged("InitiableCreatures");
            }
        }

        private ObservableCollection<InitiativeCreatureViewModel> _playerCharacters;
        public ObservableCollection<InitiativeCreatureViewModel> PlayerCharacters
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

        private List<InitiativeCreatureViewModel> _initiativeCreatures;
        public List<InitiativeCreatureViewModel> InitiativeCreatures
        {
            get
            {
                return _initiativeCreatures;
            }
            set
            {
                if (_initiativeCreatures == value)
                    return;
                _initiativeCreatures = value;
                RaisePropertyChanged("InitiativeCreatures");
            }
        }

        private string _newCreatureName;
        public string NewCreatureName
        {
            get
            {
                return _newCreatureName;
            } 
            set
            {
                if (_newCreatureName == value)
                    return;
                _newCreatureName = value;
                RaisePropertyChanged("NewCreatureName");
            }
        }

        private int? _newCreatureInitiative;
        public int? NewCreatureInitiative
        {
            get
            {
                return _newCreatureInitiative;
            }
            set
            {
                if (_newCreatureInitiative == value)
                    return;
                _newCreatureInitiative = value;
                RaisePropertyChanged("NewCreatureInitiative");
            }
        }

        private int? _newCreatureHP;
        public int? NewCreatureHP
        {
            get
            {
                return _newCreatureHP;
            }
            set
            {
                if (_newCreatureHP == value)
                    return;
                _newCreatureHP = value;
                RaisePropertyChanged("NewCreatureHP");
            }
        }

        private int _numberOfNewCreature;
        public int NumberOfNewCreature
        {
            get
            {
                return _numberOfNewCreature;
            }
            set
            {
                if (_numberOfNewCreature == value)
                    return;
                _numberOfNewCreature = value;
                RaisePropertyChanged("NumberOfNewCreature");
            }
        }

        public List<string> Dispositions
        {
            get
            {
                return new List<string>() { "Friendly", "Hostile", "Neutral" };
            }
        }

        private string _selectedDisposition;
        public string SelectedDisposition
        {
            get
            {
                return _selectedDisposition;
            } 
            set
            {
                if (_selectedDisposition == value)
                    return;
                _selectedDisposition = value;
                RaisePropertyChanged("SelectedDisposition");
            }
        }

        public ObservableCollection<InitiativeCreatureViewModel> SortedInitiativeCreatures
        {
            get
            {
                var results = InitiativeCreatures.OrderByDescending(x => x.Model.Initiative).ToList();
                return new ObservableCollection<InitiativeCreatureViewModel>(results);
            }
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            PlayerCharacters = GetPlayerCharacters();
        }
        private ObservableCollection<Creature> GetInitiableCreatures()
        {
            ObservableCollection<Creature> creatures = new ObservableCollection<Creature>();
            creatures.Add(new NPC()
            {
                Name = "Wight",
                MaxHP = 24,
                DispositionToPCs = DispositionType.Hostile
            });

            creatures.Add(new NPC()
            {
                Name = "Zombie",
                MaxHP = 15,
                DispositionToPCs = DispositionType.Hostile
            });

            creatures.Add(new NPC()
            {
                Name = "Shambling Mound",
                MaxHP = 38,
                DispositionToPCs = DispositionType.Hostile
            });

            creatures.Add(new NPC()
            {
                Name = "Bandit",
                MaxHP = 11,
                DispositionToPCs = DispositionType.Neutral
            });

            return creatures;
        }

        private InitiativeColorType GetNextMonsterColor()
        {
            var value = MonsterColorQueue.Dequeue();
            MonsterColorQueue.Enqueue(value);
            return value;
        }

        private ObservableCollection<InitiativeCreatureViewModel> GetPlayerCharacters()
        {
            var pcs = _localStorageService.RetrievePlayerCharacters();
            var pcvm = new List<InitiativeCreatureViewModel>();
            foreach (PlayerCharacter pc in pcs)
            {
                pcvm.Add(new InitiativeCreatureViewModel() { Model = pc });
            }
            return new ObservableCollection<InitiativeCreatureViewModel>(pcvm.OrderBy(x=>x.Model.Name).ToList());
        }

        private void AddNewCreatureToInitiative()
        {
            if (!String.IsNullOrWhiteSpace(NewCreatureName) && NewCreatureInitiative != null && NewCreatureHP != null && !String.IsNullOrWhiteSpace(SelectedDisposition))
            {
                for(int i = 1; i <= NumberOfNewCreature; i++)
                {
                    DispositionType dispo = DispositionType.Neutral;
                    Enum.TryParse(SelectedDisposition, out dispo);
                    var creature = new NPC()
                    {
                        Name = NumberOfNewCreature > 1 ? NewCreatureName + " " + i : NewCreatureName,
                        Initiative = NewCreatureInitiative.Value,
                        MaxHP = NewCreatureHP.Value,
                        CurrentHP = NewCreatureHP.Value,
                        DispositionToPCs = dispo,
                        InitiativeColor = GetNextMonsterColor()
                    };
                    InitiativeCreatureViewModel creatureVM = new InitiativeCreatureViewModel() { Model = creature };
                    InitiativeCreatures.Add(creatureVM);
                }
                
                RaisePropertyChanged("InitiativeCreatures");
                RaisePropertyChanged("SortedInitiativeCreatures");
                NewCreatureInitiative = null;
                NewCreatureHP = null;
                NewCreatureName = "";
                NumberOfNewCreature = 1;
            }
        }

        private void AddPlayerCharacterToInitiative(object state)
        { 
            InitiativeCreatureViewModel playerVM = state as InitiativeCreatureViewModel;
            if (!InitiativeCreatures.Contains(playerVM))
            {
                Random rnd = new Random();
                playerVM.Model.Initiative = playerVM.InitiativeEntry;
                InitiativeCreatures.Add(playerVM);
                RaisePropertyChanged("SortedInitiativeCreatures");
                RaisePropertyChanged("InitiativeCreatures");

                playerVM.InitiativeEntry = 0;
            }
        }

        private void RemoveCreatureFromInitiative(object state)
        {
            InitiativeCreatureViewModel creature = state as InitiativeCreatureViewModel;
            InitiativeCreatures.Remove(creature);
            RaisePropertyChanged("SortedInitiativeCreatures");
            RaisePropertyChanged("InitiativeCreatures");
        }

        private void SavePCStatusAndClearInitiative()
        {
            foreach (InitiativeCreatureViewModel vm in InitiativeCreatures)
            {
                var modelType = vm.Model.GetType();
                if (modelType == typeof(PlayerCharacter))
                {
                    _localStorageService.StorePlayerCharacter(vm.Model as PlayerCharacter);
                }
            }
            InitiativeCreatures.Clear();
            RaisePropertyChanged("InitiativeCreatures");
            RaisePropertyChanged("SortedInitiativeCreatures");
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

        public ICommand AddPlayerCharacterCommand
        {
            get
            {
                return new RelayCommand<object>(AddPlayerCharacterToInitiative);
            }
        }

        public ICommand AddNewCreatureCommand
        {
            get
            {
                return new RelayCommand(AddNewCreatureToInitiative);
            }
        }

        public ICommand SavePCStatusAndClearInitiativeCommand
        {
            get
            {
                return new RelayCommand(SavePCStatusAndClearInitiative);
            }
        }

        public ICommand RemoveCreatureFromInitiativeCommand
        {
            get
            {
                return new RelayCommand<object>(RemoveCreatureFromInitiative);
            }
        }
        #endregion
    }
}
