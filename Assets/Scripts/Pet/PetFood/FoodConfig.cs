using System;
using Shop;
using UnityEngine;

namespace Pet
{
    [Serializable]
    public class FoodConfig
    {
        public PetFoodType FoodType => _foodType;
        public float Satiety => _satiety;
        public Sprite FoodSprite => _foodSprite;
        public IPurchasable PurchaseInfo => _purchaseInfo;

        [SerializeField] private PetFoodType _foodType;
        [SerializeField] private float _satiety;
        [SerializeField] private Sprite _foodSprite;
        [SerializeField] private PurchaseInfo _purchaseInfo;
    }
}