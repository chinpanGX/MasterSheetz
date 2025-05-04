using Cysharp.Threading.Tasks;

namespace MasterData.Runtime
{
    public interface IMasterRepository
    {
        T  GetTable<T>() where T : class;
        UniTask LoadAsync();
    }
}
