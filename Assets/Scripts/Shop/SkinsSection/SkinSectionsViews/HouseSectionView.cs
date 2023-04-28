using Services;
using SkinSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop.SkinsSection
{
    public class HouseSectionView : AbstractSkinSectionView
    {
        public override ShopSectionsEnum ShopSection => ShopSectionsEnum.Houses;
        public override SkinCategories SkinCategory => SkinCategories.House;
        
        [SerializeField] private Image _houseImage;
        [SerializeField] private Image _houseHoleImage;
        [SerializeField] private Animator _petAnimator;

        private ISkinsService _skinsService;
        
        [Inject]
        public void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }

        private void ShowHouseAsset(HouseSkinAsset asset)
        {
            _houseImage.sprite = asset.house;
            _houseHoleImage.sprite = asset.houseHole;
        }

        public override void DisplayAssetColor(Color color)
        {
            _houseImage.color = color;
        }

        public override void DisplayAssetView(SkinAsset asset)
        {
            ShowHouseAsset(asset as HouseSkinAsset);
        }

        private void OnEnable() => ApplySkinToPet();
        
        private void ApplySkinToPet()
        {
            var skin = _skinsService.GetSkin(SkinCategories.Pet);
            var asset = skin.GetCurrentAssetVariant<PetSkinAsset>();

            _petAnimator.runtimeAnimatorController = asset.animator;
        }
    }
}