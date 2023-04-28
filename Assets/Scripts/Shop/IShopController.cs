using System;
using Cysharp.Threading.Tasks;
using Pet;

namespace Shop
{
    public interface IShopController
    {
        event Action<ShopCategoriesEnum> OnCategoryChangeEvent;
        event Action<ShopSectionsEnum> OnSectionChangeEvent;
        UniTask Load();
        void Initialize();
        void OpenCategory(ShopCategoriesEnum category);
        void OpenSection(ShopSectionsEnum section);
        void BuyFood(IPetFood food);
    }
}