using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MasterData.Runtime
{
    public interface IMasterDataLoader
    { 
        UniTask<List<MasterDataTableBase>> LoadAll();  
        void Release();
    }
}