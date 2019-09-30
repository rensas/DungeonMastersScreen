using DungeonMastersScreen.Model.Types;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersScreen.Model.Creatures
{
    public abstract class Creature : ObservableObject
    {
        public string Name { get; set; }
        public int Initiative { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Dexterity { get; set; }
        public int Charisma { get; set; }
        public int Constitution { get; set; }
        public int Strength { get; set; }
        public DispositionType DispositionToPCs { get; set; }
        public CreatureType CreatureType { get; set; }

        public void Heal(int healAmount)
        {
            CurrentHP += healAmount;
            if(CurrentHP > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }

        public void Damage(int damageAmount)
        {
            CurrentHP -= damageAmount;
            if (CurrentHP < 0)
            {
                CurrentHP = 0;
            }
        }
    }
}
