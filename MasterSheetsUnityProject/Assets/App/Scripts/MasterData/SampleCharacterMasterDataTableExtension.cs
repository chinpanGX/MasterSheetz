using System.Collections.Generic;
using System.Linq;
using MasterData.Runtime.Domain;

namespace App.MasterData
{
    public static class SampleCharacterExtension
    {
        public static SampleCharacterType GetType(this SampleCharacter character)
        {
            return (SampleCharacterType)character.Type;
        }
    }
    
    public partial class SampleCharacterMasterDataTable : MasterDataTable<SampleCharacter>
    {
        public List<SampleCharacter> GetByType(SampleCharacterType type)
        {
            var data = GetAll();
            return data.Where(x => x.Type == (int)type).ToList();
        }
    }
}
