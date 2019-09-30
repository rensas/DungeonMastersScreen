using DungeonMastersScreen.Model.Creatures;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DungeonMastersScreen.ViewModel.ChildViewModel
{
    public class InitiativeCreatureViewModel : ViewModelBase
    {
        private Creature _model;
        public Creature Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (value == _model)
                    return;
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        private int _initiativeEntry;
        public int InitiativeEntry
        {
            get
            {
                return _initiativeEntry;
            }
            set
            {
                if (value == _initiativeEntry)
                    return;
                _initiativeEntry = value;
                RaisePropertyChanged("InitiativeEntry");
            }
        }

        private int _hitpointsEntry;
        public int HitpointsEntry
        {
            get
            {
                return _hitpointsEntry;
            }
            set
            {
                if (value == _hitpointsEntry)
                    return;
                _hitpointsEntry = value;
                RaisePropertyChanged("HitpointsEntry");
            }
        }



        #region Methods
        private void DamageCreature()
        {
            Model.CurrentHP -= HitpointsEntry;
            if (Model.CurrentHP < 0)
            {
                Model.CurrentHP = 0;
            }
            HitpointsEntry = 0;
            RaisePropertyChanged("Model");
        }

        private void HealCreature()
        {
            Model.CurrentHP += HitpointsEntry;
            if (Model.CurrentHP > Model.MaxHP)
            {
                Model.CurrentHP = Model.MaxHP;
            }
            HitpointsEntry = 0;
            RaisePropertyChanged("Model");
        }

        #endregion

        #region Commands

        public ICommand DamageCreatureCommand
        {
            get
            {
                return new RelayCommand(DamageCreature);
            }
        }

        public ICommand HealCreatureCommand
        {
            get
            {
                return new RelayCommand(HealCreature);
            }
        }

        #endregion
    }
}
