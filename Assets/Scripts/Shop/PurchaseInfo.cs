using System;
using Services;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class PurchaseInfo : IPurchasable
    {
        public CurrencyType Currency => _currency;
        public int Price => _price;

        [SerializeField] private CurrencyType _currency;
        [SerializeField] private int _price;
    }
}