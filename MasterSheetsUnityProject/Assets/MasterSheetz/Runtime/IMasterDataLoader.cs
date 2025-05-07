using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MasterSheetz.Runtime
{
    public interface IMasterDataLoader
    { 
        UniTask<List<MasterDataTableBase>> LoadAll();  
        void Release();
    }
}