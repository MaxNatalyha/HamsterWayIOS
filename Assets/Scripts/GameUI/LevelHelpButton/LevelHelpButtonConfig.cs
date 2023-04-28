using Services;
using Shop;
using UnityEngine;

namespace GameUI
{
    [CreateAssetMenu(menuName = "Purchases/LevelHelpButtonConfig")]
    public class LevelHelpButtonConfig : ScriptableObject, IPurchasable
    {
        public CurrencyType Currency => _currency;
        public int Price => _price;
        public Sprite Icon => _icon;

        [SerializeField] private CurrencyType _currency;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;
    }
}