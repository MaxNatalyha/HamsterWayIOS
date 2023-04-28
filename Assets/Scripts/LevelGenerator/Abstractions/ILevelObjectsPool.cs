using Cysharp.Threading.Tasks;
using MatchingCards;
using Pipeline;
using UnityEngine;

namespace LevelGenerator
{
    public interface ILevelObjectsPool
    {
        UniTask Load();
        void Initialize();
        T GetLevelObject<T>(GameObjectTypes objectType);
        Pipe GetPipe(GameObjectTypes objectType);
        Card GetCard();
        void ReturnPipeToPool(Pipe levelObject);
        void ReturnObjectToPool(GameObject levelObject);
        void ReturnCardToPool(Card card);
    }
}