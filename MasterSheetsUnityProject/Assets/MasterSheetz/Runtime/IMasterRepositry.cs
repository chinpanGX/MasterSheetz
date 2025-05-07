using Cysharp.Threading.Tasks;

namespace MasterSheetz.Runtime
{
    public interface IMasterRepository
    {
        T  GetTable<T>() where T : class;
        UniTask LoadAsync();
    }
}
