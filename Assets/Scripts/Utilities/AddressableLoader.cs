using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Utilities
{
    public class AddressableLoader : IResourceLoader
    {
        private readonly ICustomLogger _customLogger;

        public AddressableLoader()
        {
            _customLogger = new DebugLogger();
        }
        
        public async UniTask<T> Load<T>(string key)
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            var loadedObject = await handle.Task;

            if (loadedObject == null)
            {
                _customLogger.PrintError("Addressable Loader", $"Failed load {key}");
                throw new NullReferenceException();
            }            
            
            _customLogger.PrintInfo("Addressable Loader", $"Load {key}");
            return loadedObject;
        }
    }
}