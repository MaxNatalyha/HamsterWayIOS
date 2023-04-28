using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Skins/SkinsConfiguration")]
    public class SkinsConfig : ScriptableObject
    {
        public SkinsPair[] skinsCategories = new SkinsPair[5];
    }
}