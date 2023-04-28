using UnityEngine;

namespace Pipeline
{
    [CreateAssetMenu(menuName = "Pipeline/ObjectConfig")]
    public class ObjectConfig : ScriptableObject
    {
        public GameObjectTypes gameObjectType;
        public Vector2Int contentGridSize;
    }
}
