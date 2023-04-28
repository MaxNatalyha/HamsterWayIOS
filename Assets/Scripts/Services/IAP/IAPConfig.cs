using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Services/IAPConfiguration")]
    public class IAPConfig : ScriptableObject
    {
        public ProductsInfo productsInfo;
        public bool useFakeStore;
    }
}