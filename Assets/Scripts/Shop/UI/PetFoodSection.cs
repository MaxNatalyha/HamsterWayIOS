using UnityEngine;

namespace Shop
{
    public class PetFoodSection : MonoBehaviour
    {
        public RectTransform foodContainer;
        
        public void OnSectionChange(ShopSectionsEnum sectionEnum)
        {
            gameObject.SetActive(sectionEnum == ShopSectionsEnum.Food);
        }
    }
}
