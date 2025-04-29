using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MasterData.Runtime.Domain;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MasterData.Runtime.Core
{
    public abstract class MasterDataTextAssetLoaderBase
    {
        protected readonly List<MasterDataTableBase> Tables = new();
        private readonly List<AsyncOperationHandle<TextAsset>> handles = new();
        
        public void Release()
        {
            foreach (var handle in handles)
            {
                Addressables.Release(handle);
            }
            handles.Clear();
        }
        
        protected async UniTask LoadAndRegisterAsync<TMasterData>(
            string assetKey,
            Func<IEnumerable<TMasterData>, MasterDataTableBase> createTable)
            where TMasterData : IMasterData
        {
            var data = await LoadAsync<TMasterData>(assetKey);
            var table = createTable(data);
            Tables.Add(table);
        }
        
        
        private async UniTask<T[]> LoadAsync<T>(string masterDataAssetKey)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(masterDataAssetKey);
            await handle.ToUniTask();
            if (!handle.Result.text.StartsWith("{\"root\":"))
            {
                throw new MasterDataException("Invalid master data format.");
            }
            handles.Add(handle);
            var master = JsonUtility.FromJson<MasterDataElement<T>>(handle.Result.text);
            return master.root;
        }
        
        [Serializable]
        private class MasterDataElement<T>
        {
            public T[] root;
        }
    }
}