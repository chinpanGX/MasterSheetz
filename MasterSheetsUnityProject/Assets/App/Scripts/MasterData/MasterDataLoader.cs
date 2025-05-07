using System.Collections.Generic;
using App.MasterData;
using Cysharp.Threading.Tasks;
using MasterSheetz.Runtime;

public class MasterDataLoader : MasterDataTextAssetLoaderBase, IMasterDataLoader
{
    public async UniTask<List<MasterDataTableBase>> LoadAll()
    {
        await UniTask.WhenAll(
            LoadAndRegisterAsync<SampleCharacterData>("SampleCharacter",
                data => new SampleCharacterMasterDataTable(data)
            ),
            LoadAndRegisterAsync<LocalizationData>("LocalizationMaster",
                data => new LocalizationMasterDataTable(data)
            )
        );
        return Tables;
    }
}