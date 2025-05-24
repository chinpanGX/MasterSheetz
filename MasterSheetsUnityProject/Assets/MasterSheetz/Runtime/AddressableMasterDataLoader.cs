using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MasterSheetz.Runtime
{
    internal class AddressableMasterDataLoader
    {
        private readonly List<AsyncOperationHandle<TextAsset>> handles = new();

        internal async UniTask<string> LoadAsync<T>(string masterDataAssetKey)
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(masterDataAssetKey);
            await handle.ToUniTask();
            if (!handle.Result.text.Contains("data"))
            {
                throw new MasterDataException("Invalid master data format.");
            }
            handles.Add(handle);
            return handle.Result.text;
        }

        internal void Release()
        {
            foreach (var handle in handles)
            {
                Addressables.Release(handle);
            }
            handles.Clear();
        }
    }
}