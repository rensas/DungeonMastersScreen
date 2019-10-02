using DungeonMastersScreen.Model.Creatures;
using DungeonMastersScreen.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersScreen.Service.Implementation
{
    /**
     * An incredibly dumb class to store and modify player characters in a json file on disk.
     * It's bad, it's inefficient, and it's nasty code, but it works.
     * 
     */
    public class LocalFileStorageService : ILocalStorageService
    {
        private string _filePath;

        private const string pcFileName = "PlayerCharacters.json";

        public LocalFileStorageService()
        {
            var path = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
            _filePath = Path.Combine(path, "DungeonMastersScreen");

            if (!Directory.Exists(_filePath)) {
                Directory.CreateDirectory(_filePath);
            }
        }
        public List<PlayerCharacter> RetrievePlayerCharacters()
        {
            List<PlayerCharacter> pcs = new List<PlayerCharacter>();
            if (File.Exists(Path.Combine(_filePath, pcFileName)))
            {
                string pcJson = System.IO.File.ReadAllText(Path.Combine(_filePath, pcFileName));
                if (!String.IsNullOrWhiteSpace(pcJson))
                {
                    pcs = JsonConvert.DeserializeObject<List<PlayerCharacter>>(pcJson);
                }
            }
            return pcs;
        }

        public Guid StorePlayerCharacter(PlayerCharacter pc)
        {
            string jsonToWrite = "";
            Guid pcId = new Guid(); // Intentional for all 0s
            var existingPcs = RetrievePlayerCharacters();
            if (existingPcs.Count == 0)
            {
                if (pc.Id == null)
                {
                    pc.Id = Guid.NewGuid();
                }
                pcId = pc.Id.Value;
                existingPcs.Add(pc);
            } else
            {
                if (pc.Id != null)
                {
                    var charToReplace = existingPcs.Where(x => x.Id == pc.Id).Single();
                    if (charToReplace != null)
                    {
                        existingPcs.Remove(charToReplace);
                    }
                } else
                {
                    pc.Id = Guid.NewGuid();
                }
                existingPcs.Add(pc);
            }
            jsonToWrite = JsonConvert.SerializeObject(existingPcs);
            var path = Path.Combine(_filePath, pcFileName);
            File.WriteAllText(path, jsonToWrite);
            return pcId;
        }

        public bool DeletePlayerCharacter(PlayerCharacter pc)
        {
            var existingPcs = RetrievePlayerCharacters();
            var removed = existingPcs.Remove(existingPcs.Where(x => x.Id == pc.Id).First());
            var jsonToWrite = JsonConvert.SerializeObject(existingPcs);
            var path = Path.Combine(_filePath, pcFileName);
            File.WriteAllText(path, jsonToWrite);
            return removed;
        }
    }
}
