using Services;
using SkinSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop.SkinsSection
{
    public class BowlSectionView : AbstractSkinSectionView
    {
        public override ShopSectionsEnum ShopSection => ShopSectionsEnum.Bowls;
        public override SkinCategories SkinCategory => SkinCategories.Bowl;
        
        [SerializeField] private Image _bowlImage;
        [SerializeField] private Image _bowlFoodFrontImage;
        [SerializeField] private Image _bowlFoodBackImage;

        [SerializeField] private Animator _petAnimator;

        private ISkinsService _skinsService;
        
        [Inject]
        public void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }

        private void Awake() => _petAnimator.SetTrigger("Love");

        private void ShowBowlAsset(BowlSkinAsset asset)
        {
            _bowlImage.sprite = asset.bowl;
            _bowlFoodBackImage.sprite = asset.foodBack;
            _bowlFoodFrontImage.sprite = asset.foodFront;
        }

        public override void DisplayAssetColor(Color color)
        {
            _bowlImage.color = color;
        }

        public override void DisplayAssetView(SkinAsset asset)
        {
            ShowBowlAsset(asset as BowlSkinAsset);
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