using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MasterData.Runtime.Core;
using MasterData.Runtime.Domain;
using App.MasterData;

public class MasterDataLoader : MasterDataTextAssetLoaderBase, IMasterDataLoader
{
    public async UniTask<List<MasterDataTableBase>> LoadAll()
    {
        await UniTask.WhenAll(
            LoadAndRegisterAsync<SampleCharacter>(nameof(SampleCharacter),
                data => new SampleCharacterMasterDataTable(data)
            )
        );
        return Tables;
    }
}