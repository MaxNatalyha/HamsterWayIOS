using UnityEngine;

namespace Pipeline
{
    [CreateAssetMenu(menuName = "Pipeline/HamsterConfig")]
    public class HamsterConfig : ScriptableObject
    {
        public float runningSpeed;
        public float rotateSpeed;
    }
}