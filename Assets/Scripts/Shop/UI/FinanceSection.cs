using GameUI;
using Services;
using UnityEngine;

namespace Shop
{
    public class FinanceSection : MonoBehaviour
    {
        public ShopIAPButton fastFoodPackIAP, money5kIAP, money10kIAP, money15kIAP, noAdsIAP;
        public LevelHelpButton currencyExchange;

        public void OnSectionChange(ShopSectionsEnum section)
        {
            gameObject.SetActive(section == ShopSectionsEnum.Finance);
        }
    }
}
