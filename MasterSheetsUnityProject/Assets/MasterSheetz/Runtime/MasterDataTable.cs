using System.Collections.Generic;
using System.Linq;

namespace MasterSheetz.Runtime
{
    public abstract class MasterDataTableBase
    {
        
    }
    
    public abstract class MasterDataTable<TData> : MasterDataTableBase where TData : IMasterData
    {
        private readonly Dictionary<int, TData> table = new();
        private List<TData> list = new();

        protected MasterDataTable(IEnumerable<TData> masterData)
        {
            table = masterData.ToDictionary(x => x.Id);
        }
        
        public TData GetById(int id)
        {
            return table[id];
        }

        public List<TData> GetAll()
        {
            if (list.Count == 0)
            {
                list = table.Values.ToList();
            }
            return list;
        }
    }
}