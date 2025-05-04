using System.Threading.Tasks;
using App.MasterData;
using MasterData.Runtime.Core;
using NUnit.Framework;
using UnityEngine;

namespace MasterData.Tests
{
    public class TestMasterData
    {
        [Test]
        public async Task TestMasterDataLoader()
        {
            var repository = new MasterDataRepository(new MasterDataLoader());
            await repository.LoadAsync();
            
            var sampleCharacterMasterDataTable = repository.GetTable<SampleCharacterMasterDataTable>();
            Assert.AreEqual(3, sampleCharacterMasterDataTable.GetAll().Count);
            foreach (var character in sampleCharacterMasterDataTable.GetAll())
            {
                Debug.Log(character.Id);
                Debug.Log(character.Name);
                Debug.Log(character.Type);
                Debug.Log(character.Playable);
            }
            
            var sampleCharacterTable = repository.GetTable<SampleCharacterMasterDataTable>();
            var data = sampleCharacterTable.GetById(1001);
            var dataList = sampleCharacterTable.GetAll();
        } 
    }
}
