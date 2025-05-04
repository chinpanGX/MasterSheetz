using System.Collections.Generic;
using System.Linq;

namespace App.MasterData
{
    public static class SampleCharacterExtension
    {
        public static SampleCharacterType GetType(this SampleCharacter character)
        {
            return (SampleCharacterType)character.Type;
        }
    }
    
    public partial class SampleCharacterMasterDataTable
    {
        public List<SampleCharacter> GetByType(SampleCharacterType type)
        {
            var data = GetAll().ToList();
            return data.Where(x => x.Type == (int)type).ToList();
        }
    }
}
