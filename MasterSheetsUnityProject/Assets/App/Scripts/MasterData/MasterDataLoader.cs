using System.Collections.Generic;
using App.MasterData;
using Cysharp.Threading.Tasks;
using MasterData.Runtime;

public class MasterDataLoader : MasterDataTextAssetLoaderBase, IMasterDataLoader
{
    public async UniTask<List<MasterDataTableBase>> LoadAll()
    {
        await UniTask.WhenAll(
            LoadAndRegisterAsync<SampleCharacter>(nameof(SampleCharacter),
                data => new SampleCharacterMasterDataTable(data)
            ),
            LoadAndRegisterAsync<Localization>("LocalizationMaster",
                data => new LocalizationMasterDataTable(data)
            )
        );
        return Tables;
    }
}