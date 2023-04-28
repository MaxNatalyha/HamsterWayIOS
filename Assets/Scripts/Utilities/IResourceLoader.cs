using Cysharp.Threading.Tasks;

namespace Utilities
{
    public interface IResourceLoader
    {
        UniTask<T> Load<T>(string key);
    }
}