using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MasterSheetz.Runtime
{
    public abstract class MasterDataTextAssetLoaderBase
    {
        protected readonly List<MasterDataTableBase> Tables = new();
        private readonly AddressableMasterDataLoader addressableLoader = new();

        public void Release()
        {
            addressableLoader.Release();
        }

        protected async UniTask LoadAndRegisterAsync<TMasterData>(
            string assetKey,
            Func<IEnumerable<TMasterData>, MasterDataTableBase> createTableFactory)
            where TMasterData : IMasterData
        {
            var text = await addressableLoader.LoadAsync<TMasterData>(assetKey);
            var table = createTableFactory(JsonHelper.FromJson<TMasterData>(text));
            Tables.Add(table);
        }
    }
}