using DungeonMastersScreen.Model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersScreen.Service.Interface
{
    public interface ILocalStorageService
    {
        Guid StorePlayerCharacter(PlayerCharacter pc);
        List<PlayerCharacter> RetrievePlayerCharacters();
        bool DeletePlayerCharacter(PlayerCharacter pc);
    }
}
