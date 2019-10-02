using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersScreen.Model.Creatures
{
    public class PlayerCharacter : Creature
    {
        public PlayerCharacter()
        {
            InitiativeColor = Types.InitiativeColorType.LG;
        }
    }
}
