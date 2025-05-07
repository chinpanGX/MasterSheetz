using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace MasterSheetz.Runtime
{
    public class MasterDataRepository : IMasterRepository
    {
        private List<MasterDataTableBase> tables;
        private readonly IMasterDataLoader loader;
        
        public MasterDataRepository(IMasterDataLoader loader)
        {
            this.loader = loader;
        }

        public T GetTable<T>() where T : class
        {
            var master = tables.FirstOrDefault(x => x is T);
            if (master == null)
            {
                throw new MasterDataException($"Master data table not found: {typeof(T).Name}");
            }
            return master as T;
        }

        public async UniTask LoadAsync()
        {
            tables = await loader.LoadAll();
            loader.Release();
        }
    }
}