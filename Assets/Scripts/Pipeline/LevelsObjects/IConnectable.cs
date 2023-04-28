using UnityEngine;

namespace Pipeline
{
    public interface IConnectable
    {
        ObjectConfig ObjectConfig { get; }
        
        void ToggleViewParent();
        void SetUp(Vector2 size, Vector2 position, RectTransform upperViewParent, RectTransform mainParent);
        void ReturnToPool(RectTransform pool);
    }
}