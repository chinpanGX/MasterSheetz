using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MasterData.Runtime.Domain;

namespace MasterData.Runtime.Core
{
    public interface IMasterDataLoader
    { 
        UniTask<List<MasterDataTableBase>> LoadAll();  
        void Release();
    }
}