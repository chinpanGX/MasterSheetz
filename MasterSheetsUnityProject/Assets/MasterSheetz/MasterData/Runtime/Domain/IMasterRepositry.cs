using System;
using Cysharp.Threading.Tasks;

namespace MasterData.Runtime.Domain
{
    public interface IMasterRepository
    {
        T  GetTable<T>() where T : class;
        UniTask LoadAsync();
    }
}
