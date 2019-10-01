using DungeonMastersScreen.Model.Creatures;
using DungeonMastersScreen.Model.Types;
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

        #endregion


        public CombatTrackerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitiableCreatures = GetInitiableCreatures();
            PlayerCharacters = GetPlayerCharacters();
            InitiativeCreatures = new List<InitiativeCreatureViewModel>();

            PageTitle = "Combat tracker page";
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

        private ObservableCollection<InitiativeCreatureViewModel> GetPlayerCharacters()
        {
            var pcs = new ObservableCollection<PlayerCharacter>();
            pcs.Add(new PlayerCharacter()
            {
                Name = "Luthor",
                MaxHP = 28,
                CurrentHP = 28,
                DispositionToPCs = DispositionType.Friendly
            });
            pcs.Add(new PlayerCharacter()
            {
                Name = "Zil",
                MaxHP = 19,
                CurrentHP = 19,
                DispositionToPCs = DispositionType.Friendly
            });
            pcs.Add(new PlayerCharacter()
            {
                Name = "Sly",
                MaxHP = 18,
                CurrentHP = 18,
                DispositionToPCs = DispositionType.Friendly
            });
            pcs.Add(new PlayerCharacter()
            {
                Name = "Elroy",
                MaxHP = 14,
                CurrentHP = 14,
                DispositionToPCs = DispositionType.Friendly
            });
            pcs.Add(new PlayerCharacter()
            {
                Name = "Saphyra",
                MaxHP = 21,
                CurrentHP = 21,
                DispositionToPCs = DispositionType.Friendly
            });
            var pcvm = new ObservableCollection<InitiativeCreatureViewModel>();
            foreach (PlayerCharacter pc in pcs)
            {
                pcvm.Add(new InitiativeCreatureViewModel() { Model = pc });
            }
            return pcvm;
        }
        private void AddNewCreatureToInitiative()
        {
            if (!String.IsNullOrWhiteSpace(NewCreatureName) && NewCreatureInitiative != null && NewCreatureHP != null && !String.IsNullOrWhiteSpace(SelectedDisposition))
            {
                DispositionType dispo = DispositionType.Neutral;
                Enum.TryParse(SelectedDisposition, out dispo);
                var creature = new NPC()
                {
                    Name = NewCreatureName,
                    Initiative = NewCreatureInitiative.Value,
                    MaxHP = NewCreatureHP.Value,
                    CurrentHP = NewCreatureHP.Value,
                    DispositionToPCs = dispo
                };
                InitiativeCreatureViewModel creatureVM = new InitiativeCreatureViewModel() { Model = creature };
                InitiativeCreatures.Add(creatureVM);
                RaisePropertyChanged("InitiativeCreatures");
                RaisePropertyChanged("SortedInitiativeCreatures");
                NewCreatureInitiative = null;
                NewCreatureHP = null;
                NewCreatureName = "";
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

        #endregion

        #region Commands

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
